using Engine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Factories
{
    internal static class QuestFactory  // internal cause we will use it only inside Engine project and static cause we do not need to create an instance of this object
    {
        private static readonly List<Quest> _quest = new List<Quest>(); // place where we will store quest objects

        static QuestFactory() {     // Kinda constructor as static functions doesn't have a constructor but it will be run with first time!
                                    // Declare the items need to complete the quest, and its reward items
            List<ItemQuantity> itemsToComplete = new List<ItemQuantity>();
            List<ItemQuantity> rewardItems = new List<ItemQuantity>();

            itemsToComplete.Add(new ItemQuantity(9001, 5));
            rewardItems.Add(new ItemQuantity(1002, 1));

            // Create the Quest
            _quest.Add(new Quest(1,
                                "Clear the herb garden",
                                "Defeat the snakes in the Herbalist's garden",
                                itemsToComplete,
                                25, 10,
                                rewardItems));
        }

        internal static Quest GetQuestByID(int id) {
            return _quest.FirstOrDefault(quest => quest.ID == id);      //gets first match item or default if match not found - here default is nothing (but can be set if necessary)
        }

    }
}
