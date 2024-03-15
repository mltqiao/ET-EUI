using UnityEngine;

namespace ET
{
    [FriendClass(typeof(GlobalComponent))]
    public class AfterUnitCreate_CreateUnitView: AEvent<EventType.AfterUnitCreate>
    {
        protected override void Run(EventType.AfterUnitCreate args)
        {
            // Unit View层
            // 这里可以改成异步加载，demo就不搞了
            GameObject bundleGameObject = (GameObject)ResourcesComponent.Instance.GetAsset("Unit.unity3d", "Unit");
            GameObject prefab = bundleGameObject.Get<GameObject>("Skeleton");

            GameObject unitGameObject = GameObject.Instantiate(bundleGameObject, GlobalComponent.Instance.Unit, true);
	        
            GameObject go = UnityEngine.Object.Instantiate(prefab, unitGameObject.transform, true);
            
            unitGameObject.transform.position = args.Unit.Position;
            args.Unit.AddComponent<GameObjectComponent>().GameObject = unitGameObject;
            args.Unit.AddComponent<AnimatorComponent>();
            args.Unit.AddComponent<HeadHudViewComponent>();
        }
    }
}