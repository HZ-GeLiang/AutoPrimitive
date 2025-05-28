# AutoPrimitive

当前Nuget包的作用: Type Conversion of Primitive Data Types

## Introduction

AutoPrimitive is a lightweight C# type conversion toolkit designed to simplify operations between primitive data types. It provides a JavaScript-like weak typing experience while maintaining C#'s type safety. Through the use of dynamic types and operator overloading, type conversion becomes more elegant and intuitive.

Key Features:
- Supports conversion between all primitive data types
- Provides JavaScript-like weak typing experience
- No explicit namespace reference required
- Supports special type conversions (enums, datetime, etc.)
- Zero dependencies, lightweight implementation

## 简介

AutoPrimitive 是一个轻量级的C#类型转换工具包，旨在简化基础数据类型之间的转换操作。它提供了类似JavaScript的弱类型转换体验，同时保持C#的类型安全性。通过使用动态类型和操作符重载，让类型转换变得更加优雅和直观。

主要特点：
- 支持所有基础数据类型的相互转换
- 提供类似JavaScript的弱类型转换体验
- 无需显式引用命名空间即可使用
- 支持枚举、日期时间等特殊类型的转换
- 零依赖，轻量级实现

# 项目背景

- 基础类型的值互相转换在c#这种强类型语言中转换比较麻烦,常见的方法有: 

  - 使用原生的

    - 字符类型: xxx.ToString()
    - 其他类型(含字符串): Convert.ToXXX()

  - 使用扩展方法:最后实现方式为:

    - using 扩展方法

    - 对象.ToXXX()

      缺点

      1. 需要引用命名空间,虽然可以用GolbalUsing获其他方法来绕开(这个不是致命的,本包也需要)
      2. 需要和前面的变量保持类型一致或兼容(如果变量调整了,代码有可能要调整)
    
      ```c#
      var a = 1.ToFloat()
      double b = 1.ToFloat();
      ```

- 痛点:如果有一种方式可以间接实现JS的弱类型的特点用起来就嘎嘎好,代码在增加少了性能的前提下提高可维护性。

- 希望程序的运行稳定性和人一样： 龙行龘龘（dá dá）前程朤朤（lǎng lǎng）生活䲜䲜（yè yè）健康𣊫𣊫（liù liù）财运𨰻𨰻（bǎo bǎo）

  
  
  ```
                                _ooOoo_
                               o8888888o
                               88" . "88
                               (| -_- |)
                               O\  =  /O
                            ____/`---'\____
                          .'  \\|     |//  `.
                         /  \\|||  :  |||//  \
                        /  _||||| -:- |||||-  \
                        |   | \\\  -  /// |   |
                        | \_|  ''\---/''  |   |
                        \  .-\__  `-`  ___/-. /
                      ___`. .'  /--.--\  `. . __
                   ."" '<  `.___\_<|>_/___.'  >'"".
                  | | :  `- \`.;`\ _ /`;.`/ - ` : | |
                  \  \ `-.   \_ __\ /__ _/   .-` /  /
             ======`-.____`-.___\_____/___.-`____.-'======
                                `=---='
             ^^^^^^^^^^^^^^^0 ERROR 0 Warning^^^^^^^^^^^^^^
  ```



# 现有的类型转换支持

- 枚举:PrimitiveEnum

- Primitive<T>  各种基础类型

  > 数值类型: short ushort int uint char float double long ulong decimal
  > 其他类型: bool byte sbyte
  > 其他:string
  
- PrimitiveDateOnly

- PrimitiveDateTime

- PrimitiveNullable

- PrimitiveString

# 默认约定

- 转Bool类型:

  - 数字: 非0即真

  - 字符串: 若可以被bool/int解析则返回解析结果, 否认返回false

    ```c#
    核心代码: 
    return bool.TryParse(primitive.Value, out var result1) && result1 == true ||
    int.TryParse(primitive.Value, out var result2) && result2 != 0;
    ```

# 涉及到的相关知识点

- dynamic

  > 扩展阅读: https://mp.weixin.qq.com/s/ZazwAd7kyi7rGQgCMWjGtQ

- this关键字

  > 扩展阅读: https://mp.weixin.qq.com/s/gdITACUCetucKmqW2GUJBg

- 泛型

- 隐式/显示类型转换

- 操作符和转换操作符:operator implicity

- 运算符的重载

  > 扩展阅读: https://learn.microsoft.com/zh-cn/dotnet/csharp/language-reference/operators/operator-overloading

- 方法重写: override

- 其他可增强扩展(因为用不到,所以源码未实现)

  - guid的类型支持

  - 比较器的接口

    - IComparable<T> 默认比较的实现

    - IComparer<T> 额外比较的实现

    - IEqualityComparer<T>

      >  扩展阅读:http://www.cnblogs.com/ldp615/archive/2011/08/01/distinct-entension.html

- 其他
  - TargetFrameworks
