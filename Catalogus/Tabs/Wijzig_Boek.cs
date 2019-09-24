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

        private void Button1_Click(object sender, EventArgs e)
        {
            int tempDeel;
            Dictionary<string, string> errorMessages = new Dictionary<string, string>();
            Data.Boek boek;
            if (int.TryParse(txtDeel.Text, out tempDeel))
            {
                boek = new Data.Boek(this.boek.getId(), txtTitel.Text, txtSubtitel.Text, txtAuteur.Text, tempDeel);
            }
            else
            {
                boek = new Data.Boek(this.boek.getId(), txtTitel.Text, txtSubtitel.Text, txtAuteur.Text, 1);
            }

            errorMessages = boek.validate();

            Label[] textCheckers = new Label[] { lblTitelException, lblSubtitelException, lblAuteurException, lblDeelException };

            foreach (Label label in textCheckers)
            {
                label.ResetText();
            }

            if (errorMessages.Count == 0)
            {
                List<Data.Boek> duplicates = Data.Catalogus.getDuplicates(boek);
                if (!showDuplicateDialog(duplicates))
                {
                    Data.Catalogus.setBoek(boek);
                    Data.Catalogus.setBufferOnopgeslagen(true);
                }
            }
            else
            {
                showErrors(errorMessages);
            }

        }

        private bool showDuplicateDialog(List<Data.Boek> duplicates)
        {
            if (duplicates.Count > 0)
            {
                string warning = "";
                foreach (Data.Boek boekLocal in duplicates)
                {
                    warning += "Titel: " + boekLocal.Titel + Environment.NewLine + "Subtitel: " + boekLocal.Subtitel + Environment.NewLine + "Auteur: " + boekLocal.Auteur + Environment.NewLine + "Deel: " + boekLocal.Deel.ToString() + Environment.NewLine + Environment.NewLine;
                }
                DialogResult save = MessageBox.Show("Mogelijke duplicaten ontdenkt, wilt u het boek toch toevoegen?" + Environment.NewLine + Environment.NewLine + warning, "Duplicaten ontdekt!", MessageBoxButtons.YesNo);
                if (save == DialogResult.Yes)
                {

                    return false;
                }
                return true;
            }
            return false;
        }

        private void showErrors(Dictionary<string, string> errors)
        {
            foreach (KeyValuePair<string, string> messages in errors)
            {
                foreach (Control control in tableLayoutPanel1.Controls)
                {
                    if (control.Name.Equals("lbl" + messages.Key + "Exception"))
                    {
                        control.Text = messages.Value;
                    }
                }
            }
        }
    }
}
