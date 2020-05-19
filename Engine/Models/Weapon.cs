using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Models
{
    class Weapon : GameItem // the weapon class is a subclass of GameItem
    {
        public int MinimumDamage { get; set; }
        public int MaximumDamage { get; set; }

        public Weapon(int itemTypeID, string name, int price, int minDamage, int maxDamage) // consturctor to send items to base class via Weapon class?
            : base(itemTypeID, name, price)
        {
            MinimumDamage = minDamage;
            MaximumDamage = maxDamage;
        }

        public new Weapon Clone ()
        {
            return new Weapon(ItemTypeID, Name, Price, MinimumDamage, MaximumDamage); //instuatnce new weapon
        }
    }
}
