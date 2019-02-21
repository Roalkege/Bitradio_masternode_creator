using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Renci.SshNet;
using Renci.SshNet.Sftp;
using Microsoft.VisualBasic;
using System.IO;
using System.Diagnostics;
using System.Collections;
using System.Net;


//Thanks to WlanWerner(https://github.com/WlanWerner)

namespace Bitradio_masternode_creator
{
    public partial class Form1 : Form
    {

        // create variables
        public string ip;
        public string username;
        public string password;
        public string genkey;
        public string output;
        public string output_after;
        public string masternodename;
        public string port;


        public Form1()
        {
            InitializeComponent();
            MessageBox.Show("I take no responsibility for this program." + "\r\n"
                      + "Use this program with caution." + "\r\n"
                      + "I take no responsibility for lost coins and/or their VPS." + "\r\n"
                      + "I can't get in touch with their coins." + "\r\n"
                      + "Please encrypt your wallet anyway and make a note of your password." + "\r\n"
                      + "Check also my Code(it's open Source) and don't flame I'm a student :D");
        }

        // Install the ./Bitradiod
        private void button1_Click(object sender, EventArgs e)
        {
            new Task(() => RunCommand()).Start();
        }

        private void RunCommand()
        {
            // check if the windows wallet is installed at %AppData%\Bitradio\\
            //if (!Directory.Exists(@"%AppData%\Bitradio\\"))
            //{
            //    MessageBox.Show("Have you installed the Wallet at /%AppData%/Roaming/Bitradio ?");  //If not 
            //    return;
            //}

            //check for existing masternode.conf
            if (!File.Exists(@"%AppData%\Bitradio\\masternode.conf"))
            {
                using (StreamWriter sw = new StreamWriter(Environment.ExpandEnvironmentVariables(@"%AppData%\Bitradio\\masternode.conf"), true))  //if not than create
                {

                }
            }

            //load all variables

            ip = ip_feld.Text;
            username = username_feld.Text;
            password = password_feld.Text;
            port = port_feld.Text;
            genkey = genkey_feld.Text;
            output = output_feld.Text;
            output_after = after_output_feld.Text;
            masternodename = masternodename_feld.Text;
            string TEMP_PATH = Path.GetTempPath();


            if (startup_checkbox.Checked)
            {

                //login and create connection
                using (var client = new SshClient(ip, Convert.ToInt16(port), username, password))
                {

                    // check if everything givem
                    try
                    {
                        client.Connect();
                        //port = null;
                        //masternodename = null;
                        //output_after = null;
                        //genkey = null;
                        //output = null;
                    }
                    catch
                    {
                        MessageBox.Show("Please fill out the IP, user, password, genkey, port, output, after_output and masternodename!");  //if not
                        return;
                    }


                    // Crappy way!! I don't know how to transfer a lokal variable to the vps. So I create lokal a file with the genkey as name, upload it 
                    // to the vps and read the new created directory. The output is the genkey :D

                    var command = client.CreateCommand("mkdir /root/temp_bitradio/");
                    var result = command.BeginExecute();
                    command = client.CreateCommand("cd /root/temp_bitradio/");
                    result = command.BeginExecute();

                    //create the lokale file
                    if (!File.Exists(TEMP_PATH + genkey))
                    {
                        using (StreamWriter sw = new StreamWriter(Environment.ExpandEnvironmentVariables(TEMP_PATH + genkey), true))
                        {

                        }
                    }
                    client.Disconnect();
                }
                MessageBox.Show("local erstellt");
                //upload the file
                using (var client = new SftpClient(ip, Convert.ToInt16(port), username, password))
                {
                    client.Connect();

                    FileInfo f = new FileInfo(TEMP_PATH + genkey);
                    string uploadfile = f.FullName;

                    var fileStream = new FileStream(uploadfile, FileMode.Open);
                    if (fileStream != null)
                    {

                        client.UploadFile(fileStream, "/root/temp_bitradio/" + f.Name, null);
                        client.Disconnect();
                        client.Dispose();
                    }
                }

                MessageBox.Show("gesendet");
                // execute the ./Bitradiod install script (self-made)
                using (var client = new SshClient(ip, Convert.ToInt16(port), username, password))
                {

                    client.Connect();

                    var command = client.CreateCommand("./Bitradiod getblockcount");
                    var result = command.BeginExecute();
                    command = client.CreateCommand("sudo wget https://raw.githubusercontent.com/Roalkege/bitradio_masternode_creator/master/Bitradio_MN_tool_cron.sh && bash Bitradio_MN_tool_cron.sh");  //download the script
                    result = command.BeginExecute();

                    //log vps output 
                    using (var reader =
                       new StreamReader(command.OutputStream, Encoding.UTF8, true, 1024, true))
                    {
                        while (!result.IsCompleted || !reader.EndOfStream)
                        {
                            string line = reader.ReadLine();
                            if (line != null)
                            {
                                log_feld.Invoke(
                                    (MethodInvoker)(() =>
                                        log_feld.AppendText(line + Environment.NewLine)));
                            }
                        }
                    }

                    command.EndExecute(result);


                    command = client.CreateCommand("cd ~");
                    result = command.BeginExecute();
                    command = client.CreateCommand("rm Bitradio_MN_tool.sh");  //remove the script
                    result = command.BeginExecute();
                    MessageBox.Show("Masternode installed now restart your windows wallet and start the Alias");
                    client.Disconnect();
                }
            }

            else
            {
                //login and create connection
                using (var client = new SshClient(ip, Convert.ToInt16(port), username, password))
                {

                    // check if everything givem
                    try
                    {
                        client.Connect();
                        //port = null;
                        //masternodename = null;
                        //output_after = null;
                        //genkey = null;
                        //output = null;
                    }
                    catch
                    {
                        MessageBox.Show("Please fill out the IP, user, password, genkey, port, output, after_output and masternodename!");  //if not
                        return;
                    }


                    // Crappy way!! I don't know how to transfer a lokal variable to the vps. So I create lokal a file with the genkey as name, upload it 
                    // to the vps and read the new created directory. The output is the genkey :D

                    var command = client.CreateCommand("mkdir /root/temp_bitradio/");
                    var result = command.BeginExecute();
                    command = client.CreateCommand("cd /root/temp_bitradio/");
                    result = command.BeginExecute();

                    //create the lokale file
                    if (!File.Exists(TEMP_PATH + genkey))
                    {
                        using (StreamWriter sw = new StreamWriter(Environment.ExpandEnvironmentVariables(TEMP_PATH + genkey), true))
                        {

                        }
                    }
                    client.Disconnect();
                }
                MessageBox.Show("local erstellt");
                //upload the file
                using (var client = new SftpClient(ip, Convert.ToInt16(port), username, password))
                {
                    client.Connect();

                    FileInfo f = new FileInfo(TEMP_PATH + genkey);
                    string uploadfile = f.FullName;

                    var fileStream = new FileStream(uploadfile, FileMode.Open);
                    if (fileStream != null)
                    {

                        client.UploadFile(fileStream, "/root/temp_bitradio/" + f.Name, null);
                        client.Disconnect();
                        client.Dispose();
                    }
                }

                MessageBox.Show("gesendet");
                // execute the ./Bitradiod install script (self-made)
                using (var client = new SshClient(ip, Convert.ToInt16(port), username, password))
                {

                    client.Connect();

                    var command = client.CreateCommand("./Bitradiod getblockcount");
                    var result = command.BeginExecute();
                    command = client.CreateCommand("sudo wget https://raw.githubusercontent.com/Roalkege/bitradio_masternode_creator/master/Bitradio_MN_tool.sh && bash Bitradio_MN_tool.sh");  //download the script
                    result = command.BeginExecute();

                    //log vps output 
                    using (var reader =
                       new StreamReader(command.OutputStream, Encoding.UTF8, true, 1024, true))
                    {
                        while (!result.IsCompleted || !reader.EndOfStream)
                        {
                            string line = reader.ReadLine();
                            if (line != null)
                            {
                                log_feld.Invoke(
                                    (MethodInvoker)(() =>
                                        log_feld.AppendText(line + Environment.NewLine)));
                            }
                        }
                    }

                    command.EndExecute(result);


                    command = client.CreateCommand("cd ~");
                    result = command.BeginExecute();
                    command = client.CreateCommand("rm Bitradio_MN_tool.sh");  //remove the script
                    result = command.BeginExecute();
                    MessageBox.Show("Masternode installed now restart your windows wallet and start the Alias");
                    client.Disconnect();
                }
            }

            //edit the lokal masternode.conf 
            using (StreamWriter sw = new StreamWriter(Environment.ExpandEnvironmentVariables(TEMP_PATH + @"\masternode.conf"), true))
            {
                sw.Write(masternodename + " " + ip + ":32454 " + genkey + " " + output + " " + output_after + " " + "\r\n");

            }
        }



        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            //opens the help form
            //multi-language in future
            help help = new help();
            help.ShowDialog();
            help.Focus();
        }

