using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine.Models;
using Engine.Factories;
using System.ComponentModel;

namespace Engine.ViewModels
{
    public class GameSession : BaseNotificationClass
    {
        private Location _currentLocation;
        public World CurrentWorld { get; set; }
        public Player CurrentPlayer { get; set; }
        public Location CurrentLocation                     //When CurrentLocation changes
        {   get { return _currentLocation; }
            set { 
                _currentLocation = value;                         //we reset current location ('value' - explanation https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/value
                OnPropertyChanged(nameof(CurrentLocation));       //change onproperty string into name of currentlocation property      //we raise OnPropertyChange for a CurrentLocation to redraw an image and update info
                OnPropertyChanged(nameof(HasLocationToNorth));    //we raise OnPropertyChange for boolean value "HasLocationToNorth" to show or hide direction button based on that if there is a location available North of current location
                OnPropertyChanged(nameof(HasLocationToEast));
                OnPropertyChanged(nameof(HasLocationToWest));
                OnPropertyChanged(nameof(HasLocationToSouth));    //renaming string "HasLocationToSouth" to name of CurrentLocation property 'nameof(HasLocationToSouth)' to make it instantly updated everytime we update property name, 
            }                                                     //otherwise property name will be updated in a project but not here as this is a string and does not really reflect the property name in a n active way!
                                                                  // 'OnPropertyChanged' is inherited from BaseNotificationClass <-- hover over it to confirm!
        }
        public bool HasLocationToNorth { 
            get {
                return CurrentWorld.LocationAt(CurrentLocation.XCoordinate, CurrentLocation.YCoordinate + 1) != null; // look in the currentworld try currentlocation at x coordinate and y coordinate +1 if it is not equal null 
            } 
        }

        public bool HasLocationToEast
        {
            get
            {
                return CurrentWorld.LocationAt(CurrentLocation.XCoordinate + 1, CurrentLocation.YCoordinate) != null;
            }
        }

        public bool HasLocationToSouth
        {
            get
            {
                return CurrentWorld.LocationAt(CurrentLocation.XCoordinate, CurrentLocation.YCoordinate - 1) != null;
            }
        }

        public bool HasLocationToWest
        {
            get
            {
                return CurrentWorld.LocationAt(CurrentLocation.XCoordinate - 1, CurrentLocation.YCoordinate) != null;
            }
        }

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
    }
}
