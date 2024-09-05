using Fluxor;

namespace DeVesen.Bazaar.Client.State.Title;

public class TitleReducer
{
    [ReducerMethod]
    public static TitleState SetCaption(TitleState state, TitleActions.SetCaption action)
    {
        return state with { Caption = action.Value };
    }
}