using RPG.Control;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Dialogue
{
    public class AIConversant : MonoBehaviour, IRaycastable
    {
        [SerializeField] Dialogue dialogue;
        [SerializeField] string conversantName;

        public CursorType GetCursorType()
        {
            return CursorType.Dialogue;
        }

        public string GetConversantName()
        {
            return conversantName;
        }

        public bool HandleRaycast(PlayerController callingController)
        {
            if(dialogue == null)
            {
                return false;
            }

            if (Input.GetMouseButtonDown(0))
            {
                callingController.GetComponent<PlayerConversant>().StartDialogue(this, dialogue);
            }
            return true;
        }
    }
}