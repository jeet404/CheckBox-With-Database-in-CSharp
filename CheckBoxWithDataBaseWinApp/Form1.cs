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

namespace CheckBoxWithDataBaseWinApp
{
    public partial class Form1 : Form
    {
        SqlConnection conn;
        SqlCommand cmd;
        SqlDataAdapter da;
        DataTable dt;
        string sk;
        CheckBox[] itm = new CheckBox[6];
        public Form1()
        {
            InitializeComponent();
            itm[0] = chk1;
            itm[1] = chk2;
            itm[2] = chk3;
            itm[3] = chk4;
            itm[4] = chk5;
            itm[5] = chk6;
        }

        private void skillin()
        {
            sk = "";
            foreach (CheckBox c in itm)
            {
                if (c.Checked)
                {
                    sk += c.Text + ",";
                }
            }
            sk = sk.TrimEnd(',');
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename='C:\WorkSpace\CheckBoxWithDataBaseWinApp\CheckBoxWithDataBaseWinApp\SkillsDB.mdf';Integrated Security=True");
                conn.Open();
                if (conn.State == ConnectionState.Open)
                {
                    lbl_stat.BackColor = Color.Green;
                }
            }
            catch (Exception ex)
            {
                lbl_stat.BackColor = Color.Red;
            }
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            skillin();
            cmd = new SqlCommand("INSERT INTO skill_details VALUES ('"+sk+"')", conn);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Skill Inserted..");
            foreach (CheckBox c in itm)
            {
                c.Checked = false;
            }
        }

        private void btn_show_Click(object sender, EventArgs e)
        {
            da = new SqlDataAdapter("SELECT * FROM skill_details WHERE Id = "+txt_sid.Text+"",conn);
            dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                string[] sk_disp = dt.Rows[0][1].ToString().Split(',');
                for (int i = 0; i < sk_disp.Length; i++)
                {
                    foreach (CheckBox c in itm)
                    {
                        if (c.Text == sk_disp[i])
                        {
                            c.Checked = true;
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("No Data Found!");
            }
        }

        private void txt_sid_TextChanged(object sender, EventArgs e)
        {
            foreach(CheckBox c in itm)
            {
                c.Checked = false;
            }
        }
    }
}
