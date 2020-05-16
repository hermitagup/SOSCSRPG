using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Models
{
    public class Weapon : GameItem  // The Weapon class is a sub class of GameItem class
    {
        public int MinimumDamage { get; set; }
        public int MaximumDamage { get; set; }
        public Weapon(int itemTypeID, string name, int price, int minDamage, int maxDamage)  // this will take parameters 'itemTypeID', 'name', 'price' and send their values to base class which is a 'GameItem' class in here
            : base(itemTypeID, name, price){
            MinimumDamage = minDamage;
            MaximumDamage = maxDamage;
        }
        public new Weapon Clone()  // as Weapon class is a sub class of GameItem and both have same function 'Clone' we need to confirm that we want to use class from Weapon not from GameItem by using 'new' word.
        {   
            return new Weapon(ItemTypeID, Name, Price, MinimumDamage, MaximumDamage);
        }
    }
}
