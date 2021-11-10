using System;
using System.Linq;
using Engine.EventArgs;
using Engine.Factories;
using Engine.Models;


namespace Engine.ViewModels
{
    public class GameSession : BaseNotificationClass
    {
        public event EventHandler<GameMessageEventArgs> OnMessageRaised;  //This is handler that works like "In a view model when you raise this event, you should run this function from a view object."

        #region Properties

        private Player _currentPlayer;
        private Location _currentLocation;
        private Monster _currentMonster;
        private Trader _currentTrader;
        public World CurrentWorld { get;}
        public Player CurrentPlayer{
            get { return _currentPlayer; }
            set{
                if (_currentPlayer != null) {
                    _currentPlayer.OnActionPerformed -= OnCurrentPlayerPerformedAction;
                    _currentPlayer.OnLeveledUp -= OnCurrentPlayerLeveledUp;
                    _currentPlayer.OnKilled -= OnCurrentPlayerKilled;   // if there is current player object we want to unsubscribe to an Event handler
                }

                _currentPlayer = value;

                if (_currentPlayer != null) { // to ensure that new current player is not null (possible to pass null within value)
                    _currentPlayer.OnActionPerformed -= OnCurrentPlayerPerformedAction;
                    _currentPlayer.OnLeveledUp += OnCurrentPlayerLeveledUp;
                    _currentPlayer.OnKilled += OnCurrentPlayerKilled;   // subscribe to it and run OnCurrentPlayerKilled Event handler
                }
            }
        }

        public Location CurrentLocation                     // When CurrentLocation changes
        {
            get { return _currentLocation; }
            set
            {
                _currentLocation = value;                         // we reset current location ('value' - explanation https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/value
                OnPropertyChanged();                              // change onproperty string into name of currentlocation property      //we raise OnPropertyChange for a CurrentLocation to redraw an image and update info
                OnPropertyChanged(nameof(HasLocationToNorth));    // we raise OnPropertyChange for boolean value "HasLocationToNorth" to show or hide direction button based on that if there is a location available North of current location
                OnPropertyChanged(nameof(HasLocationToEast));
                OnPropertyChanged(nameof(HasLocationToWest));
                OnPropertyChanged(nameof(HasLocationToSouth));       // renaming string "HasLocationToSouth" to name of CurrentLocation property 'nameof(HasLocationToSouth)' to make it instantly updated everytime we update property name, 
                                                                     // otherwise property name will be updated in a project but not here as this is a string and does not really reflect the property name in a n active way!
                                                                     // 'OnPropertyChanged' is inherited from BaseNotificationClass <-- hover over it to confirm!

                CompleteQuestsAtLocation();                           // 
                GivePlayerQuestsAtLocation();                        // check if there are new quests when player moves to new location
                GetMonsterAtLocation();

                CurrentTrader = CurrentLocation.TraderHere;         // CurrentTrader will be set when the player moves to a new location
            }
        }
        #endregion
        public Monster CurrentMonster
        {

            get { return _currentMonster; }
            set{

                if (_currentMonster != null){

                    _currentMonster.OnKilled -= OnCurrentMonsterKilled;
                }

                _currentMonster = value;

                if (_currentMonster != null){

                    _currentMonster.OnKilled += OnCurrentMonsterKilled;

                    RaiseMessage("");
                    RaiseMessage($"You see a {CurrentMonster.Name} here!");
                }

                OnPropertyChanged();                        // here we do not need to pass nameof(CurrentMonster) as due to changes in Lesson 10.6 it will know from what property it is invoked
                OnPropertyChanged(nameof(HasMonster));      // here we need to pass parameter as this is different parameter than property we are invoking from

            }
        }

        public Trader CurrentTrader{

            get { return _currentTrader; }


            set{
                _currentTrader = value;

                OnPropertyChanged();                        // inform UI about change | here we do not need to pass nameof(CurrentTrader) as due to changes in Lesson 10.6 it will know from what property it is invoked
                OnPropertyChanged(nameof(HasTrader));       // inform UI about change | here we need to pass parameter as this is different parameter than property we are invoking from
            }
        }

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

