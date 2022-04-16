using EnglishWordHelperClient.Infrastructure.RequestFeatures;
using EnglishWordHelperClient.Models;
using System.Threading.Tasks;

namespace EnglishWordHelperClient.HttpServices.Interfaces
{
    public interface IWordHttpService
    {
        Task<WordDetails> GetById(int id);
        Task<WordDetails> GetInfoForWordName(string wordName);
        Task<PagingResponse<Word>> GetAll(PaginationParameters parameters);
        Task<bool> Create(WordDetails entity);
        Task<bool> Update(WordDetails entity);
        Task<bool> Delete(int id);
    }
}
