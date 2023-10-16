// See https://aka.ms/new-console-template for more information
using TradeStream;

var generator = new TradeGenerator();


while(true)
{
    var trade = generator.NextTrade();
    Thread.Sleep(Random.Shared.Next(50,150));
}