        private void button2_Click(object sender, EventArgs e)
        {

            //checks the output of the masternode

            ip = ip_feld.Text;
            port = port_feld.Text;
            username = username_feld.Text;
            password = password_feld.Text;

            //login and create connection
            using (var client = new SshClient(ip, Convert.ToInt16(port), username, password))
            {
                try
                {
                    client.Connect();
                }
                catch
                {
                    MessageBox.Show("Please fill out the IP, user and password!"); //If can't connect
                    return;
                }

                var command = client.CreateCommand("./Bitradiod masternode status");
                var result = command.Execute();
                log_feld.Text = result +
                    "\r\n" + "\r\n" + "If the Node shows Status 9 and notCapableReason : Could not find suitable coins! than everything works fine!!!";
                client.Disconnect();
            }
        }

        //Restarts the ./Bitradiod process
        private void restart_masternode_Click(object sender, EventArgs e)
        {
            //Get the informations
            ip = ip_feld.Text;
            port = port_feld.Text;
            username = username_feld.Text;
            password = password_feld.Text;

            //login and create connection
            //still in test!!!
            using (var client = new SshClient(ip, Convert.ToInt16(port), username, password))
            {
                try
                {
                    client.Connect();
                }
                catch
                {
                    MessageBox.Show("Please fill out the IP, user and password!");  //If can't connect
                    return;
                }

                var command = client.CreateCommand("cd ~");
                var result = command.Execute();
                command = client.CreateCommand("./Bitradiod stop");  //stops the node
                //result = command.Execute();
                //System.Threading.Thread.Sleep(500);
                //command = client.CreateCommand("killall ./Bitradiod");  // kills the node process
                //result = command.Execute();
                command = client.CreateCommand("./Bitradiod");  // starts the wallet
                result = command.Execute(); ;
                client.Disconnect();
            }
        }

        //starts ./Bitradiod with startup and creates a cronjob
        private void startup_checkbox_CheckedChanged(object sender, EventArgs e)
        {
            //will be added in future
        }

        //check for update
        private void update_button_Click(object sender, EventArgs e)
        {
            var version_number = "Version 1.0.0";   //app verion
            int c;

            string result = null;
            string url = "https://brocoin.world/Nick/version.txt";  //checks the website and get the latest build verion
            WebResponse response = null;
            StreamReader reader = null;

            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "GET";
                response = request.GetResponse();
                reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                result = reader.ReadToEnd();
            }
            catch (Exception ex)
            {
                // handle error
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (reader != null)
                    reader.Close();
                if (response != null)
                    response.Close();
            }

            //compare app version and download version
            c = String.Compare(result, version_number);

            if (c == -1)    //if available
            {
                switch (MessageBox.Show("There is a Update available", "Update", MessageBoxButtons.YesNo))
                {
                    case DialogResult.Yes: System.Diagnostics.Process.Start("http://brocoin.world"); ; break;   //opens the default browser
                    case DialogResult.No: break;
                }
            }
            else    //no new version
            {
                MessageBox.Show("There is no Update available", "Update", MessageBoxButtons.OK);
            }
        }
    }
}
