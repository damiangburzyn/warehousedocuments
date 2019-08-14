using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WarehouseDocuments.Contracts;
using WarehouseDocuments.Services;

namespace WarehouseDocuments
{
    public partial class FormAddArticle : Form
    {
        public delegate Task VMChangedDelegate(object sender, EventArgs e);
        public event VMChangedDelegate VMChanged;
        readonly IArticlesService _articlesService;
        private WarehouseDocumentViewModel _document;

        public ArticleViewModel _article { get; private set; }

        public FormAddArticle()
        {
            InitializeComponent();
            //VMChanged += async (x, y) =>  { };
        }

        public FormAddArticle(ArticleViewModel article, IArticlesService articlesService) : this()
        {
            _articlesService = articlesService;
            _article = article;
            textBoxName.DataBindings.Add(nameof(textBoxName.Text), article, nameof(ArticleViewModel.Name), false, DataSourceUpdateMode.OnPropertyChanged);
            numericNetPrice.DataBindings.Add(nameof(numericNetPrice.Value), article, nameof(ArticleViewModel.NetPrice), false, DataSourceUpdateMode.OnPropertyChanged);
            numericCount.DataBindings.Add(nameof(numericNetPrice.Value), article, nameof(ArticleViewModel.Count), false, DataSourceUpdateMode.OnPropertyChanged);
            labelGrossPrice.DataBindings.Add(nameof(labelGrossPrice.Text), article, nameof(ArticleViewModel.GrossPrice), false, DataSourceUpdateMode.OnPropertyChanged);
        }

        private async void ButtonSave_Click(object sender, EventArgs e)
        {
            await SaveOrUpdate();
        }

        private async Task SaveOrUpdate()
        {
            await this.WrapException(async () =>
             {

                 if (_article.Id == 0)
                 {
                     await _articlesService.SaveArticle(_article);
                 }
                 else
                 {
                     await _articlesService.UpdateArticle(_article);
                 }
                 await VMChanged(this, new EventArgs());
                 this.Close();
             });
        }
    }
}
