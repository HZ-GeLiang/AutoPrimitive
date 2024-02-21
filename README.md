# AutoPrimitive

当前Nuget包的作用: Type conversion of Primitive Data Types

# 包名的由来

- 在一次无意的源码查看中发现了PrimitiveTypeKind,这个类内部定义了好多类型

- 在问了GPT后了解到

- > `PrimitiveTypeKind` 枚举是实体数据模型（EDM）的一部分，用于表示 Entity Framework 支持的各种基本数据类型。基本类型是诸如整数、字符串、日期等的基本数据类型，可用于定义数据模型中实体的属性。
  >
  > `PrimitiveTypeKind` 枚举包括诸如 `Int32`、`String`、`DateTime` 等值，每个值代表不同的基本数据类型。在定义实体数据模型的属性时，通常会使用这个枚举来指定特定属性可以保存的数据类型。
  >
  > 例如，在 C# 中，您可能会使用 `PrimitiveTypeKind.Int32` 来表示实体数据模型中可以保存 32 位整数值的属性。

- 然后我又想实现类型的自动转换,于是本Nuget就叫AutoPrimitive

- PrimitiveTypeKind的参考地址

- > https://learn.microsoft.com/zh-tw/dotnet/api/system.data.metadata.edm.primitivetypekind?view=netframework-4.8.1
  >
  > https://referencesource.microsoft.com/#System.Data.Entity/System/Data/Metadata/Edm/PrimitiveTypeKind.cs,23c66f1dff45ec22

# 项目背景

- 基础类型的值互相转换在c#这种强类型语言中转换比较麻烦,常见的方法有: 

  - 使用原生的

    - 字符类型: xxx.ToString()
    - 其他类型(含字符串): Convert.ToXXX()

  - 使用扩展方法:最后实现方式为  :

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

- 转换操作符:implicity/explicit 

- 操作符的重写: operator

- 运算符的重载

  > 扩展阅读: https://learn.microsoft.com/zh-cn/dotnet/csharp/language-reference/operators/operator-overloading

- 方法重写: override

- TargetFrameworks

- 其他可增强扩展(因为用不到,所以源码未实现)

  - guid的类型支持

  - 比较器的接口

    - IComparable<T> 默认比较的实现

    - IComparer<T> 额外比较的实现

    - IEqualityComparer<T>

      >  扩展阅读:http://www.cnblogs.com/ldp615/archive/2011/08/01/distinct-entension.html
