namespace ET
{
    public static class ConstValue
    {
        public const int Login_GateUserDisconnectTime = 20 * 1000; //顶号对内保留时间

        public const int Login_SessionDisconnectTime = 60 * 1000; //断线保留时间

        public const int Queue_MaxOnline = 3000; // 最大在线人数

        public const int Queue_TickTime = 1 * 1000; // 放人进排队间隔时间

        public const int Queue_TickCount = 1; // 每次放几个人

        public const int Queue_TickUpdate = 10 * 1000; // 排队排名更新间隔

        public const int Queue_ClearProtect = 10 * 1000; // 掉线保护检测间隔

        public const int Queue_ProtectTime = 5 * 60 * 1000; // 掉线保护时长 5min
    }
}