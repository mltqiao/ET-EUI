using System.Collections.Generic;

namespace ET
{
	 [ComponentOf(typeof(UIBaseWindow))]
	public  class DlgCharactorSelect :Entity,IAwake,IUILogic
	{

		public DlgCharactorSelectViewComponent View { get => this.Parent.GetComponent<DlgCharactorSelectViewComponent>();}

		public Dictionary<int, Scroll_Item_charactor> ScrollItemCharactors;

	}
}
