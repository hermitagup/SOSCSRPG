using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Models
{
    public class Quest
    {
        public int ID { get;}
        public string Name { get;}        // string for a Quest name
        public string Description { get;} // string for a Quest Description
        public List<ItemQuantity> ItemsToComplete { get;} // list of item quantity objects, quest items required to complete the Quest
        public int RewardExperiencePoints { get;}     // integer for a experience points reward
        public int RewardGold { get;}                 // integer for a gold reward
        public List<ItemQuantity> RewardItems { get;} // list of objects for items reward

        public Quest(int id, string name, string description, List<ItemQuantity> itemsToComplete,       // Constructor where we pass all the parameters to set proper values
                        int rewardExperiencePoints, int rewardGold, List<ItemQuantity> rewardItems){     
            ID = id;
            Name = name;
            Description = description;
            ItemsToComplete = itemsToComplete;
            RewardExperiencePoints = rewardExperiencePoints;
            RewardGold = rewardGold;
            RewardItems = rewardItems;
        }
    }
}
