﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseDocuments.Contracts;
using WarehouseDocuments.Data;

namespace WarehouseDocuments.Services
{
   public class ArticlesService : IArticlesService
    {

        private IMapper _mapper { get; }
        public ArticlesService()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ArticleViewModel, Article>().ReverseMap();
            });
            this._mapper = config.CreateMapper();
        }


        public async  Task<IEnumerable<ArticleViewModel>> GetDocumentArticles(int warehouseDocumentId)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                var data = await unitOfWork.ArticleRepository.List( a=>a.WarehouseDocumentId== warehouseDocumentId);
                return _mapper.Map<IEnumerable<ArticleViewModel>>(data);
            }
        }

        public async Task DeleteArticle(int id)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                var data = await unitOfWork.ArticleRepository.Get(x => x.Id == id);
                unitOfWork.ArticleRepository.Delete(data);
                unitOfWork.SaveChanges();
            }
        }

        public async Task <ArticleViewModel> GetArticleById(int id)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                var data = await unitOfWork.ArticleRepository.Get(x => x.Id == id);
                var vm = _mapper.Map<ArticleViewModel>(data);
                return vm;
            }
        }

        public async Task <int> SaveArticle(ArticleViewModel vm)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                var data = _mapper.Map<Article>(vm);
                var id = unitOfWork.ArticleRepository.Insert(data);
                await  unitOfWork.SaveChangesAsync();
                return id;
            }
        }

        public async Task UpdateArticle(ArticleViewModel vm)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                var data =  await unitOfWork.ArticleRepository.Get(x => x.Id == vm.Id);
                _mapper.Map<ArticleViewModel, Article>(vm, data);
                unitOfWork.ArticleRepository.Update(data);
                unitOfWork.SaveChanges();
            }
        }
    }
}
