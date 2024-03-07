
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[ObjectSystem]
	public class DlgCreateCharactorViewComponentAwakeSystem : AwakeSystem<DlgCreateCharactorViewComponent> 
	{
		public override void Awake(DlgCreateCharactorViewComponent self)
		{
			self.uiTransform = self.GetParent<UIBaseWindow>().uiTransform;
		}
	}


	[ObjectSystem]
	public class DlgCreateCharactorViewComponentDestroySystem : DestroySystem<DlgCreateCharactorViewComponent> 
	{
		public override void Destroy(DlgCreateCharactorViewComponent self)
		{
			self.DestroyWidget();
		}
	}
}
