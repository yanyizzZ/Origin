using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;

namespace Origin.Common
{
    #region string 扩展
    /// <summary>
    /// string 扩展
    /// </summary>
    public static class StrExtensions
    {
        #region 字符串格式化
        /// <summary>
        /// 字符串格式化 eg: '{0}'.Fmt(obj)
        /// </summary>
        /// <param name="format"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static string Fmt(this string format, params object[] args)
        {
            return string.Format(format, args);
        }
        #endregion

        #region 判断null 或 空
        /// <summary>
        /// 判断null 或 空
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(this string s)
        {
            return string.IsNullOrEmpty(s) || s.IsNullOrBlank();
        }
        /// <summary>
        /// null 或空白字符串
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsNullOrBlank(this string s)
        {
            return string.IsNullOrWhiteSpace(s);
        }
        #endregion

        #region 是否为中文
        private static Regex rxChiese = new Regex("^[\u4e00-\u9fa5]$", RegexOptions.Compiled | RegexOptions.Multiline);
        private static Regex rxSymbol = new Regex(@"[，。；？~！：‘“”’【】（）]", RegexOptions.Compiled | RegexOptions.Multiline);
        /// <summary>
        /// 是否为中文
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns> 

        public static bool IsChinese(this string s)
        {
            return !rxChiese.IsMatch(s);
        }
        #endregion

        #region json 序列化
        /// <summary>
        /// json 序列化
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ToJson(this object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }
        #endregion

        #region 使用 path 查找json数据  详见path参数备注
        /// <summary>
        /// 使用path 查找json数据  详见path参数备注
        /// </summary>
        /// <param name="json"></param>
        /// <param name="path">json 取值路径 $.book.authors[0].name or [0].book.authors[0].name </param>
        /// <returns></returns>
        public static string JsonPath(this string json, string path)
        {
            var token = JToken.Parse(json).SelectToken(path);
            return token.ToString();
        }
        #endregion

        #region 反序列化json 字符串
        /// <summary>
        /// 反序列化json 字符串
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T FromJson<T>(this string obj)
        {
            return JsonConvert.DeserializeObject<T>(obj);
        }
        #endregion

        #region 忽略大小写比较字符串
        /// <summary>
        /// 忽略大小写比较字符串
        /// </summary>
        /// <param name="strA"></param>
        /// <param name="compareTo"></param>
        /// <returns></returns>
        public static bool IgnorCaseTo(this string strA, string compareTo)
        {
            return string.Compare(strA, compareTo, true) == 0;
        }
        #endregion

        #region 将字符串转为int
        /// <summary>
        /// 将字符串转为int
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static int ToInt32(this string str)
        {
            return Convert.ToInt32(str);
        }
        public static short ToInt16(this string str)
        {
            return Convert.ToInt16(str);
        }
        #endregion

        #region 将字符串转为decimal
        /// <summary>
        /// 将字符串转为decimal
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static Decimal ToDecima(this string str)
        {
            return Convert.ToDecimal(str);
        }
        #endregion

        #region 将字符串转为datetime
        /// <summary>
        /// 将字符串转为datetime
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static DateTime ToDateTime(this string str)
        {
            return Convert.ToDateTime(str);
        }
        #endregion

        #region 将字符串转为GUID
        /// <summary>
        /// 将字符串转为GUID
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static Guid ToGuid(this string str)
        {
            return Guid.Parse(str);
        }
        #endregion

        #region 判断字符串是否为GUID
        /// <summary>
        /// 判断字符串是否为GUID
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsGuid(string str)
        {
            Guid g = Guid.Empty;
            return Guid.TryParse(str, out g);
        }
        #endregion
    }
    #endregion

    #region DateTime扩展
    /// <summary>
    /// DateTime扩展
    /// </summary>
    public static class DateTimeExtensions
    {
        /// <summary>
        /// DateTime转化成string类型（yyyy-MM-dd HH:mm:ss）
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static string DateTimeToStr(this DateTime time)
        {
            return time.ToString("yyyy-MM-dd HH:mm:ss");
        }
        /// <summary>
        /// DateTime转化成string类型（yyyy-MM-dd）
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static string DateTimeToShortStr(this DateTime time)
        {
            return time.ToString("yyyy-MM-dd");
        }
    }
    #endregion

    #region 集合扩展（IEnumerable）
    /// <summary>
    /// 集合扩展（IEnumerable）
    /// </summary>
    public static class EnumerableExtensions
    {

