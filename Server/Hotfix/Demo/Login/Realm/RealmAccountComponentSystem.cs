namespace ET
{
    public class RealmAccountComponentDestorySystem : DestroySystem<RealmAccountComponent>
    {
        public override void Destroy(RealmAccountComponent self)
        {
            self.Info = null;
        }
    }
}