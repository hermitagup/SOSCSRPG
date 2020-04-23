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
        private List<Location> _locations = new List<Location>(); //create locations variable, private, only accessed inside wordl object
    
        internal void AddLocation(int xCoordinate, int yCoordinate, string name, string description, string imageName) //only worldfactory uses this calss, when we call it we don't want any answer back (function doesn't return any value)
        {
            Location loc = new Location(); //properties are the parameter which we receive
            loc.XCoordinate = xCoordinate;
            loc.YCoordinate = yCoordinate;
            loc.Name = name;
            loc.Description = description;
            loc.ImageName = imageName;

            _locations.Add(loc); //put new location object in locations
     
        }

        public Location LocationAt (int xCoordinate, int yCoordinate) //public, because it will be called from other objects
        {
            foreach(Location loc in _locations) //look for each object in _locations, if loc has coordinates then return that location
            {
                if(loc.XCoordinate == xCoordinate && loc.YCoordinate == yCoordinate)
                {
                    return loc;
                }
            }

            return null; //checked every location, if none of them matches, return null
        }
    }
}