        #region IEnumerable转为IEnumerable<object>
        /// <summary>
        /// IEnumerable转为IEnumerable<object>
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static IEnumerable<object> ToEnumrableT(this IEnumerable list)
        {
            var en = list.GetEnumerator();
            while (en.MoveNext())
            {
                yield return en.Current;
            }
            en = null;
        }
        #endregion

        #region 通过索引获取集合数据
        /// <summary>
        /// 通过索引获取集合数据
        /// </summary>
        /// <param name="list"></param>
        /// <param name="idx"></param>
        /// <returns></returns>
        public static object GetByIndex(this IEnumerable list, int idx)
        {
            var en = list.GetEnumerator();
            int i = 0;
            object r = null;
            while (en.MoveNext())
            {
                if (i == idx)
                {
                    r = en.Current;
                    break;
                }
                i++;
            }
            en = null;
            return r;
        }
        #endregion

        #region 集合不为null并且有至少一个元素
        /// <summary>
        /// 集合不为null并且有至少一个元素
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static bool HasElement<T>(this IEnumerable<T> list)
        {
            return list != null && list.Any();
        }
        #endregion

        #region 遍历集合，执行act
        /// <summary>
        /// 遍历集合，执行act
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="act">每一项上执行的方法</param>
        public static void Each<T>(this IEnumerable<T> list, Action<T> act)
        {
            using (var en = list.GetEnumerator())
            {
                while (en.MoveNext())
                    act(en.Current);
            }
        }
        #endregion

        #region 合并集合数据
        /// <summary>
        /// 合并集合数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="arr"></param>
        /// <param name="separator"></param>
        /// <param name="emptyModel"></param>
        /// <returns></returns>
        public static string JoinStr<T>(this IEnumerable<T> arr, string separator = ",", int emptyModel = 1)
        {
            return emptyModel == 1 ? (string.Join(separator, arr.Select(c => c + ""))) : (string.Join(separator, arr.Select(c => c + "").Where(c => c.Length > 0)));
        }
        #endregion
    }
    #endregion

    #region 枚举扩展
    /// <summary>
    /// 枚举扩展
    /// </summary>
    public static class EnumExtensions
    {
        #region 取枚举名称
        /// <summary>
        /// 取枚举名称
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumvalue"></param>
        /// <returns></returns>
        public static string GetEnumName<T>(this T enumvalue) where T : struct
        {
            return Enum.GetName(typeof(T), enumvalue);
        }
        #endregion
    }
    #endregion

