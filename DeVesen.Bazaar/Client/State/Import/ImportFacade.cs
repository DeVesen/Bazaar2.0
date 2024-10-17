using Fluxor;
using Microsoft.AspNetCore.Components;

namespace DeVesen.Bazaar.Client.State.Import
{
    public class ImportFacade(IDispatcher dispatcher)
    {
        public IDispatcher Dispatcher { get; } = dispatcher;

        public void ClearBuffer()
            => Dispatcher.Dispatch(new ImportActions.ClearBuffer());

        public void AnalyzeDataLine(string vendorId, int lineIndex, string lineData)
            => Dispatcher.Dispatch(new ImportActions.AnalyzeDataLine(vendorId, lineIndex, lineData));

        public void ImportDataLines(IEnumerable<(int LineIndex, Models.Article Article)> lines)
            => Dispatcher.Dispatch(new ImportActions.ImportDataLines(lines));
    }
}
