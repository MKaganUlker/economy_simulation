using System;
using System.Collections.Generic;

public class Agent
{
    public string Name { get; private set; }
    public double Money { get; set; }
    public Dictionary<string, int> Inventory { get; set; }
    private Random rand;

    public double Hunger { get; set; }  // Hunger level drives the need for food
    public double Productivity { get; set; }  // Productivity needs affect the demand for tools
    public string Job { get; private set; }  // Each agent has a job to earn money
    public double Salary { get; private set; }

    public Agent(string name, double initialMoney, Random random, string job, double salary)
    {
        Name = name;
        Money = initialMoney;
        Inventory = new Dictionary<string, int>();
        rand = random;
        Hunger = rand.Next(30, 70);  // Initial hunger level
        Productivity = rand.Next(30, 70);  // Initial productivity level
        Job = job;
        Salary = salary;
    }

    public void EarnSalary()
    {
        Money += Salary;
        Console.WriteLine($"{Name} earned {Salary} money from working as a {Job}.");
    }

    // Priority-based decision-making for buying food and tools
    public void SatisfyNeeds(Market market)
    {
        // Priority: Buy food if hungry
        if (Hunger > 50 && Money >= market.Prices["Food"])
        {
            int quantityToBuy = Math.Min((int)(Hunger / 10), market.Supply["Food"]);
            double cost = quantityToBuy * market.Prices["Food"];
            if (Money >= cost)
            {
                Buy("Food", quantityToBuy, market.Prices["Food"]);
                Hunger -= quantityToBuy * 10;  // Reduce hunger
                market.Supply["Food"] -= quantityToBuy;
            }
        }

        // Secondary: Buy tools if productivity needs increase
        if (Productivity > 50 && Money >= market.Prices["Tools"])
        {
            int quantityToBuy = Math.Min(1, market.Supply["Tools"]);  // Buy one tool at a time
            double cost = quantityToBuy * market.Prices["Tools"];
            if (Money >= cost)
            {
                Buy("Tools", quantityToBuy, market.Prices["Tools"]);
                Productivity -= quantityToBuy * 20;  // Reduce productivity need
                market.Supply["Tools"] -= quantityToBuy;
            }
        }
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
    }

    public void DisplayStatus()
    {
        Console.WriteLine($"{Name} has {Money:0.00} money, Hunger: {Hunger}, Productivity: {Productivity}");
    }
}

public class Company
{
    public string Name { get; private set; }
    public string ProducedGood { get; private set; }
    public int ProductionCapacity { get; private set; }
    public double ProductionCost { get; private set; }
    private Random rand;

    public Company(string name, string producedGood, int capacity, double cost, Random random)
    {
        Name = name;
        ProducedGood = producedGood;
        ProductionCapacity = capacity;
        ProductionCost = cost;
        rand = random;
    }

    // Produce goods and add them to the market supply
    public void ProduceGoods(Market market)
    {
        int produced = rand.Next(1, ProductionCapacity);
        market.Supply[ProducedGood] += produced;
        Console.WriteLine($"{Name} produced {produced} {ProducedGood}(s).");
    }
}

public class Market
{
    public Dictionary<string, double> Prices { get; set; }  // Market prices for goods
    public Dictionary<string, int> Supply { get; set; }  // Market supply for goods
    private Random rand;
    public double InflationRate { get; private set; }

    public Market(Random random, double inflationRate)
    {
        rand = random;
        InflationRate = inflationRate;
        Prices = new Dictionary<string, double>
        {
            { "Food", RandomPrice() },
            { "Tools", RandomPrice() }
        };

        Supply = new Dictionary<string, int>
        {
            { "Food", 100 },  // Initial supply
            { "Tools", 50 }
        };
    }

    // Adjust prices based on supply and demand and apply inflation
    public void AdjustPrices()
    {
        foreach (var good in Prices.Keys)
        {
            if (Supply[good] < 50)  // Low supply -> Increase price
            {
                Prices[good] += rand.Next(1, 4);
            }
            else if (Supply[good] > 100)  // High supply -> Decrease price
            {
                Prices[good] = Math.Max(1, Prices[good] - rand.Next(1, 4));
            }

            // Apply inflation to the price
            Prices[good] *= (1 + InflationRate);
            Console.WriteLine($"The price of {good} is now {Prices[good]:0.00}. Supply: {Supply[good]}");
        }
    }

    public double RandomPrice()
    {
        return rand.Next(5, 21);
    }
}

public class Program
{
    public static void Main()
    {
        Random random = new Random();

        // Create a market with inflation (e.g., 2% inflation per round)
        double inflationRate = 0.02;
        Market market = new Market(random, inflationRate);

        // Create agents with jobs and salaries
        Agent agent1 = new Agent("Alice", 100, random, "Farmer", 20);
        Agent agent2 = new Agent("Bob", 150, random, "Carpenter", 30);

        // Create companies that produce goods
        Company foodCompany = new Company("FoodCo", "Food", 20, 5, random);
        Company toolCompany = new Company("ToolCo", "Tools", 10, 10, random);

        // Simulate 10 rounds of economy
        for (int round = 1; round <= 10; round++)
        {
            Console.WriteLine($"\n--- Round {round} ---");

            // Agents earn their salary each round
            agent1.EarnSalary();
            agent2.EarnSalary();

            // Companies produce goods each round
            foodCompany.ProduceGoods(market);
            toolCompany.ProduceGoods(market);

            // Agents try to satisfy their needs
            agent1.SatisfyNeeds(market);
            agent2.SatisfyNeeds(market);

            // Market adjusts prices based on supply, demand, and inflation
            market.AdjustPrices();

            // Display the status of each agent
            agent1.DisplayStatus();
            agent2.DisplayStatus();
        }
    }
}
