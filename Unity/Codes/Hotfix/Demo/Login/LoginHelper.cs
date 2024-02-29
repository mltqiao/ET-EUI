using System;

namespace ET
{
    public static class LoginHelper
    {
        public static async ETTask Login(Scene zoneScene, string address, string account, string password)
        {
            string url = $"http://{address}/get_realm?v={RandomHelper.RandUInt32()}";
            string result = await HttpClientHelper.Request(url);
            Http_GetRealmResponse httpGetRealmResponse = JsonHelper.FromJson<Http_GetRealmResponse>(result);
            Log.Debug($"登陆测试 HTTP_GetRealmResponse {JsonHelper.ToJson(httpGetRealmResponse)}");
        }
    }
}