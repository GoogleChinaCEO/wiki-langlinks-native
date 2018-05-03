using System;

namespace WikiLanglinks
{
    public class SearchRequest
    {
        public string SearchTerm { get; set; }
        public string Source { get; set; }
        public string[] Targets { get; set; }
    }
}
