using KnapsackProblemSolver3000;
using System.Runtime.CompilerServices;

Random random = new Random();
List<Item> items = new List<Item>();
List<Knapsack> knapsacks = new List<Knapsack>();
int itemsToFillWith = 250;
int numberOfKnapsacks = 10;
float minWeight = 1f, maxWeight = 8f;
float minValue = 1f, maxValue = 21f;

CreateItems();
InitializeKnapsacks();
FillKnapsacks();
ImprovingSearch();

Console.WriteLine("\nFinal outcome");
//PrintItems();
PrintKnapsacks();

Item GetRandomItem() => new Item(GetRandomFloat(minWeight, maxWeight), GetRandomFloat(minValue, maxValue));

float GetRandomFloat(float min, float max) => min + (float)random.NextDouble() * (max - min);

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
        knapsacks.Add(new Knapsack(GetRandomFloat(baseCapacity, baseCapacity + deviation)));
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
    PrintKnapsacks();
}

void ImprovingSearch()
{
    int changes = 1;
    while (changes > 0)
    {
        changes = 0;
        for (int i = 0; i < knapsacks.Count; i++)
        {
            for (int j = i + 1; j < knapsacks.Count; j++)
            {
                while (FindBestSwap(knapsacks[i], knapsacks[j])) { changes++; }
            }
        }
        FillKnapsacks();
    }

}

bool FindBestSwap(Knapsack knap, Knapsack sack) 
{
    int sackIndex = -1, knapIndex = -1;

    float bestCapacity = knap.RemainingCapacity;
    for (int i = 0; i < knap.Items.Count; i++)
    {
        for (int j = 0; j < sack.Items.Count; j++)
        {
            float swappedCapacity = knap.CapacityIfSwapped(i, sack.Items[j]);
            if (swappedCapacity < bestCapacity && swappedCapacity >= 0 && sack.CapacityIfSwapped(j, knap.Items[i]) >= 0)
            {
                bestCapacity = swappedCapacity;
                knapIndex = i;
                sackIndex = j;
            }
        }
    }    

    if (knapIndex >= 0 && sackIndex >= 0)
        return SwapItems();
    return false;

    bool SwapItems()
    {
        if(knap.TryRemoveItem(knapIndex, out Item knapItem) && sack.TryRemoveItem(sackIndex, out Item sackItem))
        {
            if (knap.CanAdd(sackItem) && sack.CanAdd(knapItem))
            {
                if (!knap.TryAddItem(sackItem))
                {
                    Console.WriteLine($"Knap failed to add Sack item: remaining cap => {knap.RemainingCapacity}, item weight => {sackItem.Weight}");
                }
                if (!sack.TryAddItem(knapItem))
                {
                    Console.WriteLine($"Sack failed to add Knap item: remaining cap => {sack.RemainingCapacity}, item weight => {knapItem.Weight}");
                }
            }
            else
            {
                knap.TryAddItem(knapItem);
                sack.TryAddItem(sackItem);
                return false;
            }
        }
        return true;
    }
}

void PrintKnapsacks()
{
    Console.WriteLine("Contents of the knapsacks:");
    float totalValue = 0, totalWeight = 0;
    for (int i = 0; i < knapsacks.Count; i++)
    {
        totalValue += knapsacks[i].Value;
        totalWeight += knapsacks[i].Weight;
        Console.WriteLine("Knapsack " + i + " " + knapsacks[i].ToString);
    }
    Console.WriteLine($"Total value {totalValue}, total weight {totalWeight}, comparative value {totalValue / totalWeight}");
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

