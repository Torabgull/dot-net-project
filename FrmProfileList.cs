using DevExpress.XtraReports.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ETEATestTask
{
    public partial class FrmProfileList : Form
    {
        public FrmProfileList()
        {
            InitializeComponent();
        }

        private void FrmProfileList_Load(object sender, EventArgs e)
        {
            GetProfileData();
        }
        public void GetProfileData()
        {
            SqlConnection con = ConMgr.GetConnection();
            using (SqlCommand cmd=new SqlCommand("select *from Profile"))
            {
                using (SqlDataAdapter da=new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    da.SelectCommand = cmd;
                    using (DataTable dt=new DataTable())
                    {
                        da.Fill(dt);
                        gridControl1.DataSource = dt;
                    }
                }
                
            }
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            new FrmAddNewRecord().ShowDialog();
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ProfileList pf = new ProfileList();
            ReportPrintTool report = new ReportPrintTool(pf);
            report.ShowRibbonPreview();
        }
    }
}
