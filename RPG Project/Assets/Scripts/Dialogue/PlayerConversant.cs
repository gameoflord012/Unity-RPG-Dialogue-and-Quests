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

        [SerializeField] string playerConersantName = "Mamamia";

        AIConversant currentAIConversant = null;

        Dialogue currentDialogue = null;

        DialogueNode currentNode = null;
        bool isChoosing = false;

        public bool IsActive()
        {
            return currentDialogue != null;
        }

        public void Quit()
        {
            TriggerExitAction();
            currentDialogue = null;
            currentAIConversant = null;
            currentNode = null;
            isChoosing = false;
            OnConversationUpdated?.Invoke();
        }

        public void StartDialogue(AIConversant newConversant, Dialogue newDialogue)
        {
            currentDialogue = newDialogue;
            currentAIConversant = newConversant;
            currentNode = currentDialogue.GetRootNode();
            TriggerEnterAction();
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

        public string GetCurrentConversantName()
        {
            if(isChoosing)
            {                
                return playerConersantName;
            }
            else
            {
                return currentAIConversant.GetConversantName();
            }
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
            TriggerEnterAction();
            Next();
        }

        public void Next()
        {
            int numPlayerResponses = currentDialogue.GetPlayerChildren(currentNode).Count();
            if(numPlayerResponses > 0)
            {
                isChoosing = true;
                TriggerExitAction();
                OnConversationUpdated?.Invoke();
                return;
            }

            DialogueNode[] children = currentDialogue.GetAIChildren(currentNode).ToArray();

            TriggerExitAction();
            currentNode = children[Random.Range(0, children.Length)];
            TriggerEnterAction();

            isChoosing = false;
            OnConversationUpdated?.Invoke();
        }

        public bool HasNext()
        {
            return currentDialogue.GetAllChildren(currentNode).Count() > 0;
        }

        public void TriggerEnterAction()
        {
            if (currentNode != null && currentNode.GetOnEnterAction() != null)
            {
                TriggerAction(currentNode.GetOnEnterAction());
            }
        }
        
        public void TriggerExitAction()
        {
            if (currentNode != null && currentNode.GetOnExitAction() != null)
            {
                TriggerAction(currentNode.GetOnExitAction());
            }
        }

        private void TriggerAction(string action)
        {
            if (action == "") return;

            foreach (DialogueTrigger trigger in currentAIConversant.GetComponents<DialogueTrigger>())
            {
                trigger.Trigger(action);
            }
        }
    }
}