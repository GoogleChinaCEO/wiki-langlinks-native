using System;

namespace WikiLanglinks
{
    public static class EventNames
    {
        public static readonly string SearchRequested = "SearchRequested";
        public static readonly string NewSourceLangRequested = "NewSourceLangRequested";
		public static readonly string TargetLangsSelected = "TargetLangsSelected";
		public static readonly string ErrorOccurred = "ErrorOccurred";
    }
}
