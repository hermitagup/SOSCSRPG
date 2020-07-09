using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

      // when player gets a quest, it has default IsCompleted false status
namespace Engine.Models
{
    public class QuestStatus
    {
        public Quest PlayerQuest { get; set; }
        public bool IsCompleted { get; set; }

        public QuestStatus(Quest quest) {
            PlayerQuest = quest;
            IsCompleted = false;
        }
    }
}
