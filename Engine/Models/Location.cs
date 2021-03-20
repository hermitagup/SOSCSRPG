using System;
using System.Collections.Generic;
using System.Linq;
using Engine.Factories;

namespace Engine.Models
{
    public class Location
    {
        // removed set{} in Lesson 10.6 to prevent any changes to them , where only place it should be set is below constructor (newly created)
        public int XCoordinate { get;}
        public int YCoordinate { get;}
        public string Name { get;}
        public string Description { get;}
        public string ImageName { get;}
        public List<Quest> QuestsAvailableHere { get; set; } = 
            new List<Quest>();

        public List<MonsterEncounter> MonstersHere { get;} = 
            new List<MonsterEncounter>();

        public Trader TraderHere { get; set; } //trader live here; if location has trader it will be populated 

        public Location(int xCoordinate, int yCoordinate, string name, string description, string imageName) {
            XCoordinate = xCoordinate;
            YCoordinate = yCoordinate;
            Name = name;
            Description = description;
            ImageName = imageName;
        }

        public void AddMonster(int monsterID, int chanceOfEncountering)
        {
            if(MonstersHere.Exists(m => m.MonsterID == monsterID))
            {
                // This monster has already been added to this location.
                // So, overwrite the ChanceOfEncountering with the new number.
                MonstersHere.First(m => m.MonsterID == monsterID)
                            .ChanceOfEncountering = chanceOfEncountering;
            }
            else
            {
                // This monster is not already at this location, so add it.
                MonstersHere.Add(new MonsterEncounter(monsterID, chanceOfEncountering));
            }
        }

        public Monster GetMonster()
        {
            if(!MonstersHere.Any())
            {
                return null;
            }

            // Total the percentages of all monsters at this location.
            int totalChances = MonstersHere.Sum(m => m.ChanceOfEncountering);

            // Select a random number between 1 and the total (in case the total chances is not 100).
            int randomNumber = RandomNumberGenerator.NumberBetween(1, totalChances);

            // Loop through the monster list, 
            // adding the monster's percentage chance of appearing to the runningTotal variable.
            // When the random number is lower than the runningTotal,
            // that is the monster to return.
            int runningTotal = 0;

            foreach(MonsterEncounter monsterEncounter in MonstersHere)
            {
                runningTotal += monsterEncounter.ChanceOfEncountering;

                if(randomNumber <= runningTotal)
                {
                    return MonsterFactory.GetMonster(monsterEncounter.MonsterID);
                }
            }

            // If there was a problem, return the last monster in the list.
            return MonsterFactory.GetMonster(MonstersHere.Last().MonsterID);
        }
    }
}