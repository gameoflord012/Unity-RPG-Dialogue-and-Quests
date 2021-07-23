using RPG.Quests;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestTooltipUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI title;
    [SerializeField] Transform objectiveContainer;
    [SerializeField] GameObject objectivePrefab;

    public void Setup(QuestStatus status)
    {
        foreach(Transform objective in objectiveContainer)
        {
            Destroy(objective.gameObject);
        }

        Quest quest = status.GetQuest();
        title.text = quest.GetTitle();
        foreach(string objectiveText in quest.GetObjectiveTexts())
        {
            TextMeshProUGUI textComp = Instantiate(objectivePrefab, objectiveContainer).
                GetComponentInChildren<TextMeshProUGUI>();

            textComp.text = objectiveText;
        }
    }
}
