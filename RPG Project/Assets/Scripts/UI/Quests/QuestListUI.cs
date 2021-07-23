using RPG.Quests;
using UnityEngine;

namespace RPG.UI.Quests
{
    public class QuestListUI : MonoBehaviour
    {
        [SerializeField] Quest[] tempQuests;
        [SerializeField] QuestItemUI questPrefab;

        private void Start()
        {            
            foreach(Transform child in transform)
            {
                Destroy(child.gameObject);
            }

            QuestList questList = GameObject.FindGameObjectWithTag("Player").GetComponent<QuestList>();
            foreach(QuestStatus status in questList.GetStatuses())
            {
                QuestItemUI questUI = Instantiate(questPrefab, transform);
                questUI.SetUp(status);
            }
        }
    }
}