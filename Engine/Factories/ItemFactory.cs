using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine.Models;

namespace Engine.Factories
{
    public static class ItemFactory                             // it's static as we will not need to instanciated it but just use functions from it 
    {                                                           // static class never have constructor as it is never create an instance (not being constructed)
        private static readonly List<GameItem> _standardGameItems = new List<GameItem>();   // readonly - can be set only in this line or inside a constructor

        static ItemFactory()
        {
            _standardGameItems.Add(new Weapon(1001, "Pointy Stick", 1, 1, 2));
            _standardGameItems.Add(new Weapon(1002, "Rusty Sword", 5, 1, 3));
            _standardGameItems.Add(new GameItem(9001, "Snake fang", 1));        // worth 1 gold piece
            _standardGameItems.Add(new GameItem(9002, "Snakeskin", 2));         // worth 2 gold piece
            _standardGameItems.Add(new GameItem(9003, "Rat tail", 1));          // worth 1 gold piece  
            _standardGameItems.Add(new GameItem(9004, "Rat fur", 2));           // worth 2 gold piece
            _standardGameItems.Add(new GameItem(9005, "Spider fang", 1));       // worth 1 gold piece
            _standardGameItems.Add(new GameItem(9006, "Spider silk", 2));       // worth 2 gold piece
        }

        public static GameItem CreateGameItem(int itemTypeID)
        {
            GameItem standardItem = _standardGameItems.FirstOrDefault(item => item.ItemTypeID == itemTypeID);       // this will find a first item on '_standardGameItems' list, 
                                                                                                                    // that has a 'ItemTypeID' property value equal to the one we passed to a function
                                                                                                                    // if match not found it will use default item (here null)
                                                                                                                    // conclusion: Our 'standardItem' will have matching item from our list or null
                                                                                                                    // on a list variable it uses linq to find 1 item that has properites
            if (standardItem != null)           // if we did find an item
            {
                if (standardItem is Weapon)     // checking if found item is a Weapon
                {
                    return (standardItem as Weapon).Clone();    // if standardItem is Weapon, cast it as a Weapon object (before it was  as GameItem object)
                                                                // this .Clone() is a function from Weapon class, before it was taken from GameItem class.
                }

                return standardItem.Clone();
            }
            return null;    //if standardItem not found on a list, return null;
        }
    }
}
