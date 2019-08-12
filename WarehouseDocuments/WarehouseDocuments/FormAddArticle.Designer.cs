using WarehouseDocuments.Contracts;

namespace WarehouseDocuments
{
    partial class FormAddArticle
    {
        public FormAddArticle(ArticleViewModel article) : this()
        {
            _article = article;
            //textBoxName.DataBindings.Add(nameof(textBoxName.Text), document, nameof(WarehouseDocumentViewModel.Name), false, DataSourceUpdateMode.OnPropertyChanged);
            //textBoxClientNo.DataBindings.Add(nameof(textBoxClientNo.Text), document, nameof(WarehouseDocumentViewModel.ClientNo), false, DataSourceUpdateMode.OnPropertyChanged);
            //numericNetPrice.DataBindings.Add(nameof(numericNetPrice.Value), document, nameof(WarehouseDocumentViewModel.NetPrice), false, DataSourceUpdateMode.OnPropertyChanged);
            //dateTimePickerDate.DataBindings.Add(nameof(dateTimePickerDate.Value), document, nameof(WarehouseDocumentViewModel.Date), false, DataSourceUpdateMode.OnPropertyChanged);
        }

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        public ArticleViewModel _article { get; private set; }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Text = "FormAddArticle";
        }

        #endregion
    }
}