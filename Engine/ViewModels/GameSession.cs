using System;
using System.Linq;
using Engine.Models;
using Engine.Factories;
using System.ComponentModel;
using Engine.EventArgs;
using System.Runtime.Remoting.Messaging;

namespace Engine.ViewModels
{
    public class GameSession : BaseNotificationClass
    {
        public event EventHandler<GameMessageEventArgs> OnMessageRaised;  //This is handler that works like "In a view model when you raise this event, you should run this function from a view object."

        #region Properties
        private Location _currentLocation;
        private Monster _currentMonster;
        public World CurrentWorld { get; set; }
        public Player CurrentPlayer { get; set; }
        public Location CurrentLocation                     // When CurrentLocation changes
        {
            get { return _currentLocation; }
            set
            {
                _currentLocation = value;                         // we reset current location ('value' - explanation https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/value
                OnPropertyChanged(nameof(CurrentLocation));       // change onproperty string into name of currentlocation property      //we raise OnPropertyChange for a CurrentLocation to redraw an image and update info
                OnPropertyChanged(nameof(HasLocationToNorth));    // we raise OnPropertyChange for boolean value "HasLocationToNorth" to show or hide direction button based on that if there is a location available North of current location
                OnPropertyChanged(nameof(HasLocationToEast));
                OnPropertyChanged(nameof(HasLocationToWest));
                OnPropertyChanged(nameof(HasLocationToSouth));       // renaming string "HasLocationToSouth" to name of CurrentLocation property 'nameof(HasLocationToSouth)' to make it instantly updated everytime we update property name, 
                                                                     // otherwise property name will be updated in a project but not here as this is a string and does not really reflect the property name in a n active way!
                                                                     // 'OnPropertyChanged' is inherited from BaseNotificationClass <-- hover over it to confirm!
                CompleteQuestsAtLocation();
                GivePlayerQuestsAtLocation();                        // check if there are new quests when player moves to new location
                GetMonsterAtLocation();
            }
        }
        #endregion
        public Monster CurrentMonster {
            get { return _currentMonster; }
            set { _currentMonster = value;
                OnPropertyChanged(nameof(CurrentMonster));  // inform UI about change
                OnPropertyChanged(nameof(HasMonster));      // inform UI about change
            if (CurrentMonster != null)                     // if current monster is not equal to null
                {                                           // raise a massage with its name
                    RaiseMessage(""); // This is only to create blank line between messages!
                    RaiseMessage($"You see a {CurrentMonster.Name} here!"); 
                }
            
            }
        }

        public Weapon CurrentWeapon { get; set; }   // property of data type Weapon to know what's the current selected weapon is for a player

        // Before refactoring this bit of a code (example)
        // public bool HasLocationToNorth { 
        //     get {
        //         return CurrentWorld.LocationAt(CurrentLocation.XCoordinate, CurrentLocation.YCoordinate + 1) != null; // look in the currentworld try currentlocation at x coordinate and y coordinate +1 if it is not equal null 
        //     } 
        // }
        //
        // After refactoring this bit of a code (example)
        public bool HasLocationToNorth =>
            CurrentWorld.LocationAt(CurrentLocation.XCoordinate, CurrentLocation.YCoordinate + 1) != null;

        public bool HasLocationToEast =>            //  the value of that property is determined by checking if the HasLocationToEast property is null
            CurrentWorld.LocationAt(CurrentLocation.XCoordinate + 1, CurrentLocation.YCoordinate) != null;

        public bool HasLocationToSouth =>           // the value of that property is determined by checking if the HasLocationToSouth property is null
            CurrentWorld.LocationAt(CurrentLocation.XCoordinate, CurrentLocation.YCoordinate - 1) != null;

        public bool HasLocationToWest =>            // the value of that property is determined by checking if the HasLocationToWest property is null
            CurrentWorld.LocationAt(CurrentLocation.XCoordinate - 1, CurrentLocation.YCoordinate) != null;

        public bool HasMonster => CurrentMonster != null;   //bool property to check if there is a monster. '=>' - this is an expression body and is used instead of get like in HasLocationToXYZ , same like 'return CurrentWorld.,, calculation'
        public GameSession()    // GameSession constructor - part of a code run when object is being created.
       
