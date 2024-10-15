Economy Simulation

This project is a simple economy simulation written in C#, where agents with jobs earn salaries, satisfy their needs, and make financial decisions such as saving and investing. The simulation incorporates inflation, supply and demand dynamics, and company investment behaviors. Agents can earn dividends from their investments, and the market prices adjust based on the supply and inflation rate.

Features

- Agent Simulation: Agents earn salaries from jobs, spend money on goods, save a portion of their earnings, and invest in companies.

- Savings and Investments: Agents set aside part of their income as savings, and when their savings reach a certain threshold, they invest in companies and earn dividends.

- Company Simulation: Companies produce goods for the market and distribute dividends to investors based on their performance.

- Market: The market dynamically adjusts the prices of goods based on supply and demand, while also factoring in an inflation rate.

- Inflation: Prices of goods are affected by inflation each round of the simulation.

How It Works

- Each simulation round represents an economic cycle where:
1. Agents earn their salaries.
2. Companies produce goods.
3. Agents purchase goods based on their hunger and productivity needs.
4. Agents can invest in companies if they have enough savings.
5. Companies distribute dividends to their investors.
6. The market adjusts prices based on supply, demand, and inflation.
- Savings and Investment: Agents will save between 10% and 40% of their income and invest in companies when their savings exceed a predefined threshold. They receive dividends based on the company’s performance.

Example Output

		--- Round 1 ---
		Alice earned 20 money and saved 4.00. Current savings: 4.00
		Bob earned 30 money and saved 7.50. Current savings: 7.50
		FoodCo produced 12 Food(s).
		ToolCo produced 6 Tool(s).
		The price of Food is now 9.79. Supply: 112
		The price of Tools is now 11.55. Supply: 56
		Alice bought 2 Food(s) for 19.58 money.
		Bob bought 1 Tool(s) for 11.55 money.
		Alice has 0.42 money, 4.00 in savings, Hunger: 30, Productivity: 60
		Bob has 15.45 money, 7.50 in savings, Hunger: 45, Productivity: 30
