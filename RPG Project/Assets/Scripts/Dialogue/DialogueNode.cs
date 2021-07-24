using RPG.Core;
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace RPG.Dialogue
{
    [System.Serializable]
    public class DialogueNode : ScriptableObject
    {
        [SerializeField] bool isPlayerSpeaking = false;
        [SerializeField] string text;
        [SerializeField] List<string> children = new List<string>();
        [SerializeField] Rect rect = new Rect(100, 100, 200, 100);

        [SerializeField] string OnEnterAction;
        [SerializeField] string OnExitAction;
        [SerializeField]
        Condition condition;

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

        public bool CheckCondition(IEnumerable<IPredicateEvaluator> evaluators)
        {
            return condition.Check(evaluators);
        }

        public bool ContainsChild(string childID)
        {
            return children.Contains(childID);
        }

        public bool IsPlayerSpeaking()
        {
            return isPlayerSpeaking;
        }

        public string GetOnEnterAction()
        {
            return OnEnterAction;
        }

        public string GetOnExitAction()
        {
            return OnExitAction;
        }

#if UNITY_EDITOR
        public void SetIsPlayerSpeaking(bool value)
        {
            Undo.RecordObject(this, "Change Dialogue Speaker");
            isPlayerSpeaking = value;
            EditorUtility.SetDirty(this);
        }

        public void RemoveChild(string childID)
        {
            Undo.RecordObject(this, "Remove child");
            children.Remove(childID);
            EditorUtility.SetDirty(this);
        }

        public void AddChild(string childID)
        {
            Undo.RecordObject(this, "Add new child");
            children.Add(childID);
            EditorUtility.SetDirty(this);
        }

        public void SetPosition(Vector2 position)
        {
            Undo.RecordObject(this, "Move Dialogue Node");
            rect.position = position;
            EditorUtility.SetDirty(this);
        }

        public void SetText(string newText)
        {
            if (newText != text)
            {
                Undo.RecordObject(this, "Update Dialogue Text");
                text = newText;
                EditorUtility.SetDirty(this);
            }
        }
#endif
    }
}