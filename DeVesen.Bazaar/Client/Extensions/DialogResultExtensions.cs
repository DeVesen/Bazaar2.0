using MudBlazor;

namespace DeVesen.Bazaar.Client.Extensions;

public static class DialogResultExtensions
{
    public static async Task<bool> WaitForResult(this Task<IDialogReference> dialogReferenceTask)
    {
        var dlgReference = await dialogReferenceTask;
        var dataResult = await dlgReference.Result;

        return dataResult!.Canceled;
    }

    public static async Task<(bool Canceled, T Data)> WaitForResult<T>(this Task<IDialogReference> dialogReferenceTask)
    {
        var dlgReference = await dialogReferenceTask;
        var dataResult = await dlgReference.Result;

        return (dataResult!.Canceled, (T)dataResult.Data!);
    }
}