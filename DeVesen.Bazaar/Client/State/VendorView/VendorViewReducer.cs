using DeVesen.Bazaar.Client.Models;
using DeVesen.Bazaar.Shared.Statistics;
using Fluxor;

namespace DeVesen.Bazaar.Client.State.VendorView;

public static class VendorViewReducer
{
    [ReducerMethod(typeof(VendorViewActions.Fetch))]
    public static VendorViewState Fetch(VendorViewState state)
        => state with { Vendors = [], IsLoaded = false };

    [ReducerMethod]
    public static VendorViewState SetList(VendorViewState state, VendorViewActions.SetList action)
        => state with { Vendors = action.Items.OrderBy(p => p.Vendor.GetTotalName()).ToArray(), IsLoaded = true };

    [ReducerMethod]
    public static VendorViewState SetList(VendorViewState state, VendorViewActions.CleanUp action)
        => state with { Vendors = [], IsLoaded = true };


    [ReducerMethod]
    public static VendorViewState SetMasterData(VendorViewState state, VendorViewActions.SetMasterData action)
    {
        var newList = state.Vendors.ToList();
        var existingElement = newList.FirstOrDefault(p => p.Vendor.Id == action.Vendor.Id);
        
        if (existingElement != null)
        {
            var existingIndex = newList.IndexOf(existingElement);

            var updatedElement = existingElement with { Vendor = action.Vendor };

            newList[existingIndex] = updatedElement;
        }
        else
        {
            newList.Add(new VendorOverviewItem
            {
                Vendor = action.Vendor,
                Counts = CountsStatistics.Empty,
                Values = ValuesStatistics.Empty
            });
        }

        return state with { Vendors = newList.OrderBy(p => p.Vendor.GetTotalName()).ToArray() };
    }

    [ReducerMethod]
    public static VendorViewState SetStatistic(VendorViewState state, VendorViewActions.SetStatistic action)
    {
        var newList = state.Vendors.ToList();
        var existingElement = newList.FirstOrDefault(p => p.Vendor.Id == action.Id);

        if (existingElement == null)
        {
            return state with { Vendors = newList.OrderBy(p => p.Vendor.GetTotalName()).ToArray() };
        }

        var existingIndex = newList.IndexOf(existingElement);

        var updatedElement = existingElement with { Counts = action.Counts, Values = action.Values};

        newList[existingIndex] = updatedElement;

        return state with { Vendors = newList.OrderBy(p => p.Vendor.GetTotalName()).ToArray() };
    }

    [ReducerMethod]
    public static VendorViewState Removed(VendorViewState state, VendorViewActions.Removed action)
        => state with { Vendors = state.Vendors.Where(p => p.Vendor.Id != action.Id).ToArray() };
}