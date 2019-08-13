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
        public delegate void VMChangedDelegate(object sender, EventArgs e);
        public event VMChangedDelegate VMChanged;
        public IArticlesService _articlesService { get; }

        private WarehouseDocumentViewModel _document;

        public ArticleViewModel _article { get; private set; }

        public FormAddArticle()
        {
            InitializeComponent();
            _articlesService = new ArticlesService();
            VMChanged += (x, y) => { };
        }

        public FormAddArticle(ArticleViewModel article, WarehouseDocumentViewModel document) : this()
        {
            _document = document;
            _article = article;
            textBoxName.DataBindings.Add(nameof(textBoxName.Text), article, nameof(ArticleViewModel.Name), false, DataSourceUpdateMode.OnPropertyChanged);
            numericNetPrice.DataBindings.Add(nameof(numericNetPrice.Value), article, nameof(ArticleViewModel.NetPrice), false, DataSourceUpdateMode.OnPropertyChanged);
            numericCount.DataBindings.Add(nameof(numericNetPrice.Value), article, nameof(ArticleViewModel.Count), false, DataSourceUpdateMode.OnPropertyChanged);
            labelGrossPrice.DataBindings.Add(nameof(labelGrossPrice.Text), article, nameof(ArticleViewModel.GrossPrice), false, DataSourceUpdateMode.OnPropertyChanged);
        }

        private void ButtonSave_Click(object sender, EventArgs e)
        {
            SaveOrUpdate();
        }

        private void SaveOrUpdate()
        {
            this.WrapException(() =>
            {

                if (_article.Id == 0)
                {
                    if (_document.Id != 0)
                    {
                        _article.WarehouseDocumentId = _document.Id;
                        _articlesService.SaveArticle(_article);
                        VMChanged(this, new EventArgs());
                    }
                    else
                    {
                        _document.Articles.Add(_article);
                    }
                }
                else
                {
                    _articlesService.UpdateArticle(_article);
                    VMChanged(this, new EventArgs());
                }
                this.Close();
            });
        }
    }
}
