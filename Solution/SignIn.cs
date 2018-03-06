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
using System.Data.SqlTypes;

namespace WindowsFormsApplication2
{
    public partial class SignIn : Form
    {
        public SignIn()
        {
            InitializeComponent();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            String connectionString = @"Data Source=(LocalDB)\ProjectsV12;Initial Catalog=FullerTV;Integrated Security=true";
            String commandString = "EXEC [dbo].[spLoginMember] @Email,@Password";

            using (var conn = new SqlConnection(connectionString))
            {
                try
                {
                    try
                    {
                        conn.Open();

                        string txtEmail = tbEmail.Text;
                        string txtPass = tbPass.Text;

                        if (string.IsNullOrWhiteSpace(txtEmail))
                            MessageBox.Show("Email required");
                        else if (string.IsNullOrWhiteSpace(txtPass))
                            MessageBox.Show("Password required");

                        using (var cmd = new SqlCommand(commandString, conn))
                        {
                            var param1 = new SqlParameter();
                            param1.ParameterName = "@Email";
                            param1.SqlDbType = SqlDbType.VarChar;
                            param1.Direction = ParameterDirection.Input;
                            param1.Value = txtEmail;

                            var param2 = new SqlParameter();
                            param2.ParameterName = "@Password";
                            param2.SqlDbType = SqlDbType.VarChar;
                            param2.Direction = ParameterDirection.Input;
                            param2.Value = txtPass;

                            cmd.Parameters.Add(param1);
                            cmd.Parameters.Add(param2);

                            cmd.ExecuteNonQuery();

                            //MessageBox.Show("Successful Login");

                            Globals.email = txtEmail;

                            this.Hide();
                            var login = new SetAccountForm();
                            login.FormClosed += (s, args) => this.Close();
                            login.Show();
                        }
                    }

                    catch(SqlException)
                    {
                        MessageBox.Show("Invalid Email or Password");
                    }
                }

                catch (Exception ee)
                {
                    MessageBox.Show(ee.ToString());
                    ClearTextBoxes();
                }
            }
        }

        private void ClearTextBoxes()
        {
            Action<Control.ControlCollection> func = null;

            func = (controls) =>
            {
                foreach (Control control in controls)
                    if (control is TextBox)
                        (control as TextBox).Clear();
                    else
                        func(control.Controls);
            };

            func(Controls);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            var main = new MainForm();
            main.FormClosed += (s, args) => this.Close();
            main.Show();
        }
    }
}
