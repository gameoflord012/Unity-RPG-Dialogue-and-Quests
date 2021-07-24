using GameDevTV.Inventories;
using GameDevTV.Saving;
using System;
using System.Collections.Generic;
using UnityEngine;
using RPG.Core;

namespace RPG.Quests
{
    public class QuestList : MonoBehaviour, ISaveable, IPredicateEvaluator
    {
        public event Action OnQuestListUpdated;

        List<QuestStatus> statuses = new List<QuestStatus>();

        public void AddQuest(Quest quest)
        {
            if (HasQuest(quest))
            {
                return;
            }

            QuestStatus newStatus = new QuestStatus(quest);
            statuses.Add(newStatus);
            OnQuestListUpdated?.Invoke();
        }

        public void ComleteObjective(Quest quest, string objective)
        {
            QuestStatus status = GetQuestStatus(quest);

            if(status.IsComplete())
            {
                GiveReward(quest);
            }

            if (status != null)
            {
                status.CompleteObjective(objective);
                OnQuestListUpdated?.Invoke();
            }
        }

        private void GiveReward(Quest quest)
        {
            foreach(var reward in quest.GetRewards())
            {
                bool success = GetComponent<Inventory>().AddToFirstEmptySlot(reward.item, reward.number);
                if(!success)
                {
                    GetComponent<ItemDropper>().DropItem(reward.item, reward.number);
                }
            }
        }

        private bool HasQuest(Quest quest)
        {
            return GetQuestStatus(quest) != null;
        }

        public QuestStatus GetQuestStatus(Quest quest)
        {
            foreach (QuestStatus status in statuses)
            {
                if (status.GetQuest() == quest)
                {
                    return status;
                }
            }
            return null;
        }

        public IEnumerable<QuestStatus> GetStatuses()
        {
            return statuses;
        }

        public object CaptureState()
        {
            List<object> state = new List<object>();
            foreach (QuestStatus status in statuses)
            {
                state.Add(status.CaptureState());
            }
            return state;
        }

        public void RestoreState(object restoreState)
        {
            statuses.Clear();
            List<object> state = (List<object>)restoreState;

            foreach (object objectStatus in state)
            {
                statuses.Add(new QuestStatus(objectStatus));
            }
        }

        public bool? Evaluate(string predicate, string[] parameters)
        {
            if (predicate != "HasQuest") return null;
            return HasQuest(Quest.GetByName(parameters[0]));
        }
    }
}