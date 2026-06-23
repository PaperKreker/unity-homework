using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Modules.AI
{
    [Serializable, InlineProperty]
    public sealed class Vector3BlackboardInstaller : IBlackboardInstaller
    {
        [SerializeField]
        [BlackboardValueKey(typeof(Vector3))]
        private string key;

        [SerializeField]
        private Vector3 value;

        public void Install(Blackboard blackboard)
        {
            blackboard.AddPrimitiveValue(key, this.value);
        }
    }
}