    #region 动态Linq扩展
    /// <summary>
    /// 动态Linq扩展
    /// </summary>
    public static class DynamicLinqExpressions
    {
        public static Expression<Func<T, bool>> True<T>() { return f => true; }
        public static Expression<Func<T, bool>> False<T>() { return f => false; }
        public static Expression<T> Compose<T>(this Expression<T> first, Expression<T> second, Func<Expression, Expression, Expression> merge)
        {
            // build parameter map (from parameters of second to parameters of first)  
            var map = first.Parameters.Select((f, i) => new { f, s = second.Parameters[i] }).ToDictionary(p => p.s, p => p.f);

            // replace parameters in the second lambda expression with parameters from the first  
            var secondBody = ParameterRebinder.ReplaceParameters(map, second.Body);

            // apply composition of lambda expression bodies to parameters from the first expression   
            return Expression.Lambda<T>(merge(first.Body, secondBody), first.Parameters);
        }

        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> first, Expression<Func<T, bool>> second)
        {
            return first.Compose(second, Expression.And);
        }

        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> first, Expression<Func<T, bool>> second)
        {
            return first.Compose(second, Expression.Or);
        }
    }

    public class ParameterRebinder : ExpressionVisitor
    {
        private readonly Dictionary<ParameterExpression, ParameterExpression> map;

        public ParameterRebinder(Dictionary<ParameterExpression, ParameterExpression> map)
        {
            this.map = map ?? new Dictionary<ParameterExpression, ParameterExpression>();
        }

        public static Expression ReplaceParameters(Dictionary<ParameterExpression, ParameterExpression> map, Expression exp)
        {
            return new ParameterRebinder(map).Visit(exp);
        }

        protected override Expression VisitParameter(ParameterExpression p)
        {
            ParameterExpression replacement;
            if (map.TryGetValue(p, out replacement))
            {
                p = replacement;
            }
            return base.VisitParameter(p);
        }
    }
    #endregion

    #region 异常及日志扩展
    /// <summary>
    /// 异常及日志扩展
    /// </summary>
    public static class ExceptLogExtensions
    {
        //[ThreadStatic]
        //private static Logger _loger = null;
        /// <summary>
        /// 线程唯一的日志对象
        /// </summary>
        //public static Logger Loger { get { return _loger ?? LogManager.GetCurrentClassLogger(); } }

        #region 记录异常日志
        /// <summary>
        /// 记录异常日志
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="message">错误信息（null=》ex.Message）</param>
        public static void LogError(this Exception ex, string message = "")
        {
            //Loger.Error(message, ex);
        }

        public static void LogErrorEntityValidationErrors(this Exception ex, string message = "")
        {
            dynamic exdynamic = ex;
            var stb = new StringBuilder();
            try
            {

                var eee = exdynamic;
                if (eee.EntityValidationErrors != null)
                {
                    foreach (var item in eee.EntityValidationErrors)
                    {
                        if (item.ValidationErrors != null)
                        {
                            foreach (var item2 in item.ValidationErrors)
                            {
                                stb.Append(item2.ErrorMessage + ";");
                            }

                        }

                    }
                }
               // Loger.Error(message + stb.ToString(), ex);
            }
            catch (Exception e)
            {
                //Loger.Error(message, e);
            }

        }
        #endregion

        #region 调试时写入消息和异常。
        /// <summary>
        /// 调试时写入消息和异常。 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="args"></param>
        public static void LogMessage(string message, params object[] args)
        {
            //Loger.Debug(message, args);
        }
        #endregion

        public static void LogInfoMessage(string message, params object[] args)
        {
            //var logger = LogManager.GetCurrentClassLogger();

            //logger.Info(message, args);
        }

        #region 在一个try{}catch(){} 中执行方法
        /// <summary>
        /// 在一个try{}catch(){} 中执行方法
        /// </summary>
        /// <param name="act"></param>
        /// <param name="onerror">用于决定是否抛出异常。 返回 true:抛出异常 </param>
        /// <param name="onfinaly"></param>
        public static void TryDo(this Action act, Func<Exception, bool> onerror = null, Action onfinaly = null)
        {

            try { act(); }
            catch (Exception ex)
            {
                if (onerror == null)
                {
                    throw ex;
                }
                if (onerror(ex))
                {
                    throw ex;
                }
            }
            finally
            {
                if (onfinaly != null) onfinaly();
            }
        }
        #endregion
    }
    #endregion

    #region 程序集扩展
    /// <summary>
    /// 程序集扩展
    /// </summary>
    public static class AssemblyExtender
    {
        /// <summary>
        /// 应用中需要处理的程序集名称前缀
        /// </summary>
        public static string[] AppAssNames = new string[] { "NYUWarehouse.", "Admin." };

        #region 从所有程序集中取得指定前缀程序集
        /// <summary>
        /// 从所有程序集中取得指定前缀程序集
        /// </summary>
        /// <param name="domain"></param>
        /// <param name="assNamePrefixs">程序集全称前缀集合，默认为AssemblyExtender.AppAssNames</param>
        /// <returns></returns>
        public static IEnumerable<Assembly> GetMyAssemblies(this AppDomain domain, string[] assNamePrefixs = null)
        {
            if (assNamePrefixs == null)
                assNamePrefixs = AppAssNames;
            return AppDomain.CurrentDomain.GetAssemblies().Where(c => assNamePrefixs.Any(s => c.FullName.StartsWith(s)));
        }
        #endregion

        #region 获取方法属性
        /// <summary>
        /// 获取方法属性
        /// </summary>
        /// <param name="assembly"></param>
        /// <param name="extendedType"></param>
        /// <returns></returns>
        static IEnumerable<MethodInfo> GetExtensionMethods(this Assembly assembly, Type extendedType)
        {
            var query = from type in assembly.GetTypes()
                        where type.IsSealed && !type.IsGenericType && !type.IsNested
                        from method in type.GetMethods(BindingFlags.Static
                            | BindingFlags.Public | BindingFlags.NonPublic)
                        where method.IsDefined(typeof(ExtensionAttribute), false)
                        where method.GetParameters()[0].ParameterType == extendedType
                        select method;
            return query;
        }
        #endregion
    }
    #endregion

    public static class QueryableExt
    {
        #region IQueryable扩展
        public static IQueryable<TSource> Pager<TSource>(this IQueryable<TSource> source, int curpage, int pagesize)
        {
            return source.Skip(pagesize * (curpage - 1)).Take(pagesize);
        }
        #endregion}
    }
}
