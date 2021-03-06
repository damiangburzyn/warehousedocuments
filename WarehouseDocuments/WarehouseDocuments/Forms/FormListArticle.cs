﻿using System;
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
    public partial class FormListArticle : Form
    {
        public delegate Task VMChangedDelegate(object sender, EventArgs e);
        public event VMChangedDelegate VMChanged;

        private WarehouseDocumentViewModel _wareHouseDocument { get; set; }
        public IArticlesService _articlesService { get; }

        public FormListArticle()
        {
            InitializeComponent();
            //VMChanged += (x, y) => { };
        }

        public FormListArticle(WarehouseDocumentViewModel wareHouseDocument, IArticlesService articlesService) :this()
        {
            _wareHouseDocument = wareHouseDocument;
            _articlesService = articlesService;
            RefreshDataGridView();
        }

        private async Task RefreshDataGridView()
        {
           await this.WrapException(async () =>
            {
                var dataSource = await _articlesService.GetDocumentArticles(_wareHouseDocument.Id);
                dataGridView1.DataSource = dataSource;
                DataGridViewColumn column = dataGridView1.Columns[0];
                column.Visible= false;
                dataGridView1.Columns[1].HeaderText = "Nazwa";
                dataGridView1.Columns[2].HeaderText = "Liczba";
                dataGridView1.Columns[3].HeaderText = "Cena netto";
                dataGridView1.Columns[4].HeaderText = "Cena brutto";
                dataGridView1.Columns[5].Visible = false;
                // Automatically resize the visible rows.
                dataGridView1.AutoSizeRowsMode =
                DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;

                // Set the DataGridView control's border.
                dataGridView1.BorderStyle = BorderStyle.Fixed3D;

                // Put the cells in edit mode when user enters them. 
                dataGridView1.EditMode = DataGridViewEditMode.EditOnEnter;
            });
        }


        private void ButtonAdd_Click(object sender, EventArgs e)
        {
            FormAddArticle form = new FormAddArticle(new ArticleViewModel() { WarehouseDocumentId = _wareHouseDocument.Id }, _articlesService);
            form.VMChanged += ReloadDataSource;
            form.Show();

        }

        private async void ButtonUpdate_Click(object sender, EventArgs e)
        {
            await (this).WrapException(async() =>
            {
                DataGridViewRow row = FindSelectedRow();

                if (row != null)
                {
                    var id = (int)row.Cells["Id"].Value;
                    var vm = await _articlesService.GetArticleById(id);
                    FormAddArticle form = new FormAddArticle(vm, _articlesService);
                    form.VMChanged +=  ReloadDataSource;
                    form.Show();
                }
            });
        }

        private async void ButtonDelete_Click(object sender, EventArgs e)
        {
           await (this).WrapException( async () =>
            {
                DataGridViewRow row = FindSelectedRow();

                if (row != null)
                {
                    var id = (int)row.Cells["Id"].Value;
                    await _articlesService.DeleteArticle(id);
                    MessageBox.Show("Artykuł usunięty");
                   await RefreshDataGridView();
                   await this.VMChanged(this, new EventArgs());
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

        public async Task ReloadDataSource(object sender, EventArgs e)
        {
            await RefreshDataGridView();
            await this.VMChanged(this, new EventArgs());
        }

    }
}
