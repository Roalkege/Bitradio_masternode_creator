using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bitradio_masternode_creator
{
    public partial class help : Form
    {
        public string language_info;

        public help()
        {
            InitializeComponent();
            language_info = System.Globalization.CultureInfo.CurrentCulture.ThreeLetterISOLanguageName;
            if (language_info == "deu")
            {
                this.Name = "Hilfe";
                this.Text = "Hilfe";
                this.label1.Text = "Sie benötigen ein VPS, 2500 Bro und eine synchronisierte Windows Wallet." + "\r\n" + "\r\n"
                      + "Zuerst erstelle eine neue Bro Adresse mit dem Befehl *getnewaddress < label for node >*." + "\r\n"
                      + "Sende dann genau 2500 Bro zur neuen Adresse mit *sendtoaddress <address generated> 2500*, warte dann auf 10+ Bestätigungen." + "\r\n"
                      + "Als nächstes brauchst du die masternode outputs, die bekommst du mit *masternode outputs*." + "\r\n"
                      + "Nun brauchst du den Genkey, denn erhälst du mit *masternode genkey*." + "\r\n" + "\r\n"
                      + "d4b84d0f9b10a18b7fedded0e6617818e0f78b92c42601226c9f4f0fa493ff72 : 0 (Beispiel output)" + "\r\n"
                      + "d4b84d0f9b10a18b7fedded0e6617818e0f78b92c42601226c9f4f0fa493ff72 = output" + "\r\n"
                      + "0 = Number after output";
            }
            else if (language_info == "rus")
            {
                this.Name = "Помощь";
                this.Text = "Помощь";
                this.label2.Visible = true;
                this.label1.Text = "Вам нужен VPS, 2500 BRO и полностью синхронизированный бумажник для Windows" + "\r\n" + "\r\n"
                     + "Сначала создайте новый BRO адрес с *getnewaddress < label for node >*." + "\r\n"
                     + "Во-вторых, отправьте 2500 монет на новый адрес *sendtoaddress < address generated > 2500* и подождите 10 или более подтверждений."
                     + "После этого получите выходы (outputs) с *masternode outputs*." + "\r\n"
                     + "В - четвертых, получите ваш ключ с помощью *masternode genkey*." + "\r\n" + "\r\n"
                     + "d4b84d0f9b10a18b7fedded0e6617818e0f78b92c42601226c9f4f0fa493ff72 : 0 (Beispiel output)" + "\r\n"
                     + "d4b84d0f9b10a18b7fedded0e6617818e0f78b92c42601226c9f4f0fa493ff72 = output" + "\r\n"
                     + "0 = Number after output";
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
