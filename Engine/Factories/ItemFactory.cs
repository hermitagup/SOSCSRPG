using Engine.Models;
using System.Collections.Generic;
using System.Linq;
using Engine.Actions;

namespace Engine.Factories
{
    public static class ItemFactory                             // it's static as we will not need to instanciated it but just use functions from it 
    {                                                           // static class never have constructor as it is never create an instance (not being constructed)
        private static readonly List<GameItem> _standardGameItems = new List<GameItem>();   // readonly - can be set only in this line or inside a constructor

        static ItemFactory(){
            BuildWeapon(1001, "Pointy Stick", 1, 1, 2);
            BuildWeapon(1002, "Rusty Sword", 5, 1, 3);

            BuildMiscellaneousItem(9001, "Snake fang", 1);        // worth 1 gold piece
            BuildMiscellaneousItem(9002, "Snakeskin", 2);         // worth 2 gold piece
            BuildMiscellaneousItem(9003, "Rat tail", 1);          // worth 1 gold piece  
            BuildMiscellaneousItem(9004, "Rat fur", 2);           // worth 2 gold piece
            BuildMiscellaneousItem(9005, "Spider fang", 1);       // worth 1 gold piece
            BuildMiscellaneousItem(9006, "Spider silk", 2);       // worth 2 gold piece
        }

        public static GameItem CreateGameItem(int itemTypeID) {
            return _standardGameItems.FirstOrDefault(item => item.ItemTypeID == itemTypeID)?.Clone();
        }

        private static void BuildMiscellaneousItem(int id, string name, int price) {
            _standardGameItems.Add(new GameItem(GameItem.ItemCategory.Miscellaneous, id, name, price));
        }

        private static void BuildWeapon(int id, string name, int price,
                                        int minimumDamage, int maximumDamage) {
            GameItem weapon = new GameItem(GameItem.ItemCategory.Weapon, id, name, price, true);
            weapon.Action = new AttackWithWeapon(weapon, minimumDamage, maximumDamage);
            _standardGameItems.Add(weapon);
        }
    }
}