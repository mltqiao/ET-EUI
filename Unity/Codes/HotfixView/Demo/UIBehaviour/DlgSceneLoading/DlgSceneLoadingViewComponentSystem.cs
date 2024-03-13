
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[ObjectSystem]
	public class DlgSceneLoadingViewComponentAwakeSystem : AwakeSystem<DlgSceneLoadingViewComponent> 
	{
		public override void Awake(DlgSceneLoadingViewComponent self)
		{
			self.uiTransform = self.GetParent<UIBaseWindow>().uiTransform;
		}
	}


	[ObjectSystem]
	public class DlgSceneLoadingViewComponentDestroySystem : DestroySystem<DlgSceneLoadingViewComponent> 
	{
		public override void Destroy(DlgSceneLoadingViewComponent self)
		{
			self.DestroyWidget();
		}
	}
}
