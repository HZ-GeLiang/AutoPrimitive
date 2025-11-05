using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Text;
namespace AutoPrimitive.Test.Tests
{
#if DEBUG

    // 测试类需标记 [TestClass]
    [TestClass]
    public class DateTimeConverterTests
    {
        // 测试1：正常格式（14位数字，合法日期）
        [TestMethod]
        public void TryParseYmdHms_ValidFormat_ReturnsTrue()
        {
            // Arrange
            string input = "20250815080732"; // 2025-08-15 08:07:32

            // Act
            bool result = DateTimeConverter.TryParseYmdHms(input, out DateTime output);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(2025, output.Year);
            Assert.AreEqual(8, output.Month);
            Assert.AreEqual(15, output.Day);
            Assert.AreEqual(8, output.Hour);
            Assert.AreEqual(7, output.Minute);
            Assert.AreEqual(32, output.Second);
        }

        // 测试2：输入为空或空白（使用 [DataRow] 传递多组参数）
        [DataTestMethod]
        [DataRow(null)]
        [DataRow("")]
        [DataRow("   ")]
        public void TryParseYmdHms_NullOrWhitespace_ReturnsFalse(string input)
        {
            // Act
            bool result = DateTimeConverter.TryParseYmdHms(input, out DateTime output);

            // Assert
            Assert.IsFalse(result);
            Assert.AreEqual(default(DateTime), output); // 输出应为默认值
        }

        // 测试3：长度不是14位
        [DataTestMethod]
        [DataRow("202508150807")]  // 12位
        [DataRow("2025081508073")] // 13位
        [DataRow("202508150807321")] // 15位
        public void TryParseYmdHms_InvalidLength_ReturnsFalse(string input)
        {
            // Act
            bool result = DateTimeConverter.TryParseYmdHms(input, out DateTime output);

            // Assert
            Assert.IsFalse(result);
        }

        // 测试4：包含非数字字符
        [DataTestMethod]
        [DataRow("20250815080a32")] // 字母
        [DataRow("2025081508@732")] // 符号
        [DataRow("2025-0815080732")] // 分隔符
        public void TryParseYmdHms_NonNumericChars_ReturnsFalse(string input)
        {
            // Act
            bool result = DateTimeConverter.TryParseYmdHms(input, out DateTime output);

            // Assert
            Assert.IsFalse(result);
        }

        // 测试5：日期逻辑无效（如月份>12、日期>当月最大天数）
        [DataTestMethod]
        [DataRow("20251301000000")] // 月份13（无效）
        [DataRow("20250230000000")] // 2月30日（2025年非闰年，2月只有28天）
        [DataRow("20250431000000")] // 4月31日（4月只有30天）
        [DataRow("20250101250000")] // 小时25（无效，0-23）
        [DataRow("20250101006000")] // 分钟60（无效，0-59）
        [DataRow("20250101000060")] // 秒60（无效，0-59）
        public void TryParseYmdHms_InvalidDateLogic_ReturnsFalse(string input)
        {
            // Act
            bool result = DateTimeConverter.TryParseYmdHms(input, out DateTime output);

            // Assert
            Assert.IsFalse(result);
        }

        // 测试6：闰年2月29日（合法）
        [TestMethod]
        public void TryParseYmdHms_LeapYearFebruary29_ReturnsTrue()
        {
            // 2024年是闰年，2月有29天
            string input = "20240229235959";

            bool result = DateTimeConverter.TryParseYmdHms(input, out DateTime output);

            Assert.IsTrue(result);
            Assert.AreEqual(2024, output.Year);
            Assert.AreEqual(2, output.Month);
            Assert.AreEqual(29, output.Day);
            Assert.AreEqual(23, output.Hour);
            Assert.AreEqual(59, output.Minute);
            Assert.AreEqual(59, output.Second);
        }

        // 测试7：边界值（如最小/最大合法日期）
        [TestMethod]
        public void TryParseYmdHms_BoundaryValues_ReturnsTrue()
        {
            // 最小合法日期（0001年1月1日 00:00:00）
            string minInput = "00010101000000";
            bool minResult = DateTimeConverter.TryParseYmdHms(minInput, out DateTime minOutput);
            Assert.IsTrue(minResult);
            Assert.AreEqual(new DateTime(1, 1, 1, 0, 0, 0), minOutput);

            // 最大合法日期（9999年12月31日 23:59:59）
            string maxInput = "99991231235959";
            bool maxResult = DateTimeConverter.TryParseYmdHms(maxInput, out DateTime maxOutput);
            Assert.IsTrue(maxResult);
            Assert.AreEqual(new DateTime(9999, 12, 31, 23, 59, 59), maxOutput);
        }
    }

#endif


}