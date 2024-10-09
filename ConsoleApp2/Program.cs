using System;
using System.Collections.Generic;

public class Agent
{
    public string Name { get; private set; }
    public double Money { get; set; }
    public Dictionary<string, int> Inventory { get; set; }
    private Random rand;

    public Agent(string name, double initialMoney, Random random)
    {
        Name = name;
        Money = initialMoney;
        Inventory = new Dictionary<string, int>();
        rand = random;
    }

    public void Buy(string good, int quantity, double price)
    {
        double totalCost = quantity * price;
        if (Money >= totalCost)
        {
            Money -= totalCost;
            if (Inventory.ContainsKey(good))
            {
                Inventory[good] += quantity;
            }
            else
            {
                Inventory[good] = quantity;
            }
            Console.WriteLine($"{Name} bought {quantity} {good}(s) for {totalCost} money.");
        }
        else
        {
            Console.WriteLine($"{Name} cannot afford to buy {quantity} {good}(s).");
        }
    }

    public void Sell(string good, int quantity, double price)
    {
        if (Inventory.ContainsKey(good) && Inventory[good] >= quantity)
        {
            Inventory[good] -= quantity;
            Money += quantity * price;
            Console.WriteLine($"{Name} sold {quantity} {good}(s) for {quantity * price} money.");
        }
        else
        {
            Console.WriteLine($"{Name} does not have enough {good}(s) to sell.");
        }
    }

    // Random action: Buy or Sell
    public void RandomAction(Market market)
    {
        string[] goods = { "Food", "Tools" };
        string good = goods[rand.Next(goods.Length)];
        double price = market.Prices[good];
        int quantity = rand.Next(1, 5); // Random quantity between 1 and 4
        
        if (rand.Next(2) == 0) // 50% chance to buy or sell
        {
            Buy(good, quantity, price);
        }
        else
        {
            Sell(good, quantity, price);
        }
    }

    // Display the agent's money
    public void DisplayMoney()
    {
        Console.WriteLine($"{Name} has {Money:0.00} money remaining.");
    }
}

public class Market
{
    public Dictionary<string, double> Prices { get; set; }
    private Random rand;

    public Market(Random random)
    {
        rand = random;
        Prices = new Dictionary<string, double>
        {
            { "Food", RandomPrice() },
            { "Tools", RandomPrice() }
        };
    }

    // Generate random prices between 5 and 20
    public double RandomPrice()
    {
        return rand.Next(5, 21);
    }

    // Adjust prices with some randomness
    public void AdjustPrices()
    {
        foreach (var good in Prices.Keys)
        {
            double priceChange = rand.Next(-2, 3); // Random change between -2 and +2
            Prices[good] = Math.Max(1, Prices[good] + priceChange); // Ensure price is >= 1
            Console.WriteLine($"The price of {good} is now {Prices[good]:0.00}.");
        }
    }
}

public class Program
{
    public static void Main()
    {
        Random random = new Random();
        
        // Create a simple market
        Market market = new Market(random);
        
        // Create two agents with some initial money
        Agent agent1 = new Agent("Alice", 100, random);
        Agent agent2 = new Agent("Bob", 200, random);
        
        // Run simulation for 10 rounds
        for (int round = 1; round <= 10; round++)
        {
            Console.WriteLine($"\n--- Round {round} ---");
            
            // Agents take random actions (buying or selling)
            agent1.RandomAction(market);
            agent2.RandomAction(market);
            
            // Market prices fluctuate after each round
            market.AdjustPrices();

            // Display each agent's money after the round
            agent1.DisplayMoney();
            agent2.DisplayMoney();
        }
    }
}
