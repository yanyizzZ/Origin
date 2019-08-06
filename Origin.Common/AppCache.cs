using System;
using System.Collections.Generic;

namespace Origin.Common
{
    /// <summary>
    /// 系统缓存
    /// 提供系统缓存的基础方法
    /// </summary>
    public class AppCache
    {
        /// <summary>
        /// 容器
        /// </summary>
        private Dictionary<CacheType, Dictionary<string, ObjCache>> mainCache = new Dictionary<CacheType, Dictionary<string, ObjCache>>();

        private static AppCache instance = null;

        private AppCache() { }

        /// <summary>
        /// 实例化
        /// </summary>
        /// <returns></returns>
        public static AppCache GetInstance()
        {
            if (instance == null)
            {
                instance = new AppCache();
            }
            return instance;
        }

        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns></returns>
        public Dictionary<string, ObjCache> GetCache(CacheType type)
        {
            if (mainCache.ContainsKey(type))
            {
                foreach (KeyValuePair<CacheType, Dictionary<string, ObjCache>> mainKvp in mainCache)
                {
                    if (mainKvp.Key == type)
                    {
                        return mainKvp.Value;
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="key">标识key</param>
        /// <returns></returns>
        public ObjCache GetCache(CacheType type, string key)
        {
            if (mainCache.ContainsKey(type))
            {
                foreach (KeyValuePair<CacheType, Dictionary<string, ObjCache>> mainKvp in mainCache)
                {
                    if (mainKvp.Key == type && mainKvp.Value.ContainsKey(key))
                    {
                        foreach (KeyValuePair<string, ObjCache> contentKvp in mainKvp.Value)
                        {
                            if (contentKvp.Key == key)
                            {
                                return contentKvp.Value;
                            }
                        }
                        break;
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// 总缓存数
        /// </summary>
        public int Count
        {
            get
            {
                int count = 0;
                foreach (KeyValuePair<CacheType, Dictionary<string, ObjCache>> mainKvp in mainCache)
                {
                    count += mainKvp.Value.Count;
                }
                return count;
            }
        }

        /// <summary>
        /// 存入缓存
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="key">标识key</param>
        /// <param name="value">缓存值</param>
        /// <returns></returns>
        public void SetCache(CacheType type, string key, object value)
        {
            lock (mainCache)
            {
                Dictionary<string, ObjCache> dictContent = null;

                if (mainCache.ContainsKey(type))
                {
                    foreach (KeyValuePair<CacheType, Dictionary<string, ObjCache>> mainKvp in mainCache)
                    {
                        if (mainKvp.Key == type)
                        {
                            dictContent = mainKvp.Value;
                            break;
                        }
                    }
                    mainCache.Remove(type);
                }

                if (dictContent == null)
                {
                    dictContent = new Dictionary<string, ObjCache>();
                }
                else if (dictContent.ContainsKey(key))
                {
                    dictContent.Remove(key);
                }

                ObjCache newCache = new ObjCache(DateTime.Now, value);
                dictContent.Add(key, newCache);

                mainCache.Add(type, dictContent);
            }
        }

        /// <summary>
        /// 清除所有缓存
        /// </summary>
        public void Clear()
        {
            lock (mainCache)
            {
                mainCache.Clear();
            }
        }

        /// <summary>
        /// 根据类型和Key清除缓存（key为null时，清理类型的所有缓存）
        /// </summary>
        /// <param name="type">缓存类型</param>
        /// <param name="key"></param>
        public void Remove(CacheType type, string key)
        {
            lock (mainCache)
            {
                if (mainCache.ContainsKey(type))
                {
                    if (key == null)
                    {
                        mainCache.Remove(type);
                    }
                    else
                    {
                        foreach (KeyValuePair<CacheType, Dictionary<string, ObjCache>> mainKvp in mainCache)
                        {
                            if (mainKvp.Key == type && mainKvp.Value.ContainsKey(key))
                            {
                                mainKvp.Value.Remove(key);
                                mainCache.Remove(type);
                                mainCache.Add(type, mainKvp.Value);
                                break;
                            }
                        }
                    }
                }
            }
        }
    }






    /// <summary>
    /// 缓存类型
    /// </summary>
    public enum CacheType
    {
        /// <summary>
        /// 普通缓存
        /// </summary>
        Normal = 0,
        /// <summary>
        /// 验证码
        /// </summary>
        VerificationCode = 1,
        /// <summary>
        /// 微信网页Code兑换UserKey
        /// </summary>
        WeixinCodeKey = 2
    }

    /// <summary>
    /// 缓存对象
    /// </summary>
    public class ObjCache
    {
        /// <summary>
        /// 实例化
        /// </summary>
        /// <param name="cacheTime">存入缓存的时间</param>
        /// <param name="content">缓存内容</param>
        public ObjCache(DateTime cacheTime, object content)
        {
            this.CacheTime = cacheTime;
            this.Content = content;
        }
        /// <summary>
        /// 存入缓存的时间
        /// </summary>
        public DateTime CacheTime { get; set; }

        /// <summary>
        /// 缓存内容
        /// </summary>
        public object Content
        {
            get;
            set;
        }
    }
}
