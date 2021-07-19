using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Dialogue
{
    [CreateAssetMenu(fileName = "New Dialogue", menuName = "Dialogue", order = 0)]
    public class Dialogue : ScriptableObject
    {
        [SerializeField]
        List<DialogueNode> nodes = new List<DialogueNode>();

        Dictionary<string, DialogueNode> nodeLookup = new Dictionary<string, DialogueNode>();

#if UNITY_EDITOR
        private void Awake()
        {
            Debug.Log("Awake from " + name);
            if(nodes.Count == 0)
            {
                nodes.Add(new DialogueNode());
            }
        }
#endif

        private void OnValidate()
        {
            RebuildNodeLookup();
        }

        private void RebuildNodeLookup()
        {
            nodeLookup.Clear();
            foreach (DialogueNode node in GetAllNodes())
            {
                nodeLookup.Add(node.uniqueID, node);
            }
        }

        public IEnumerable<DialogueNode> GetAllNodes()        
        {
            return nodes;
        }

        public DialogueNode GetRootNode()
        {
            return nodes[0];
        }

        public IEnumerable<DialogueNode> GetAllChildren(DialogueNode parentNode)
        {
            foreach(string childName in parentNode.children)
            {
                if(nodeLookup.ContainsKey(childName))
                {
                    yield return nodeLookup[childName];
                }
            }
        }
    }
}