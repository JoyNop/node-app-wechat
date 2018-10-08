
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;

namespace AddInConfigJson
{
    /// <summary>
    /// JSON字符串格式化后的对象
    /// </summary>
    public class JsonObj
    {
        readonly Dictionary<string, object> list;

        /// <summary>
        /// 将Json字符串转为Json对象(键值对)
        /// </summary>
        public static JsonObj Parse(string json)
        {
            if (string.IsNullOrWhiteSpace(json))
            {
                return new JsonObj(new Dictionary<string, object>());
            }

            return new JsonObj(json);
        }

        public static ArrayList ParseArray(string json)
        {
            ArrayList array = new ArrayList();
            try
            {
                if (!string.IsNullOrWhiteSpace(json))
                {
                    JavaScriptSerializer jss = new JavaScriptSerializer();
                    array = jss.Deserialize<ArrayList>(json);
                }
            }
            catch (Exception)
            { 
            }           
            return array;
        }

        protected JsonObj()
        {
        }

        protected JsonObj(string json)
        {
            try
            {
                JavaScriptSerializer jss = new JavaScriptSerializer();
                jss.MaxJsonLength = 1024 * 1024 * 10;
                list = jss.Deserialize<Dictionary<string, object>>(json);

            }
            catch (Exception ex)
            {
                list = new Dictionary<string, object>();
            }
        }

        protected JsonObj(Dictionary<string, object> list)
        {
            this.list = list;
        }

        /// <summary>
        /// 获取指定关键字的值
        /// </summary>
        public string Get(string key)
        {
            if (list.ContainsKey(key))
            {
                return MdConvert.ToString(list[key]);
            }
            return "";
        }

        /// <summary>
        /// 通过指定关键字获取子Json对象，可以以“.”对关键字做路径分割
        /// </summary>
        public JsonObj GetSon(string key)
        {
            if (list.ContainsKey(key))
            {
                return new JsonObj((Dictionary<string, object>)list[key]);
            }
            else if (key.Contains('.'))
            {
                string[] keys = key.Split('.');
                JsonObj o = this;

                for (int i = 0; i < keys.Length - 1; i++)
                {
                    o = o.GetSon(keys[i]);
                }
                return o.GetSon(keys[keys.Length - 1]);
            }
            return null;
        }

        /// <summary>
        /// 获取所有的键值对队列
        /// </summary>
        public Dictionary<string, object> GetList()
        {
            return list;
        }

        public ArrayList GetArrayList(string key)
        {
            if (list.ContainsKey(key))
            {
                ArrayList array = list[key] as ArrayList;
                return array;
            }
            else if (key.Contains(','))
            {
                string[] keys = key.Split(',');
                JsonObj o = this;

                for (int i = 0; i < keys.Length - 1; i++)
                {
                    o = o.GetSon(keys[i]);
                }
                return o.GetArrayList(keys[keys.Length - 1]);
            }
            return null;
        }


        /// <summary>
        /// 获取指定关键字的Json对象列表
        /// </summary>
        public JsonObj[] GetList(string key)
        {
            if (list.ContainsKey(key))
            {
                ArrayList array = list[key] as ArrayList;
                JsonObj[] listObj = new JsonObj[array.Count];

                for (int i = 0; i < array.Count; i++)
                {
                    listObj[i] = new JsonObj(array[i] as Dictionary<string, object>);
                }

                return listObj;
            }
            else if (key.Contains('.'))
            {
                string[] keys = key.Split('.');
                JsonObj o = this;

                for (int i = 0; i < keys.Length - 1; i++)
                {
                    o = o.GetSon(keys[i]);
                }
                return o.GetList(keys[keys.Length - 1]);
            }
            return null;
        }

        /// <summary>
        /// 各种类型转换为Json
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string Obj2Json<T>(T data)
        {
            try
            {
                System.Runtime.Serialization.Json.DataContractJsonSerializer serializer = new System.Runtime.Serialization.Json.DataContractJsonSerializer(data.GetType());
                using (MemoryStream ms = new MemoryStream())
                {
                    serializer.WriteObject(ms, data);
                    return Encoding.UTF8.GetString(ms.ToArray());
                }
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Json转换为Obj类型
        /// </summary>
        /// <param name="json"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public static object Json2Obj(string json, Type t)
        {
            try
            {
                System.Runtime.Serialization.Json.DataContractJsonSerializer serializer = new System.Runtime.Serialization.Json.DataContractJsonSerializer(t);
                using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(json)))
                {
                    return serializer.ReadObject(ms);
                }
            }
            catch
            {
                return null;
            }
        }
    }
}
