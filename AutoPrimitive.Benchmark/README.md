# AutoPrimitive 性能基准

对比主库动态包装路径（`ToPrimitive()`）与无包装直接转换基线。

## 运行

```bash
# 本机默认运行时与目标框架不一致时, 加 rollforward
DOTNET_ROLL_FORWARD=LatestMajor dotnet run -c Release --project AutoPrimitive.Benchmark
```

## 测试环境

- macOS ARM64（Apple M5），.NET SDK 10.0.404，基准进程经 rollforward 跑在 .NET 10 运行时
- BenchmarkDotNet 0.13.12，WarmupCount=3，IterationCount=5，启用 MemoryDiagnoser
- 输入经 `[Params]` 注入，避免 JIT 把常量/只读字段折叠成零耗时

## 本次实测（2026-11-19）

| 场景 | 动态路径 `ToPrimitive` | 直接基线 | 动态分配 | 基线分配 |
|---|---:|---:|---:|---:|
| int → int | 2.60 ns | 身份转换 ≈ 0 | 24 B | 0 B |
| long → long | 2.59 ns | 身份转换 ≈ 0 | 24 B | 0 B |
| double → double | 2.97 ns | 0.061 ns | 24 B | 0 B |
| int → long（拓宽） | 2.64 ns | 身份拓宽 ≈ 0 | 24 B | 0 B |
| int → string | 7.49 ns | 4.22 ns（`ToString()`） | 64 B | 40 B |
| string → int | 7.14 ns | 5.34 ns（`int.Parse`） | 24 B | 0 B |
| string → long | 7.23 ns | 3.68 ns（`long.Parse`） | 24 B | 0 B |
| BigInteger → long | 7.22 ns | 直接强转 ≈ 0 | 64 B | 0 B |

## 结论

1. **固定分配**：每次动态调用数值路径分配 24 B，string / BigInteger 路径 64 B；直接基线基本 0 分配。
2. **身份/拓宽转换**：动态路径多 2.5–3 ns（包装构造 + dynamic 装箱 + 运行时绑定 + `Convert` 分发），直接转换近乎免费。
3. **有真实工作量的场景**（`Parse` / `ToString`）：动态路径慢约 1.3–2.0 倍，且额外分配。
4. 单次调用为纳秒级，只有在高频批量循环中才构成实际瓶颈；配置读取、IO 解析等低频场景可忽略。

## 备注

- 部分身份/拓宽基线被 JIT 折叠为零耗时，BDN 的 Ratio 列以 `Int_Direct` 为基准，数值会被放大，倍数请以上表同类别直接基线为准。
- 本次未在真实 net8 运行时上实测（本机仅 .NET 10 运行时，net8 程序集经 rollforward 运行）。
