using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

      // when player gets a quest, it has default IsCompleted false status
namespace Engine.Models
{
    public class QuestStatus : BaseNotificationClass
    {
        private bool _isCompleted;      //Lesson 10.6 - updated code to solve bug;
        public Quest PlayerQuest { get; }   // we removed set{} as the only time this property should ever be set is within a constructor below 'QuestStatus'
        public bool IsCompleted {
            get { return _isCompleted; }
            set {
                _isCompleted = value;
                OnPropertyChanged(); // cause of a change in BaseNotificationClass (10.6 - [CallerMemberName]) we do not need anymore any properties like before
                                     // OnPropertyChanged(nameof(IsCompleted)) - it will work but now code is more clean
                                     // We can use now short version because, when going to 'OnPropertyChanged' function, it's going to look for 'CallerMemberName'
                                     // which in our case is 'IsCompleted' property and it will use it's name as a property name parameter.
            }
        }

        public QuestStatus(Quest quest) {
            PlayerQuest = quest;
            IsCompleted = false;
        }
    }
}
