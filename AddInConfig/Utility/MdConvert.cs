using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;

namespace AddInConfigJson.Utility
{
    /// <summary>
    ///转换类
    /// </summary>
    public static class MdConvert
    {
        /// 转成整型，失败可指定默认值
        /// 作者：Administrator
        /// 日期：2012-07-31
        /// </summary>
        public static int ToInt32(object value, int defaultValue = 0)
        {
            if (value == null)
            {
                return defaultValue;
            }

            int result;
            if (!int.TryParse(value.ToString(), out result))
            {
                result = defaultValue;
            }

            return result;
        }

        /// 转成Long，失败可指定默认值
        /// 作者：Administrator
        /// 日期：2012-07-31
        /// </summary>
        public static long ToInt64(object value, long defaultValue = 0)
        {
            if (value == null)
            {
                return defaultValue;
            }

            long result;
            if (!long.TryParse(value.ToString(), out result))
            {
                result = defaultValue;
            }

            return result;
        }

        /// <summary>
        /// 转成decimal 失败可指定默认值
        /// 作者：Administrator
        /// 日期：2013-05-22
        /// </summary>
        public static decimal ToDecimal(object value, decimal defaultValue = 0M)
        {
            if (value == null)
            {
                return defaultValue;
            }

            decimal result;
            if (!decimal.TryParse(value.ToString(), out result))
            {
                result = defaultValue;
            }

            return result;
        }

        /// <summary>
        /// 转成双精度，失败可指定默认值
        /// 作者：Administrator
        /// 日期：2012-07-31
        /// </summary>
        public static double ToDouble(object value, double defaultValue = 0D)
        {
            if (value == null)
            {
                return defaultValue;
            }

            double result;
            if (!double.TryParse(value.ToString(), out result))
            {
                result = defaultValue;
            }

            return result;
        }

        /// <summary>
        /// 转成单精度，失败可指定默认值
        /// 作者：Administrator
        /// 日期：2012-07-31
        /// </summary>
        public static float ToSingle(object value, float defaultValue = 0F)
        {
            if (value == null)
            {
                return defaultValue;
            }

            float result;
            if (!float.TryParse(value.ToString(), out result))
            {
                result = defaultValue;
            }

            return result;
        }

        /// <summary>
        /// 转换成boolean类型,默认值
        /// </summary>
        public static Boolean ToBoolean(object value, Boolean defaultValue = false)
        {
            if (value == null)
                return defaultValue;

            bool result = false;
            if (!bool.TryParse(value.ToString(), out result))
            {
                result = defaultValue;
            }

            return result;
        }

        /// <summary>
        /// 转成字符串，失败默认转为defaultValue
        /// 作者：Administrator
        /// 日期：2012-07-31
        /// </summary>
        public static string ToString(object value, string defaultValue = "")
        {
            if (value != null)
            {
                return value.ToString();
            }

            return defaultValue;
        }

        /// <summary>
        /// 将日期转为指定格式的字符串,默认使用yyyy-MM-dd,小于或等于1900年时取空
        /// 作者:Administrator
        /// 日期:2013-03-18
        /// </summary>
        public static string ToDateTimeString(object value, string format = "yyyy-MM-dd", bool isSimple = false)
        {
            if (value == null)
                return "";

            DateTime result;
            if (DateTime.TryParse(value.ToString(), out result))
            {
                if (!isSimple || format.Contains("d"))
                {
                    return result.ToString(format);
                }
                else
                {
                    if (DateTime.Now.Year > result.Year || DateTime.Now.Month > result.Month ||
                        (DateTime.Now.Day - 1) > result.Day)
                    {
                        return result.ToString("yyyy-MM-dd " + format);
                    }
                    else
                    {
                        if (result.Year > DateTime.Now.Year || result.Month > DateTime.Now.Month ||
                            result.Day > DateTime.Now.Day)
                        {
                            return result.ToString("yyyy-MM-dd " + format);
                        }

                        if (DateTime.Now.Day == result.Day)
                        {
                            return result.ToString(format);
                        }

                        return "昨天 " + result.ToString(format);
                    }
                }
            }

            return "";
        }

        /// <summary>
        /// 转为日期类型
        /// </summary>
        public static DateTime ToDateTime(object value)
        {
            return ToDateTime(value, DateTime.Now);
        }

