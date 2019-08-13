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
using WarehouseDocuments.Data;
using WarehouseDocuments.Services;

namespace WarehouseDocuments
{
    public partial class FormAddDocument : Form
    {
        private IWarehouseDocumentsService _documentsService { get;  set; }
        private IArticlesService _articlesService { get; set; }


        public delegate void VMChangedDelegate(object sender, EventArgs e);
        public event VMChangedDelegate VMChanged;

        public FormAddDocument()
        {           
            _documentsService = new WarehouseDocumentsService();
            _articlesService = new ArticlesService();
            InitializeComponent();
            VMChanged += (x,y) => { };
        }

        public FormAddDocument(WarehouseDocumentViewModel document) : this()
        {
            _document = document;
            textBoxName.DataBindings.Add(nameof(textBoxName.Text), document, nameof(WarehouseDocumentViewModel.Name), false, DataSourceUpdateMode.OnPropertyChanged);
            textBoxClientNo.DataBindings.Add(nameof(textBoxClientNo.Text), document, nameof(WarehouseDocumentViewModel.ClientNo), false, DataSourceUpdateMode.OnPropertyChanged);          
            dateTimePickerDate.DataBindings.Add(nameof(dateTimePickerDate.Value), document, nameof(WarehouseDocumentViewModel.Date), false, DataSourceUpdateMode.OnPropertyChanged);    
        }


        private void ButtonSave_Click(object sender, EventArgs e)
        {
            SaveOrUpdate();
        }

        private void SaveOrUpdate()
        {
            this.WrapException(() =>
            {
                if (_document.Id == 0)
                {
                    _documentsService.SaveWarehouseDocumet(_document);
                }
                else {
                    _documentsService.UpdateWarehouseDocumet(_document);
                }
                VMChanged(this, new EventArgs());
                this.Close();
            });
        }
    }
}
