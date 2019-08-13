using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseDocuments.Contracts;
using WarehouseDocuments.Data;

namespace WarehouseDocuments.Services
{
    public class WarehouseDocumentsService : IWarehouseDocumentsService
    {
        
        private IMapper _mapper { get; }
        public WarehouseDocumentsService()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<WarehouseDocumentViewModel, WarehouseDocument>().ReverseMap()
                .ForMember(a => a.NetPrice, opt => opt.MapFrom(a => a.Articles.Sum(b=>b.Count * b.NetPrice)))
                .ForMember(a => a.GrossPrice, opt => opt.MapFrom(a => decimal.Round(a.Articles.Sum(b => b.Count * b.NetPrice) * 1.23m, 2, MidpointRounding.AwayFromZero)));
                cfg.CreateMap<ArticleViewModel, Article>().ReverseMap();
            });
            this._mapper = config.CreateMapper();
        }

        public IEnumerable<WarehouseDocumentViewModel> GetWarehouseDocumets()
        {
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                var includes = new List<string>() { nameof(WarehouseDocumentViewModel.Articles) };
                var data = unitOfWork.WarehouseDocumentRepository.List(null, null,includes);
                return _mapper.Map<IEnumerable<WarehouseDocumentViewModel>>(data);
            }          
        }


        public int SaveWarehouseDocumet(WarehouseDocumentViewModel vm)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                var data = _mapper.Map<WarehouseDocument>(vm);
                var id = unitOfWork.WarehouseDocumentRepository.Insert(data);
                unitOfWork.SaveChanges();
                return id;
            }       
        }

        public void UpdateWarehouseDocumet(WarehouseDocumentViewModel vm)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                var data = unitOfWork.WarehouseDocumentRepository.Get(x => x.Id == vm.Id);
                _mapper.Map<WarehouseDocumentViewModel, WarehouseDocument>(vm, data);
                unitOfWork.WarehouseDocumentRepository.Update(data);
                unitOfWork.SaveChanges();
            }
        }

        public WarehouseDocumentViewModel GetWareHouseDocumentById(int id)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                var includes = new List<string>() { nameof(WarehouseDocumentViewModel.Articles) };
                var data = unitOfWork.WarehouseDocumentRepository.Get(x => x.Id == id, includes);
                var vm = _mapper.Map<WarehouseDocumentViewModel>(data);
                return vm;
            }
        }


        public void DeleteWareHouseDocument(int id)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                var data = unitOfWork.WarehouseDocumentRepository.Get(x => x.Id == id);
                unitOfWork.WarehouseDocumentRepository.Delete(data);
                unitOfWork.SaveChanges();
            }
        }
    }
}
