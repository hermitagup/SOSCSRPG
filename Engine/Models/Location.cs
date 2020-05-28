using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Models
{
    public class Location
    {
        public int XCoordinate { get; set; }
        public int YCoordinate { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageName { get; set; }
        public List<Quest> QuestsAvailableHere { get; set; } = new List<Quest>();   // Scope of this property is public, the data type is a list of Quest objects (to have ability to have multiple Quests in the location), 
                                                                                    // the '= new List<Quest>()' will initialize this property with an empty list to start with . 
                                                                                    // Because of that we do not need of creating constructor  and initialize the List inside the constructor.


    }
}
