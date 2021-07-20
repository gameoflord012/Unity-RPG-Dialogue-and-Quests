using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace RPG.Dialogue
{
    [System.Serializable]
    public class DialogueNode : ScriptableObject
    {
        [SerializeField] string text;
        [SerializeField] List<string> children = new List<string>();
        [SerializeField] Rect rect = new Rect(100, 100, 200, 100);

        public string GetText()
        {
            return text;
        }

        public Rect GetRect()
        {
            return rect;
        }

        public IEnumerable<string> GetChildrenIENumberable()
        {
            return children;
        }

        public bool ContainsChild(string childID)
        {
            return children.Contains(childID);
        }

#if UNITY_EDITOR
        public void RemoveChild(string childID)
        {
            Undo.RecordObject(this, "Remove child");
            children.Remove(childID);
        }

        public void AddChild(string childID)
        {
            Undo.RecordObject(this, "Add new child");
            children.Add(childID);
        }

        public void SetPosition(Vector2 position)
        {
            Undo.RecordObject(this, "Move Dialogue Node");
            rect.position = position;
        }

        public void SetText(string newText)
        {
            if (newText != text)
            {
                Undo.RecordObject(this, "Update Dialogue Text");
                text = newText;
            }
        }
#endif
    }
}