using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Engine.Models
{
    // abstract class means you can never instantiate this class (you can instantiate a child that is using this class f.ex: player, monster, trader class are going to do 
    // one of the principles of Object Oriented Programming is encapsulation - mean one object can't modify directly properties of another object, instead first object has to call a function on a second object to mofidy property
    // (instead of Object 1 - I am changing your properties Object 2, Object 1 is asking Object 2 to change property - plus we can add more logic to those changes like overheal or set value outside of scope)
    public abstract class LivingEntity : BaseNotificationClass
    {
        #region Properties

        private string _name;
        private int _currentHitPoints;
        private int _maximumHitPoints;
        private int _gold;

        public string Name {
            get { return _name; }
            private set {                           // private set means that it can be only changed inside this class
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public int CurrentHitPoints {
            get { return _currentHitPoints; }
            private set {
                _currentHitPoints = value;
                OnPropertyChanged(nameof(CurrentHitPoints));
            }
        }

        // this will prevent to get more health than max
        public int MaximumHitPoints
        {
            get { return _maximumHitPoints; }
            private set
            {
                _maximumHitPoints = value;
                OnPropertyChanged(nameof(MaximumHitPoints));
            }
        }

        public int Gold
        {
            get { return _gold; }
            private set
            {
                _gold = value;
                OnPropertyChanged(nameof(Gold));
            }
        }

        public ObservableCollection<GameItem> Inventory { get; set; }

        public ObservableCollection<GroupedInventoryItem> GroupedInventory { get; set; }

        public List<GameItem> Weapons =>
                Inventory.Where(i => i is Weapon).ToList();

        public bool IsDead => CurrentHitPoints <= 0;

        #endregion

        public event EventHandler OnKilled;

        protected LivingEntity(string name, int maximumHitPoints, int currentHitPoints, int gold)   // added as void - no return type required ?
        {
            Name = name;
            MaximumHitPoints = maximumHitPoints;
            CurrentHitPoints = currentHitPoints;
            Gold = gold;

            Inventory = new ObservableCollection<GameItem>();
            GroupedInventory = new ObservableCollection<GroupedInventoryItem>();
        }

        public void TakeDamage(int hitPointsOfDamage) {
            CurrentHitPoints -= hitPointsOfDamage;
            if (IsDead)
            {
                CurrentHitPoints = 0;   // this will ensure that we are not going to negative value of hp (is it mandatory? if(IsDead) whatever HP value is , we are in and do RaiseOnKilledEvent()
                RaiseOnKilledEvent();
            }
        }

        public void Heal(int hitPointsToHeal) {
            CurrentHitPoints += hitPointsToHeal;
            if (CurrentHitPoints >MaximumHitPoints)
            {
                CurrentHitPoints = MaximumHitPoints;
            }
        }

        public void CompletelyHeal() {
            CurrentHitPoints = MaximumHitPoints;
        }

        public void ReceiveGold(int amountOfGold) {
            Gold += amountOfGold;
        }

        public void SpendGold(int amountOfGold) {
            if (amountOfGold > Gold)
            {
                throw new ArgumentOutOfRangeException($"{Name} only has {Gold} gold, and cannot spend {amountOfGold} gold");
            }

            Gold -= amountOfGold;
        } 

        public void AddItemToInventory(GameItem item) {
            Inventory.Add(item);

            if (item.IsUnique) // If item is unique
            {
                GroupedInventory.Add(new GroupedInventoryItem(item, 1)); // add new item with quantity =1
            }
            else //if item is not unique
            {
                if (!GroupedInventory.Any(gi => gi.Item.ItemTypeID == item.ItemTypeID)) // if there is not at least 1 item of a given ID
                {
                    GroupedInventory.Add(new GroupedInventoryItem(item, 0)); // create a new GroupedInventory item with quantity == 0, we are adding 0 as we always running next line where we increment item quantity by 1
                }
                GroupedInventory.First(gi => gi.Item.ItemTypeID == item.ItemTypeID).Quantity++; // here incerement item quantity
            }

            OnPropertyChanged(nameof(Weapons));
        }
        public void RemoveItemFromInventory(GameItem item)
        {
            Inventory.Remove(item);

            GroupedInventoryItem groupedInventoryItemToRemove =
                GroupedInventory.FirstOrDefault(gi => gi.Item == item); // getting 1st item from GroupedInventory property where the item matches item we want to remove

            if (groupedInventoryItemToRemove != null)   // check if we found any matching object in inventory
            {
                if (groupedInventoryItemToRemove.Quantity ==1) // if item quantity is eq 1 , remove whole entry
                {
                    GroupedInventory.Remove(groupedInventoryItemToRemove);
                }
                else // if item quantity is more than 1
                {
                    groupedInventoryItemToRemove.Quantity--; // decrement by 1
                }
            }

            OnPropertyChanged(nameof(Weapons)); // this functions will raise PropertyChange event for Weapons
        }

        #region Private functions

        private void RaiseOnKilledEvent() {
            OnKilled?.Invoke(this, new System.EventArgs()); // What does it mean ? "?." | this will look into OnKilled event and see if any subscribers are into it and if yes rais an event 
        }                                                   // (subscriber here is GameSession obj and will know if the Player is killed or a Monster)

        #endregion
    }
}