        /// <summary>
        /// 转为日期类型
        /// </summary>
        public static DateTime ToDateTime(object value, DateTime defaultTime)
        {
            if (value == null)
                return defaultTime;

            DateTime result;
            if (DateTime.TryParse(value.ToString(), out result))
            {
                return result;
            }

            return defaultTime;
        }

        /// <summary>
        /// 取字符串左侧指定数量字符串
        /// </summary>
        public static string Left(string value, int leftCount)
        {
            if (string.IsNullOrWhiteSpace(value))
                return "";

            if (value.Length < leftCount)
                return value;

            return value.Substring(0, leftCount);
        }

        /// <summary>
        /// 将DataTable的指定列以嵌套的方式转为Json字符串
        /// </summary>
        public static string ToJson(this DataSet ds, bool isList = false, params string[] columns)
        {
            return ToJson(ds.Tables[0], isList, columns);
        }

        /// <summary>
        /// 将DataTable的指定列以嵌套的方式转为Json字符串
        /// </summary>
        public static string ToJson(this DataTable dt, bool isList = false, params string[] columns)
        {
            //组合json字符串
            if (isList)
            {
                string[] rowArray = new string[dt.Rows.Count];

                if (columns == null || columns.Length == 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        rowArray[i] = "{" + ToJson(dt.Rows[i], dt.Columns) + "}";
                    }
                }
                else
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        rowArray[i] = "{" + ToJson(dt.Rows[i], columns) + "}";
                    }
                }

