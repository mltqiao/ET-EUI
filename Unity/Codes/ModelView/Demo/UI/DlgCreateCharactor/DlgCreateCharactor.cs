namespace ET
{
	 [ComponentOf(typeof(UIBaseWindow))]
	public  class DlgCreateCharactor :Entity,IAwake,IUILogic
	{

		public DlgCreateCharactorViewComponent View { get => this.Parent.GetComponent<DlgCreateCharactorViewComponent>();} 

		 

	}
}
