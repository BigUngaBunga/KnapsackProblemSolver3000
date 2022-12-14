using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnapsackProblemSolver3000
{
    public class Item: IComparable
    {
        private float weight, value, comparativeValue;
        public float Weight => weight;
        public float Value => value;
        public float ComparativeValue => comparativeValue;
        public string ToString => $"Value {Math.Round(Value, 1)}, Weight {Math.Round(Weight, 1)}, Comparative Value {Math.Round(ComparativeValue, 2)}";

        public Item (float weight, float value)
        {
            this.weight = weight;
            this.value = value;
            comparativeValue = value / weight;
        }


        public int CompareTo(object? obj)
        {
            if (obj == null) return 1;
            if ((obj is Item item))
            {
                if (item.ComparativeValue > ComparativeValue)
                    return 1;
                else if (item.ComparativeValue < ComparativeValue)
                    return -1;
            }
            return 0;
        }
    }
}