        {
            CurrentPlayer = new Player
            {                                   // Instantiating Player object|| Evaluate what is on the right side of '=' and put on left side (property CurrentPlayer) // Lesson 4.5 change () to {}                  
                                                // We instanciated object that have public properties (Name Parameters Method!)
                                                // Below data are temp and for testing time only (This will display This data from This View Model via XAML Main Window
                Name = "Scott",
                CharacterClass = "Fighter",     
                HitPoints = 10,
                Gold = 1000000,
                ExperiencePoints = 0,
                Level = 1
            };

            if (!CurrentPlayer.Weapons.Any()) {                         // if current player doesn't have any weapon (Weapons list == empty) it will equip item ID 1001 = Pointy stick
                CurrentPlayer.AddItemToInventory(ItemFactory.CreateGameItem(1001));
            }




            CurrentWorld = WorldFactory.CreateWorld();  // As we use this instance class to CreateWorld only we are changing it from instance to static (Global) class = do something and give me result in and out!
                                                        // using static class to create object is called Factory Design Pattern) - To Remember: if class is static all of it's functions and private variables need to be static too!
                                                        // goes to WorldFactory class (which is static) and call CreateWorld function (static again)

            CurrentLocation = CurrentWorld.LocationAt(0,0);

            //CurrentPlayer.Inventory.Add(ItemFactory.CreateGameItem(1001));  //leaving to show up how to inject items for testing
            //CurrentPlayer.Inventory.Add(ItemFactory.CreateGameItem(1002));  //temp
            //CurrentPlayer.Inventory.Add(ItemFactory.CreateGameItem(1002));  //temp
        }

        public void MoveNorth()
        {
            if (HasLocationToNorth)
            {
                CurrentLocation = CurrentWorld.LocationAt(CurrentLocation.XCoordinate, CurrentLocation.YCoordinate + 1);
            }
        }

        public void MoveEast()
        {
            if (HasLocationToEast)
            {
                CurrentLocation = CurrentWorld.LocationAt(CurrentLocation.XCoordinate + 1, CurrentLocation.YCoordinate);
            }
        }

        public void MoveSouth()
        {
            if (HasLocationToSouth)
            {
                CurrentLocation = CurrentWorld.LocationAt(CurrentLocation.XCoordinate, CurrentLocation.YCoordinate - 1);
            }
        }
        public void MoveWest()
        {
            if (HasLocationToWest)
            {
                CurrentLocation = CurrentWorld.LocationAt(CurrentLocation.XCoordinate - 1, CurrentLocation.YCoordinate);
            }
        }

        private void CompleteQuestsAtLocation() {         
            foreach (Quest quest in CurrentLocation.QuestsAvailableHere)                                        // for each quest available in curent location...
            {
                QuestStatus questToComplete =
                    CurrentPlayer.Quests.FirstOrDefault(q => q.PlayerQuest.ID == quest.ID && !q.IsCompleted);   // Check players quests and get first one with matches ID and is not completed
                                                                                                                // If player has already completed quest, this will return Default (as no new quest for Player to complete)
                if (questToComplete != null){
                    if (CurrentPlayer.HasAllTheseItems(quest.ItemsToComplete)){
                        // Remove the quest completion items from the player's inventory
                        foreach (ItemQuantity itemQuantity in quest.ItemsToComplete){
                            for (int i = 0; i < itemQuantity.Quantity; i++){
                                CurrentPlayer.RemoveItemFromInventory(CurrentPlayer.Inventory.First(item => item.ItemTypeID == itemQuantity.ItemID));
                            }
                        }

                        RaiseMessage("");
                        RaiseMessage($"You completed the '{quest.Name}' quest");

                        // Give the player the quest rewards
                        CurrentPlayer.ExperiencePoints += quest.RewardExperiencePoints;
                        RaiseMessage($"You receive {quest.RewardExperiencePoints} experience points");

                        CurrentPlayer.Gold += quest.RewardGold;
                        RaiseMessage($"You receive {quest.RewardGold} gold");

                        foreach (ItemQuantity itemQuantity in quest.RewardItems)
                        {
                            GameItem rewardItem = ItemFactory.CreateGameItem(itemQuantity.ItemID);

                            CurrentPlayer.AddItemToInventory(rewardItem);
                            RaiseMessage($"You receive a {rewardItem.Name}");
                        }

                        // Mark the Quest as completed
                        questToComplete.IsCompleted = true;
                    }
                }
            }
        }

