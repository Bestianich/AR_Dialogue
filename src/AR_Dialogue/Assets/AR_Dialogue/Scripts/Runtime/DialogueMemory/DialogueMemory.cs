using System;
using System.Collections.Generic;
using UnityEngine;


    
    [Serializable]
    public class DialogueMemory
    {
        public List<MemoryData> MemoryDatas = new List<MemoryData> { new MemoryData() };
        public object Get(string name)
        {
            foreach (var data in MemoryDatas)
            {
                if (data.Name == name)
                {
                    if (data.Value is ObjectWrapper) return ConvertObjectWrapper(data.Value as ObjectWrapper);
                    return data.Value;
                }
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

        public UnityEngine.Object ConvertObjectWrapper(ObjectWrapper wrapper)
        {
            return wrapper.Target;
        }
    }


    