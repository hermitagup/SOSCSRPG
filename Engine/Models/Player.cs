using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Engine.Models
{
    public class Player : LivingEntity     // Before--> "Player : INotifyPropertyChanged" klasa player, z parametrami { INotifyPropertyChange will notify that change to any of the property in a Player class was made and anything that is using those properties will know to update values accordingly
    {
        #region Properties

        private string _characterClass;
        private int _experiencePoints;
        private int _level;

        #endregion

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
            //Inventory = new ObservableCollection<GameItem>();   Removed on 10.1 as inheriting from LivingEntity class
            Quests = new ObservableCollection<QuestStatus>();   //This will instanciate new ObserverCollevtion list of QuestsStatus and set Quests property to that value
        }

        public bool HasAllTheseItems(List<ItemQuantity> items) // this function check if the player has all the items required to complete the quest
        {                                                      // function accepts a list of ItemQuantity objects and looks through the playr's inventory
            foreach (ItemQuantity item in items)
            {
                if (Inventory.Count(i => i.ItemTypeID == item.ItemID) < item.Quantity) //if the count of items is less than the number required in the parameter, the function returns false; if the player has a large enough quantity for all the items passed into the function it will return true
                {
                    return false;
                }
            }

            return true;
        }
    }
}
