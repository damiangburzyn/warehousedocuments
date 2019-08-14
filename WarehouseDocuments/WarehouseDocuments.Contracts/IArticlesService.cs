using System.Collections.Generic;
using System.Threading.Tasks;
using WarehouseDocuments.Contracts;

namespace WarehouseDocuments.Contracts
{
    public interface IArticlesService
    {
        Task DeleteArticle(int id);
        Task<ArticleViewModel> GetArticleById(int id);
        Task<IEnumerable<ArticleViewModel>> GetDocumentArticles(int warehouseDocumentId);
        Task<int> SaveArticle(ArticleViewModel vm);
        Task UpdateArticle(ArticleViewModel vm);
    }
}