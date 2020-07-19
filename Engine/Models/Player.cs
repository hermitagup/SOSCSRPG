using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Models
{
    public class Player : BaseNotificationClass     // Before--> "Player : INotifyPropertyChanged" klasa player, z parametrami { INotifyPropertyChange will notify that change to any of the property in a Player class was made and anything that is using those properties will know to update values accordingly
    {
        private string _name;
        private string _characterClass;
        private int _hitPoints;
        private int _experiencePoints;
        private int _level;
        private int _gold;

        public string Name {
            get { return _name; }
            set { 
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }
        public string CharacterClass {
            get { return _characterClass; }
            set {
                _characterClass = value;
                OnPropertyChanged(nameof(CharacterClass));
            }
        }
        public int HitPoints {
            get { return _hitPoints; }
            set {
                _hitPoints = value;
                OnPropertyChanged(nameof(HitPoints));
            }
        }
        public int ExperiencePoints
        {
            get { return _experiencePoints; }
            set 
            { 
                _experiencePoints = value;
                OnPropertyChanged(nameof(ExperiencePoints));  // This is exact property name that will be used in below OnPropertyChanged
            }
        }
        public int Level {
            get { return _level; }
            set {
                _level = value;
                OnPropertyChanged(nameof(Level));
            }
        }
        public int Gold {
            get { return _gold; }
            set {
                _gold = value;
                OnPropertyChanged(nameof(Gold));
            }
        }
        public ObservableCollection<GameItem> Inventory { get; set; }   // New data type 'OvservableCollection' with new property 'Inventory' with getter and setter 
                                                                        // new data type requires refference to Collection.ObjectModel namespace 
                                                                        // we are using this data type as it automatically handles all the notifications (no need of like 'OnPropertyChanged'))

        public List<GameItem> Weapons =>
            Inventory.Where(i => i is Weapon).ToList();                 // instead of using getter & setter we use this method (called link statements), saying whenever anything is accessing the Weapons property, it's going
                                                                        // to return the inventory items, where the item is a weapon.
                                                                        // the .ToList() allows us to force this to materialize the results | as deffered query, without forcing sometimes we would not get results.
                                                                        // List is not automatically updated like ObservableCollection, we need to raise property change event to notify UI, whenever Weapon List change => AddItemToInventory()


        public ObservableCollection<QuestStatus> Quests { get; set; }   // New data type 'OvservableCollection' with new property 'Quests' with getter and setter 
                                                                        // new data type requires refference to Collection.ObjectModel namespace 
                                                                        // we are using this data type as it automatically updates UI when new Quest or completes current

        public Player() {
            Inventory = new ObservableCollection<GameItem>();   //This will instanciate new ObserverCollevtion list of GameItems and set Inventory property to that value
            Quests = new ObservableCollection<QuestStatus>();   //This will instanciate new ObserverCollevtion list of QuestsStatus and set Quests property to that value
        }

        public void AddItemToInventory(GameItem item) {
            Inventory.Add(item);                    // this will add new item to our inventory
            OnPropertyChanged(nameof(Weapons));     // this function will raise PropertyChange event for Weapons. UI will know that need to check if there is an update for Weapon list (UI will check and know if need to update ComboBox)

        }
            
    }
}
