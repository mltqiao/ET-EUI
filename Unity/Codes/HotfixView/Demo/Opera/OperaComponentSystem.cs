using UnityEngine;

namespace ET
{
    [ObjectSystem]
    public class OperaComponentAwakeSystem : AwakeSystem<OperaComponent>
    {
        public override void Awake(OperaComponent self)
        {
            self.mapMask = LayerMask.GetMask("Map");
        }
    }

    [ObjectSystem]
    public class OperaComponentUpdateSystem : UpdateSystem<OperaComponent>
    {
        public override void Update(OperaComponent self)
        {
            self.Update();
        }
    }
    
    [FriendClass(typeof(OperaComponent))]
    [FriendClassAttribute(typeof(ET.MoveComponent))]
    public static class OperaComponentSystem
    {
        public static void JoyMove(this OperaComponent self, Vector3 moveDir)
        {
            Unit unit = self.ZoneScene().CurrentScene().GetComponent<UnitComponent>().MyUnit;

            Vector3 unitPos = unit.Position;
            unitPos.y = 0;
            Vector3 newPos = unitPos + (moveDir * 4f);

            using (ListComponent<Vector3> list = ListComponent<Vector3>.Create())
            {
                list.Add(unit.Position);
                list.Add(newPos);
                unit.MoveToAsync(list).Coroutine();
            }

            self.frameClickMap.X = newPos.x;
            self.frameClickMap.Y = newPos.y;
            self.frameClickMap.Z = newPos.z;
            
            self.ZoneScene().ZoneScene().GetComponent<SessionComponent>().Session.Send(self.frameClickMap);
        }

        public static void Stop(this OperaComponent self)
        {
            Unit unit = self.ZoneScene().CurrentScene().GetComponent<UnitComponent>().MyUnit;
            
            unit.GetComponent<MoveComponent>().StopForce();

            Vector3 unitPos = unit.Position;

            self.frameClickMap.X = unitPos.x;
            self.frameClickMap.Y = unitPos.y;
            self.frameClickMap.Z = unitPos.z;

            self.C2MJoyStop.X = unitPos.x;
            self.C2MJoyStop.Y = unitPos.y;
            self.C2MJoyStop.Z = unitPos.z;

            self.C2MJoyStop.A = unit.Forward.x;
            self.C2MJoyStop.B = unit.Forward.y;
            self.C2MJoyStop.C = unit.Forward.z;
            
            self.ZoneScene().ZoneScene().GetComponent<SessionComponent>().Session.Send(self.C2MJoyStop);
        }
        
        public static void Update(this OperaComponent self)
        {
            if (InputHelper.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 1000, self.mapMask))
                {
                    self.ClickPoint = hit.point;
                    self.frameClickMap.X = self.ClickPoint.x;
                    self.frameClickMap.Y = self.ClickPoint.y;
                    self.frameClickMap.Z = self.ClickPoint.z;
                    self.ZoneScene().GetComponent<SessionComponent>().Session.Send(self.frameClickMap);
                }
            }

            // KeyCode.R
            if (InputHelper.GetKeyDown(114))
            {
                CodeLoader.Instance.LoadLogic();
                Game.EventSystem.Add(CodeLoader.Instance.GetHotfixTypes());
                Game.EventSystem.Load();
                Log.Debug("hot reload success!");
            }
            
            // KeyCode.T
            if (InputHelper.GetKeyDown(116))
            {
                C2M_TransferMap c2MTransferMap = new C2M_TransferMap();
                self.ZoneScene().GetComponent<SessionComponent>().Session.Call(c2MTransferMap).Coroutine();
            }
        }
    }
}