using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnapsackProblemSolver3000
{
    public class Knapsack
    {
        private List<Item> items;
        private float weightCapacity;
        private float currentWeight;
        private float currentValue;

        public List<Item> Items => items;
        public float RemainingCapacity => weightCapacity - currentWeight;
        public float Value => currentValue;
        public float Weight => currentWeight;
        public string ToString => $"The knapsack carries {items.Count} items weighing {Math.Round(Weight, 1)}, a total capacity of {Math.Round(weightCapacity, 1)} " +
            $"and with a total value of {Math.Round(Value, 1)}";
        
        public Knapsack(float capacity)
        {
            weightCapacity = capacity;
            items = new List<Item>();
        }


        public bool CanAdd(Item item) => item.Weight <= RemainingCapacity;

        public bool TryAddItem(Item item)
        {
            if (CanAdd(item))
            {
                items.Add(item);
                currentValue += item.Value;
                currentWeight += item.Weight;
                return true;
            }
            return false;
        }

        public bool TryRemoveItem(int index, out Item item)
        {
            item = null;
            if (index >= 0 && index < items.Count)
            {
                item = items[index];
                currentValue -= item.Value;
                currentWeight -= item.Weight;
                items.RemoveAt(index);
                return true;
            }
            return false;
        }

        public float CapacityIfSwapped(int itemIndex, Item swappingItem) => RemainingCapacity + items[itemIndex].Weight - swappingItem.Weight;

    }
}
