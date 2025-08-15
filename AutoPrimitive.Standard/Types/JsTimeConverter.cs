using System.Globalization;
using System.Text.RegularExpressions;

namespace AutoPrimitive
{
    internal class JsTimeConverter
    {
        public static Type[] _JS_timestamp = new Type[] { typeof(int), typeof(long), typeof(string) };

        // 统一入口方法，通过重载区分两种值类型
        public static bool Convert_JS_timestamp<T>(Primitive<T> primitive, out DateTime? dateTime) where T : struct
        {
            return Convert_JS_timestamp_Internal(typeof(T), primitive.Value, out dateTime);
        }

        public static bool Convert_JS_timestamp<T>(PrimitiveNullable<T> primitive, out DateTime? dateTime)
        {
            return Convert_JS_timestamp_Internal(typeof(T), primitive.Value, out dateTime);
        }


        // 核心转换逻辑（提取为内部方法，共享实现）
        private static bool Convert_JS_timestamp_Internal(Type t_type, object js_timestamp, out DateTime? dateTime)
        {
            if (js_timestamp == null)
            {
                dateTime = default;
                return false;
            }


#if NETCOREAPP1_0_OR_GREATER || NETSTANDARD1_3_OR_GREATER
            try
            {

                // 处理long类型
                if (t_type == typeof(long))
                {
                    long longValue = (long)js_timestamp; //timestamp
                    int len = longValue.ToString().Length;

                    if (len == 13 || len == 14)
                    {
                        var dateTimeOffset = DateTimeOffset.FromUnixTimeMilliseconds(longValue);
                        dateTime = dateTimeOffset.LocalDateTime;
                        return true;
                    }
                    else if (len == 10 || len == 11)
                    {
                        var dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(longValue);
                        dateTime = dateTimeOffset.LocalDateTime;
                        return true;
                    }
                }
                // 处理int类型
                else if (t_type == typeof(int))
                {
                    int intValue = (int)js_timestamp; //timestamp
                    int len = intValue.ToString().Length;

                    if (len == 10 || len == 11)
                    {
                        var dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(intValue);
                        dateTime = dateTimeOffset.LocalDateTime;
                        return true;
                    }
                }
                // 处理string类型
                else if (t_type == typeof(string))
                {
                    string timestamp = (string)js_timestamp;
                    if (JsTimeConverter.Convert_JS_timestamp(timestamp, out dateTime))
                    {
                        return true;
                    }

                    if (JsTimeConverter.Convert_JS_DateObject(timestamp, out dateTime))
                    {
                        return true;
                    }
                }
            }
            catch (InvalidCastException ex1)
            {
                // 类型转换失败时返回false
            }
            catch (Exception)
            {
                // 其他异常处理
            }
#endif
            dateTime = default;
            return false;
        }

        /// <summary>
        /// JavaScript时间戳
        /// </summary>
        /// <param name="js_timestamp"></param>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static bool Convert_JS_timestamp(string js_timestamp, out DateTime? dateTime)
        {
#if NETCOREAPP1_0_OR_GREATER || NETSTANDARD1_3_OR_GREATER

            if (js_timestamp == null)
            {
                dateTime = default;
                return false;
            }

            try
            {
                if (js_timestamp.Length == 13 || js_timestamp.Length == 14) //负数时, 长度要+1
                {
                    // 毫秒级时间戳：毫秒级时间戳是指自 1970 年 1 月 1 日 00:00:00 UTC 起的毫秒数。毫秒级时间戳通常是 13 位长度的数字
                    if (long.TryParse(js_timestamp, out var timestamp))
                    {
                        var dateTimeOffset = DateTimeOffset.FromUnixTimeMilliseconds(timestamp);
                        dateTime = dateTimeOffset.LocalDateTime;
                        return true;
                    }
                }
                else if (js_timestamp.Length == 10 || js_timestamp.Length == 11)  //负数时, 长度要+1
                {
                    // 秒级时间戳是指自 1970 年 1 月 1 日 00:00:00 UTC 起的秒数。秒级时间戳通常是 10 位长度的数字
                    if (long.TryParse(js_timestamp, out var timestamp))
                    {
                        var dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(timestamp);
                        dateTime = dateTimeOffset.LocalDateTime;
                        return true;
                    }
                }
            }
            catch (Exception)
            {
            }
#endif
            dateTime = default;
            return false;
        }


        public const string js_data_format = "ddd MMM dd yyyy HH:mm:ss 'GMT'zzz";

        /// <summary>
        /// JavaScript时间戳
        /// </summary>
        /// <param name="timeString"></param>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static bool Convert_JS_DateObject(string timeString, out DateTime? dateTime)
        {
            if (timeString == null)
            {
                dateTime = default;
                return false;
            }

            /*
Fri Aug 15 2025 08:07:32 GMT+0800 (香港标准时间)

Wed：星期缩写（Wednesday）；
Aug：月份缩写（August）；
13：日期；
2025：年份；
15:50:32：24 小时制的时分秒；
GMT+0800：时区偏移（东八区，比 UTC 快 8 小时）；
(中国标准时间)：中文时区名称（通常由环境语言设置决定）。
            */

            // 第一步：移除末尾的中文时区部分（如 (香港标准时间) 或 (中国标准时间)）
            // 使用正则表达式匹配并删除 "(任意中文内容)" 格式的后缀
#if NETSTANDARD1_0
            // .NET Standard 1.0不支持RegexOptions.Compiled，直接省略该参数
            string cleanedTime = Regex.Replace(timeString, @"\s*\(.*?\)$", "");
#else
            string cleanedTime = Regex.Replace(timeString, @"\s*\(.*?\)$", "", RegexOptions.Compiled);
#endif

            if (string.IsNullOrWhiteSpace(cleanedTime))
            {
                dateTime = default;
                return false;
            }

            if (cleanedTime.EndsWith(" "))
            {
                cleanedTime = cleanedTime.TrimEnd();
            }


            // 尝试解析
            if (DateTimeOffset.TryParseExact(cleanedTime, js_data_format, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTimeOffset result))
            {
                dateTime = result.LocalDateTime;
                return true;
                //Console.WriteLine("解析成功：");
                //Console.WriteLine($"DateTimeOffset: {result}");
                //Console.WriteLine($"本地时间: {result.LocalDateTime}");
                //Console.WriteLine($"UTC时间: {result.UtcDateTime}");
            }

            dateTime = default;
            return false;
        }

    }
}
