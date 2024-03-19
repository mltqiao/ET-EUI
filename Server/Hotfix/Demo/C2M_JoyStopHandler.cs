using System.Collections.Generic;
using UnityEngine;

namespace ET
{
    [ActorMessageHandler]
    public class C2M_JoyStopHandler: AMActorLocationHandler<Unit, C2M_JoyStop>
    {
        protected override async ETTask Run(Unit unit, C2M_JoyStop message)
        {
            using (var list = ListComponent<Vector3>.Create())
            {
                Vector3 target = new Vector3(message.X, message.Y, message.Z);
                unit.GetComponent<PathfindingComponent>().Find(unit.Position, target, list);

                List<Vector3> path = list;
                if (path.Count < 2)
                {
                    unit.Stop(-1);
                    return;
                }
                
                // 广播寻路路径
                M2C_PathfindingResult m2CPathfindingResult = new M2C_PathfindingResult();
                m2CPathfindingResult.Id = unit.Id;
                for (int i = 0; i < list.Count; i++)
                {
                    Vector3 vector3 = list[i];
                    m2CPathfindingResult.Xs.Add(vector3.x);
                    m2CPathfindingResult.Ys.Add(vector3.y);
                    m2CPathfindingResult.Zs.Add(vector3.z);
                }
                
                MessageHelper.Broadcast(unit, m2CPathfindingResult);
                float speed = unit.GetComponent<NumericComponent>().GetAsFloat(NumericType.Speed);
                bool ret = await unit.GetComponent<MoveComponent>().MoveToAsync(path, speed);
                unit.Forward = new Vector3(message.A, message.B, message.C);
                unit.Stop(0);
            }

            await ETTask.CompletedTask;
        }
    }
}