        private void GivePlayerQuestsAtLocation(){
            foreach (Quest quest in CurrentLocation.QuestsAvailableHere) {
                if (!CurrentPlayer.Quests.Any(q => q.PlayerQuest.ID == quest.ID)){                 
                    CurrentPlayer.Quests.Add(new QuestStatus(quest));

                    RaiseMessage("");
                    RaiseMessage($"You receive the '{quest.Name}' quest");
                    RaiseMessage(quest.Description);

                    RaiseMessage("Return with:");
                    foreach (ItemQuantity itemQuantity in quest.ItemsToComplete)
                    {
                        RaiseMessage($"   {itemQuantity.Quantity} {ItemFactory.CreateGameItem(itemQuantity.ItemID).Name}");
                    }

                    RaiseMessage("And you will receive:");
                    RaiseMessage($"   {quest.RewardExperiencePoints} experience points");
                    RaiseMessage($"   {quest.RewardGold} gold");
                    foreach (ItemQuantity itemQuantity in quest.RewardItems)
                    {
                        RaiseMessage($"   {itemQuantity.Quantity} {ItemFactory.CreateGameItem(itemQuantity.ItemID).Name}");
                    }
                }
            }
        }

        private void GetMonsterAtLocation() {
            CurrentMonster = CurrentLocation.GetMonster();
        }

        public void AttackCurrentMonster() {
            if (CurrentWeapon == null)              // Guard clause! - we will not run all stuff below if player doesn't have weapon equiped!
            {
                RaiseMessage("Mighty Hero! You must select a weapon, to attack.");
                return;
            }

            // Determine damage to monster
            int damageToMonster = RandomNumberGenerator.NumberBetween(CurrentWeapon.MinimumDamage, CurrentWeapon.MaximumDamage);

            if (damageToMonster == 0)
            {
                RaiseMessage($"You missed the {CurrentMonster.Name}.");
            }
            else {
                CurrentMonster.HitPoints -= damageToMonster;
                RaiseMessage($"You hit the {CurrentMonster.Name} for {damageToMonster} points.");
            }

            //if Monster killed, collect reward & loot
            if (CurrentMonster.HitPoints <= 0)
            {
                RaiseMessage("");
                RaiseMessage($"You defeated the {CurrentMonster.Name}!");

                CurrentPlayer.ExperiencePoints += CurrentMonster.RewardExperiencePoints;
                RaiseMessage($"You received {CurrentMonster.RewardExperiencePoints} experience points.");

                CurrentPlayer.Gold += CurrentMonster.RewardGold;
                RaiseMessage($"You receive {CurrentMonster.RewardGold} gold.");

                foreach (ItemQuantity itemQuantity in CurrentMonster.Inventory)
                {
                    GameItem item = ItemFactory.CreateGameItem(itemQuantity.ItemID);
                    CurrentPlayer.AddItemToInventory(item);
                    RaiseMessage($"You receive {itemQuantity.Quantity} {item.Name}.");
                }

                // Get another monster to fight
                GetMonsterAtLocation();
            }
            else {
                // if monster is still alive, let the monster attack
                int damageToPlayer = RandomNumberGenerator.NumberBetween(CurrentMonster.MinimumDamage, CurrentMonster.MaximumDamage);

                if (damageToPlayer == 0)
                {
                    RaiseMessage($"The {CurrentMonster.Name} attacks, but misses you.");
                }
                else {
                    CurrentPlayer.HitPoints -= damageToPlayer;
                    RaiseMessage($"The {CurrentMonster.Name} hit you for {damageToPlayer} points.");
                }

                // If player is killed, move them back to their home location.
                if (CurrentPlayer.HitPoints <= 0) {
                    RaiseMessage("");
                    RaiseMessage($"The {CurrentMonster.Name} killed you in a fight!");

                    CurrentLocation = CurrentWorld.LocationAt(0, -1); // Player's home location
                    CurrentPlayer.HitPoints = CurrentPlayer.Level * 10; // Completely heal the player with hitpoints equal to player level * 10                
                }
            }
        }

        private void RaiseMessage(string message) {
            OnMessageRaised?.Invoke(this, new GameMessageEventArgs(message)); //
        }
    }
}