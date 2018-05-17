using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WikiLanglinks
{
    public interface IAppPropertiesProvider
    {
        Language SourceLanguage { get; set; }

        IList<Language> TargetLanguages { get; set; }

        Task SaveAsync();
    }
}
