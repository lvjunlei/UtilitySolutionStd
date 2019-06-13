using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Utility.Cache
{
    /// <summary>
    /// 简单内存缓存服务
    /// </summary>
    public class SimpleCacheService : ICacheService, IDisposable
    {
        private static readonly ConcurrentDictionary<string, CacheObject> _cache;

        private static readonly Task _monitorTask;

        public static bool Start { get; private set; }

        #region 构造函数

        /// <summary>
        /// 
        /// </summary>
        static SimpleCacheService()
        {
            _cache = new ConcurrentDictionary<string, CacheObject>();
            Start = true;
            _monitorTask = Task.Run(() =>
            {
                CacheMonitor();
            });
        }

        #endregion

        #region Exists

        /// <summary>
        /// 是否存在指定键的缓存
        /// </summary>
        /// <param name="key">缓存键</param>
        public bool Exists(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                return false;
            }

            return _cache.ContainsKey(key);
        }

        #endregion

        #region Get

        /// <summary>
        /// 从缓存中获取数据
        /// </summary>
        /// <typeparam name="T">缓存数据类型</typeparam>
        /// <param name="key">缓存键</param>
        /// <returns></returns>
        public T Get<T>(string key)
        {
            CheckKeyIsNull(key);
            CheckKeyExists(key);
            var co = _cache[key];
            co.Update();
            return (T)co.Value;
        }

        /// <summary>
        /// 从缓存中获取数据
        /// </summary>
        /// <typeparam name="T">缓存数据类型</typeparam>
        /// <param name="key">缓存键</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public T TryGet<T>(string key, T defaultValue = default(T))
        {
            if (string.IsNullOrEmpty(key) || !_cache.ContainsKey(key))
            {
                return defaultValue;
            }
            var co = _cache[key];
            co.Update();
            return (T)co.Value;
        }

        /// <summary>
        /// 从缓存中获取数据，如果不存在，则执行获取数据操作并添加到缓存中
        /// </summary>
        /// <typeparam name="T">缓存数据类型</typeparam>
        /// <param name="key">缓存键</param>
        /// <param name="func">获取数据操作</param>
        public T Get<T>(string key, Func<T> func)
        {
            CheckKeyIsNull(key);
            if (!_cache.ContainsKey(key))
            {
                var obj = func.Invoke();
                if (!_cache.TryAdd(key, new CacheObject(key, obj)))
                {
                    throw new Exception($"添加缓存 {key} 失败");
                }
            }
            var co = _cache[key];
            co.Update();
            return (T)co.Value;
        }

        /// <summary>
        /// 从缓存中获取数据，如果不存在，则执行获取数据操作并添加到缓存中
        /// </summary>
        /// <typeparam name="T">缓存数据类型</typeparam>
        /// <param name="key">缓存键</param>
        /// <param name="func">获取数据操作</param>
        /// <param name="expiration">过期时间间隔</param>
        public T Get<T>(string key, Func<T> func, TimeSpan? expiration)
        {
            CheckKeyIsNull(key);
            if (!_cache.ContainsKey(key))
            {
                var obj = func.Invoke();
                if (!_cache.TryAdd(key, new CacheObject(key, obj)))
                {
                    throw new Exception($"添加缓存 {key} 失败");
                }
            }
            var co = _cache[key];
            co.Update();
            return (T)co.Value;
        }

        #endregion

        #region Add

        /// <summary>
        /// 当缓存数据不存在则添加，已存在不会添加，添加成功返回true
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="value">值</param>
        public bool Add(string key, object value)
        {
            CheckKeyIsNull(key);
            if (_cache.ContainsKey(key))
            {
                throw new Exception($"已存在 KEY 为：{key} 的缓存项");
            }
            return _cache.TryAdd(key, new CacheObject(key, value));
        }

        /// <summary>
        /// 当缓存数据不存在则添加，已存在不会添加，添加成功返回true
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="value">值</param>
        /// <param name="expiration">过期时间间隔</param>
        public bool Add(string key, object value, TimeSpan expiration)
        {
            CheckKeyIsNull(key);
            if (_cache.ContainsKey(key))
            {
                throw new Exception($"已存在 KEY 为：{key} 的缓存项");
            }
            return _cache.TryAdd(key, new CacheObject(key, value, null, expiration));
        }

        /// <summary>
        /// 添加缓存
        /// </summary>
        /// <param name="key">缓存Key</param>
        /// <param name="value">缓存Value</param>
        /// <param name="expiration">缓存时长</param>
        /// <param name="isSliding">是否滑动过期（如果在过期时间内有操作，则以当前时间点延长过期时间）</param>
        /// <returns></returns>
        public bool Add(string key, object value, TimeSpan? expiration, bool isSliding = true)
        {
            CheckKeyIsNull(key);
            if (_cache.ContainsKey(key))
            {
                throw new Exception($"已存在 KEY 为：{key} 的缓存项");
            }
            return _cache.TryAdd(key, new CacheObject(key, value, isSliding ? expiration : null, isSliding ? null : expiration));
        }

        /// <summary>
        /// 添加缓存
        /// </summary>
        /// <param name="key">缓存Key</param>
        /// <param name="value">缓存Value</param>
        /// <param name="expirationSliding">滑动过期时长（如果在过期时间内有操作，则以当前时间点延长过期时间）</param>
        /// <param name="expirationAbsoulte">绝对过期时长</param>
        /// <returns></returns>
        public bool Add(string key, object value, TimeSpan expirationSliding, TimeSpan expirationAbsoulte)
        {
            CheckKeyIsNull(key);
            if (_cache.ContainsKey(key))
            {
                throw new Exception($"已存在 KEY 为：{key} 的缓存项");
            }
            return _cache.TryAdd(key, new CacheObject(key, value, expirationSliding, expirationAbsoulte));
        }

        #endregion

        #region TryAdd

        /// <summary>
        /// 当缓存数据不存在则添加，已存在不会添加，添加成功返回true
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="value">值</param>
        public bool TryAdd(string key, object value)
        {
            CheckKeyIsNull(key);
            if (_cache.ContainsKey(key))
            {
                return false;
            }
            return _cache.TryAdd(key, new CacheObject(key, value));
        }

        /// <summary>
        /// 当缓存数据不存在则添加，已存在不会添加，添加成功返回true
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="value">值</param>
        /// <param name="expiration">过期时间间隔</param>
        public bool TryAdd(string key, object value, TimeSpan expiration)
        {
            CheckKeyIsNull(key);
            if (_cache.ContainsKey(key))
            {
                return false;
            }
            return _cache.TryAdd(key, new CacheObject(key, value, null, expiration));
        }

        /// <summary>
        /// 添加缓存
        /// </summary>
        /// <param name="key">缓存Key</param>
        /// <param name="value">缓存Value</param>
        /// <param name="expiration">缓存时长</param>
        /// <param name="isSliding">是否滑动过期（如果在过期时间内有操作，则以当前时间点延长过期时间）</param>
        /// <returns></returns>
        public bool TryAdd(string key, object value, TimeSpan? expiration, bool isSliding = true)
        {
            CheckKeyIsNull(key);
            if (_cache.ContainsKey(key))
            {
                return false;
            }
            return _cache.TryAdd(key, new CacheObject(key, value, isSliding ? expiration : null, isSliding ? null : expiration));
        }

        /// <summary>
        /// 添加缓存
        /// </summary>
        /// <param name="key">缓存Key</param>
        /// <param name="value">缓存Value</param>
        /// <param name="expirationSliding">滑动过期时长（如果在过期时间内有操作，则以当前时间点延长过期时间）</param>
        /// <param name="expirationAbsoulte">绝对过期时长</param>
        /// <returns></returns>
        public bool TryAdd(string key, object value, TimeSpan expirationSliding, TimeSpan expirationAbsoulte)
        {
            CheckKeyIsNull(key);
            if (_cache.ContainsKey(key))
            {
                return false;
            }
            return _cache.TryAdd(key, new CacheObject(key, value, expirationSliding, expirationAbsoulte));
        }

        #endregion

        #region Remove

        /// <summary>
        /// 移除缓存
        /// </summary>
        /// <param name="keys">缓存键集合</param>
        public void Remove(params string[] keys)
        {
            if (keys == null || keys.Any(u => string.IsNullOrEmpty(u)))
            {
                throw new ArgumentNullException(nameof(keys));
            }
            foreach (var key in keys)
            {
                if (_cache.ContainsKey(key))
                {
                    _cache.TryRemove(key, out _);
                }
            }
        }

        #endregion

        #region Clear

        /// <summary>
        /// 清空缓存
        /// </summary>
        public void Clear()
        {
            _cache.Clear();
        }

        #endregion

        #region Common

        /// <summary>
        /// 检查 KEY 是否为空
        /// </summary>
        /// <param name="key">key</param>
        private static void CheckKeyIsNull(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException(nameof(key));
            }
        }

        /// <summary>
        /// 检查 KEY 是否存在
        /// </summary>
        /// <param name="key">key</param>
        private static void CheckKeyExists(string key)
        {
            if (!_cache.ContainsKey(key))
            {
                throw new Exception($"不存在 KEY 为：{key} 的缓存");
            }
        }

        /// <summary>
        /// 缓存过期检测
        /// </summary>
        private static async void CacheMonitor()
        {
            while (Start)
            {
                if (!_cache.Any())
                {
                    await Task.Delay(1000);
                }
                var keys = _cache.Keys.ToList();
                foreach (var key in keys)
                {
                    if (_cache.ContainsKey(key))
                    {
                        if (_cache[key].IsExpiration())
                        {
                            // 移除过期缓存
                            _cache.TryRemove(key, out _);
                        }
                    }
                }
                await Task.Delay(100);
            }
        }

        public void Dispose()
        {
            Start = false;
            Clear();
        }

        #endregion
    }

    /// <summary>
    /// 缓存对象
    /// </summary>
    class CacheObject : IDisposable
    {
        /// <summary>
        /// Key
        /// </summary>
        public string Key { get; }

        /// <summary>
        /// 缓存对象
        /// </summary>
        public object Value { get; private set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedTime { get; }

        /// <summary>
        /// 更新/访问时间
        /// </summary>
        public DateTime UpdateTime { get; private set; }

        /// <summary>
        /// 滑动过期时间
        /// </summary>
        public TimeSpan? ExpirationSliding { get; }

        /// <summary>
        /// 绝对过期时间
        /// </summary>
        public TimeSpan? ExpirationAbsoulte { get; }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="value">对象</param>
        /// <param name="expirationSliding">滑动过期时间</param>
        /// <param name="expirationAbsoulte">绝对过期时间</param>
        public CacheObject(string key, object value, TimeSpan? expirationSliding = null, TimeSpan? expirationAbsoulte = null)
        {
            Key = key;
            Value = value;
            ExpirationSliding = expirationSliding;
            ExpirationAbsoulte = expirationAbsoulte;
            CreatedTime = UpdateTime = DateTime.Now;
        }

        /// <summary>
        /// 更新缓存访问时间
        /// </summary>
        public void Update()
        {
            UpdateTime = DateTime.Now;
        }

        /// <summary>
        /// 更新缓存
        /// </summary>
        /// <param name="value">对象</param>
        public void Update(object value)
        {
            Value = value;
            Update();
        }

        /// <summary>
        /// 是否过期
        /// </summary>
        /// <returns></returns>
        public bool IsExpiration()
        {
            if (ExpirationAbsoulte.HasValue)
            {
                if (CreatedTime.Add(ExpirationAbsoulte.Value) <= DateTime.Now)
                {
                    return true;
                }
            }

            if (ExpirationSliding.HasValue)
            {
                if (UpdateTime.Add(ExpirationSliding.Value) <= DateTime.Now)
                {
                    return true;
                }
            }

            return false;
        }

        public void Dispose()
        {
            Value = null;
        }
    }
}
