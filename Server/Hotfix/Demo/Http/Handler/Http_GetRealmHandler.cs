using System.Net;

namespace ET
{
    [HttpHandler(SceneType.RealmInfo, "/get_realm")]
    public class Http_GetRealmHandler : IHttpHandler
    {
        public async ETTask Handle(Entity domain, HttpListenerContext context)
        {
            Http_GetRealmResponse httpGetRealmResponse = new Http_GetRealmResponse();

            foreach (StartSceneConfig startSceneConfig in StartSceneConfigCategory.Instance.Realms)
            {
                httpGetRealmResponse.Realms.Add(startSceneConfig.InnerIPOutPort.ToString());
            }
            HttpHelper.Response(context, httpGetRealmResponse);
            await ETTask.CompletedTask;
        }
    }
}