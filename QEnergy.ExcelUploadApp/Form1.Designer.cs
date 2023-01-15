namespace QEnergy.ExcelUploadApp
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            this.btnBrowse = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.cboSheet = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtFilename = new System.Windows.Forms.TextBox();
            this.btnImport = new System.Windows.Forms.Button();
            this.projectRowBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.idDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.projectNumberDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.acquisitionDateDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.number3lCodeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dealTypeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.statusDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.wTGNumbersDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.kwDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.monthsAquiredDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.companyIdDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.projectRowBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(747, 337);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(41, 23);
            this.btnBrowse.TabIndex = 0;
            this.btnBrowse.Text = "...";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.BtnBrowse_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.idDataGridViewTextBoxColumn,
            this.nameDataGridViewTextBoxColumn,
            this.projectNumberDataGridViewTextBoxColumn,
            this.acquisitionDateDataGridViewTextBoxColumn,
            this.number3lCodeDataGridViewTextBoxColumn,
            this.dealTypeDataGridViewTextBoxColumn,
            this.groupDataGridViewTextBoxColumn,
            this.statusDataGridViewTextBoxColumn,
            this.wTGNumbersDataGridViewTextBoxColumn,
            this.kwDataGridViewTextBoxColumn,
            this.monthsAquiredDataGridViewTextBoxColumn,
            this.companyIdDataGridViewTextBoxColumn});
            this.dataGridView1.DataSource = this.projectRowBindingSource;
            this.dataGridView1.Location = new System.Drawing.Point(12, 12);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.Size = new System.Drawing.Size(776, 316);
            this.dataGridView1.TabIndex = 1;
            // 
            // cboSheet
            // 
            this.cboSheet.FormattingEnabled = true;
            this.cboSheet.Location = new System.Drawing.Point(87, 365);
            this.cboSheet.Name = "cboSheet";
            this.cboSheet.Size = new System.Drawing.Size(183, 21);
            this.cboSheet.TabIndex = 2;
            this.cboSheet.SelectedIndexChanged += new System.EventHandler(this.CboSheet_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 339);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "File name:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 365);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Sheet:";
            // 
            // txtFilename
            // 
            this.txtFilename.Location = new System.Drawing.Point(87, 339);
            this.txtFilename.Name = "txtFilename";
            this.txtFilename.ReadOnly = true;
            this.txtFilename.Size = new System.Drawing.Size(654, 20);
            this.txtFilename.TabIndex = 5;
            // 
            // btnImport
            // 
            this.btnImport.Location = new System.Drawing.Point(713, 415);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(75, 23);
            this.btnImport.TabIndex = 6;
            this.btnImport.Text = "&Import";
            this.btnImport.UseVisualStyleBackColor = true;
            this.btnImport.Click += new System.EventHandler(this.BtnImport_Click);
            // 
            // projectRowBindingSource
            // 
            this.projectRowBindingSource.DataSource = typeof(QEnergy.ExcelUploadApp.ProjectRow);
            // 
            // idDataGridViewTextBoxColumn
            // 
            this.idDataGridViewTextBoxColumn.DataPropertyName = "Id";
            this.idDataGridViewTextBoxColumn.HeaderText = "Id";
            this.idDataGridViewTextBoxColumn.Name = "idDataGridViewTextBoxColumn";
            // 
            // nameDataGridViewTextBoxColumn
            // 
            this.nameDataGridViewTextBoxColumn.DataPropertyName = "Name";
            this.nameDataGridViewTextBoxColumn.HeaderText = "Name";
            this.nameDataGridViewTextBoxColumn.Name = "nameDataGridViewTextBoxColumn";
            // 
            // projectNumberDataGridViewTextBoxColumn
            // 
            this.projectNumberDataGridViewTextBoxColumn.DataPropertyName = "ProjectNumber";
            this.projectNumberDataGridViewTextBoxColumn.HeaderText = "ProjectNumber";
            this.projectNumberDataGridViewTextBoxColumn.Name = "projectNumberDataGridViewTextBoxColumn";
            // 
            // acquisitionDateDataGridViewTextBoxColumn
            // 
            this.acquisitionDateDataGridViewTextBoxColumn.DataPropertyName = "AcquisitionDate";
            this.acquisitionDateDataGridViewTextBoxColumn.HeaderText = "AcquisitionDate";
            this.acquisitionDateDataGridViewTextBoxColumn.Name = "acquisitionDateDataGridViewTextBoxColumn";
            // 
            // number3lCodeDataGridViewTextBoxColumn
            // 
            this.number3lCodeDataGridViewTextBoxColumn.DataPropertyName = "Number3lCode";
            this.number3lCodeDataGridViewTextBoxColumn.HeaderText = "Number3lCode";
            this.number3lCodeDataGridViewTextBoxColumn.Name = "number3lCodeDataGridViewTextBoxColumn";
            // 
            // dealTypeDataGridViewTextBoxColumn
            // 
            this.dealTypeDataGridViewTextBoxColumn.DataPropertyName = "DealType";
            this.dealTypeDataGridViewTextBoxColumn.HeaderText = "DealType";
            this.dealTypeDataGridViewTextBoxColumn.Name = "dealTypeDataGridViewTextBoxColumn";
            // 
            // groupDataGridViewTextBoxColumn
            // 
            this.groupDataGridViewTextBoxColumn.DataPropertyName = "Group";
            this.groupDataGridViewTextBoxColumn.HeaderText = "Group";
            this.groupDataGridViewTextBoxColumn.Name = "groupDataGridViewTextBoxColumn";
            // 
            // statusDataGridViewTextBoxColumn
            // 
            this.statusDataGridViewTextBoxColumn.DataPropertyName = "Status";
            this.statusDataGridViewTextBoxColumn.HeaderText = "Status";
            this.statusDataGridViewTextBoxColumn.Name = "statusDataGridViewTextBoxColumn";
            // 
            // wTGNumbersDataGridViewTextBoxColumn
            // 
            this.wTGNumbersDataGridViewTextBoxColumn.DataPropertyName = "WTGNumbers";
            this.wTGNumbersDataGridViewTextBoxColumn.HeaderText = "WTGNumbers";
            this.wTGNumbersDataGridViewTextBoxColumn.Name = "wTGNumbersDataGridViewTextBoxColumn";
            // 
            // kwDataGridViewTextBoxColumn
            // 
            this.kwDataGridViewTextBoxColumn.DataPropertyName = "Kw";
            this.kwDataGridViewTextBoxColumn.HeaderText = "Kw";
            this.kwDataGridViewTextBoxColumn.Name = "kwDataGridViewTextBoxColumn";
            // 
            // monthsAquiredDataGridViewTextBoxColumn
            // 
            this.monthsAquiredDataGridViewTextBoxColumn.DataPropertyName = "MonthsAquired";
            this.monthsAquiredDataGridViewTextBoxColumn.HeaderText = "MonthsAquired";
            this.monthsAquiredDataGridViewTextBoxColumn.Name = "monthsAquiredDataGridViewTextBoxColumn";
            // 
            // companyIdDataGridViewTextBoxColumn
            // 
            this.companyIdDataGridViewTextBoxColumn.DataPropertyName = "CompanyId";
            this.companyIdDataGridViewTextBoxColumn.HeaderText = "CompanyId";
            this.companyIdDataGridViewTextBoxColumn.Name = "companyIdDataGridViewTextBoxColumn";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnImport);
            this.Controls.Add(this.txtFilename);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cboSheet);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btnBrowse);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Read excel *.xls or *.xlsx";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.projectRowBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ComboBox cboSheet;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtFilename;
        private System.Windows.Forms.Button btnImport;
        private System.Windows.Forms.DataGridViewTextBoxColumn idDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn projectNumberDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn acquisitionDateDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn number3lCodeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dealTypeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn groupDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn statusDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn wTGNumbersDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn kwDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn monthsAquiredDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn companyIdDataGridViewTextBoxColumn;
        private System.Windows.Forms.BindingSource projectRowBindingSource;
    }
}

