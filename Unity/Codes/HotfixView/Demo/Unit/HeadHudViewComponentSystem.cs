using TMPro;
using UnityEngine;

namespace ET
{
    public class HeadHudViewComponentAwakeSystem : AwakeSystem<HeadHudViewComponent>
    {
        public override void Awake(HeadHudViewComponent self)
        {
            self.MainCamera = Camera.main;
            self.HeadPointTransform = self.GetParent<Unit>().GetComponent<GameObjectComponent>().GameObject.Get<GameObject>("HeadPoint").transform;
            self.NameText = self.GetParent<Unit>().GetComponent<GameObjectComponent>().GameObject.GetComponentFormRC<TextMeshPro>("NameText");
            self.SetName(self.GetParent<Unit>().GetComponent<UnitInfoComponent>().Name);
        }
    }

    public class HeadHudViewComponentUpdateSystem : UpdateSystem<HeadHudViewComponent>
    {
        public override void Update(HeadHudViewComponent self)
        {
            if (self.MainCamera == null)
            {
                return;
            }

            if (self.HeadPointTransform == null)
            {
                return;
            }

            self.HeadPointTransform.forward = self.MainCamera.transform.forward;
        }
    }
    
    public class HeadHudViewComponentDestroySystem : DestroySystem<HeadHudViewComponent>
    {
        public override void Destroy(HeadHudViewComponent self)
        {
            self.MainCamera = null;
            self.NameText = null;
            self.HeadPointTransform = null;
        }
    }

    [FriendClassAttribute(typeof(ET.HeadHudViewComponent))]
    public static class HeadHudViewComponentSystem
    {
        public static void SetName(this HeadHudViewComponent self, string name)
        {
            if (self.NameText == null)
            {
                return;
            }

            self.NameText.text = name;
        }
    }
}