using SmartBazaar.Shared.Models;

namespace SmartBazaar.Client.States.DealerInfoState;

public class DealerInfoActions
{
    public record ReLoadAllDealer;

    public record ReLoadAllDealerFailed;


    public record ReLoadDealerById(string DealerId);

    public record ReLoadDealerModel(DealerModel DealerModel);
    
    public record UpsertDealerView(DealerModel DealerModel, StatisticModel StatisticModel);


    public record ReLoadDealerStatistic(string DealerId);

    public record SetDealerStatistic(string DealerId, StatisticModel StatisticModel);


    public record RemoveDealer(string Id);
}

// Wenn Ich ein neuer Articel hinzufüge, dann sollte auch eine Action geben das für diesen Dealer die Statistic neu berechnet wird.

// Wenn ich einen Artikel Modiviziere, dann sollte ich den Status anpassen können
// Wenn Ich ein Articel Modiviziere, dann sollte auch eine Action geben das für diesen Dealer die Statistic neu berechnet wird.

// Weitere Statistic: Nach Artikel-Kategorie pro Dealer
// Weitere Statistic: Umsätze pro Dealer