using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Catalogus.Tabs
{
    public partial class Wijzig_Boek : UserControl
    {
        private Data.Boek boek;
        public Wijzig_Boek(Data.Boek boek)
        {
            InitializeComponent();
            this.boek = boek;
        }

        private void fillControls()
        {
            txtTitel.Text = boek.Titel;
            txtSubtitel.Text = boek.Subtitel;
            txtAuteur.Text = boek.Auteur;
            txtDeel.Text = boek.Deel.ToString();
        }

        private void Wijzig_Boek_Load(object sender, EventArgs e)
        {
            fillControls();
        }
    }
}
