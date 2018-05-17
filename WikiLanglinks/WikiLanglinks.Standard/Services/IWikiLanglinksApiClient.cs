using System.Threading.Tasks;

namespace WikiLanglinks
{
    public interface IWikiLanglinksApiClient
    {
        Task<SearchResults> GetLanglinks(SearchRequest searchRequest);
    }
}