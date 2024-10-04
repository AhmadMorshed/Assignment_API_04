namespace Store.Services.Services.CacheService
{
    public interface ICacheService
    {
        Task SetCacheResponseAsync(string Key, object response, TimeSpan timeToLive);
        Task<string> GetCacheResponseAsync(string Key);

    }
}
