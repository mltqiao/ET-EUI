namespace ET
{
	 [ComponentOf(typeof(UIBaseWindow))]
	public  class DlgSceneLoading :Entity,IAwake,IUILogic
	{

		public DlgSceneLoadingViewComponent View { get => this.Parent.GetComponent<DlgSceneLoadingViewComponent>();}

		public long TimerId;

	}
}
