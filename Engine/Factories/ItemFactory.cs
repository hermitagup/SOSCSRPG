using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine.Models;

namespace Engine.Factories
{
    public static class itemFactory                             // it's static as we will not need to instanciated it but just use functions from it 
    {                                                           // static class never have constructor as it is never create an instance (not being constructed)
        private static List<GameItem> _standardGameItems;
        static itemFactory() {                                   // this function is being used with first run (something like constructor but for static functions.
            _standardGameItems = new List<GameItem>();           // this will create an empty list as we can add items only to empty list , can't add to null

            _standardGameItems.Add(new Weapon(1001, "Pointy Stick", 1, 1, 2));
            _standardGameItems.Add(new Weapon(1002, "Rusty Sword", 5, 1, 3));
        }
        public static GameItem CreateGameItem(int itemTypeID)
        {
            GameItem standardItem = _standardGameItems.FirstOrDefault(item => item.ItemTypeID == itemTypeID);       // this will find a first item on '_standardGameItems' list, 
                                                                                                                    // that has a 'ItemTypeID' property value equal to the one we passed to a function
                                                                                                                    // if match not found it will use default item (here null)
                                                                                                                    // conclusion: Our 'standardItem' will have matching item from our list or null
            if (standardItem != null)
            {
                return standardItem.Clone();
            }
            return null;    //if standardItem not found on a list, return null;
        }
    }
}