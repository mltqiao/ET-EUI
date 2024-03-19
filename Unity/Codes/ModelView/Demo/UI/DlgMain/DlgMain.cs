using UnityEngine;

namespace ET
{
	 [ComponentOf(typeof(UIBaseWindow))]
	public  class DlgMain :Entity,IAwake,IUILogic
	{

		public DlgMainViewComponent View { get => this.Parent.GetComponent<DlgMainViewComponent>();}

		public float raidus = 10f;

		public Vector2 originPos = Vector2.zero;

		public Vector2 moveDir = Vector2.zero;

		public Vector2 lastMoveDir = Vector2.zero;

		public float coolTime = 0f;

		public bool isUpdate = false;

		public long joyMoveTimerId = 0;

	}
}
