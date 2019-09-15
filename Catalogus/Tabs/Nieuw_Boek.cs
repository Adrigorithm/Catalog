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
    public partial class Nieuw_Boek : UserControl
    {
        public Nieuw_Boek()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int tempDeel;
            Dictionary<string, string> errorMessages = new Dictionary<string, string>();
            Data.Boek boek;
            if (int.TryParse(txtDeel.Text, out tempDeel))
            {
                boek = new Data.Boek(Data.Catalogus.getBoeken().Count + 1, txtTitel.Text, txtSubtitel.Text, txtAuteur.Text, tempDeel);
            }
            else
            {
                boek = new Data.Boek(Data.Catalogus.getBoeken().Count + 1, txtTitel.Text, txtSubtitel.Text, txtAuteur.Text, 1);
            }

            errorMessages = boek.validate();

            Label[] textCheckers = new Label[] { lblTitelException, lblSubtitelException, lblAuteurException, lblDeelException };

            foreach(Label label in textCheckers)
            {
                label.ResetText();
            }

            if (errorMessages.Count == 0)
            {
                List<Data.Boek> duplicates = getDuplicates(boek);
                if (!showDuplicateDialog(duplicates))
                {
                    Data.Catalogus.addBoek(boek);
                    Data.Catalogus.setBufferOnopgeslagen(true);
                }
            }
            else
            {
                showErrors(errorMessages);
            }

        }

        private void showErrors(Dictionary<string, string> errors)
        {
            foreach(KeyValuePair<string, string> messages in errors)
            {
                foreach(Control control in tableLayoutPanel1.Controls)
                {
                    if(control.Name.Equals("lbl" + messages.Key + "Exception"))
                    {
                        control.Text = messages.Value;
                    }
                }
            }
        }

        private bool showDuplicateDialog(List<Data.Boek> duplicates)
        {
            if(duplicates.Count > 0)
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

        private List<Data.Boek> getDuplicates(Data.Boek boek)
        {
            List<Data.Boek> duplicates_local = new List<Data.Boek>();
            foreach(Data.Boek boek_local in Data.Catalogus.getBoeken())
            {
                if (boek.Titel.ToLower().Equals(boek_local.Titel.ToLower()) && boek.Auteur.ToLower().Equals(boek_local.Auteur.ToLower()) && boek.Deel == boek_local.Deel)
                {
                    duplicates_local.Add(boek_local);
                }
            }
            return duplicates_local;
        }
    }
}
