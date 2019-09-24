using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;

namespace Catalogus.Tabs
{
    public partial class BoekenLijst : UserControl
    {
        public BoekenLijst()
        {
            InitializeComponent();
            dataGridView1.SelectionChanged += DataGridView1_SelectionChanged;
        }

        private void DataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            Data.Catalogus.resetBoekenSelectie();
            foreach(DataGridViewRow row in dataGridView1.SelectedRows)
            {
                Data.Boek boekSelected = (Data.Boek)row.DataBoundItem;
                Data.Catalogus.addBoekenSelectie(boekSelected);
            }
        }

        private void BoekenLijst_Load(object sender, EventArgs e)
        {
            fillDataGrid();
            fillSearchBox();
            comboBox1.SelectedIndex = 0;
        }

        private void fillSearchBox()
        {
            foreach (PropertyInfo p in typeof(Data.Boek).GetProperties())
            {
                comboBox1.Items.Add(p.Name);
            }
        }

        private void fillDataGrid()
        {
            BindingSource source = new BindingSource();
            source.DataSource = Data.Catalogus.getBoeken();
            dataGridView1.DataSource = source;
            dataGridView1.AutoGenerateColumns = true;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(textBox1.Text))
            {
                BindingSource source = new BindingSource();
                source.DataSource = Data.Catalogus.getBoekenContains(comboBox1.Text, textBox1.Text);
                dataGridView1.DataSource = source;
            }
            else
            {
                fillDataGrid();
            }
        }
    }
}
