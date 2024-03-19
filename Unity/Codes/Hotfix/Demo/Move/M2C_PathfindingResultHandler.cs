using UnityEngine;

namespace ET
{
    [MessageHandler]
    [FriendClassAttribute(typeof(ET.MoveComponent))]
    public class M2C_PathfindingResultHandler : AMHandler<M2C_PathfindingResult>
    {
        protected override void Run(Session session, M2C_PathfindingResult message)
        {
            Unit unit = session.DomainScene().CurrentScene().GetComponent<UnitComponent>().Get(message.Id);

            if (unit == null)
            {
                return;
            }

            float speed = unit.GetComponent<NumericComponent>().GetAsFloat(NumericType.Speed);

            //非当前玩家控制的游戏角色 直接进行移动
            if (!unit.IsMyUnit())
            {
                unit.GetComponent<MoveComponent>().StopForce();
                using (ListComponent<Vector3> list = ListComponent<Vector3>.Create())
                {
                    for (int i = 0; i < message.Xs.Count; ++i)
                    {
                        Vector3 pos = new Vector3(message.Xs[i], message.Ys[i] + 0.3f, message.Zs[i]);
                        list.Add(pos);
                        // Game.EventSystem.Publish(new SpawnPathPoint() { ZoneScene = session.ZoneScene(), Pos = pos });
                    }

                    unit.GetComponent<MoveComponent>().MoveToAsync(list, speed).Coroutine();
                }
                return;
            }

            // 获取当前玩家控制的游戏角色再本地客户端的移动时间
            long moveTime = TimeHelper.ServerNow() - unit.GetComponent<MoveComponent>().BeginTime;
            long needTime = 0; // 每一段路径点之间移动所需的时间
            long totalTime = 0; // 总共移动所需的时间
            int N = 0; // 移动的路径点索引
            Vector3 prePos = new Vector3(message.Xs[0], message.Ys[0] + 0.3f, message.Zs[0]);
            for (int i = 1; i < message.Xs.Count; i++)
            {
                Vector3 nextPos = new Vector3(message.Xs[i], message.Ys[i] + 0.3f, message.Zs[i]);

                Vector3 v = nextPos - prePos;
                float distance = v.magnitude;
                prePos = nextPos;
                needTime = (long)(distance / speed * 1000);

                totalTime += needTime;
                N++;
                if (totalTime >= moveTime)
                {
                    N++;
                    break;
                }
            }
            
            // 如果本地客户端引动时间大于路径移动时间，则不进行同步移动
            if (totalTime <= moveTime)
            {
                return;
            }

            using (ListComponent<Vector3> list = ListComponent<Vector3>.Create())
            {
                list.Add(unit.Position);

                for (int i = N; i < message.Xs.Count; i++)
                {
                    list.Add(new Vector3(message.Xs[i], message.Ys[i] + 0.3f, message.Zs[i]));
                }

                if (list.Count <= 1)
                {
                    list.Clear();
                    return;
                }
                
                unit.GetComponent<MoveComponent>().MoveToAsync(list, speed).Coroutine();
            }
        }
    }
}
