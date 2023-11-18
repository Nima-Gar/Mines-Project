using StackExchange.Redis;
using Contracts;
using Microsoft.Extensions.Configuration;
using System.Text.Json;

namespace Services;

public class CacheService : ICacheService
{
    private readonly IDatabase _cacheDb;
    private readonly IConfiguration _config;

    public CacheService(IConfiguration configuration)
    {
        _config = configuration;

        var redis = ConnectionMultiplexer.Connect(_config["Cache:Url"]);
        _cacheDb = redis.GetDatabase();
    }

    public T GetData<T>(string key)
    {
        var value = _cacheDb.StringGet(key);
        if (!string.IsNullOrEmpty(value))
        {
            return JsonSerializer.Deserialize<T>(value);
        }

        return default;
    }

    public bool RemoveData(string key)
    {
        bool exists = _cacheDb.KeyExists(key);
        if (!exists)
        {
            return _cacheDb.KeyDelete(key);
        }

        return false;
    }

    public bool SetData<T>(string key, T value, DateTimeOffset expirationTime)
    {
        var expiaryTime = expirationTime.DateTime.Subtract(DateTime.Now);
        return _cacheDb.StringSet(key, JsonSerializer.Serialize(value), expiaryTime);
    }
}