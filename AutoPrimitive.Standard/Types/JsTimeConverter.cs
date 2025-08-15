using System;
using System.Collections.Generic;
using System.Text;

namespace AutoPrimitive
{
    internal class JsTimeConverter
    {
        public static Type[] _JS_timestamp = new Type[] { typeof(int), typeof(long), typeof(string) };

        // 统一入口方法，通过重载区分两种值类型
        public static bool Convert_JS_timestamp<T>(Primitive<T> primitive, out DateTime? dateTime) where T : struct
        {
            return Convert_JS_timestampInternal(typeof(T), primitive.Value, out dateTime);
        }

        public static bool Convert_JS_timestamp<T>(PrimitiveNullable<T> primitive, out DateTime? dateTime)
        {
            return Convert_JS_timestampInternal(typeof(T), primitive.Value, out dateTime);
        }


        // 核心转换逻辑（提取为内部方法，共享实现）
        private static bool Convert_JS_timestampInternal(Type t_type, object js_timestamp, out DateTime? dateTime)
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

    }
}
