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


        public delegate Task VMChangedDelegate(object sender, EventArgs e);
        public event VMChangedDelegate VMChanged;

        public FormAddDocument()
        {           
            InitializeComponent();
           // VMChanged += (x,y) => { };
        }

        public FormAddDocument(WarehouseDocumentViewModel document, IWarehouseDocumentsService documentsService, IArticlesService articlesService) : this()
        {
            _documentsService = documentsService;
            _articlesService = articlesService;
            _document = document;
            textBoxName.DataBindings.Add(nameof(textBoxName.Text), document, nameof(WarehouseDocumentViewModel.Name), false, DataSourceUpdateMode.OnPropertyChanged);
            textBoxClientNo.DataBindings.Add(nameof(textBoxClientNo.Text), document, nameof(WarehouseDocumentViewModel.ClientNo), false, DataSourceUpdateMode.OnPropertyChanged);          
            dateTimePickerDate.DataBindings.Add(nameof(dateTimePickerDate.Value), document, nameof(WarehouseDocumentViewModel.Date), false, DataSourceUpdateMode.OnPropertyChanged);    
        }


        private async void ButtonSave_Click(object sender, EventArgs e)
        {
         await    SaveOrUpdate();
        }

        private async Task SaveOrUpdate()
        {
            await this.WrapException(async () =>
            {
                if (_document.Id == 0)
                {
                    await _documentsService.SaveWarehouseDocumet(_document);
                }
                else {
                   await  _documentsService.UpdateWarehouseDocumet(_document);
                }
               await  VMChanged(this, new EventArgs());
                this.Close();
            });
        }
    }
}
