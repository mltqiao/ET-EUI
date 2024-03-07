
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[ObjectSystem]
	public class DlgCharactorSelectViewComponentAwakeSystem : AwakeSystem<DlgCharactorSelectViewComponent> 
	{
		public override void Awake(DlgCharactorSelectViewComponent self)
		{
			self.uiTransform = self.GetParent<UIBaseWindow>().uiTransform;
		}
	}


	[ObjectSystem]
	public class DlgCharactorSelectViewComponentDestroySystem : DestroySystem<DlgCharactorSelectViewComponent> 
	{
		public override void Destroy(DlgCharactorSelectViewComponent self)
		{
			self.DestroyWidget();
		}
	}
}
