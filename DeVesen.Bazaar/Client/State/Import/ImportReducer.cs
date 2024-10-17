using Fluxor;

namespace DeVesen.Bazaar.Client.State.Import;

public static class ImportReducer
{
    [ReducerMethod(typeof(ImportActions.ClearBuffer))]
    public static ImportState ClearBuffer(ImportState state)
        => state with { Items = Enumerable.Empty<ImportInfo>(), IsImporting = false };

    [ReducerMethod(typeof(ImportActions.ImportDataLines))]
    public static ImportState ImportDataLines(ImportState state)
        => state with { IsImporting = true };

    [ReducerMethod(typeof(ImportActions.AllIDataLinesImported))]
    public static ImportState AllIDataLinesImported(ImportState state)
        => state with { IsImporting = false };

    [ReducerMethod]
    public static ImportState AddAnalyzedDataLine(ImportState state, ImportActions.DataLineAnalyzed action)
    {
        var itemsBuffer = state.Items.ToList();

        itemsBuffer.Add(action.Info);

        return state with { Items = itemsBuffer };
    }

    [ReducerMethod]
    public static ImportState DataLineValidated(ImportState state, ImportActions.DataLineValidated action)
    {
        var itemsBuffer = state.Items.ToList();
        var item = itemsBuffer.FirstOrDefault(p => p.LineIndex == action.LineIndex);

        if (item == null)
        {
            return state;
        }

        itemsBuffer[itemsBuffer.IndexOf(item)] = item with { Validated = action.Success, ErrorMessages = action.ErrorMessages };

        return state with { Items = itemsBuffer };
    }

    [ReducerMethod]
    public static ImportState DataLineImported(ImportState state, ImportActions.DataLineImported action)
    {
        var itemsBuffer = state.Items.ToList();
        var item = itemsBuffer.FirstOrDefault(p => p.LineIndex == action.LineIndex);

        if (item == null)
        {
            return state;
        }

        itemsBuffer[itemsBuffer.IndexOf(item)] = item with { Imported = action.Success, ErrorMessages = action.ErrorMessages};

        return state with { Items = itemsBuffer };
    }
}