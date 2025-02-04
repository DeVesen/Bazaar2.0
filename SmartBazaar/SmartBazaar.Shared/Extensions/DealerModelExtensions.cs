using Mapster;
using MudBlazor;
using SmartBazaar.Shared.Models;
using SmartBazaar.Shared.Types;

namespace SmartBazaar.Shared.Extensions;

public static class DealerModelExtensions
{
    public static bool IsPrivate(this DealerDataModel dealerModel)
        => dealerModel.SalesShareRatio.Type == DealerTypes.Private;

    public static bool IsCompany(this DealerDataModel dealerModel)
        => dealerModel.SalesShareRatio.Type == DealerTypes.Company;

    public static bool IsAssociation(this DealerDataModel dealerModel)
        => dealerModel.SalesShareRatio.Type == DealerTypes.Association;

    public static string GetFullName(this DealerDataModel dealerModel)
    {
        return dealerModel.IsCompany()
            ? dealerModel.Name.LastName
            : $"{dealerModel.Name.FirstName} {dealerModel.Name.LastName}";
    }

    public static string GetTypeName(this DealerDataModel dealerModel)
    {
        if (dealerModel.IsPrivate())
        {
            return "Privat";
        }
        if (dealerModel.IsCompany())
        {
            return "Händler";
        }
        if (dealerModel.IsAssociation())
        {
            return "Intern";
        }

        throw new InvalidOperationException("Unknown dealer type.");
    }

    public static string GetTypeIcon(this DealerModel dealerModel)
    {
        if (dealerModel.IsPrivate())
        {
            return Icons.Material.TwoTone.PersonOutline;
        }
        if (dealerModel.IsCompany())
        {
            return Icons.Material.TwoTone.Business;
        }
        if (dealerModel.IsAssociation())
        {
            return Icons.Material.TwoTone.ChildCare;
        }

        throw new InvalidOperationException("Unknown dealer type.");
    }


    public static DealerCreateModel Adapt(this DealerCreateModel model, DealerDataModel dataToAppend)
    {
        dataToAppend.Adapt(model);
        return model;
    }

    public static DealerModel Adapt(this DealerModel model, DealerDataModel dataToAppend)
    {
        dataToAppend.Adapt(model);
        return model;
    }
}