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
        private  WarehouseDocumentsService _documentService { get;  set; }

        public delegate void VMChangedDelegate(object sender, EventArgs e);
        public event VMChangedDelegate VMChanged;

        public FormAddDocument()
        {           
            _documentService = new WarehouseDocumentsService();
            InitializeComponent();
            VMChanged += (x,y) => { };
        }

        public FormAddDocument(WarehouseDocumentViewModel document) : this()
        {
            _document = document;
            textBoxName.DataBindings.Add(nameof(textBoxName.Text), document, nameof(WarehouseDocumentViewModel.Name), false, DataSourceUpdateMode.OnPropertyChanged);
            textBoxClientNo.DataBindings.Add(nameof(textBoxClientNo.Text), document, nameof(WarehouseDocumentViewModel.ClientNo), false, DataSourceUpdateMode.OnPropertyChanged);
            numericNetPrice.DataBindings.Add(nameof(numericNetPrice.Value), document, nameof(WarehouseDocumentViewModel.NetPrice), false, DataSourceUpdateMode.OnPropertyChanged);
            dateTimePickerDate.DataBindings.Add(nameof(dateTimePickerDate.Value), document, nameof(WarehouseDocumentViewModel.Date), false, DataSourceUpdateMode.OnPropertyChanged);
            labelGrossPrice.DataBindings.Add(nameof(labelGrossPrice.Text), document, nameof(WarehouseDocumentViewModel.GrossPrice), false, DataSourceUpdateMode.OnPropertyChanged);
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void buttonArticleList_Click(object sender, EventArgs e)
        {
            var d = _document.Name;

        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            SaveOrUpdate();
        }

        private void SaveOrUpdate()
        {
            this.WrapException(() =>
            {

                if (_document.Id == 0)
                {
                    _documentService.SaveWarehouseDocumet(_document);
                }

                else {
                    _documentService.UpdateWarehouseDocumet(_document);
                }
                VMChanged(this, new EventArgs());
                this.Close();

            });
        }
    }
}
