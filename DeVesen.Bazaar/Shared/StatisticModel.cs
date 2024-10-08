using System.Diagnostics.CodeAnalysis;

namespace DeVesen.Bazaar.Shared;

[ExcludeFromCodeCoverage]
public record StatisticModel
{
    public required VendorsModel Vendors { get; init; }
    public required ArticlesModel Articles { get; init; }
    public required ActualStockModel ActualStock { get; init; }
    public required IncomeStockModel Income { get; init; }

    public record VendorsModel
    {
        public required double TotalCount { get; init; }
    }

    public record ArticlesModel
    {
        public required double ApprovedCount { get; init; }
        public required double ApprovedValue { get; init; }
        public required double TotalCount { get; init; }
        public required double TotalValue { get; init; }
    }

    public record ActualStockModel
    {
        public required double IsOnSaleCount { get; init; }
        public required double IsSoldCount { get; init; }
        public required double IsReturnedCount { get; init; }
        public required double IsSettledCount { get; init; }
    }

    public record IncomeStockModel
    {
        public required double Turnover { get; init; }
        public required double TurnoverVendorShare { get; init; }
        public required double TurnoverClubShare { get; init; }
        public required double WorkingFee { get; init; }
    }
}