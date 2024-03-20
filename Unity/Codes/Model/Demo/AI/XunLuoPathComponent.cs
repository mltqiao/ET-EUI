using System.Collections.Generic;
using UnityEngine;

namespace ET
{
    [ComponentOf(typeof(Unit))]
    public class XunLuoPathComponent: Entity, IAwake
    {
        public List<Vector3> path = new List<Vector3>();

        public int Index;
    }
}