using KnapsackProblemSolver3000;
using System.Runtime.CompilerServices;

Random random = new Random();
List<Item> items = new List<Item>();
int itemsToFillWith = 250;
float maxWeight = 15f;
float maxValue = 21f;

Console.WriteLine("Hello, World!");
Console.WriteLine("This just runs?");
Console.WriteLine("Where's the main method?");
Console.WriteLine("What the hell is going on?");
CreateItems();
PrintItems();

void CreateItems()
{
	for (int i = 0; i < itemsToFillWith; i++)
		items.Add(GetRandomItem());
    items.Sort();
}

void PrintItems()
{
    for (int i = 0; i < items.Count; i++)
        Console.WriteLine("Item number " + i + " " + items[i].ToString());
}

Item GetRandomItem() => new Item((float)random.NextDouble() * maxWeight, (float)random.NextDouble() * maxValue);