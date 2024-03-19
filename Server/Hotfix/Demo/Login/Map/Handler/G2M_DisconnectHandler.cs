using System;

namespace ET
{
    public class G2M_DisconnectHandler : AMActorLocationRpcHandler<Unit, G2M_Disconnect, M2G_Disconnect>
    {
        protected override async ETTask Run(Unit unit, G2M_Disconnect request, M2G_Disconnect response, Action reply)
        {
            await unit.OfflineInMap();

            reply();
            
            await ETTask.CompletedTask;
        }
    }
}