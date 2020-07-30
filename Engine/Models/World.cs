using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Models
{
    public class World
    {
        private List<Location> _locations = new List<Location>(); //create private _locations variable, only accessed inside Word class
    
        internal void AddLocation(int xCoordinate, int yCoordinate, string name, string description, string imageName) //only worldfactory uses this calss. used void, as we don't want any answer back (function doesn't return any value, just do some magic)
        {
            Location loc = new Location(); //creating new Location class instance under name 'loc' & filling properties with data provided from input from function AddLocation
            loc.XCoordinate = xCoordinate;
            loc.YCoordinate = yCoordinate;
            loc.Name = name;
            loc.Description = description;
            loc.ImageName = $"/Engine;component/Images/Locations/{imageName}";

            _locations.Add(loc); //Add just created 'loc' class instance to '_locations' list
     
        }

        public Location LocationAt (int xCoordinate, int yCoordinate) //public, because it will be called from other objects
        {
            foreach(Location loc in _locations) //look for each object in _locations
            {
                if(loc.XCoordinate == xCoordinate && loc.YCoordinate == yCoordinate) //if location have same set of X & Y coordinates with provided coordinates, return that object ('loc')
                {
                    return loc;
                }
            }

            return null; //checked every location, if none of them matches, return null
        }
    }
}
