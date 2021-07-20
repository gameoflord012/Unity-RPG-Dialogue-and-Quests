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
                DialogueNode rootNode = new DialogueNode();
                rootNode.uniqueID = Guid.NewGuid().ToString();
                nodes.Add(rootNode);
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

        public void CreateNode(DialogueNode parent)
        {
            DialogueNode child = new DialogueNode();
            child.uniqueID = Guid.NewGuid().ToString();
            parent.children.Add(child.uniqueID);
            nodes.Add(child);
            OnValidate();
        }

        public void DeleteNode(DialogueNode node)
        {
            nodes.Remove(node);
            nodeLookup.Remove(node.uniqueID);

            foreach(DialogueNode parent in GetAllNodes())
            {
                parent.children.Remove(node.uniqueID);
            }

            OnValidate();
        }
    }
}