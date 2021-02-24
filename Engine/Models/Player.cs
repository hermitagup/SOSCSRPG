using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Models
{
    public class Player : LivingEntity     // Before--> "Player : INotifyPropertyChanged" klasa player, z parametrami { INotifyPropertyChange will notify that change to any of the property in a Player class was made and anything that is using those properties will know to update values accordingly
    {
        private string _characterClass;
        private int _experiencePoints;
        private int _level;

        public string CharacterClass {
            get { return _characterClass; }
            set {
                _characterClass = value;
                OnPropertyChanged(nameof(CharacterClass));
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

        public ObservableCollection<QuestStatus> Quests { get; set; }   // New data type 'OvservableCollection' with new property 'Quests' with getter and setter 
                                                                        // new data type requires refference to Collection.ObjectModel namespace 
                                                                        // we are using this data type as it automatically updates UI when new Quest or completes current

        public Player() {
            Quests = new ObservableCollection<QuestStatus>();   //This will instanciate new ObserverCollevtion list of QuestsStatus and set Quests property to that value
        }

        public bool HasAllTheseItems(List<ItemQuantity> items) {                        // Passing list of quantity objects (Quest objects required to complete quest)
            foreach (ItemQuantity item in items)                                        // Checking each of passed item
            {
                if (Inventory.Count(i=> i.ItemTypeID == item.ItemID) < item.Quantity)   // Looking into players inventory and count how many items they have in their inventory, where the item Id matches
                {                                                                       // If the count is less than count needed from the passed parameter (passed list), returning false, else return true
                    return false;
                }
            }
            return true;
        }

            
    }
}
