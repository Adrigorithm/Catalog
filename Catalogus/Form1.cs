using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Catalogus
{
    public partial class Form1 : Form
    {
        private List<Data.Boek> boekenStart;

        public Form1()
        {
            InitializeComponent();
            centerMessage(new Point(panel3.Width / 2 - label1.Width / 2, 30), label1);
            centerMessage(new Point(panel3.Width / 2 - label2.Width / 2, 30), label2);
            Resize += Form1_Resize;
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            centerMessage(new Point(panel3.Width / 2 - label1.Width / 2, 30), label1);
            centerMessage(new Point(panel3.Width / 2 - label2.Width / 2, 30), label2);

            foreach (UserControl tab in panel5.Controls)
            {
                tab.Size = panel5.Size;
                tab.Refresh();
            }
        }

        private void centerMessage(Point point, Label label)
        {
            label.Location = point;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Data.Catalogus.loadBoeken();
            boekenStart = Data.Catalogus.getBoeken();
            Tabs.BoekenLijst boekenLijst = new Tabs.BoekenLijst();
            panel5.Controls.Add(boekenLijst);
        } 

        private void button1_Click(object sender, EventArgs e)
        {
            panel5.Controls.Clear();
            Tabs.BoekenLijst boekenLijst = new Tabs.BoekenLijst();
            panel5.Controls.Add(boekenLijst);
            Form1_Resize(sender, e);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            panel5.Controls.Clear();
            Tabs.Nieuw_Boek nieuw_Boek = new Tabs.Nieuw_Boek();
            panel5.Controls.Add(nieuw_Boek);
            Form1_Resize(sender, e);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Data.Catalogus.getBufferOnopgeslagen())
            {
                DialogResult save = MessageBox.Show("Onopgeslagen wijzigingen ontdekt, wilt u deze bijwerken?", "Buffer", MessageBoxButtons.YesNo);
                if(save == DialogResult.Yes)
                {
                    Data.Catalogus.saveBoeken();
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Data.Catalogus.saveBoeken();
            Data.Catalogus.setBufferOnopgeslagen(false);
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            if(Data.Catalogus.getBoekenSelectie().Count == 1)
            {
                panel5.Controls.Clear();
                Tabs.Wijzig_Boek wijzig_Boek = new Tabs.Wijzig_Boek(Data.Catalogus.getBoekenSelectie()[0]);
                panel5.Controls.Add(wijzig_Boek);
                Data.Catalogus.resetBoekenSelectie();
                Form1_Resize(sender, e);
            }
            else
            {
                MessageBox.Show("Selecteer exact één boek om aan te passen.", "Selectiefout");
            }
        }
    }
}
