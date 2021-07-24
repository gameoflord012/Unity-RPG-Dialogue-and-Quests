using RPG.Quests;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestTooltipUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI title;
    [SerializeField] Transform objectiveContainer;
    [SerializeField] GameObject objectivePrefab;
    [SerializeField] TextMeshProUGUI rewardText;

    public void Setup(QuestStatus status)
    {
        foreach (Transform objective in objectiveContainer)
        {
            Destroy(objective.gameObject);
        }

        Quest quest = status.GetQuest();
        title.text = quest.GetTitle();
        foreach (var objectiveText in quest.GetObjectiveTexts())
        {
            GameObject objectiveInstance = Instantiate(objectivePrefab, objectiveContainer);
            TextMeshProUGUI textComp = objectiveInstance.GetComponentInChildren<TextMeshProUGUI>();
            textComp.text = objectiveText.description;

            Toggle objectiveToggle = objectiveInstance.GetComponentInChildren<Toggle>();
            objectiveToggle.isOn = status.IsObjectiveComplete(objectiveText.reference);
        }

        rewardText.text = GetRewardText(quest);
    }

    private string GetRewardText(Quest quest)
    {
        string result = "";
        foreach(var reward in quest.GetRewards())
        {
            if (result != "")
            {
                result += ", ";
            }

            if(reward.number > 1)
            {
                result += reward.number.ToString() + " ";
            }
            result += reward.item.GetDisplayName();
        }

        if(result == "")
        {
            result = "No reward";
        }

        result += ".";
        return result;
    }
}
