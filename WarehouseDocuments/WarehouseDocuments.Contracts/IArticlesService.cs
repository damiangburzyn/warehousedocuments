using System.Collections.Generic;
using WarehouseDocuments.Contracts;

namespace WarehouseDocuments.Contracts
{
    public interface IArticlesService
    {
        void DeleteArticle(int id);
        ArticleViewModel GetArticleById(int id);
        IEnumerable<ArticleViewModel> GetDocumentArticles(int warehouseDocumentId);
        int SaveArticle(ArticleViewModel vm);
        void UpdateArticle(ArticleViewModel vm);
    }
}