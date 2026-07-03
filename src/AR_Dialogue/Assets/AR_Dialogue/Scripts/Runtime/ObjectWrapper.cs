using UnityEngine;

namespace AR_Dialogue.Scripts.Runtime
{
    [System.Serializable]
    public class ObjectWrapper
    {
        public Object Target;

        public ObjectWrapper()
        {
            Target = null;
        }

        public ObjectWrapper(Object target)
        {
            Target = target;
        }
    }
}