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
        public Weapon(int itemTypeID, string name, int price, int minDamage, int maxDamage)  // Construct new object that take parameters 'itemTypeID', 'name', 'price' from base class which is a 'GameItem' and add two more 'MinimumDamage', 'MaximumDamage'
            : base(itemTypeID, name, price, true){
            MinimumDamage = minDamage;
            MaximumDamage = maxDamage;
        }
        public new Weapon Clone()  // as Weapon class is a sub class of GameItem and both have same function 'Clone' we need to confirm that we want to use class from Weapon not from GameItem by using 'new' word.
        {   
            return new Weapon(ItemTypeID, Name, Price, MinimumDamage, MaximumDamage); //This 'new' is creating an instance of a return object
        }
    }
}
