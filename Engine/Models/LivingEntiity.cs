using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Engine.Models
{
    // abstract class means you can never instantiate this class (you can instantiate a child that is using this class f.ex: player, monster, trader class are going to do 
    public abstract class LivingEntity : BaseNotificationClass
    {
        private string _name;
        private int _currentHitPoints;
        private int _maximumHitPoints;
        private int _gold;

        public string Name {
            get { return _name; }
            set {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public int CurrentHitPoints { 
            get { return _currentHitPoints; }
            set { 
                _currentHitPoints = value;
                OnPropertyChanged(nameof(CurrentHitPoints));
            }
        }

        // this will prevent to get more health than max
        public int MaximumHitPoints
        {
            get { return _maximumHitPoints; }
            set
            {
                _maximumHitPoints = value;
                OnPropertyChanged(nameof(MaximumHitPoints));
            }
        }

        public int Gold
        {
            get { return _gold; }
            set
            {
                _gold = value;
                OnPropertyChanged(nameof(Gold));
            }
        }

        public ObservableCollection<GameItem> Inventory { get; set; }

        public ObservableCollection<GroupedInventoryItem> GroupedInventory { get; set; }

        public List<GameItem> Weapons =>
                Inventory.Where(i => i is Weapon).ToList();

        protected LivingEntity()   // added as void - no return type required ?
        {
            Inventory = new ObservableCollection<GameItem>();
            GroupedInventory = new ObservableCollection<GroupedInventoryItem>();
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
    }
}
