using UnityEngine;
using UnityEngine.Events;

namespace RPG.Dialogue
{
    public class DialogueTrigger : MonoBehaviour
    {
        [SerializeField] string action;
        [SerializeField] UnityEvent OnTrigger;

        public void Trigger(string actionToTrigger)
        {
            if (actionToTrigger == action)
            {
                OnTrigger.Invoke();
            }
        }
    }
}