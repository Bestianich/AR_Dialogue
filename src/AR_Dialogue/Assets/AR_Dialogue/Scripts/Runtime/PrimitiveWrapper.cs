using System;
using UnityEngine;

namespace AR_Dialogue.Scripts.Runtime
{
    [Serializable]
    public class PrimitiveWrapper
    {
        [SerializeReference]
        public object Target;

        public PrimitiveWrapper()
        {
            Target = null;
        }
        
        public PrimitiveWrapper(object target)
        {
            Target = target;
        }
    }
}