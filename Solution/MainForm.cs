using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.SqlTypes;

namespace WindowsFormsApplication2
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            this.dtpDOB.MaxDate = DateTime.Today;
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            
            if (tbEmail.Text == tbConfirmEmail.Text)
            {
                Globals.email = tbEmail.Text;

                if (tbPass.Text == tbConfirmPass.Text)
                {

                    if (CheckEntries() == 0)
                    {

                        String connectionString = @"Data Source=(LocalDB)\ProjectsV12;Initial Catalog=FullerTV;Integrated Security=true";
                        String commandString = "EXEC [dbo].[spInsertNewMember] @FirstName,@LastName,@Email,@Password,@Address,@City,@State,@ZipCode,@DOB,@Sex";

                        using (var conn = new SqlConnection(connectionString))
                        {
                            try
                            {
                                conn.Open();

                                string txtSex;
                                string txtAddress = tbAddress.Text;
                                string txtCity = tbCity.Text;
                                string txtState = cbState.Text;
                                string txtZipCode = tbZipCode.Text;
                                var txtDOB = dtpDOB.Value;
                                string txtEmail = tbEmail.Text;
                                string txtPass = tbPass.Text;
                                string txtFirstName = tbFirstName.Text;
                                string txtLastName = tbLastName.Text;
                                if (rbMale.Checked)
                                    txtSex = "M";
                                else
                                    txtSex = "F";

                                using (var cmd = new SqlCommand(commandString, conn))
                                {
                                    var param1 = new SqlParameter();
                                    param1.ParameterName = "@FirstName";
                                    param1.SqlDbType = SqlDbType.VarChar;
                                    param1.Direction = ParameterDirection.Input;
                                    param1.Value = txtFirstName;

                                    var param2 = new SqlParameter();
                                    param2.ParameterName = "@LastName";
                                    param2.SqlDbType = SqlDbType.VarChar;
                                    param2.Direction = ParameterDirection.Input;
                                    param2.Value = txtLastName;

                                    var param3 = new SqlParameter();
                                    param3.ParameterName = "@Email";
                                    param3.SqlDbType = SqlDbType.VarChar;
                                    param3.Direction = ParameterDirection.Input;
                                    param3.Value = txtEmail;

                                    var param4 = new SqlParameter();
                                    param4.ParameterName = "@Password";
                                    param4.SqlDbType = SqlDbType.VarChar;
                                    param4.Direction = ParameterDirection.Input;
                                    param4.Value = txtPass;

                                    var param5 = new SqlParameter();
                                    param5.ParameterName = "@Address";
                                    param5.SqlDbType = SqlDbType.VarChar;
                                    param5.Direction = ParameterDirection.Input;
                                    param5.Value = txtAddress;

                                    var param6 = new SqlParameter();
                                    param6.ParameterName = "@City";
                                    param6.SqlDbType = SqlDbType.VarChar;
                                    param6.Direction = ParameterDirection.Input;
                                    param6.Value = txtCity;

                                    var param7 = new SqlParameter();
                                    param7.ParameterName = "@State";
                                    param7.SqlDbType = SqlDbType.VarChar;
                                    param7.Direction = ParameterDirection.Input;
                                    param7.Value = txtState;

                                    var param8 = new SqlParameter();
                                    param8.ParameterName = "@ZipCode";
                                    param8.SqlDbType = SqlDbType.VarChar;
                                    param8.Direction = ParameterDirection.Input;
                                    param8.Value = txtZipCode;

                                    var param9 = new SqlParameter();
                                    param9.ParameterName = "@DOB";
                                    param9.SqlDbType = SqlDbType.Date;
                                    param9.Direction = ParameterDirection.Input;
                                    param9.Value = txtDOB;

                                    var param10 = new SqlParameter();
                                    param10.ParameterName = "@Sex";
                                    param10.SqlDbType = SqlDbType.VarChar;
                                    param10.Direction = ParameterDirection.Input;
                                    param10.Value = txtSex;

                                    cmd.Parameters.Add(param1);
                                    cmd.Parameters.Add(param2);
                                    cmd.Parameters.Add(param3);
                                    cmd.Parameters.Add(param4);
                                    cmd.Parameters.Add(param5);
                                    cmd.Parameters.Add(param6);
                                    cmd.Parameters.Add(param7);
                                    cmd.Parameters.Add(param8);
                                    cmd.Parameters.Add(param9);
                                    cmd.Parameters.Add(param10);

                                    cmd.ExecuteNonQuery();

                                    MessageBox.Show(string.Format("{0} {1} added successfully :)", txtFirstName, txtLastName, MessageBoxButtons.OK));
                                    ClearTextBoxes();

                                    this.Hide();
                                    var setact = new SetAccountForm();
                                    setact.FormClosed += (s, args) => this.Close();
                                    setact.Show();
                                }
                            }

                            catch (Exception ee)
                            {
                                string str = ee.ToString();
                                if (str.Contains("email already exists"))
                                {
                                    MessageBox.Show("Error: Member with this email already exists!");
                                }
                                else
                                    MessageBox.Show(str);
                                ClearTextBoxes();
                            }

                        }
                    }

                }

                else
                    MessageBox.Show("Passwords do not match!");
            }

            else
                MessageBox.Show("Emails do not match!"); 
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
            cbState.SelectedIndex = -1;
        }       

        private int CheckEntries()
        {
            int i = 0;

            if (tbFirstName.Text.Any(c => char.IsDigit(c)))
            {
                MessageBox.Show("Invalid name: No digits");
                i++;
            }
            else if (tbLastName.Text.Any(c => char.IsDigit(c)))
            {
                MessageBox.Show("Invalid name: No digits");
                i++;
            }
            else if (tbCity.Text.Any(c => char.IsDigit(c)))
            {
                MessageBox.Show("Invalid city: No digits");
                i++;
            }
            else if (!tbEmail.Text.Contains("@"))
            {
                MessageBox.Show("Invalid email address: Must contain @");
                i++;
            }
            else if (tbZipCode.Text.Any(char.IsLetter))
            {
                MessageBox.Show("Invalid Zip Code: No letters");
                i++;
            }
            else if (GetAge(dtpDOB.Value) < 18)
            {
                MessageBox.Show("Must be older than 18 to sign up for Fuller TV");
                i++;
            }
            else if (cbState.SelectedIndex < 0)
            {
                MessageBox.Show("Please select a State");
                i++;
            }
            else
            {
                if (string.IsNullOrWhiteSpace(tbFirstName.Text))
                {
                    MessageBox.Show("First Name required");
                    i++;
                }
                else if (string.IsNullOrWhiteSpace(tbLastName.Text))
                {
                    MessageBox.Show("Last Name required");
                    i++;
                }
                else if (string.IsNullOrWhiteSpace(tbEmail.Text))
                {
                    MessageBox.Show("Email required");
                    i++;
                }
                else if (string.IsNullOrWhiteSpace(tbPass.Text))
                {
                    MessageBox.Show("Password required");
                    i++;
                }
                else if (string.IsNullOrWhiteSpace(tbAddress.Text))
                {
                    MessageBox.Show("Address required");
                    i++;
                }
                else if (string.IsNullOrWhiteSpace(tbCity.Text))
                {
                    MessageBox.Show("City required");
                    i++;
                }
                else if (string.IsNullOrWhiteSpace(tbZipCode.Text))
                {
                    MessageBox.Show("Zip Code required");
                    i++;
                }
                else if (!Controls.OfType<RadioButton>().Any(rb => rb.Checked))
                {
                    MessageBox.Show("Select Male or Female");
                    i++;
                }
            }

            return i;
        }

        private int GetAge(DateTime dob)
        {
            DateTime today = DateTime.Today;
            int age = today.Year - dob.Year;
            if (dob > today.AddYears(-age))
                age--;

            return age;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            var signin = new SignIn();
            signin.FormClosed += (s, args) => this.Close();
            signin.Show();
        }
    }
}
