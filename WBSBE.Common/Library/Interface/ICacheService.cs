using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WBSBE.Common.Library.Interface
{
    public interface ICacheService
    {
        Task<string> GetCacheValueAsync(string key, int db = -1);
        void SetChangeValueAsync(string key, string value, TimeSpan? timeSpan = null, int db = -1);
        void DeleteCacheRedis(string key, int db = -1);
        bool KeyExists(string key, int db = -1);
        void SetKeyExpire(string key, DateTime? expiry, int db = -1);
        void SetKeyExpire(string key, TimeSpan? expiry, int db = -1);
    }
}
