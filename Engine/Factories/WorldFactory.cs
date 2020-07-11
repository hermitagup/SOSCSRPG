using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine.Models;

namespace Engine.Factories
{
    internal static class WorldFactory // only in Engine Project (internal is from default, but it is good to distinct the access type)
    {
        internal static World CreateWorld() //internal function only in Engine Project
        {
            World newWorld = new World(); //instantiating the World object under name 'newWorld'

            newWorld.AddLocation(-2, -1, "Farmer's Field",
                "There are rows of corn growing here, with giant rats hiding between them.",
                "/Engine;component/Images/Locations/FarmFields.png");

            newWorld.LocationAt(-2, -1).AddMonster(2, 100); // 2- monster ID, 100- chances of encountering

            newWorld.AddLocation(-1, -1, "Farmer's House",
                "This is the house of your neighbor, Farmer Ted.",
                "/Engine;component/Images/Locations/Farmhouse.png");

            newWorld.AddLocation(0, -1, "Home", 
                "This is your home",
                "/Engine;component/Images/Locations/Home.png");

            newWorld.AddLocation(-1, 0, "Trading Shop",
                "The shop of Susan, the trader.",
                "/Engine;component/Images/Locations/Trader.png");

            newWorld.AddLocation(0, 0, "Town square",
                "You see a fountain here.",
                "/Engine;component/Images/Locations/TownSquare.png");

            newWorld.AddLocation(1, 0, "Town Gate",
                "There is a gate here, protecting the town from giant spiders.",
                "/Engine;component/Images/Locations/TownGate.png");

            newWorld.AddLocation(2, 0, "Spider Forest",
                "The trees in this forest are covered with spider webs.",
                "/Engine;component/Images/Locations/SpiderForest.png");

            newWorld.LocationAt(2, 0).AddMonster(3, 100);

            newWorld.AddLocation(0, 1, "Herbalist's hut",
                "You see a small hut, with plants drying from the roof.",
                "/Engine;component/Images/Locations/HerbalistsHut.png");

            newWorld.LocationAt(0, 1).QuestsAvailableHere.Add(QuestFactory.GetQuestByID(1)); //(0) This takes from World, that takes From Location and gives ass property 'QuestAvailableHere'
                                                                                             //Line (1) & (2) doing same as line (0) 
                                                                                             //(1) Location hh = newWorld.LocationAt(0, 1);
                                                                                             //(2) hh.QuestsAvailableHere.Add(QuestFactory.GetQuestByID(1));

            newWorld.AddLocation(0, 2, "Herbalist's garden",
                "There are many plants here, with snakes hiding behind them.",
                "/Engine;component/Images/Locations/HerbalistsGarden.png");

            newWorld.LocationAt(0, 2).AddMonster(1, 100);

            return newWorld; //return newWorld instance with filled data
        }
    }
}
