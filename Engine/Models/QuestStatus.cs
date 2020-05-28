// class to track completion of a quest
namespace Engine.Models
{
    // when player gets a quest, it has default IsCompleted false status
    public class QuestStatus
    {
        public Quest PlayerQuest { get; set; }
        public bool IsCompleted { get; set; }

        public QuestStatus(Quest quest)
        {
            PlayerQuest = quest;
            IsCompleted = false;
        }
    }
}