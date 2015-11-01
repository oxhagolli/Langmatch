using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Resources;
using System.Globalization;

namespace Langmatch_for_Windows_Desktop
{
    public partial class Form1 : Form
    {
        ResourceSet rs;
        ArrayList strs;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            strs = new ArrayList();

            rs = Properties.Resources.ResourceManager.GetResourceSet(CultureInfo.CurrentUICulture, true, true);
            foreach (DictionaryEntry entry in rs)
            {
                if (entry.Key.ToString().Contains("_"))
                    continue;
                strs.Add(entry.Key);
            }
            strs.Sort();
            foreach(string s in strs)
                comboBox1.Items.Add(s);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (DictionaryEntry entry in rs)
            {
                if ((string)entry.Key == comboBox1.Text)
                    pictureBox2.Image = (Image)entry.Value;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if( !strs.Contains(comboBox1.Text) )
            {
                MessageBox.Show("Please pick a supported language to learn.");
                return;
            }
            if ( !comboBox2.Items.Contains(comboBox2.Text) )
            {
                MessageBox.Show("Please pick a valid game mode.");
                return;
            }
            if (!comboBox3.Items.Contains(comboBox3.Text))
            {
                MessageBox.Show("Please pick a valid topic.");
                return;
            }
            Game frm = new Game(comboBox1.Text, comboBox2.Text, comboBox3.Text);
            frm.Show();
            this.Close();
        }
    }
}
