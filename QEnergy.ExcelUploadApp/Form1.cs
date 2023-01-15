using ExcelDataReader;
using QEnergy.ExcelUploadApp.Utils.Enums;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;
using Z.Dapper.Plus;

namespace QEnergy.ExcelUploadApp
{
    public partial class Form1 : Form
    {
        DataTableCollection TableCollection;

        public Form1()
        {
            InitializeComponent();
        }

        private void CboSheet_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = TableCollection[cboSheet.SelectedItem.ToString()];
            //dataGridView1.DataSource = dt;
            if (dt != null)
            {
                List<ProjectRow> projects = new List<ProjectRow>();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ProjectRow project = new ProjectRow();
                    project.Id = i + 2;
                    project.Name = dt.Rows[i]["project_name"].ToString();
                    project.ProjectNumber = dt.Rows[i]["project_name"].ToString();
                    if (!string.IsNullOrWhiteSpace(dt.Rows[i]["acquisition_date"].ToString()))
                        project.AcquisitionDate = (DateTime)dt.Rows[i]["acquisition_date"];
                    else
                        project.AcquisitionDate = null;
                    project.Number3lCode = dt.Rows[i]["number_3l_code"].ToString();
                    if (Enum.TryParse(dt.Rows[i]["project_deal_type"].ToString(), out ProjectDealTypes dealType))
                        project.DealType = dealType;
                    project.Group = dt.Rows[i]["project_group"].ToString();
                    project.Status = (ProjectStatuses)int.Parse(dt.Rows[i]["project_status"].ToString().Substring(0, 1));
                    project.WTGNumbers = dt.Rows[i]["WTG_numbers"].ToString();
                    project.Kw = long.Parse(dt.Rows[i]["kW"].ToString());
                    if (!string.IsNullOrWhiteSpace(dt.Rows[i]["months_acquired"].ToString()))
                        project.MonthsAquired = int.Parse(dt.Rows[i]["months_acquired"].ToString());
                    else
                        project.MonthsAquired = null;
                    project.CompanyId = int.Parse(dt.Rows[i]["company_id"].ToString());
                    project.Created = DateTime.Now;
                    project.Modified = null;
                    project.CreatedBy = default;
                    project.ModifiedBy = null;
                    project.Deleted = false;

                    projects.Add(project);
                }

                projectRowBindingSource.DataSource = projects;
            }
        }

        private void BtnBrowse_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog() { Filter = "Excel 90-2003 Workbook|*.xls|Excel Workbook|*xlsx" })
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    txtFilename.Text = ofd.FileName;

                    using (var strean = File.Open(ofd.FileName, FileMode.Open, FileAccess.Read))
                    {
                        using (IExcelDataReader reader = ExcelReaderFactory.CreateReader(strean))
                        {
                            DataSet result = reader.AsDataSet(new ExcelDataSetConfiguration()
                            {
                                ConfigureDataTable = (_) => new ExcelDataTableConfiguration() { UseHeaderRow = true }
                            });
                            TableCollection = result.Tables;
                            cboSheet.Items.Clear();
                            foreach (DataTable table in TableCollection)
                                cboSheet.Items.Add(table.TableName); // Add sheet to combobox
                        }
                    }
                }
            }
        }

        private void BtnImport_Click(object sender, EventArgs e)
        {
            try
            {
                string connectionString = "Data Source=localhost;Initial Catalog=QEnergy;Trusted_Connection=True;TrustServerCertificate=Yes;MultipleActiveResultSets=true;Connection Timeout=120;";
                DapperPlusManager.Entity<ProjectRow>().Table("Projects");
                List<ProjectRow> projects = projectRowBindingSource.DataSource as List<ProjectRow>;
                if (projects != null)
                {
                    using (IDbConnection db = new SqlConnection(connectionString))
                    {
                        db.BulkInsert(projects);
                    }
                }
                MessageBox.Show("Import finished!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
    }
}
