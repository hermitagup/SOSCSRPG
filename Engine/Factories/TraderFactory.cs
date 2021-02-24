using System;
using System.Collections.Generic;
using System.Linq;
using Engine.Models;

namespace Engine.Factories
{
    public static class TraderFactory {
        private static readonly List<Trader> _traders = new List<Trader>(); //list of traders

        static TraderFactory() {
            Trader susan = new Trader("Susan"); //initialize trader, initialize inventory
            susan.AddItemToInventory(ItemFactory.CreateGameItem(1001));

            Trader farmerTed = new Trader("Farmer Ted");
            farmerTed.AddItemToInventory(ItemFactory.CreateGameItem(1001));

            Trader peteTheHerbalist = new Trader("Pete the Herbalist");
            peteTheHerbalist.AddItemToInventory(ItemFactory.CreateGameItem(1001));

            AddTraderToList(susan); //add trader to the list
            AddTraderToList(farmerTed);
            AddTraderToList(peteTheHerbalist);
        }

        public static Trader GetTraderByName(string name) { //static function to look in trader list by name and check if it matches (first one) and return that
            return _traders.FirstOrDefault(t => t.Name == name);
        }

        private static void AddTraderToList(Trader trader) { //check if trader list has a trader name (each trader has an unique name)
            if (_traders.Any(t => t.Name == trader.Name)) {
                throw new ArgumentException($"There is already a trader named '{trader.Name}'"); //if trader is in a list, there will be an exception
            }

            _traders.Add(trader); //if there's no trader in the list, it will be added 
        }
    }
}