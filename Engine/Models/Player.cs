using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Engine.Models
{
    public class Player :LivingEntity     // Before--> "Player : INotifyPropertyChanged" klasa player, z parametrami { INotifyPropertyChange will notify that change to any of the property in a Player class was made and anything that is using those properties will know to update values accordingly
    {
        #region Properties

        private string _characterClass;
        private int _experiencePoints;

        public string CharacterClass {
            get { return _characterClass; }
            set {
                _characterClass = value;
                OnPropertyChanged(); // we do not need to pass parameter as changes done in Lesson 10.6 | was OnPropertyChanged(nameof(CharacterClass));
            }
        }

        public int ExperiencePoints {
            get { return _experiencePoints; }
            private set {
                _experiencePoints = value;
                OnPropertyChanged();                // This is exact property name that will be used in below OnPropertyChanged,
                                                    // which is ExperiencedPoints taken automatically (as no parameter given) - check Lesson 10.6, BaseNotofication.cs
                SetLevelAndMaximumHitPoints();      // give the player experience
            }
        }

        public ObservableCollection<QuestStatus> Quests { get; }   // New data type 'OvservableCollection' with new property 'Quests' with getter and setter 
                                                                   // new data type requires refference to Collection.ObjectModel namespace 
                                                                   // we are using this data type as it automatically updates UI when new Quest or completes current

        public ObservableCollection<Recipe> Recipes { get; }        //Recipes property with data type ObservableCollection of <Recipe> objects


        #endregion

        public event EventHandler OnLeveledUp;
        public Player(string name, string characterClass, int experiencePoints, int maximumHitPoints, int currentHitPoints, int gold) : base (name, maximumHitPoints, currentHitPoints, gold) {

            CharacterClass = characterClass;
            ExperiencePoints = experiencePoints;

            Quests = new ObservableCollection<QuestStatus>();   //This will instanciate new ObserverCollevtion list of QuestsStatus and set Quests property to that value
            Recipes = new ObservableCollection<Recipe>();       //Initialization of Recipes property with an empty ObservableCollection of <Recipe> objects
        }

        public void AddExperience(int experiencePoints) {
            ExperiencePoints += experiencePoints;
        }

        public void LearnRecipe(Recipe recipe) {                //Learn Recipe function for a Player
            if (!Recipes.Any(r => recipe.ID == recipe.ID)) {    //check if Player already has this recipe learned, if not he will learn it
                Recipes.Add(recipe);
            }
        }


        private void SetLevelAndMaximumHitPoints() { // function that saves original level valuer, recalculates the lvel and handles leveling up
            int originalLevel = Level;
 
            Level = (ExperiencePoints / 100) + 1;
 
            if (Level != originalLevel) {
                MaximumHitPoints = Level * 10;
 
                OnLeveledUp?.Invoke(this, System.EventArgs.Empty);
            }
        }
    }
}
