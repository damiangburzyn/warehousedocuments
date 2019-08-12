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
    public partial class FormList : Form
    {

        readonly IWarehouseDocumentsService _documentService;
        public FormList()
        {
            UnitOfWork uow = new UnitOfWork();
            _documentService = new WarehouseDocumentsService();

            InitializeComponent();
            RefreshDataGridView();
        }
        private void RefreshDataGridView()
        {
            this.WrapException(() =>
            {
                var dataSource = _documentService.GetWarehouseDocumets();
                dataGridView1.DataSource = dataSource;

                // Automatically resize the visible rows.
                dataGridView1.AutoSizeRowsMode =
                    DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;

                // Set the DataGridView control's border.
                dataGridView1.BorderStyle = BorderStyle.Fixed3D;

                // Put the cells in edit mode when user enters them. 
                dataGridView1.EditMode = DataGridViewEditMode.EditOnEnter;
            });

        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            FormAddDocument form = new FormAddDocument(new WarehouseDocumentViewModel());
            form.VMChanged += ReloadDataSource;
            form.Show();

        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            (this).WrapException(() =>
            {
                DataGridViewRow row = FindSelectedRow();

                if (row != null)
                {
                    var id = (int)row.Cells["Id"].Value;
                    var vm = _documentService.GetWareHouseDocumentById(id);
                    FormAddDocument form = new FormAddDocument(vm);
                    form.VMChanged += ReloadDataSource;
                    form.Show();
                }
            });
        }

        private DataGridViewRow FindSelectedRow()
        {
            DataGridViewRow row = null;
            if (dataGridView1.SelectedRows.Count != 0)
            {
                row = this.dataGridView1.SelectedRows[0];
            }
            else if (dataGridView1.SelectedCells.Count != 0)
            {
                var cell = this.dataGridView1.SelectedCells[0];
                row = dataGridView1.Rows[cell.RowIndex];
            }

            return row;
        }

        public void ReloadDataSource(object sender, EventArgs e)
        {
            RefreshDataGridView();
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            (this).WrapException(() =>
            {
                DataGridViewRow row = FindSelectedRow();

                if (row != null)
                {
                    var id = (int)row.Cells["Id"].Value;
                    _documentService.DeleteWareHouseDocument(id);
                    MessageBox.Show("Dokument usunięty");
                    RefreshDataGridView();    
                }
            });
        }
    }
}
