using System.Collections.Generic;
using UnityEngine;


    [CreateAssetMenu(fileName = "new DialogueGraph", menuName = "AR_Dialogue/DialogueGraph", order = 0)]
    public class DialogueGraph : ScriptableObject
    {
        public List<ANode> Nodes = new List<ANode>();
    }