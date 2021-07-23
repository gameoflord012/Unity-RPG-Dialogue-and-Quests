using RPG.Quests;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestItemUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI title;
    [SerializeField] TextMeshProUGUI progress;

    QuestStatus status;

    public void SetUp(QuestStatus status)
    {
        this.status = status;
        title.text = status.GetQuest().GetTitle();
        progress.text = $"{this.status.GetCompleteddObjectiveCount()}/{this.status.GetQuest().GetObjectiveCount()}";
    }

    public QuestStatus GetQuestStatus()
    {
        return status;
    }
}
