using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bitradio_Masternode_Creator
{
    public partial class donate_form : Form
    {

        System.Windows.Forms.ToolTip btnToolTip = new System.Windows.Forms.ToolTip();
        public string language_info;

        public donate_form()
        {
            InitializeComponent();
            language_info = System.Globalization.CultureInfo.CurrentCulture.ThreeLetterISOLanguageName;
            if (language_info == "deu")
                this.label2.Text = "Spende mir ein paar Coins, klicke einfach auf die Adresse. ^^";
        }

        private void donate_form_Load(object sender, EventArgs e)
        {

        }

        private void bro_Click(object sender, EventArgs e)
        {
            Clipboard.SetText("Jh2hw7Ktifk6mMRFmBckFtyDADsNZmsDqs");
            if (language_info == "deu")
                btnToolTip.SetToolTip(this.bro_label, "Gespeichert");
            else
                btnToolTip.SetToolTip(this.bro_label, "Saved");
        }

        private void btc_label_Click(object sender, EventArgs e)
        {
            Clipboard.SetText("bc1qhj6uxhzrzp3ymcd3tf6daayvskccj35swnzyvf");
            if (language_info == "deu")
                btnToolTip.SetToolTip(this.btc_label, "Gespeichert");
            else
                btnToolTip.SetToolTip(this.btc_label, "Saved");
        }

        private void eth_label_Click(object sender, EventArgs e)
        {
            Clipboard.SetText("0x3d4AA9B18320a8DD0587C3BC57640C9aaEb4eca4");
            if (language_info == "deu")
                btnToolTip.SetToolTip(this.eth_label, "Gespeichert");
            else
                btnToolTip.SetToolTip(this.eth_label, "Saved");
        }

        private void doge_label_Click(object sender, EventArgs e)
        {
            Clipboard.SetText("DTDBRFfzLVF7TrqZ7WAUKzmJjT5CSwitqR");
            if (language_info == "deu")
                btnToolTip.SetToolTip(this.doge_label, "Gespeichert");
            else
                btnToolTip.SetToolTip(this.doge_label, "Saved");
        }
    }
}
