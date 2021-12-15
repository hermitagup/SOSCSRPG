using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Engine.Models
{
    public abstract class LivingEntity : BaseNotificationClass{
        #region Properties

        private string _name;
        private int _currentHitPoints;
        private int _maximumHitPoints;
        private int _gold;
        private int _level;
        private GameItem _currentWeapon;
        private GameItem _currentConsumable;

        public string Name{
            get { return _name; }
            private set{
                _name = value;
                OnPropertyChanged();
            }
        }

        public int CurrentHitPoints{
            get { return _currentHitPoints; }
            private set{
                _currentHitPoints = value;
                OnPropertyChanged();
            }
        }

        public int MaximumHitPoints{
            get { return _maximumHitPoints; }
            protected set{
                _maximumHitPoints = value;
                OnPropertyChanged();
            }
        }

        public int Gold{
            get { return _gold; }
            private set{
                _gold = value;
                OnPropertyChanged();
            }
        }

        public int Level{
            get { return _level; }
            protected set{
                _level = value;
                OnPropertyChanged();
            }
        }

        public GameItem CurrentWeapon { //This is where the player (or monster/trader) watches for events raised by their weapon’s action
            get { return _currentWeapon; }
            set {
                if (_currentWeapon != null) {
                    _currentWeapon.Action.OnActionPerformed -= RaiseActionPerformedEvent;
                }

                _currentWeapon = value;

                OnPropertyChanged();
            }
        }
        public GameItem CurrentConsumable { // the setter and getter for the CurrentConsumable, it subscribes to and unsubscribes from, the item's action's OnActionPerformed
            get => _currentConsumable;
            set {
                if (_currentConsumable != null) {
                    _currentConsumable.Action.OnActionPerformed -= RaiseActionPerformedEvent;
                }

                _currentConsumable = value;

                if (_currentConsumable != null) {
                    _currentConsumable.Action.OnActionPerformed += RaiseActionPerformedEvent;
                }

                OnPropertyChanged();
            }
        }

        public ObservableCollection<GameItem> Inventory { get; }

        public ObservableCollection<GroupedInventoryItem> GroupedInventory { get; }

        public List<GameItem> Weapons =>
            Inventory.Where(i => i.Category == GameItem.ItemCategory.Weapon).ToList();
        public List<GameItem> Consumables =>        //property to bind the UI's combobox
            Inventory.Where(i => i.Category == GameItem.ItemCategory.Consumable).ToList();
        public bool HasConsumable => Consumables.Any(); //property to hide or show the combobox

        public bool IsDead => CurrentHitPoints <= 0;

        #endregion

        public event EventHandler<string> OnActionPerformed; // UI will watch this event for any messages that are raised when the LivingEntity performs an action
        public event EventHandler OnKilled;

        protected LivingEntity(string name, int maximumHitPoints, int currentHitPoints,
                               int gold, int level = 1){
            Name = name;
            MaximumHitPoints = maximumHitPoints;
            CurrentHitPoints = currentHitPoints;
            Gold = gold;
            Level = level;

            Inventory = new ObservableCollection<GameItem>();
            GroupedInventory = new ObservableCollection<GroupedInventoryItem>();
        }
        public void UseCurrentWeaponOn(LivingEntity target) { //wrapper function that the ViewModel will use to initiate an attack
            CurrentWeapon.PerformAction(this, target);
        }
        public void UseCurrentConsumable() {     // helper function, when the player uses their currently-selected consumable
            CurrentConsumable.PerformAction(this, this);
            RemoveItemFromInventory(CurrentConsumable);
        }

        public void TakeDamage(int hitPointsOfDamage){
            CurrentHitPoints -= hitPointsOfDamage;

            if (IsDead){
                CurrentHitPoints = 0;
                RaiseOnKilledEvent();
            }
        }

        public void Heal(int hitPointsToHeal){
            CurrentHitPoints += hitPointsToHeal;

            if (CurrentHitPoints > MaximumHitPoints){
                CurrentHitPoints = MaximumHitPoints;
            }
        }

        public void CompletelyHeal(){
            CurrentHitPoints = MaximumHitPoints;
        }

        public void ReceiveGold(int amountOfGold){
            Gold += amountOfGold;
        }

        public void SpendGold(int amountOfGold){
            if (amountOfGold > Gold){
                throw new ArgumentOutOfRangeException($"{Name} only has {Gold} gold, and cannot spend {amountOfGold} gold");
            }
            Gold -= amountOfGold;
        }

        public void AddItemToInventory(GameItem item){
            Inventory.Add(item);

            if (item.IsUnique){
                GroupedInventory.Add(new GroupedInventoryItem(item, 1));
            }
            else{
                if (!GroupedInventory.Any(gi => gi.Item.ItemTypeID == item.ItemTypeID)){
                    GroupedInventory.Add(new GroupedInventoryItem(item, 0));
                }
                GroupedInventory.First(gi => gi.Item.ItemTypeID == item.ItemTypeID).Quantity++;
            }
            OnPropertyChanged(nameof(Weapons));
            OnPropertyChanged(nameof(Consumables));
            OnPropertyChanged(nameof(HasConsumable));
        }

        public void RemoveItemFromInventory(GameItem item) {
            Inventory.Remove(item);

            GroupedInventoryItem groupedInventoryItemToRemove = item.IsUnique ?
                GroupedInventory.FirstOrDefault(gi => gi.Item == item) :        // if true
                GroupedInventory.FirstOrDefault(gi => gi.Item.ItemTypeID == item.ItemTypeID); // if false
                                                                                              // Conditional operator :? (ternary operator) => condition ? consequent : alternative
                                                                                              // similar to if/else. It evaluates a Bool expression and returns the result of one of the two expressions (one is for = true, one is for =false)
                                                                                              // source: https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/conditional-operator
            if (groupedInventoryItemToRemove != null) {
                if(groupedInventoryItemToRemove.Quantity == 1) {
                    GroupedInventory.Remove(groupedInventoryItemToRemove);
                }
                else {
                    groupedInventoryItemToRemove.Quantity--;
                }
            }

            OnPropertyChanged(nameof(Weapons));
            OnPropertyChanged(nameof(Consumables));
            OnPropertyChanged(nameof(HasConsumable));
        }
        #region Private functions
        private void RaiseOnKilledEvent(){
            OnKilled?.Invoke(this, new System.EventArgs());
        }
        private void RaiseActionPerformedEvent(object sender, string result) { //pass the weapon’s message up to the UI – which is only watching for events on the LivingEntity
            OnActionPerformed?.Invoke(this, result);
               }
        #endregion
    }
}