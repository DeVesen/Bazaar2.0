namespace SmartBazaar.Shared.Models;

public class DealerInfoAggregate
{
    public required DealerModel Dealer { get; set; }
    public required StatisticModel Statistic { get; set; }
}