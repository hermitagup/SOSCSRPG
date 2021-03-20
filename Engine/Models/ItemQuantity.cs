using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Models
{
    public class ItemQuantity
    {
        // removed set{} in Lesson 10.6 to prevent any changes to them , where only place it should be set is below constructor
        public int ItemID { get;}
        public int Quantity { get;}

        public ItemQuantity(int itemID, int quantity) {
            ItemID = itemID;
            Quantity = quantity;
        }
    }
}
