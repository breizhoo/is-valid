using System;
using System.Windows.Forms;
using Microsoft.Practices.ServiceLocation;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void launch_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            var instance = ServiceLocator.Current.GetInstance<SearchConfig>();
            dataGridView1.DataSource = instance;
            instance.Launch(txtDirectorySearch.Text);

        }

        private void SetConnectionString(string text)
        {
            if (this.InvokeRequired)
            {
                Action<string> setConnectionStringCallback = SetConnectionString;
                Invoke(setConnectionStringCallback, new object[] {text});
            }
            else
            {
                listView1.Items.Add(text);
            }

        }

        public void FindConnectionString(string name, string providerName, string connectionString)
        {
            SetConnectionString(connectionString);
        }
    }
}
