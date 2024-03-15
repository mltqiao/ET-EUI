using TMPro;
using UnityEngine;

namespace ET
{
    [ComponentOf(typeof(Unit))]
    public class HeadHudViewComponent : Entity, IAwake, IUpdate, IDestroy
    {
        public Transform HeadPointTransform = null;

        public TextMeshPro NameText = null;

        public Camera MainCamera = null;
    }
}