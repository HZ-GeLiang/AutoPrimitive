# AGENTS.md

本文件为 Codex/AI agent 在本仓库中工作的项目指南。开始改动前请先阅读本文件，并以现有代码风格为准。

## 项目概览

AutoPrimitive 是一个 C# 基础类型转换库，目标是用 `ToPrimitive()`、`dynamic`、隐式转换和运算符重载，提供接近 JavaScript 弱类型体验的基础类型转换能力，同时保持 C# 类型系统边界。

核心包项目：

- `AutoPrimitive.Standard/`: 主库源码，NuGet 包信息也在这里。
- `AutoPrimitive.Test/`: MSTest 单元测试，覆盖基础类型、字符串、枚举、日期、可空类型、默认值和运算符重载。
- `AutoPrimitive.Test_Console/`: 控制台试验/示例项目。
- `MyTest/`: 旧版/兼容性控制台验证项目。
- `README.md` 与 `AutoPrimitive.Standard/Readme.md`: 包说明和 NuGet README。

## 常用命令

优先在仓库根目录运行：

```bash
dotnet test AutoPrimitive.Test/AutoPrimitive.Test.csproj
dotnet build AutoPrimitive.Standard/AutoPrimitive.Standard.csproj
dotnet build AutoPrimitive.sln
```

注意：当前主库目标框架包含 `net9.0`，如果本机 SDK 低于 .NET 9，完整 solution 或主库多目标构建可能失败。只改文档时无需强行构建；改库代码时请说明本机 SDK 版本和实际跑过的命令。

## 目标框架与条件编译

`AutoPrimitive.Standard` 当前多目标：

- `netstandard1.0`
- `net6.0`
- `net7.0`
- `net8.0`
- `net9.0`

请特别注意：

- `DateOnly`、`TimeOnly` 只能在 `#if NET6_0_OR_GREATER` 下使用。
- 面向 `netstandard1.0` 的代码不能依赖较新的 BCL API。
- 枚举 DescriptionAttribute 相关逻辑当前在 `NETSTANDARD2_1_OR_GREATER || NETCOREAPP2_0_OR_GREATER` 条件下。
- 修改公共转换行为时，需要考虑所有目标框架下的编译和行为一致性。

## 代码风格

遵循 `.editorconfig`、`Directory.Build.props` 和现有源码：

- C# 使用 4 空格缩进。
- 大括号换行。
- `if` 等控制语句必须写 `{}`。
- 保持现有命名风格：主类型在 `AutoPrimitive` 命名空间下，测试方法常用中文描述行为。
- 现有源码中有大量中文注释，新增注释可以中文为主，但只在能解释边界行为、兼容性或转换规则时添加。
- 不要做无关格式化或大范围重排；这个库依赖大量显式转换和运算符重载，微小改动也可能影响调用绑定。

## 转换行为约定

修改转换逻辑时请保持以下原则：

- `ToPrimitive()` 扩展方法是主要入口，位于 `AutoPrimitive.Standard/PrimitiveExtensions.cs`。
- 基础值类型由 `Primitive<T>` 处理。
- 可空基础类型由 `PrimitiveNullable<T>` 处理。
- 字符串、枚举、Guid、DateTime、DateOnly 等都有专门类型。
- Bool 转换约定：数字非 0 即真；字符串优先尝试 `bool` 或 `int` 解析，无法解析时为 `false`。
- DateTime 转换支持普通日期、JS 时间戳、`yyyyMMddHHmmss`、`yyyyMMdd` 和 JS Date object 风格，请先看 `Utils/DateTimeConverter.cs` 与 `Utils/JsTimeConverter.cs`。
- `Equals`、`GetHashCode`、隐式转换、运算符重载是公开行为的一部分，改动前请补测试。

## 测试要求

改库代码时，优先补或更新 `AutoPrimitive.Test/Tests/` 下的 MSTest 测试。测试项目启用了方法级并行：

```csharp
[assembly: Parallelize(Scope = ExecutionScope.MethodLevel)]
```

因此新增测试不要依赖全局可变状态、测试执行顺序或本机区域性隐式行为。涉及日期/数字/字符串解析时，尽量写明确输入和期望输出。

## NuGet 与包元数据

包元数据在 `AutoPrimitive.Standard/AutoPrimitive.Standard.csproj`：

- `PackageId`: `AutoPrimitive`
- `Version`: 当前项目版本
- `PackageReadmeFile`: `Readme.md`
- `GeneratePackageOnBuild`: `true`

不要无意修改版本号、作者、License、PackageId 或打包配置。只有用户明确要求发布/版本调整时才改这些字段。

## 文件与生成物

- 不要提交 `bin/`、`obj/`、`.vs/` 等生成物。
- 不要改动 `AutoPrimitive.SourceGenerator.zip`，除非用户明确要求处理该压缩包。
- 改 README 时同步考虑根目录 `README.md` 和 `AutoPrimitive.Standard/Readme.md` 是否都需要更新。

## 工作边界

优先做小而精确的改动：

- 修 bug 时先定位对应类型和测试，再改实现。
- 新增转换能力时同时添加扩展入口、专门类型或运算符，并补覆盖测试。
- 保持公开 API 兼容，避免移除或重命名现有 `ToPrimitive` 重载。
- 如果完整构建受 SDK 版本限制，请在最终说明中明确没有跑过哪些目标框架。
