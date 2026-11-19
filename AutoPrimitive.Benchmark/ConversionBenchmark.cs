using System.Numerics;
using BenchmarkDotNet.Attributes;

namespace AutoPrimitive.Benchmark;

/// <summary>
/// 对比主库动态包装路径(ToPrimitive)与无包装直接转换基线。
/// 输入经 [Params] 注入, 避免 JIT 把静态字段/常量折叠成零耗时。
/// </summary>
[MemoryDiagnoser]
[SimpleJob(warmupCount: 3, iterationCount: 5)]
public class ConversionBenchmark
{
    [Params(123456)]
    public int IntValue { get; set; }

    [Params(123456789012L)]
    public long LongValue { get; set; }

    [Params(1.5)]
    public double DoubleValue { get; set; }

    [Params("123456")]
    public string StringValue { get; set; } = null!;

    private static readonly BigInteger Bi = BigInteger.Parse("123456789012");

    // ===== 同类型往返: 动态包装路径 =====

    [Benchmark]
    public int Int_ToPrimitive_ToInt() => IntValue.ToPrimitive();

    [Benchmark]
    public long Long_ToPrimitive_ToLong() => LongValue.ToPrimitive();

    [Benchmark]
    public double Double_ToPrimitive_ToDouble() => DoubleValue.ToPrimitive();

    // ===== 同类型往返: 直接基线 =====

    [Benchmark(Baseline = true)]
    public int Int_Direct() => IntValue;

    [Benchmark]
    public long Long_Direct() => LongValue;

    [Benchmark]
    public double Double_Direct() => DoubleValue;

    // ===== int -> long 跨类型 =====

    [Benchmark]
    public long Int_ToPrimitive_ToLong() => IntValue.ToPrimitive();

    [Benchmark]
    public long Int_Direct_ToLong() => IntValue;

    // ===== int -> string =====

    [Benchmark]
    public string Int_ToPrimitive_ToString() => IntValue.ToPrimitive();

    [Benchmark]
    public string Int_Direct_ToString() => IntValue.ToString();

    // ===== string -> 数值 =====

    [Benchmark]
    public int String_ToPrimitive_ToInt() => StringValue.ToPrimitive();

    [Benchmark]
    public int String_Direct_Parse() => int.Parse(StringValue);

    [Benchmark]
    public long String_ToPrimitive_ToLong() => StringValue.ToPrimitive();

    [Benchmark]
    public long String_Direct_ParseLong() => long.Parse(StringValue);

    // ===== BigInteger 反向转 long(新类型专用包装) =====

    [Benchmark]
    public long BigInteger_ToPrimitive_ToLong() => Bi.ToPrimitive();

    [Benchmark]
    public long BigInteger_Direct_ToLong() => (long)Bi;
}
