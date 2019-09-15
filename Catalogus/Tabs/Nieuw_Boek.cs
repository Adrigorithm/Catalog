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
            TextBox[] textValues = new TextBox[] { txtTitel, txtSubtitel, txtAuteur, txtDeel };
            Label[] textCheckers = new Label[] { lblTitelException, lblSubtitelException, lblAuteurException, lblDeelException };
            int[] errorBits = checkValues(textValues);

            foreach(Label label in textCheckers)
            {
                label.ResetText();
            }

            if (!areExceptions(errorBits, textCheckers))
            {
                Data.Boek boek = new Data.Boek(Data.Catalogus.getBoeken().Count + 1, txtTitel.Text, txtAuteur.Text, txtSubtitel.Text, Convert.ToInt16(txtDeel.Text));
                List<Data.Boek> duplicates = getDuplicates(boek);
                if (!showDuplicateDialog(duplicates))
                {
                    Data.Catalogus.addBoek(boek);
                    Data.Catalogus.setBufferOnopgeslagen(true);
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

        private int[] checkValues(TextBox[] textValues)
        {
            int[] errorBits = new int[] {0, 0, 0, 0};
            for (int index = 0; index < textValues.Length; index++)
            {
                if(index == 0 || index == 2)
                {
                    if (String.IsNullOrWhiteSpace(textValues[index].Text))
                    {
                        errorBits[index] = 1;
                    }
                }
                else if (index == 2)
                {
                    if (String.IsNullOrWhiteSpace(textValues[index].Text) && !String.IsNullOrEmpty(textValues[index].Text))
                    {
                        errorBits[index] = 1;    
                    }
                }
                else if(index == 3)
                {
                    if (!int.TryParse(textValues[index].Text, out int deel))
                    {
                        errorBits[index] = 2;
                    }
                }
            }
            return errorBits;
        }

        private bool areExceptions(int[] errorBits, Label[] textCheckers)
        {
            if(errorBits.SequenceEqual(new int[] { 0, 0, 0, 0 }))
            {
                return false;
            }
            else
            {
                Dictionary<int, string> errorMessages = new Dictionary<int, string> { { 1, "Vereist!" }, { 2, "Geen nummer!" } };
                for (int index = 0; index < errorBits.Length; index++)
                {
                    switch (index)
                    {
                        case 0:
                            switch (errorBits[index])
                            {
                                case 1:
                                    textCheckers[index].Text = errorMessages[1];
                                    break;
                                case 2:
                                    textCheckers[index].Text = errorMessages[2];
                                    break;
                            }
                            break;
                        case 1:
                            switch (errorBits[index])
                            {
                                case 1:
                                    textCheckers[index].Text = errorMessages[1];
                                    break;
                                case 2:
                                    textCheckers[index].Text = errorMessages[2];
                                    break;
                            }
                            break;
                        case 2:
                            switch (errorBits[index])
                            {
                                case 1:
                                    textCheckers[index].Text = errorMessages[1];
                                    break;
                                case 2:
                                    textCheckers[index].Text = errorMessages[2];
                                    break;
                            }
                            break;
                        case 3:
                            switch (errorBits[index])
                            {
                                case 1:
                                    textCheckers[index].Text = errorMessages[1];
                                    break;
                                case 2:
                                    textCheckers[index].Text = errorMessages[2];
                                    break;
                            }
                            break;
                    }
                }
                return true;
            }
        }
    }
}