        public bool HasTrader => CurrentTrader != null;     // to use for the UI to decide whether or not to display the “Trade” button – based on whether or not there is a CurrentTrader
        public GameSession()    // GameSession constructor - part of a code run when object is being created.
        {
            CurrentPlayer = new Player("Scott", "Fighter", 0, 10, 10, 1000000);

            if (!CurrentPlayer.Weapons.Any())
            {                         // if current player doesn't have any weapon (Weapons list == empty) it will equip item ID 1001 = Pointy stick
                CurrentPlayer.AddItemToInventory(ItemFactory.CreateGameItem(1001));
            }

            CurrentWorld = WorldFactory.CreateWorld();  // As we use this instance class to CreateWorld only we are changing it from instance to static (Global) class = do something and give me result in and out!
                                                        // using static class to create object is called Factory Design Pattern) - To Remember: if class is static all of it's functions and private variables need to be static too!
                                                        // goes to WorldFactory class (which is static) and call CreateWorld function (static again)

            CurrentLocation = CurrentWorld.LocationAt(0, 0);

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
        private void CompleteQuestsAtLocation()
        {
            foreach (Quest quest in CurrentLocation.QuestsAvailableHere)                                // for each quest available in curent location...
            {
                QuestStatus questToComplete =
                    CurrentPlayer.Quests.FirstOrDefault(q => q.PlayerQuest.ID == quest.ID && !q.IsCompleted);   // Check players quests and get first one with matches ID and is not completed
                                                                                                                // If player has already completed quest, this will return Default (as no new quest for Player to complete)

                if (questToComplete != null)
                {
                    if (CurrentPlayer.HasAllTheseItems(quest.ItemsToComplete))
                    {
                        // Remove the quest completion items from the player's inventory
                        foreach (ItemQuantity itemQuantity in quest.ItemsToComplete)
                        {
                            for (int i = 0; i < itemQuantity.Quantity; i++)
                            {
                                CurrentPlayer.RemoveItemFromInventory(CurrentPlayer.Inventory.First(item => item.ItemTypeID == itemQuantity.ItemID));
                            }
                        }

                        RaiseMessage("");
                        RaiseMessage($"You completed the '{quest.Name}' quest");

                        //Give the player the quest rewards
                        RaiseMessage($"You receive {quest.RewardExperiencePoints} experience points");
                        CurrentPlayer.AddExperience(quest.RewardExperiencePoints);

                        RaiseMessage($"You receive {quest.RewardGold} gold");
                        CurrentPlayer.ReceiveGold(quest.RewardGold);

                        foreach (ItemQuantity itemQuantity in quest.RewardItems)
                        {
                            GameItem rewardItem = ItemFactory.CreateGameItem(itemQuantity.ItemID);

                            RaiseMessage($"You receive a {rewardItem.Name}");
                            CurrentPlayer.AddItemToInventory(rewardItem);
                        }

                        //Marking Quest as completed
                        questToComplete.IsCompleted = true;
                    }
                }
            }
        }

        private void GivePlayerQuestsAtLocation()
        {

            foreach (Quest quest in CurrentLocation.QuestsAvailableHere)
            {

                if (!CurrentPlayer.Quests.Any(q => q.PlayerQuest.ID == quest.ID))
                {

                    CurrentPlayer.Quests.Add(new QuestStatus(quest));

                    RaiseMessage("");
                    RaiseMessage($"You receive the '{quest.Name}' quest");
                    RaiseMessage(quest.Description);

                    RaiseMessage("Return with:");
                    foreach (ItemQuantity itemQuantity in quest.ItemsToComplete)
                    {

                        RaiseMessage($"    {itemQuantity.Quantity} {ItemFactory.CreateGameItem(itemQuantity.ItemID).Name}");
                    }

                    RaiseMessage("And you will receive:");
                    RaiseMessage($"    {quest.RewardExperiencePoints} experience points");
                    RaiseMessage($"    {quest.RewardGold} gold");
                    foreach (ItemQuantity itemQuantity in quest.RewardItems)
                    {

                        RaiseMessage($"    {itemQuantity.Quantity} {ItemFactory.CreateGameItem(itemQuantity.ItemID).Name}");
                    }
                }
            }
        }

        private void GetMonsterAtLocation()
        {

            CurrentMonster = CurrentLocation.GetMonster();
        }
        public void AttackCurrentMonster(){
            if (CurrentPlayer.CurrentWeapon == null) {              // Guard clause! - we will not run all stuff below if player doesn't have weapon equiped!

                RaiseMessage("Mighty Hero! You must select a weapon, to attack.");
                return;
            }

            CurrentPlayer.UseCurrentWeaponOn(CurrentMonster);

            // If monster is killed, collect rewards and loot												 
            if (CurrentMonster.IsDead) {
                GetMonsterAtLocation();
            }

            else{
                // Let the monster attack
                int damageToPlayer = RandomNumberGenerator.NumberBetween(CurrentMonster.MinimumDamage, CurrentMonster.MaximumDamage);

                if (damageToPlayer == 0){
                    RaiseMessage($"The {CurrentMonster.Name} attacks, but misses you.");
                }
                else{
                    RaiseMessage($"The {CurrentMonster.Name} hit you for {damageToPlayer} points.");
                    CurrentPlayer.TakeDamage(damageToPlayer);
                }
            }
        }
        private void OnCurrentPlayerPerformedAction(object sender, string result) {
            RaiseMessage(result);
        }

        private void OnCurrentPlayerKilled(object sender, System.EventArgs eventArgs){
            RaiseMessage("");
            RaiseMessage("You have been killed.");

            CurrentLocation = CurrentWorld.LocationAt(0, -1);
            CurrentPlayer.CompletelyHeal();
        }
        private void OnCurrentMonsterKilled(object sender, System.EventArgs eventArgs){
            RaiseMessage("");
            RaiseMessage($"You defeated the {CurrentMonster.Name}!");

            RaiseMessage($"You receive {CurrentMonster.RewardExperiencePoints} experience points.");
            CurrentPlayer.AddExperience(CurrentMonster.RewardExperiencePoints);

            RaiseMessage($"You receive {CurrentMonster.Gold} gold.");
            CurrentPlayer.ReceiveGold(CurrentMonster.Gold);

            foreach (GameItem gameItem in CurrentMonster.Inventory){
                RaiseMessage($"You receive one {gameItem.Name}.");
                CurrentPlayer.AddItemToInventory(gameItem);
            }
        }

        private void OnCurrentPlayerLeveledUp(object sender, System.EventArgs eventArgs){
            RaiseMessage($"You are now level {CurrentPlayer.Level}!");
        }

        private void RaiseMessage(string message){
            OnMessageRaised?.Invoke(this, new GameMessageEventArgs(message));
        }
    }
}