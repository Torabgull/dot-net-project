using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
namespace ETEATestTask
{
    public partial class FrmAddNewRecord : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public FrmAddNewRecord()
        {
            InitializeComponent();
        }
        public static byte[] ImageToByteArray(Image imagin,string extension)
        {
            MemoryStream ms=new MemoryStream();
            imagin.Save(ms,System.Drawing.Imaging.ImageFormat.Jpeg);
            return ms.ToArray();
        }
        public Byte[] picture=null;
        private void btnsubmit_Click(object sender, EventArgs e)
        {
            picture = ImageToByteArray(pictureEdit1.Image, ".jpg");
            if(txtnic.Text.Trim().ToString()=="" || txtname.Text.Trim().ToString()==""|| txtfname.Text.Trim().ToString()=="")
            {
                MessageBox.Show("All fileds are neccessary...!");
                return;
            }
            using (SqlConnection con=ConMgr.GetConnection())
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                try 
	            {	        
		 using (SqlTransaction trans=con.BeginTransaction())
                {
                    using (SqlCommand cmd=new SqlCommand("insert into Profile (name,fathername,cnic,dob,picture) Values ('"+txtfname.Text+"','"+txtfname.Text+"','"+txtnic.Text+"','"+Convert.ToDateTime(txtdate.EditValue.ToString()).ToString("yyyy/MM/dd")+"',@picture)",con))
                    {
                        cmd.Transaction=trans;
                        cmd.Parameters.AddWithValue("@picture",(picture==null)? (Object)DBNull.Value: picture).SqlDbType = SqlDbType.VarBinary;
                   cmd.ExecuteNonQuery();
                    }
                    trans.Commit();
                    MessageBox.Show("Data Saved...!");
                }
	            }
	            catch (Exception)
	            {
	
		            //throw;
	            }
                con.Close();
            }
            
            
        }
    }
}
