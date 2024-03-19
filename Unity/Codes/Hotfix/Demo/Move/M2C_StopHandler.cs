using UnityEngine;

namespace ET
{
	[MessageHandler]
	public class M2C_StopHandler : AMHandler<M2C_Stop>
	{
		protected override void Run(Session session, M2C_Stop message)
		{
			Unit unit = session.DomainScene().CurrentScene().GetComponent<UnitComponent>().Get(message.Id);
			if (unit == null)
			{
				return;
			}

			if (unit.IsMyUnit() && message.Error == ErrorCode.ERR_Success)
			{
				unit.GetComponent<ObjectWait>()?.Notify(new WaitType.Wait_UnitStop() { Error = 0 });
				return;
			}

			Vector3 pos = new Vector3(message.X, message.Y + 0.3f, message.Z);
			Quaternion rotation = new Quaternion(message.A, message.B, message.C, message.W);
			
			MoveToAndForward(unit, pos, rotation).Coroutine();
		}

		public async ETTask MoveToAndForward(Unit unit, Vector3 pos, Quaternion rotation)
		{
			using (ListComponent<Vector3> list = ListComponent<Vector3>.Create())
			{
				list.Add(unit.Position);
				list.Add(pos);
				float speed = unit.GetComponent<NumericComponent>().GetAsFloat(NumericType.Speed);
				bool ret = await unit.GetComponent<MoveComponent>().MoveToAsync(list, speed);

				if (ret)
				{
					unit.Position = pos;
					unit.Rotation = rotation;
				}
				unit.GetComponent<ObjectWait>()?.Notify(new WaitType.Wait_UnitStop() { Error = 0 });
			}
		}
	}
}
