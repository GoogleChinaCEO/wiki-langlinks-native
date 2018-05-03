using System.Threading.Tasks;
using WikiLanglinks.Models;

namespace WikiLanglinks.Services
{
    public interface IWikiLanglinksApiClient
    {
        Task<SearchResults> GetLanglinks(SearchRequest searchRequest);
    }
}