
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[ObjectSystem]
	public class Scroll_Item_charactorDestroySystem : DestroySystem<Scroll_Item_charactor> 
	{
		public override void Destroy( Scroll_Item_charactor self )
		{
			self.DestroyWidget();
		}
	}
}
