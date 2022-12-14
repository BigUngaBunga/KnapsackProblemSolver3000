using KnapsackProblemSolver3000;
using System.Runtime.CompilerServices;

Random random = new Random();
List<Item> items = new List<Item>();
List<Knapsack> knapsacks = new List<Knapsack>();
int itemsToFillWith = 100;
int numberOfKnapsacks = 5;
float maxWeight = 15f;
float maxValue = 21f;
//Console.WriteLine("Hello, World!");
//Console.WriteLine("This just runs?");
//Console.WriteLine("Where's the main method?");
//Console.WriteLine("What the hell is going on?");

CreateItems();
PrintItems();
InitializeKnapsacks();
FillKnapsacks();
PrintItems();

void CreateItems()
{
    Console.WriteLine("Creating items");
    for (int i = 0; i < itemsToFillWith; i++)
		items.Add(GetRandomItem());
    items.Sort();
}

void InitializeKnapsacks()
{
    Console.WriteLine("Creating knapsacks");
    int baseCapacity = 25, deviation = 25;
    for (int i = 0; i < numberOfKnapsacks; i++)
    {
        knapsacks.Add(new Knapsack(baseCapacity + random.Next(deviation)));
        Console.WriteLine("Knapsack " + i + " " + knapsacks[i].ToString);
    }
}

void FillKnapsacks()
{
    Console.WriteLine("Filling knapsacks");
    for (int i = 0; i < items.Count; i++)
    {
        for (int j = 0; j < knapsacks.Count; j++)
        {
            if (PutItemInKnapsack(i, j))
            {
                i--;
                break;
            }
        }
    }

    for (int i = 0; i < knapsacks.Count; i++)
        Console.WriteLine("Knapsack " + i + " " + knapsacks[i].ToString);
}

void PrintItems()
{
    Console.WriteLine("Remaining items");
    for (int i = 0; i < items.Count; i++)
        Console.WriteLine("Item number " + i + " " + items[i].ToString);
}

bool PutItemInKnapsack(int itemIndex, int knapsackIndex)
{
    if (knapsacks[knapsackIndex].TryAddItem(items[itemIndex]))
    {
        items.RemoveAt(itemIndex);
        return true;
    }
    return false;
}

Item GetRandomItem() => new Item((float)random.NextDouble() * maxWeight, (float)random.NextDouble() * maxValue);