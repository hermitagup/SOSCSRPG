using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Models
{
    public class Quest
    {
        public int ID { get; set; }
        public string Name { get; set; }        // string for a Quest name
        public string Description { get; set; } // string for a Quest Description

        public List<ItemQuantity> ItemsToComplete { get; set; } // list of item quantity objects, quest items required to complete the Quest
        public int RewardExperiencePoints { get; set; }     // integer for a experience points reward
        public int RewardGold { get; set; }                 // integer for a gold reward
        public List<ItemQuantity> RewardItems { get; set; } // list of objects for items reward

        public Quest(int id, string name, string description, List<ItemQuantity> itemsToComplete,       // Constructor where we pass all the parameters to set proper values
                        int rewardExperiencePoints, int rewardGold, List<ItemQuantity> rewardItems){     
            ID = id;
            Name = name;
            Description = description;
            ItemsToComplete = itemsToComplete;
            RewardExperiencePoints = rewardExperiencePoints;
            RewardGold = RewardGold;
            RewardItems = RewardItems;
        }
    }
}
