﻿using Fluxor;

namespace DeVesen.Bazaar.Client.State.Import
{
    public class ImportFacade
    {
        private readonly IDispatcher _dispatcher;

        public ImportFacade(IDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        public void ClearBuffer()
            => _dispatcher.Dispatch(new ImportActions.ClearBuffer());

        public void AnalyzeDataLine(string vendorId, int lineIndex, string lineData)
            => _dispatcher.Dispatch(new ImportActions.AnalyzeDataLine(vendorId, lineIndex, lineData));

        public void ImportDataLine(int lineIndex, Models.Article article)
            => _dispatcher.Dispatch(new ImportActions.ImportDataLine(lineIndex, article));
    }
}
