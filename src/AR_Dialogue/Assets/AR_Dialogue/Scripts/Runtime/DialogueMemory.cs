using System;
using System.Collections.Generic;
using UnityEngine;

namespace AR_Dialogue.Scripts.Runtime
{
    
    [Serializable]
    public class DialogueMemory
    {
        public List<MemoryData> MemoryDatas = new List<MemoryData>();
        public object Get(string name)
        {
            foreach (var data in MemoryDatas)
            {
                if(data.Name == name) return data.Value;
            }
            Debug.LogError("Dialogue Memory not found: " + name);
            return null;
        }

        public void Set(string name, object value)
        {
            foreach (var data in MemoryDatas)
            {
                if (data.Name == name)
                {
                    data.Value = value;
                    return;
                }
            }
            Debug.LogWarning("Creating new MemoryData");
            MemoryDatas.Add(new MemoryData { Name = name, Value = value });
        }

        public bool Has(string name)
        {
            foreach (var data in MemoryDatas)
            {
                if(data.Name == name) return true;
            }
            return false;
        }
    }


    [Serializable]
    public class MemoryData
    {
        public string Name;
        public object Value;
    }
}