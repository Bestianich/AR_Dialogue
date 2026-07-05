using System;
using UnityEngine;

namespace AR_Dialogue.Scripts.Runtime
{
    [Serializable]
    public class MemoryData
    {
        public string Name;
        public DataType DataType;
        [SerializeReference]
        public object Value;

        public MemoryData()
        {
            Name = "new Data#1";
            DataType = DataType.INT;
            Value = 0;
        }

        public MemoryData(string name, DataType dataType, object value)
        {
            Name = name;
            DataType = dataType;
            Value = value;
        }
    }
    public enum DataType
    {
        INT,
        FLOAT,
        STRING,
        BOOLEAN,
        GAMEOBJECT,
        TEXTMESHPRO,
        SPRITE
    }
}