using System.Collections.Generic;

namespace ET
{
    public class QueueInfo: Entity, IAwake, IDestroy
    {
        public long UnitId;
        
        public long GateActorId;
        
        public string Account;
        
        public int Index;
        
        // vip等级、ip、权限等
    }

    // 掉线保护信息
    public struct ProtectInfo
    {
        public long UnitId;

        public long Time;
    }
    
    [ChildType(typeof(QueueInfo))]
    [ComponentOf(typeof(Scene))]
    public class QueueMgrComponent : Entity, IAwake, IDestroy
    {
        // 允许在线的玩家
        public HashSet<long> Online = new HashSet<long>();
        
        // 排队队列
        public HashLinkedList<long, QueueInfo> Queue = new HashLinkedList<long, QueueInfo>();
        
        // 掉线保护的玩家
        public HashLinkedList<long, ProtectInfo> Protects = new HashLinkedList<long, ProtectInfo>();

        public long Timer_Tick;

        public long Timer_ClearProtect;

        public long Timer_Update;
    }
}