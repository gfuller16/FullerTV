using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlTypes;
using System.Data.SqlClient;

namespace WindowsFormsApplication2
{
    public partial class SetAccountForm : Form
    {
        public SetAccountForm()
        {
            InitializeComponent();

            var con1 = new SqlConnection(Globals.connStr);

            StationList[] stations = null;
            string commandString1 = @"SELECT [stShortName], [stPrice] FROM [tblStation] WHERE [st_pkgID] = 0 ORDER BY [stShortName]";

            using (var cmd = new SqlCommand(commandString1, con1))
            {
                con1.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    var list = new List<StationList>();
                    while (reader.Read())
                        list.Add(new StationList { shortName = reader.GetString(0), Price = reader.GetDecimal(1) });
                    stations = list.ToArray();
                }
            }

            for (int i = 0; stations.Length > i; i++)
            {
                lbAvailable.Items.Add(stations[i].shortName + " - $" + stations[i].Price.ToString("0.##"));
            }
        }

        private void btnSelectOne_Click(object sender, EventArgs e)
        {
            try
            {
                decimal ttl = Convert.ToDecimal(lblTotalValue.Text);
                string str = lbAvailable.SelectedItem.ToString();
                int startindex = str.IndexOf("$") + 1;
                int length = str.Length - startindex;
                string value = str.Substring(startindex, length);
                decimal amt = Convert.ToDecimal(value);
                ttl += amt;
                lblTotalValue.Text = ttl.ToString();

                lbSelected.Items.Add(lbAvailable.SelectedItem.ToString());
                lbAvailable.SelectedIndex++;
                lbAvailable.Items.RemoveAt(lbAvailable.SelectedIndex - 1);
            }
            catch
            {
                try
                {
                    lbAvailable.SelectedIndex--;
                    lbAvailable.Items.RemoveAt(lbAvailable.SelectedIndex + 1);
                }

                catch
                {
                    MessageBox.Show("Error: No station selected");
                }
            }


        }

        private void btnReturnOne_Click(object sender, EventArgs e)
        {
            try
            {
                decimal ttl = Convert.ToDecimal(lblTotalValue.Text);
                string str = lbSelected.SelectedItem.ToString();
                int startindex = str.IndexOf("$") + 1;
                int length = str.Length - startindex;
                string value = str.Substring(startindex, length);
                decimal amt = Convert.ToDecimal(value);
                ttl -= amt;
                lblTotalValue.Text = ttl.ToString();

                lbAvailable.Items.Add(lbSelected.SelectedItem.ToString());
                lbSelected.SelectedIndex++;
                lbSelected.Items.RemoveAt(lbSelected.SelectedIndex - 1);
            }
            catch
            {
                try
                {
                    lbSelected.SelectedIndex--;
                    lbSelected.Items.RemoveAt(lbSelected.SelectedIndex + 1);
                }

                catch
                {
                    MessageBox.Show("Error: No station selected");
                }
            }
        }

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            try
            {
                int s = lbAvailable.Items.Count;

                lbAvailable.SelectedIndex = 0;

                for (int x = 0; x <= s; x++)
                {
                    decimal ttl = Convert.ToDecimal(lblTotalValue.Text);
                    string str = lbAvailable.SelectedItem.ToString();
                    int startindex = str.IndexOf("$") + 1;
                    int length = str.Length - startindex;
                    string value = str.Substring(startindex, length);
                    decimal amt = Convert.ToDecimal(value);
                    ttl += amt;
                    lblTotalValue.Text = ttl.ToString();

                    lbSelected.Items.Add(lbAvailable.SelectedItem.ToString());
                    lbAvailable.SelectedIndex++;
                    lbAvailable.Items.RemoveAt(lbAvailable.SelectedIndex - 1);
                }
            }