                return "[" + string.Join(",", rowArray) + "]";
            }
            else
            {
                if (columns == null || columns.Length == 0)
                {
                    return "{" + ToJson(dt.Rows[0], dt.Columns) + "}";
                }
                else
                {
                    return "{" + ToJson(dt.Rows[0], columns) + "}";
                }
            }
        }

        /// <summary>
        /// 逐行转出json字符串
        /// </summary>
        private static string ToJson(DataRow row, DataColumnCollection columns)
        {
            string[] columnValues = new string[columns.Count];

            for (int i = 0; i < columnValues.Length; i++)
            {
                object obj = row[columns[i]];

                columnValues[i] = "\"" + columns[i] + "\":" + StringFormat(obj.ToString(), obj.GetType());
            }

            return string.Join(",", columnValues);
        }

        /// <summary>
        /// 逐行转出json字符串
        /// </summary>
        private static string ToJson(DataRow row, string[] columns)
        {
            string[] columnValues = new string[columns.Length];

            for (int i = 0; i < columnValues.Length; i++)
            {
                object obj = row[columns[i]];

                columnValues[i] = "\"" + columns[i] + "\":" + StringFormat(obj.ToString(), obj.GetType());
            }

            return string.Join(",", columnValues);
        }

        /// <summary> 
        /// 格式化字符型、日期型、布尔型 
        /// </summary> 
        private static string StringFormat(string str, Type type)
        {
            if (str.Length == 0)
            {
                return "\"\"";
            }

            if (type == typeof(string))
            {
                return "\"" + FormatForJson(str) + "\"";
            }
            else if (type == typeof(DateTime))
            {
                return DateTime.Parse(str).Ticks.ToString();
            }
            else if (type == typeof(bool))
            {
                return str.ToLower();
            }
            else if (type == typeof(System.Single))
            {
                return Double.Parse(str).ToString();
            }
            else
            {
                return str;
            }
        }

        /// <summary> 
        /// 过滤特殊字符 
        /// </summary> 
        public static string FormatForJson(string s)
        {
            StringBuilder builder = new StringBuilder();

            for (int i = 0; i < s.Length; i++)
            {
                char c = s[i];

                switch (c)
                {
                    case '\"':
                        builder.Append("\\\"");
                        break;
                    case '\\':
                        builder.Append("\\\\");
                        break;
                    case '/':
                        builder.Append("\\/");
                        break;
                    case '\b':
                        builder.Append("\\b");
                        break;
                    case '\f':
                        builder.Append("\\f");
                        break;
                    case '\n':
                        builder.Append("\\n");
                        break;
                    case '\r':
                        builder.Append("\\r");
                        break;
                    case '\t':
                        builder.Append("\\t");
                        break;
                    default:
                        builder.Append(c);
                        break;
                }
            }

            return builder.ToString();
        }

        /// <summary>
        /// 格式化文件大小显示
        /// </summary>
        /// <param name="length">以字节的方式显示</param>
        /// <returns>标准的显示方式</returns>
        public static string FormatFileLength(long length)
        {
            if (length < 1024)
            {
                return length + "B"; //字节
            }
            else if (length < 1048576)
            {
                return (length / 1024D).ToString("0.##") + "KB";
            }
            else if (length < 1073741824)
            {
                return (length / 1048576D).ToString("0.##") + "MB";
            }
            else if (length < 1099511627776)
            {
                return (length / 1073741824D).ToString("0.##") + "GB";
            }
            else
            {
                return (length / 1099511627776D).ToString("0.##") + "TB";
            }
        }

        /// <summary>
        /// 文件大小类型
        /// </summary>
        public enum FileSize
        {
            B,
            KB,
            MB,
            GB,
            TB
        }

        /// <summary>
        /// 判断文件是否过大
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="maxSize"></param>
        /// <param name="fileSize"></param>
        /// <returns></returns>
        public static bool IsTooLarge(string filePath, long maxSize, FileSize fileSize)
        {
            long bytesLength = 0;
            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                bytesLength = fs.Length;
            }

            switch (fileSize)
            {
                case FileSize.KB:
                    maxSize = maxSize * 1024;
                    break;
                case FileSize.MB:
                    maxSize = maxSize * 1024 * 1024;
                    break;
                case FileSize.GB:
                    maxSize = maxSize * 1024 * 1024 * 1024;
                    break;
                case FileSize.TB:
                    maxSize = maxSize * 1024 * 1024 * 1024 * 1024;
                    break;
            }

            if (bytesLength > maxSize)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// 判断文件是否过大
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="maxSize"></param>
        /// <param name="fileSize"></param>
        /// <returns></returns>
        public static bool IsTooLarge(long srcSize, long maxSize, FileSize fileSize)
        {
            long bytesLength = srcSize;
            switch (fileSize)
            {
                case FileSize.KB:
                    maxSize = maxSize * 1024;
                    break;
                case FileSize.MB:
                    maxSize = maxSize * 1024 * 1024;
                    break;
                case FileSize.GB:
                    maxSize = maxSize * 1024 * 1024 * 1024;
                    break;
                case FileSize.TB:
                    maxSize = maxSize * 1024 * 1024 * 1024 * 1024;
                    break;
            }

            if (bytesLength > maxSize)
            {
                return true;
            }

            return false;
        }


        public static double ToFormatLength(string filePath, FileSize fileSize)
        {
            long bytesLength = 0;
            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                bytesLength = fs.Length;
            }

            double size = 0;
            switch (fileSize)
            {
                case FileSize.B:
                    size = bytesLength;
                    break;
                case FileSize.KB:
                    size = ToDouble((bytesLength / 1024D).ToString("0.##"));
                    break;
                case FileSize.MB:
                    size = ToDouble((bytesLength / 1048576D).ToString("0.##"));
                    break;
                case FileSize.GB:
                    size = ToDouble((bytesLength / 1073741824D).ToString("0.##"));
                    break;
                case FileSize.TB:
                    size = ToDouble((bytesLength / 1099511627776D).ToString("0.##"));
                    break;
            }

            return size;
        }

        /// <summary>
        /// 将图片路径转化为base64
        /// </summary>
        /// <param name="filePath">源路径</param>
        /// <param name="isDelSrc">是否删除源文件以及文件夹</param>
        /// <returns></returns>
        public static string ImageToBase64(string filePath, bool isDelSrc = true)
        {
            string base64 = "";
            using (FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                byte[] bytes = new byte[fileStream.Length];
                fileStream.Read(bytes, 0, (int) fileStream.Length);
                base64 = Convert.ToBase64String(bytes);
            }

            if (isDelSrc)
            {
                try
                {
                    DirectoryInfo dirInfo = new DirectoryInfo(Path.GetDirectoryName(filePath));
                    if (dirInfo.Exists)
                    {
                        dirInfo.Delete(true);
                    }
                }
                catch (Exception)
                {
                }
            }

            return "data:image/png;base64," + base64;
        }
    }
}