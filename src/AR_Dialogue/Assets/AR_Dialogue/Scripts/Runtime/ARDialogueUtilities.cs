
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public static class ARDialogueUtilities
{
       public static IOrderedEnumerable<Type> GetAllTypeOfANodes()
       {
              // var nodes = new List<ANode>();
              // string[] guids = AssetDatabase.FindAssets("t:ScriptableObject");
              // foreach (var guid in guids)
              // {
              //        string path = AssetDatabase.GUIDToAssetPath(guid);
              //        UnityEngine.Object obj = AssetDatabase.LoadAssetAtPath(path, typeof(UnityEngine.Object));
              //        if (obj is ANode node)
              //        {
              //               nodes.Add(node);
              //        }
              // }
              
              var assemblies = AppDomain.CurrentDomain.GetAssemblies().Where(asm => !asm.IsDynamic && !string.IsNullOrEmpty(asm.Location));
              
              var nodeTypes = assemblies.SelectMany(assembly => assembly.GetTypes())
                     .Where(type => type.IsSubclassOf(typeof(ANode)) && !type.IsAbstract).OrderBy(t => t.Name);
              return nodeTypes;
       }
}
