using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

namespace RPG.Dialogue
{    
    public class PlayerConversant : MonoBehaviour
    {
        public event System.Action OnConversationUpdated;

        [SerializeField] Dialogue testDialogue;
        Dialogue currentDialogue;

        DialogueNode currentNode = null;
        bool isChoosing = false;

        public bool IsActive()
        {
            return currentDialogue != null;
        }

        IEnumerator Start()
        {
            yield return new WaitForSeconds(2f);
            StartDialogue(currentDialogue);
        }

        public void StartDialogue(Dialogue newDialogue)
        {
            currentDialogue = testDialogue;
            currentNode = currentDialogue.GetRootNode();
            OnConversationUpdated?.Invoke();
        }

        public string GetText()
        {
            if(currentNode == null)
            {
                return "NULL";
            }

            return currentNode.GetText();
        }

        public bool IsChoosing()
        {
            return isChoosing;
        }

        public IEnumerable<DialogueNode> GetChoiceNode()
        {
           foreach(DialogueNode child in currentDialogue.GetPlayerChildren(currentNode))
            {
                yield return child;
            }
        }

        public void SelectChoice(DialogueNode chosenNode)
        {
            currentNode = chosenNode;
        }

        public void Next()
        {
            int numPlayerResponses = currentDialogue.GetPlayerChildren(currentNode).Count();
            if(numPlayerResponses > 0)
            {
                isChoosing = true;
                OnConversationUpdated?.Invoke();
                return;
            }

            DialogueNode[] children = currentDialogue.GetAIChildren(currentNode).ToArray();
            currentNode = children[Random.Range(0, children.Length)];
            isChoosing = false;
            OnConversationUpdated?.Invoke();
        }

        public bool HasNext()
        {
            return currentDialogue.GetAllChildren(currentNode).Count() > 0;
        }
    }
}