using System;
using System.Collections.Generic;

interface ITrade
{
    double Value { get; }
    string ClientSector { get; }
    DateTime NextPaymentDate { get; }
}

class Trade : ITrade
{
    public double Value { get; set; }
    public string ClientSector { get; set; }
    public DateTime NextPaymentDate { get; set; }
}

interface ITradeCategory
{
    bool IsInCategory(ITrade trade);
    string Name { get; }
}

class ExpiredTradeCategory : ITradeCategory
{
    private DateTime referenceDate;
    public string Name => "EXPIRED";

    public ExpiredTradeCategory(DateTime referenceDate)
    {
        this.referenceDate = referenceDate;
    }

    public bool IsInCategory(ITrade trade)
    {
        return trade.NextPaymentDate < referenceDate.AddDays(-30);
    }
}

class HighRiskTradeCategory : ITradeCategory
{
    public string Name => "HIGHRISK";

    public bool IsInCategory(ITrade trade)
    {
        return trade.Value > 1000000 && trade.ClientSector == "Private";
    }
}

class MediumRiskTradeCategory : ITradeCategory
{
    public string Name => "MEDIUMRISK";

    public bool IsInCategory(ITrade trade)
    {
        return trade.Value > 1000000 && trade.ClientSector == "Public";
    }
}

class TradeClassifier
{
    private List<ITradeCategory> categories;

    public TradeClassifier()
    {
        categories = new List<ITradeCategory>();
    }

    public void AddCategory(ITradeCategory category)
    {
        categories.Add(category);
    }

    public string Classify(ITrade trade)
    {
        foreach (var category in categories)
        {
            if (category.IsInCategory(trade))
            {
                return category.Name;
            }
        }

        return "DEFAULT";
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Read the reference date
        DateTime referenceDate = DateTime.Parse(Console.ReadLine());

        // Read the number of trades
        int n = int.Parse(Console.ReadLine());

        // Create the trade classifier and add the categories
        TradeClassifier tradeClassifier = new TradeClassifier();
        tradeClassifier.AddCategory(new ExpiredTradeCategory(referenceDate));
        tradeClassifier.AddCategory(new HighRiskTradeCategory());
        tradeClassifier.AddCategory(new MediumRiskTradeCategory());

        // Read and classify the trades
        for (int i = 0; i < n; i++)
        {
            string[] tradeData = Console.ReadLine().Split(' ');
            double value = double.Parse(tradeData[0]);
            string clientSector = tradeData[1];
            DateTime nextPaymentDate = DateTime.Parse(tradeData[2]);

            ITrade trade = new Trade
            {
                Value = value,
                ClientSector = clientSector,
                NextPaymentDate = nextPaymentDate
            };

            string category = tradeClassifier.Classify(trade);
            Console.WriteLine(category);
        }
    }
}
