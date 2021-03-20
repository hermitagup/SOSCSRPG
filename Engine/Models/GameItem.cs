using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Models
{
    public class GameItem
    {
        // removed set{} in Lesson 10.6 to prevent any changes to them , where only place it should be set is below constructor
        public int ItemTypeID { get; }
        public string Name { get; }
        public int Price { get; }
        public bool IsUnique { get; }

        public GameItem(int itemTypeID, string name, int price, bool isUnique = false) { //consturctor
            ItemTypeID = itemTypeID;
            Name = name;
            Price = price;
            IsUnique = isUnique;
        }

        public GameItem Clone() {
            return new GameItem(ItemTypeID, Name, Price, IsUnique); //?
        }
    }
}
