using System;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Quests
{
    [System.Serializable]
    public class QuestStatus
    {
        [SerializeField] Quest quest;
        [SerializeField] List<string> completedObjectives = new List<string>();

        [Serializable]
        private class QuestStatusRecord
        {
            public string questName;
            public List<string> completedObjectives;

            public QuestStatusRecord(string questName, List<string> completedObjectives)
            {
                this.questName = questName;
                this.completedObjectives = completedObjectives;
            }
        }

        public QuestStatus(Quest quest)
        {
            this.quest = quest;
        }

        public bool IsComplete()
        {
            return quest.GetObjectiveCount() == completedObjectives.Count;
        }

        public QuestStatus(object statusObject)
        {
            QuestStatusRecord state = (QuestStatusRecord)statusObject;
            this.quest = Quest.GetByName(state.questName);
            this.completedObjectives = state.completedObjectives;
        }

        public void CompleteObjective(string objective)
        {
            if (quest.HasObjective(objective))
            {
                completedObjectives.Add(objective);
            }
        }

        public Quest GetQuest()
        {
            return quest;
        }

        public int GetCompleteddObjectiveCount()
        {
            return completedObjectives.Count;
        }

        public bool IsObjectiveComplete(string objective)
        {
            return completedObjectives.Contains(objective);
        }

        public object CaptureState()
        {
            return new QuestStatusRecord(quest.name, completedObjectives);
        }        
    }
}