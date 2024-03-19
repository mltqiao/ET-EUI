﻿using System;

using UnityEngine;

namespace ET
{
	[ComponentOf(typeof(Scene))]
	public class OperaComponent: Entity, IAwake, IUpdate
    {
        public Vector3 ClickPoint;

	    public int mapMask;

	    public readonly C2M_PathfindingResult frameClickMap = new C2M_PathfindingResult();

	    public readonly C2M_JoyStop C2MJoyStop = new C2M_JoyStop();

	    public GameObject MoveClickEffect = null;
    }
}