            catch
            {
                lbAvailable.Items.RemoveAt(lbAvailable.SelectedIndex);
            }

        }

        private void btnReturnAll_Click(object sender, EventArgs e)
        {
            try
            {
                int s = lbSelected.Items.Count;

                lbSelected.SelectedIndex = 0;

                for (int x = 0; x <= s; x++)
                {
                    decimal ttl = Convert.ToDecimal(lblTotalValue.Text);
                    string str = lbSelected.SelectedItem.ToString();
                    int startindex = str.IndexOf("$") + 1;
                    int length = str.Length - startindex;
                    string value = str.Substring(startindex, length);
                    decimal amt = Convert.ToDecimal(value);
                    ttl -= amt;
                    lblTotalValue.Text = ttl.ToString();

                    lbAvailable.Items.Add(lbSelected.SelectedItem.ToString());
                    lbSelected.SelectedIndex++;
                    lbSelected.Items.RemoveAt(lbSelected.SelectedIndex - 1);
                }
            }

            catch
            {
                lbSelected.Items.RemoveAt(lbSelected.SelectedIndex);
            }
        }

        private void btnContinue_Click(object sender, EventArgs e)
        {
            Globals.actPrice = Convert.ToDecimal(lblTotalValue.Text);

            var channels = new List<string>();

            lbSelected.SelectedIndex = 0;

            for (int x = 0; x < lbSelected.Items.Count; x++)
            {
                string str = lbSelected.SelectedItem.ToString();
                int endindex = str.IndexOf(" - ");
                string value = str.Substring(0, endindex);
                try
                {
                    lbSelected.SelectedIndex++;
                    channels.Add(value);
                }
                catch
                {
                    channels.Add(value);
                }
            }

            String connectionString = @"Data Source=(LocalDB)\ProjectsV12;Initial Catalog=FullerTV;Integrated Security=true";
            String cmdString = "EXEC [dbo].[spSetUpAccount] @Email,@ChannelName";

            using (var conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    for (int x = 0; x < channels.Count; x++)
                    {
                        using (var cmd = new SqlCommand(cmdString,conn))
                        {
                            var param1 = new SqlParameter();
                            param1.ParameterName = "@Email";
                            param1.SqlDbType = SqlDbType.VarChar;
                            param1.Direction = ParameterDirection.Input;
                            param1.Value = Globals.email;

                            var param2 = new SqlParameter();
                            param2.ParameterName = "@ChannelName";
                            param2.SqlDbType = SqlDbType.VarChar;
                            param2.Direction = ParameterDirection.Input;
                            param2.Value = channels[x];

                            cmd.Parameters.Add(param1);
                            cmd.Parameters.Add(param2);

                            cmd.ExecuteNonQuery();
                        }
                    }

                    MessageBox.Show("Transaction Completed");
                }

                catch(Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            var signin = new SignIn();
            signin.FormClosed += (s, args) => this.Close();
            signin.Show();
        }

        private void llSports_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var conn = new SqlConnection(Globals.connStr);

            StationList[] stations = null;
            string cmdString = @"SELECT [stShortName] FROM [tblStation] WHERE [st_pkgID] = 1 ORDER BY [stShortName]";

            using(var cmd = new SqlCommand(cmdString,conn))
            {
                conn.Open();

                using(var reader = cmd.ExecuteReader())
                {
                    var list = new List<StationList>();
                    while (reader.Read())
                        list.Add(new StationList { shortName = reader.GetString(0) });
                    stations = list.ToArray();
                }
            }

            ShowPackage(stations);         
        }

        private void llDIY_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var conn = new SqlConnection(Globals.connStr);

            StationList[] stations = null;
            string cmdString = @"SELECT [stShortName] FROM [tblStation] WHERE [st_pkgID] = 2 ORDER BY [stShortName]";

            using (var cmd = new SqlCommand(cmdString, conn))
            {
                conn.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    var list = new List<StationList>();
                    while (reader.Read())
                        list.Add(new StationList { shortName = reader.GetString(0) });
                    stations = list.ToArray();
                }
            }

            ShowPackage(stations);
        }

        private void llNews_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var conn = new SqlConnection(Globals.connStr);

            StationList[] stations = null;
            string cmdString = @"SELECT [stShortName] FROM [tblStation] WHERE [st_pkgID] = 3 ORDER BY [stShortName]";

            using (var cmd = new SqlCommand(cmdString, conn))
            {
                conn.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    var list = new List<StationList>();
                    while (reader.Read())
                        list.Add(new StationList { shortName = reader.GetString(0) });
                    stations = list.ToArray();
                }
            }

            ShowPackage(stations);
        }

        private void llMovie_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var conn = new SqlConnection(Globals.connStr);

            StationList[] stations = null;
            string cmdString = @"SELECT [stShortName] FROM [tblStation] WHERE [st_pkgID] = 4 ORDER BY [stShortName]";

            using (var cmd = new SqlCommand(cmdString, conn))
            {
                conn.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    var list = new List<StationList>();
                    while (reader.Read())
                        list.Add(new StationList { shortName = reader.GetString(0) });
                    stations = list.ToArray();
                }
            }

            ShowPackage(stations);
        }

        private void llAllAround_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var conn = new SqlConnection(Globals.connStr);

            StationList[] stations = null;
            string cmdString = @"SELECT [stShortName] FROM [tblStation] WHERE [st_pkgID] = 5 ORDER BY [stShortName]";

            using (var cmd = new SqlCommand(cmdString, conn))
            {
                conn.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    var list = new List<StationList>();
                    while (reader.Read())
                        list.Add(new StationList { shortName = reader.GetString(0) });
                    stations = list.ToArray();
                }
            }

            ShowPackage(stations);
        }

        private void rbSports_CheckedChanged(object sender, EventArgs e)
        {
            lbAvailable.Items.Clear();

            var conn = new SqlConnection(Globals.connStr);

            StationList[] stations0 = null;
            string commandString0 = @"SELECT [stShortName] FROM [tblStation] WHERE [stNumber] IN"
                + "(SELECT [stNumber] FROM [tblStation] WHERE [st_pkgID] = 1) AND [stPrice] IS NOT NULL "
                + "ORDER BY [stShortName]";

            using (var cmd = new SqlCommand(commandString0, conn))
            {
                conn.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    var list = new List<StationList>();
                    while (reader.Read())
                        list.Add(new StationList { shortName = reader.GetString(0) });
                    stations0 = list.ToArray();
                }
            }

            try
            {
                lbSelected.SelectedIndex = 0;
            }
            catch
            {
                lbSelected.SelectedIndex = -1;
            }

            for (int i = 0; i < lbSelected.Items.Count; i++)
            {
                lbSelected.SelectedIndex = i;

                for (int x = 0; x < stations0.Length; x++)
                {
                    if (lbSelected.SelectedItem.ToString().Contains(stations0[x].shortName))
                    {
                        lbSelected.Items.Remove(lbSelected.SelectedItem);
                        if (lbSelected.Items.Count > 0)
                        {
                            i = 0;
                            lbSelected.SelectedIndex = 0;
                        }
                        else
                        {
                            i = -1;
                            lbSelected.SelectedIndex = -1;
                        }
                        x = -1;
                    }
                }
            }

            /******************************Remove Package Stations to Available Stations******************************/
            var con1 = new SqlConnection(@"Data Source=(LocalDB)\ProjectsV12;Initial Catalog=FullerTV;Integrated Security=true");

            StationList[] stations1 = null;
            string commandString1 = @"SELECT [stShortName],[stPrice] FROM [tblStation] WHERE [stNumber] NOT IN"
                + "(SELECT [stNumber] FROM [tblStation] WHERE [st_pkgID] = 1) AND [stPrice] IS NOT NULL "
                + "ORDER BY [stShortName]";

            using (var cmd = new SqlCommand(commandString1, con1))
            {
                con1.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    var list = new List<StationList>();
                    while (reader.Read())
                        list.Add(new StationList { shortName = reader.GetString(0), Price = reader.GetDecimal(1) });
                    stations1 = list.ToArray();
                }
            }

            for (int i = 0; stations1.Length > i; i++)
            {
                lbAvailable.Items.Add(stations1[i].shortName + " - $" + stations1[i].Price.ToString("0.##"));
            }

            if (rbSports.Checked == true)
            {
                decimal ttl;

                try
                {
                    lbSelected.SelectedIndex = 0;
                    ttl = 0;
                }
                catch
                {
                    ttl = 0;
                }

                for (int i = 0; i < (lbSelected.Items.Count); i++)
                {
                    try
                    {
                        lbSelected.SelectedIndex = i;

                        string item = lbSelected.SelectedItem.ToString();
                        int startindex = item.IndexOf("$") + 1;
                        int length = item.Length - startindex;
                        string value = item.Substring(startindex, length);
                        decimal amt = Convert.ToDecimal(value);
                        ttl += amt;
                    }
                    catch
                    {
                        string item = lbSelected.SelectedItem.ToString();
                        int startindex = item.IndexOf("$") + 1;
                        int length = item.Length - startindex;
                        string value = item.Substring(startindex, length);
                        decimal amt = Convert.ToDecimal(value);
                        ttl += amt;
                    }
                }

                var con2 = new SqlConnection(@"Data Source=(LocalDB)\ProjectsV12;Initial Catalog=FullerTV;Integrated Security=true");

                StationList[] stations2 = null;
                string cmdString = @"SELECT SUM(p.[pkgPrice]) FROM [tblStation] s JOIN [tblPackage] p ON p.[pkgID] = s.[st_pkgID] WHERE [st_pkgID] = 1";

                using (var cmd = new SqlCommand(cmdString, con2))
                {
                    con2.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        var list = new List<StationList>();
                        while (reader.Read())
                            list.Add(new StationList { Price = reader.GetDecimal(0) });
                        stations2 = list.ToArray();
                    }

                    ttl += stations2[0].Price;

                    lblTotalValue.Text = ttl.ToString("0.##");
                }

                try
                {
                    lbSelected.SelectedIndex = 0;
                    ttl = 0;
                }
                catch
                {
                    ttl = 0;
                }

                for (int i = 0; i < (lbSelected.Items.Count); i++)
                {
                    try
                    {
                        lbSelected.SelectedIndex = i;

                        string item = lbSelected.SelectedItem.ToString();
                        int startindex = item.IndexOf("$") + 1;
                        int length = item.Length - startindex;
                        string value = item.Substring(startindex, length);
                        decimal amt = Convert.ToDecimal(value);
                        ttl += amt;
                    }
                    catch
                    {
                        string item = lbSelected.SelectedItem.ToString();
                        int startindex = item.IndexOf("$") + 1;
                        int length = item.Length - startindex;
                        string value = item.Substring(startindex, length);
                        decimal amt = Convert.ToDecimal(value);
                        ttl += amt;
                    }
                }
            }

            lbSelected.SelectedIndex = -1;
        }

        private void rbDIY_CheckedChanged(object sender, EventArgs e)
        {
            lbAvailable.Items.Clear();

            var conn = new SqlConnection(Globals.connStr);

            StationList[] stations0 = null;
            string commandString0 = @"SELECT [stShortName] FROM [tblStation] WHERE [stNumber] IN"
                + "(SELECT [stNumber] FROM [tblStation] WHERE [st_pkgID] = 2) AND [stPrice] IS NOT NULL "
                + "ORDER BY [stShortName]";

            using (var cmd = new SqlCommand(commandString0, conn))
            {
                conn.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    var list = new List<StationList>();
                    while (reader.Read())
                        list.Add(new StationList { shortName = reader.GetString(0) });
                    stations0 = list.ToArray();
                }
            }

            try
            {
                lbSelected.SelectedIndex = 0;
            }
            catch
            {
                lbSelected.SelectedIndex = -1;
            }

            for (int i = 0; i < lbSelected.Items.Count; i++)
            {
                lbSelected.SelectedIndex = i;

                for (int x = 0; x < stations0.Length; x++)
                {
                    if (lbSelected.SelectedItem.ToString().Contains(stations0[x].shortName))
                    {
                        lbSelected.Items.Remove(lbSelected.SelectedItem);
                        if (lbSelected.Items.Count > 0)
                        {
                            i = 0;
                            lbSelected.SelectedIndex = 0;
                        }
                        else
                        {
                            i = -1;
                            lbSelected.SelectedIndex = -1;
                        }
                        x = -1;
                    }
                }
            }

            var con1 = new SqlConnection(@"Data Source=(LocalDB)\ProjectsV12;Initial Catalog=FullerTV;Integrated Security=true");

            StationList[] stations1 = null;
            string commandString1 = @"SELECT [stShortName],[stPrice] FROM [tblStation] WHERE [stNumber] NOT IN"
                + "(SELECT [stNumber] FROM [tblStation] WHERE [st_pkgID] = 2) AND [stPrice] IS NOT NULL "
                + "ORDER BY [stShortName]";

            using (var cmd = new SqlCommand(commandString1, con1))
            {
                con1.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    var list = new List<StationList>();
                    while (reader.Read())
                        list.Add(new StationList { shortName = reader.GetString(0), Price = reader.GetDecimal(1) });
                    stations1 = list.ToArray();
                }
            }

            for(int i = 0; stations1.Length > i; i++)
            {
                lbAvailable.Items.Add(stations1[i].shortName + " - $" + stations1[i].Price.ToString("0.##"));
            }

            if (rbDIY.Checked == true)
            {
                decimal ttl;

                try
                {
                    lbSelected.SelectedIndex = 0;
                    ttl = 0;
                }
                catch
                {
                    ttl = 0;
                }

                for (int i = 0; i < (lbSelected.Items.Count); i++)
                {
                    try
                    {
                        lbSelected.SelectedIndex = i;

                        string item = lbSelected.SelectedItem.ToString();
                        int startindex = item.IndexOf("$") + 1;
                        int length = item.Length - startindex;
                        string value = item.Substring(startindex, length);
                        decimal amt = Convert.ToDecimal(value);
                        ttl += amt;
                    }
                    catch
                    {
                        string item = lbSelected.SelectedItem.ToString();
                        int startindex = item.IndexOf("$") + 1;
                        int length = item.Length - startindex;
                        string value = item.Substring(startindex, length);
                        decimal amt = Convert.ToDecimal(value);
                        ttl += amt;
                    }
                }

                var con2 = new SqlConnection(@"Data Source=(LocalDB)\ProjectsV12;Initial Catalog=FullerTV;Integrated Security=true");

                StationList[] stations2 = null;
                string cmdString = @"SELECT SUM(p.[pkgPrice]) FROM [tblStation] s JOIN [tblPackage] p ON p.[pkgID] = s.[st_pkgID] WHERE [st_pkgID] = 2";

                using (var cmd = new SqlCommand(cmdString, con2))
                {
                    con2.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        var list = new List<StationList>();
                        while (reader.Read())
                            list.Add(new StationList { Price = reader.GetDecimal(0) });
                        stations2 = list.ToArray();
                    }

                    ttl += stations2[0].Price;

                    lblTotalValue.Text = ttl.ToString("0.##");
                }

                try
                {
                    lbSelected.SelectedIndex = 0;
                    ttl = 0;
                }
                catch
                {
                    ttl = 0;
                }

                for (int i = 0; i < (lbSelected.Items.Count); i++)
                {
                    try
                    {
                        lbSelected.SelectedIndex = i;

                        string item = lbSelected.SelectedItem.ToString();
                        int startindex = item.IndexOf("$") + 1;
                        int length = item.Length - startindex;
                        string value = item.Substring(startindex, length);
                        decimal amt = Convert.ToDecimal(value);
                        ttl += amt;
                    }
                    catch
                    {
                        string item = lbSelected.SelectedItem.ToString();
                        int startindex = item.IndexOf("$") + 1;
                        int length = item.Length - startindex;
                        string value = item.Substring(startindex, length);
                        decimal amt = Convert.ToDecimal(value);
                        ttl += amt;
                    }
                }
            }

            lbSelected.SelectedIndex = -1;
        }

        private void rbNews_CheckedChanged(object sender, EventArgs e)
        {
            lbAvailable.Items.Clear();

            var conn = new SqlConnection(Globals.connStr);

            StationList[] stations0 = null;
            string commandString0 = @"SELECT [stShortName] FROM [tblStation] WHERE [stNumber] IN"
                + "(SELECT [stNumber] FROM [tblStation] WHERE [st_pkgID] = 3) AND [stPrice] IS NOT NULL "
                + "ORDER BY [stShortName]";

            using (var cmd = new SqlCommand(commandString0, conn))
            {
                conn.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    var list = new List<StationList>();
                    while (reader.Read())
                        list.Add(new StationList { shortName = reader.GetString(0) });
                    stations0 = list.ToArray();
                }
            }

            try
            {
                lbSelected.SelectedIndex = 0;
            }
            catch
            {
                lbSelected.SelectedIndex = -1;
            }

            for (int i = 0; i < lbSelected.Items.Count; i++)
            {
                lbSelected.SelectedIndex = i;

                for (int x = 0; x < stations0.Length; x++)
                {
                    if (lbSelected.SelectedItem.ToString().Contains(stations0[x].shortName))
                    {
                        lbSelected.Items.Remove(lbSelected.SelectedItem);
                        if (lbSelected.Items.Count > 0)
                        {
                            i = 0;
                            lbSelected.SelectedIndex = 0;
                        }
                        else
                        {
                            i = -1;
                            lbSelected.SelectedIndex = -1;
                        }
                        x = -1;
                    }
                }
            }

            var con1 = new SqlConnection(@"Data Source=(LocalDB)\ProjectsV12;Initial Catalog=FullerTV;Integrated Security=true");

            StationList[] stations1 = null;
            string commandString1 = @"SELECT [stShortName],[stPrice] FROM [tblStation] WHERE [stNumber] NOT IN"
                + "(SELECT [stNumber] FROM [tblStation] WHERE [st_pkgID] = 3) AND [stPrice] IS NOT NULL "
                + "ORDER BY [stShortName]";

            using (var cmd = new SqlCommand(commandString1, con1))
            {
                con1.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    var list = new List<StationList>();
                    while (reader.Read())
                        list.Add(new StationList { shortName = reader.GetString(0), Price = reader.GetDecimal(1) });
                    stations1 = list.ToArray();
                }
            }

            for (int i = 0; stations1.Length > i; i++)
            {
                lbAvailable.Items.Add(stations1[i].shortName + " - $" + stations1[i].Price.ToString("0.##"));
            }

            if (rbNews.Checked == true)
            {
                decimal ttl;

                try
                {
                    lbSelected.SelectedIndex = 0;
                    ttl = 0;
                }
                catch
                {
                    ttl = 0;
                }

                for (int i = 0; i < (lbSelected.Items.Count); i++)
                {
                    try
                    {
                        lbSelected.SelectedIndex = i;

                        string item = lbSelected.SelectedItem.ToString();
                        int startindex = item.IndexOf("$") + 1;
                        int length = item.Length - startindex;
                        string value = item.Substring(startindex, length);
                        decimal amt = Convert.ToDecimal(value);
                        ttl += amt;
                    }
                    catch
                    {
                        string item = lbSelected.SelectedItem.ToString();
                        int startindex = item.IndexOf("$") + 1;
                        int length = item.Length - startindex;
                        string value = item.Substring(startindex, length);
                        decimal amt = Convert.ToDecimal(value);
                        ttl += amt;
                    }
                }

                var con2 = new SqlConnection(@"Data Source=(LocalDB)\ProjectsV12;Initial Catalog=FullerTV;Integrated Security=true");

                StationList[] stations2 = null;
                string cmdString = @"SELECT SUM(p.[pkgPrice]) FROM [tblStation] s JOIN [tblPackage] p ON p.[pkgID] = s.[st_pkgID] WHERE [st_pkgID] = 3";

                using (var cmd = new SqlCommand(cmdString, con2))
                {
                    con2.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        var list = new List<StationList>();
                        while (reader.Read())
                            list.Add(new StationList { Price = reader.GetDecimal(0) });
                        stations2 = list.ToArray();
                    }

                    ttl += stations2[0].Price;

                    lblTotalValue.Text = ttl.ToString("0.##");
                }

                try
                {
                    lbSelected.SelectedIndex = 0;
                    ttl = 0;
                }
                catch
                {
                    ttl = 0;
                }

                for (int i = 0; i < (lbSelected.Items.Count); i++)
                {
                    try
                    {
                        lbSelected.SelectedIndex = i;

                        string item = lbSelected.SelectedItem.ToString();
                        int startindex = item.IndexOf("$") + 1;
                        int length = item.Length - startindex;
                        string value = item.Substring(startindex, length);
                        decimal amt = Convert.ToDecimal(value);
                        ttl += amt;
                    }
                    catch
                    {
                        string item = lbSelected.SelectedItem.ToString();
                        int startindex = item.IndexOf("$") + 1;
                        int length = item.Length - startindex;
                        string value = item.Substring(startindex, length);
                        decimal amt = Convert.ToDecimal(value);
                        ttl += amt;
                    }
                }
            }

            lbSelected.SelectedIndex = -1;
        }

        private void rbMovie_CheckedChanged(object sender, EventArgs e)
        {
            lbAvailable.Items.Clear();

            var conn = new SqlConnection(Globals.connStr);

            StationList[] stations0 = null;
            string commandString0 = @"SELECT [stShortName] FROM [tblStation] WHERE [stNumber] IN"
                + "(SELECT [stNumber] FROM [tblStation] WHERE [st_pkgID] = 4) AND [stPrice] IS NOT NULL "
                + "ORDER BY [stShortName]";

            using (var cmd = new SqlCommand(commandString0, conn))
            {
                conn.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    var list = new List<StationList>();
                    while (reader.Read())
                        list.Add(new StationList { shortName = reader.GetString(0) });
                    stations0 = list.ToArray();
                }
            }

            try
            {
                lbSelected.SelectedIndex = 0;
            }
            catch
            {
                lbSelected.SelectedIndex = -1;
            }

            for (int i = 0; i < lbSelected.Items.Count; i++)
            {
                lbSelected.SelectedIndex = i;

                for (int x = 0; x < stations0.Length; x++)
                {
                    if (lbSelected.SelectedItem.ToString().Contains(stations0[x].shortName))
                    {
                        lbSelected.Items.Remove(lbSelected.SelectedItem);
                        if (lbSelected.Items.Count > 0)
                        {
                            i = 0;
                            lbSelected.SelectedIndex = 0;
                        }
                        else
                        {
                            i = -1;
                            lbSelected.SelectedIndex = -1;
                        }
                        x = -1;
                    }
                }
            }

            var con1 = new SqlConnection(@"Data Source=(LocalDB)\ProjectsV12;Initial Catalog=FullerTV;Integrated Security=true");

            StationList[] stations1 = null;
            string commandString1 = @"SELECT [stShortName],[stPrice] FROM [tblStation] WHERE [stNumber] NOT IN"
                + "(SELECT [stNumber] FROM [tblStation] WHERE [st_pkgID] = 4) AND [stPrice] IS NOT NULL "
                + "ORDER BY [stShortName]";

            using (var cmd = new SqlCommand(commandString1, con1))
            {
                con1.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    var list = new List<StationList>();
                    while (reader.Read())
                        list.Add(new StationList { shortName = reader.GetString(0), Price = reader.GetDecimal(1) });
                    stations1 = list.ToArray();
                }
            }

            for (int i = 0; stations1.Length > i; i++)
            {
                lbAvailable.Items.Add(stations1[i].shortName + " - $" + stations1[i].Price.ToString("0.##"));
            }

            if (rbMovie.Checked == true)
            {
                decimal ttl;

                try
                {
                    lbSelected.SelectedIndex = 0;
                    ttl = 0;
                }
                catch
                {
                    ttl = 0;
                }

                for (int i = 0; i < (lbSelected.Items.Count); i++)
                {
                    try
                    {
                        lbSelected.SelectedIndex = i;

                        string item = lbSelected.SelectedItem.ToString();
                        int startindex = item.IndexOf("$") + 1;
                        int length = item.Length - startindex;
                        string value = item.Substring(startindex, length);
                        decimal amt = Convert.ToDecimal(value);
                        ttl += amt;
                    }
                    catch
                    {
                        string item = lbSelected.SelectedItem.ToString();
                        int startindex = item.IndexOf("$") + 1;
                        int length = item.Length - startindex;
                        string value = item.Substring(startindex, length);
                        decimal amt = Convert.ToDecimal(value);
                        ttl += amt;
                    }
                }

                var con2 = new SqlConnection(@"Data Source=(LocalDB)\ProjectsV12;Initial Catalog=FullerTV;Integrated Security=true");

                StationList[] stations2 = null;
                string cmdString = @"SELECT SUM(p.[pkgPrice]) FROM [tblStation] s JOIN [tblPackage] p ON p.[pkgID] = s.[st_pkgID] WHERE [st_pkgID] = 4";

                using (var cmd = new SqlCommand(cmdString, con2))
                {
                    con2.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        var list = new List<StationList>();
                        while (reader.Read())
                            list.Add(new StationList { Price = reader.GetDecimal(0) });
                        stations2 = list.ToArray();
                    }

                    ttl += stations2[0].Price;

                    lblTotalValue.Text = ttl.ToString("0.##");
                }

                try
                {
                    lbSelected.SelectedIndex = 0;
                    ttl = 0;
                }
                catch
                {
                    ttl = 0;
                }

                for (int i = 0; i < (lbSelected.Items.Count); i++)
                {
                    try
                    {
                        lbSelected.SelectedIndex = i;

                        string item = lbSelected.SelectedItem.ToString();
                        int startindex = item.IndexOf("$") + 1;
                        int length = item.Length - startindex;
                        string value = item.Substring(startindex, length);
                        decimal amt = Convert.ToDecimal(value);
                        ttl += amt;
                    }
                    catch
                    {
                        string item = lbSelected.SelectedItem.ToString();
                        int startindex = item.IndexOf("$") + 1;
                        int length = item.Length - startindex;
                        string value = item.Substring(startindex, length);
                        decimal amt = Convert.ToDecimal(value);
                        ttl += amt;
                    }
                }
            }

            lbSelected.SelectedIndex = -1;
        }

        private void rbAllAround_CheckedChanged(object sender, EventArgs e)
        {
            lbAvailable.Items.Clear();

            var conn = new SqlConnection(Globals.connStr);

            StationList[] stations0 = null;
            string commandString0 = @"SELECT [stShortName] FROM [tblStation] WHERE [stNumber] IN"
                + "(SELECT [stNumber] FROM [tblStation] WHERE [st_pkgID] = 5) AND [stPrice] IS NOT NULL "
                + "ORDER BY [stShortName]";

            using (var cmd = new SqlCommand(commandString0, conn))
            {
                conn.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    var list = new List<StationList>();
                    while (reader.Read())
                        list.Add(new StationList { shortName = reader.GetString(0) });
                    stations0 = list.ToArray();
                }
            }

            try
            {
                lbSelected.SelectedIndex = 0;
            }
            catch
            {
                lbSelected.SelectedIndex = -1;
            }

            for (int i = 0; i < lbSelected.Items.Count; i++)
            {
                lbSelected.SelectedIndex = i;

                for (int x = 0; x < stations0.Length; x++)
                {
                    if (lbSelected.SelectedItem.ToString().Contains(stations0[x].shortName))
                    {
                        lbSelected.Items.Remove(lbSelected.SelectedItem);
                        if (lbSelected.Items.Count > 0)
                        {
                            i = 0;
                            lbSelected.SelectedIndex = 0;
                        }
                        else
                        {
                            i = -1;
                            lbSelected.SelectedIndex = -1;
                        }
                        x = -1;
                    }
                }
            }

            var con1 = new SqlConnection(@"Data Source=(LocalDB)\ProjectsV12;Initial Catalog=FullerTV;Integrated Security=true");

            StationList[] stations1 = null;
            string commandString1 = @"SELECT [stShortName],[stPrice] FROM [tblStation] WHERE [stNumber] NOT IN"
                + "(SELECT [stNumber] FROM [tblStation] WHERE [st_pkgID] = 5) AND [stPrice] IS NOT NULL "
                + "ORDER BY [stShortName]";

            using (var cmd = new SqlCommand(commandString1, con1))
            {
                con1.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    var list = new List<StationList>();
                    while (reader.Read())
                        list.Add(new StationList { shortName = reader.GetString(0), Price = reader.GetDecimal(1) });
                    stations1 = list.ToArray();
                }
            }

            for (int i = 0; stations1.Length > i; i++)
            {
                lbAvailable.Items.Add(stations1[i].shortName + " - $" + stations1[i].Price.ToString("0.##"));
            }

            if (rbAllAround.Checked == true)
            {
                decimal ttl;

                try
                {
                    lbSelected.SelectedIndex = 0;
                    ttl = 0;
                }
                catch
                {
                    ttl = 0;
                }

                for (int i = 0; i < (lbSelected.Items.Count); i++)
                {
                    try
                    {
                        lbSelected.SelectedIndex = i;

                        string item = lbSelected.SelectedItem.ToString();
                        int startindex = item.IndexOf("$") + 1;
                        int length = item.Length - startindex;
                        string value = item.Substring(startindex, length);
                        decimal amt = Convert.ToDecimal(value);
                        ttl += amt;
                    }
                    catch
                    {
                        string item = lbSelected.SelectedItem.ToString();
                        int startindex = item.IndexOf("$") + 1;
                        int length = item.Length - startindex;
                        string value = item.Substring(startindex, length);
                        decimal amt = Convert.ToDecimal(value);
                        ttl += amt;
                    }
                }

                var con2 = new SqlConnection(@"Data Source=(LocalDB)\ProjectsV12;Initial Catalog=FullerTV;Integrated Security=true");

                StationList[] stations2 = null;
                string cmdString = @"SELECT SUM(p.[pkgPrice]) FROM [tblStation] s JOIN [tblPackage] p ON p.[pkgID] = s.[st_pkgID] WHERE [st_pkgID] = 5";

                using (var cmd = new SqlCommand(cmdString, con2))
                {
                    con2.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        var list = new List<StationList>();
                        while (reader.Read())
                            list.Add(new StationList { Price = reader.GetDecimal(0) });
                        stations2 = list.ToArray();
                    }

                    ttl += stations2[0].Price;

                    lblTotalValue.Text = ttl.ToString("0.##");
                }

                try
                {
                    lbSelected.SelectedIndex = 0;
                    ttl = 0;
                }
                catch
                {
                    ttl = 0;
                }

                for (int i = 0; i < (lbSelected.Items.Count); i++)
                {
                    try
                    {
                        lbSelected.SelectedIndex = i;

                        string item = lbSelected.SelectedItem.ToString();
                        int startindex = item.IndexOf("$") + 1;
                        int length = item.Length - startindex;
                        string value = item.Substring(startindex, length);
                        decimal amt = Convert.ToDecimal(value);
                        ttl += amt;
                    }
                    catch
                    {
                        string item = lbSelected.SelectedItem.ToString();
                        int startindex = item.IndexOf("$") + 1;
                        int length = item.Length - startindex;
                        string value = item.Substring(startindex, length);
                        decimal amt = Convert.ToDecimal(value);
                        ttl += amt;
                    }
                }
            }

            lbSelected.SelectedIndex = -1;
        }

        private void rbNoPackage_CheckedChanged(object sender, EventArgs e)
        {
            lbAvailable.Items.Clear();

            var con1 = new SqlConnection(Globals.connStr);

            StationList[] stations = null;
            string commandString1 = @"SELECT [stShortName], [stPrice] FROM [tblStation] WHERE [st_pkgID] = 0 ORDER BY [stShortName]";

            using (var cmd = new SqlCommand(commandString1, con1))
            {
                con1.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    var list = new List<StationList>();
                    while (reader.Read())
                        list.Add(new StationList { shortName = reader.GetString(0), Price = reader.GetDecimal(1) });
                    stations = list.ToArray();
                }
            }

            for (int i = 0; stations.Length > i; i++)
            {
                lbAvailable.Items.Add(stations[i].shortName + " - $" + stations[i].Price.ToString("0.##"));
            }

            if (rbNoPackage.Checked == true)
            {
                decimal ttl;

                try
                {
                    lbSelected.SelectedIndex = 0;
                    ttl = 0;
                }
                catch
                {
                    ttl = 0;
                }

                for (int i = 0; i < (lbSelected.Items.Count); i++)
                {
                    try
                    {
                        lbSelected.SelectedIndex = i;

                        string item = lbSelected.SelectedItem.ToString();
                        int startindex = item.IndexOf("$") + 1;
                        int length = item.Length - startindex;
                        string value = item.Substring(startindex, length);
                        decimal amt = Convert.ToDecimal(value);
                        ttl += amt;
                    }
                    catch
                    {
                        string item = lbSelected.SelectedItem.ToString();
                        int startindex = item.IndexOf("$") + 1;
                        int length = item.Length - startindex;
                        string value = item.Substring(startindex, length);
                        decimal amt = Convert.ToDecimal(value);
                        ttl += amt;
                    }
                }

                lblTotalValue.Text = ttl.ToString("0.##");
            }

            lbSelected.SelectedIndex = -1;
        }

        private void ShowPackage(StationList[] st)
        {
            string str = "";

            for (int i = 0; st.Length > i; i++)
            {
                if (st.Length == (i + 1))
                    str += st[i].shortName;
                else
                    str += st[i].shortName + ", ";
            }

            MessageBox.Show(str);
        }
    }
}
