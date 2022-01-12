using System.Collections.Generic;
using System.Linq;

namespace Engine.Models {
    public class Recipe {
        public int ID { get; }
        public string Name { get; }
        public List<ItemQuantity> Ingredients { get; } = new List<ItemQuantity>();      //ItemQuantity list - To make a granola bar you need --> 1x Oats 1x Honey 1x Raisins
        public List<ItemQuantity> OutputItems { get; } = new List<ItemQuantity>();      //Output item - Granola  --> 1x granola bar

        public Recipe(int id, string name) {
            ID = id;
            Name = name;
        }

        public void AddIngredient(int itemID, int quantity) {               //helper function for adding an ingredients
            if (!Ingredients.Any(x => x.ItemID == itemID)) {                //this is to avoid adding already added ingredient again
                Ingredients.Add(new ItemQuantity(itemID, quantity));
            }
        }

        public void AddOutputItem(int itemID, int quantity) {               //helper function for addin output item
            if (!OutputItems.Any(x => x.ItemID == itemID)) {                //this is to avoid giving already given output item
                OutputItems.Add(new ItemQuantity(itemID, quantity));
            }
        }
    }
}