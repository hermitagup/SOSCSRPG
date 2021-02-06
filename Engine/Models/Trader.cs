namespace Engine.Models
{
    public class Trader : LivingEntity
    {
        //public string Name { get; set; } Removed on 10.1 as inheriting from LivingEntity class

        //public ObservableCollection<GameItem> Inventory { get; set; } Removed on 10.1 as inheriting from LivingEntity class

        public Trader(string name)
        {
            Name = name;
            //Inventory = new ObservableCollection<GameItem>(); Removed on 10.1 as inheriting from LivingEntity class
        }

        /*Removed on 10.1 as inheriting from LivingEntity class
        public void AddItemToInventory(GameItem item)
        {
            Inventory.Add(item);
        }*/

        /*Removed on 10.1 as inheriting from LivingEntity class
        public void RemoveItemFromInventory(GameItem item)
        {
            Inventory.Remove(item);
        }*/
    }
}