using Engine.Actions;

namespace Engine.Models
{
    public class GameItem {
        public enum ItemCategory {
            Miscellaneous,
            Weapon
        }
        // removed set{} in Lesson 10.6 to prevent any changes to them , where only place it should be set is below constructor
        public ItemCategory Category { get; }
        public int ItemTypeID { get; }
        public string Name { get; }
        public int Price { get; }
        public bool IsUnique { get; }
        public AttackWithWeapon Action { get; set; }

        public GameItem(ItemCategory category, int itemTypeID, string name, int price, 
            bool isUnique = false, AttackWithWeapon action = null) { //consturctor
            Category = category;
            ItemTypeID = itemTypeID;
            Name = name;
            Price = price;
            IsUnique = isUnique;
            Action = action;
        }
        public void PerformAction(LivingEntity actor, LivingEntity target) {
            Action?.Execute(actor, target);
        }

        public GameItem Clone() {
            return new GameItem(Category, ItemTypeID, Name, Price, 
                IsUnique, Action);
        }
    }
}