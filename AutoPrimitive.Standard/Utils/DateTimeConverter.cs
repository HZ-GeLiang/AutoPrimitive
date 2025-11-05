using System;
using System.Collections.Generic;
using System.Text;

namespace AutoPrimitive
{
#if DEBUG
    public sealed class DateTimeConverter//单元测试
#else
    internal sealed class DateTimeConverter
#endif
    {

        /// <summary>
        /// 将 yyyymmddhhmmss 格式字符串转换为 DateTime 对象
        /// </summary>
        /// <param name="value">输入字符串（格式：yyyyMMddHHmmss，必须14位数字）</param>
        /// <param name="dateTime">转换成功时返回的 DateTime 对象；失败时返回默认值</param>
        /// <returns>转换成功返回 true，失败返回 false</returns>
        public static bool TryParseYmdHms(string value, out DateTime dateTime)
        {
            // 初始化输出参数（默认值）
            dateTime = default;

            // 验证输入是否为空或长度不符
            if (string.IsNullOrWhiteSpace(value) || value.Length != 14)
            {
                return false;
            }

            // 验证是否为纯数字
            if (!long.TryParse(value, out var value_long) && value_long > 0)
            {
                return false;
            }

            // 使用 TryParseExact 解析（格式：yyyyMMddHHmmss，24小时制）
            return DateTime.TryParseExact(
                value,
                "yyyyMMddHHmmss",
                System.Globalization.CultureInfo.InvariantCulture, //表示一个 “固定不变的文化区域”（与系统当前语言 / 地区无关）
                System.Globalization.DateTimeStyles.None,  //表示不应用任何额外的解析规则（使用默认行为）。
                out dateTime
            );
        }


        /// <summary>
        /// 将 yyyymmdd 格式字符串转换为 DateTime 对象
        /// </summary>
        /// <param name="value">输入字符串（格式：yyyyMMd，必须8位数字）</param>
        /// <param name="dateTime">转换成功时返回的 DateTime 对象；失败时返回默认值</param>
        /// <returns>转换成功返回 true，失败返回 false</returns>
        public static bool TryParseYmd(string value, out DateTime dateTime)
        {
            // 初始化输出参数（默认值）
            dateTime = default;

            // 验证输入是否为空或长度不符
            if (string.IsNullOrWhiteSpace(value) || value.Length != 8)
            {
                return false;
            }

            // 验证是否为纯数字
            if (!int.TryParse(value, out var value_int) && value_int > 0)
            {
                return false;
            }

            // 使用 TryParseExact 解析（格式：yyyyMMddHHmmss，24小时制）
            return DateTime.TryParseExact(
                value,
                "yyyyMMdd",
                System.Globalization.CultureInfo.InvariantCulture, //表示一个 “固定不变的文化区域”（与系统当前语言 / 地区无关）
                System.Globalization.DateTimeStyles.None,  //表示不应用任何额外的解析规则（使用默认行为）。
                out dateTime
            );
        }
    }
}
