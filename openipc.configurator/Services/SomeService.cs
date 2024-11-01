//using Avalonia.Controls;
//using Microsoft.VisualBasic;
//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Diagnostics;
//using System.IO;
//using System.Linq;
//using System.Net;
//using System.Net.NetworkInformation;
//using System.Reflection.Emit;
//using System.Runtime.InteropServices;
//using System.Text;
//using System.Text.Encodings.Web;
//using System.Threading;
//using System.Threading.Tasks;
//using System.Timers;

//namespace openipc.configurator.Services
//{
//    internal class SomeService
//    {
//        public string OpenIPCIP;
//        public string NVRIP;
//        public string RadxaIP;

//        public void DownloadStart()
//        {
//            downloader = new WebClient();

//            if (cmbVersion.Text == "ssc338q_fpv_emax-wyvern-link-nor" ||
//                cmbVersion.Text == "ssc338q_fpv_openipc-mario-aio-nor" ||
//                cmbVersion.Text == "ssc338q_fpv_openipc-urllc-aio-nor" ||
//                cmbVersion.Text == "ssc338q_fpv_runcam-wifilink-nor")
//            {
//                downloader.DownloadFileAsync(new Uri("https://github.com/OpenIPC/builder/releases/download/latest/" + cmbVersion.Text + ".tgz"), cmbVersion.Text + ".tgz");
//            }
//            else
//            {
//                downloader.DownloadFileAsync(new Uri("https://github.com/OpenIPC/firmware/releases/download/latest/" + cmbVersion.Text + ".tgz"), cmbVersion.Text + ".tgz");
//            }
//        }
//        private void downloader_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
//        {
//            string externFile = "extern.bat";

//            if (!File.Exists(externFile))
//            {
//                MessageBox.Show("File " + externFile + " not found!");
//                return;
//            }

//            progressBar1.Value = e.ProgressPercentage;

//            if (progressBar1.Value == 100)
//            {
//                using (Process process = new Process())
//                {
//                    process.StartInfo.UseShellExecute = false;
//                    process.StartInfo.FileName = externFile;
//                    process.StartInfo.Arguments = $"offlinefw {txtIP.Text} {txtPassword.Text} {cmbVersion.Text} {txtSOC.Text}";
//                    process.StartInfo.RedirectStandardOutput = false;
//                    process.Start();
//                }
//            }
//        }
//        private void btnGet_Click(object sender, EventArgs e)
//        {
//            string settingsconf = "settings.conf";

//            if (!File.Exists(settingsconf))
//            {
//                File.Create(settingsconf).Dispose();
//                bool fileExists = File.Exists(settingsconf);
//                using (StreamWriter sw = new StreamWriter(File.Open(settingsconf, FileMode.OpenOrCreate)))
//                {
//                    sw.WriteLine("openipc:192.168.0.1");
//                    sw.WriteLine("nvr:192.168.0.1");
//                    sw.WriteLine("radxa:192.168.0.1");
//                }
//                MessageBox.Show("File " + settingsconf + " not found and default created!");
//            }

//            string externFile = "extern.bat";
//            if (!File.Exists(externFile))
//            {
//                MessageBox.Show("File " + externFile + " not found!");
//                return;
//            }

//            if (IsValidIP(txtIP.Text))
//            {
//                using (Process process = new Process())
//                {
//                    process.StartInfo.UseShellExecute = false;
//                    process.StartInfo.FileName = externFile;
//                    string[] lines = File.ReadAllLines(settingsconf);
//                    for (int x = 0; x < lines.Length; x++)
//                    {
//                        if (rBtnNVR.Checked)
//                        {
//                            if (lines[x].StartsWith("nvr:"))
//                            {
//                                lines[x] = "nvr:" + txtIP.Text;
//                            }
//                            process.StartInfo.Arguments = "dlvrx " + txtIP.Text + " " + txtPassword.Text;
//                        }
//                        else if (rBtnRadxaZero3w.Checked)
//                        {
//                            if (lines[x].StartsWith("radxa:"))
//                            {
//                                lines[x] = "radxa:" + txtIP.Text;
//                            }
//                            process.StartInfo.Arguments = "dlwfbng " + txtIP.Text + " " + txtPassword.Text;
//                        }
//                        else
//                        {
//                            if (lines[x].StartsWith("openipc:"))
//                            {
//                                lines[x] = "openipc:" + txtIP.Text;
//                            }
//                            process.StartInfo.Arguments = "dl " + txtIP.Text + " " + txtPassword.Text;
//                        }
//                    }
//                    File.WriteAllLines(settingsconf, lines);
//                    process.StartInfo.RedirectStandardOutput = false;
//                    process.Start();
//                }
//            }
//            else
//            {
//                MessageBox.Show("Please enter a valid IP address");
//            }
//        }
//        private void btnSend_Click(object sender, EventArgs e)
//        {
//            string externFile = "extern.bat";

//            if (!File.Exists(externFile))
//            {
//                MessageBox.Show("File " + externFile + " not found!");
//                return;
//            }

//            if (IsValidIP(txtIP.Text))
//            {
//                using (Process process = new Process())
//                {
//                    process.StartInfo.UseShellExecute = false;
//                    process.StartInfo.FileName = externFile;

//                    if (rBtnNVR.Checked)
//                    {
//                        process.StartInfo.Arguments = "ulvrx " + txtIP.Text + " " + txtPassword.Text;
//                    }
//                    else if (rBtnRadxaZero3w.Checked)
//                    {
//                        process.StartInfo.Arguments = "ulwfbng " + txtIP.Text + " " + txtPassword.Text;
//                    }
//                    else
//                    {
//                        process.StartInfo.Arguments = "ul " + txtIP.Text + " " + txtPassword.Text;
//                    }

//                    process.StartInfo.RedirectStandardOutput = false;
//                    process.Start();
//                }
//            }
//            else
//            {
//                MessageBox.Show("Please enter a valid IP address");
//            }
//        }
//        public string ReadLine(int lineNumber, List<string> lines)
//        {
//            try
//            {
//                return lines[lineNumber - 1];
//            }
//            catch (ArgumentOutOfRangeException)
//            {
//                return null; // Или обработка ошибки по вашему выбору
//            }
//        }
//        private void txtSaveFreq_Click(object sender, EventArgs e)
//        {
//            if (!string.IsNullOrEmpty(txtFrequency.Text))
//            {
//                string wfbconf = "wfb.conf";

//                if (!File.Exists(wfbconf))
//                {
//                    MessageBox.Show("File " + wfbconf + " not found!" + Environment.NewLine + "Install the latest version of Putty and try again.");
//                    return;
//                }

//                string[] lines = File.ReadAllLines(wfbconf);

//                for (int x = 0; x < lines.Length; x++)
//                {
//                    if (rBtnRadxaZero3w.Checked)
//                    {
//                        string wfbng = "wifibroadcast.cfg";

//                        if (!File.Exists(wfbng))
//                        {
//                            MessageBox.Show("File " + wfbng + " not found!" + Environment.NewLine + "Install the latest version of Putty and try again.");
//                            return;
//                        }

//                        string[] WFBlines = File.ReadAllLines(wfbng);

//                        if (WFBlines[1].StartsWith("wifi_channel = "))
//                        {
//                            WFBlines[1] = "wifi_channel = " + txtFrequency.Text; // добавлен префикс для корректности
//                        }
//                        if (WFBlines[7].StartsWith("peer = 'connect://"))
//                        {
//                            WFBlines[7] = "peer = 'connect://" + txtMCS.Text; // добавлен префикс для корректности
//                        }
//                        if (WFBlines[11].StartsWith("peer = 'connect://"))
//                        {
//                            WFBlines[11] = "peer = 'connect://" + txtSTBC.Text; // добавлен префикс для корректности
//                        }

//                        File.WriteAllLines(wfbng, WFBlines);

//                        if (lines[x].StartsWith("options 88XXau_wfb rtw_tx_pwr_idx_override="))
//                        {
//                            lines[x] = "options 88XXau_wfb rtw_tx_pwr_idx_override=" + txtPower.Text; // добавлен префикс для корректности
//                        }
//                    }
//                    else
//                    {
//                        if (lines[x].StartsWith("channel="))
//                        {
//                            lines[x] = "channel=" + txtFrequency.Text; // добавлен префикс для корректности
//                        }
//                        if (lines[x].StartsWith("driver_txpower_override="))
//                        {
//                            lines[x] = "driver_txpower_override=" + txtPower.Text; // добавлен префикс для корректности
//                        }
//                        if (lines[x].StartsWith("frequency="))
//                        {
//                            lines[x] = "frequency=" + txtFreq24.Text; // добавлен префикс для корректности
//                        }
//                        if (lines[x].StartsWith("txpower="))
//                        {
//                            lines[x] = "txpower=" + txtPower24.Text; // добавлен префикс для корректности
//                        }
//                        if (rBtnNVR.Checked)
//                        {
//                            if (lines[x].StartsWith("udp_addr="))
//                            {
//                                lines[x] = "udp_addr=" + txtMCS.Text; // добавлен префикс для корректности
//                            }
//                            if (lines[x].StartsWith("udp_port="))
//                            {
//                                lines[x] = "udp_port=" + txtSTBC.Text; // добавлен префикс для корректности
//                            }
//                        }
//                        else
//                        {
//                            if (lines[x].StartsWith("stbc="))
//                            {
//                                lines[x] = "stbc=" + txtSTBC.Text; // добавлен префикс для корректности
//                            }
//                            if (lines[x].StartsWith("ldpc="))
//                            {
//                                lines[x] = "ldpc=" + txtLDPC.Text; // добавлен префикс для корректности
//                            }
//                            if (lines[x].StartsWith("mcs_index="))
//                            {
//                                lines[x] = "mcs_index=" + txtMCS.Text; // добавлен префикс для корректности
//                            }
//                            if (lines[x].StartsWith("fec_k="))
//                            {
//                                lines[x] = "fec_k=" + txtFECK.Text; // добавлен префикс для корректности
//                            }
//                            if (lines[x].StartsWith("fec_n="))
//                            {
//                                lines[x] = "fec_n=" + txtFECN.Text; // добавлен префикс для корректности
//                            }
//                        }
//                    }
//                }

//                File.WriteAllLines(wfbconf, lines);
//                MessageBox.Show("Settings saved successfully", "OpenIPC", MessageBoxButtons.OK, MessageBoxIcon.Information);
//            }
//        }
//        private void txtSaveCam_Click(object sender, EventArgs e)
//        {
//            if (!string.IsNullOrEmpty(txtResolution.Text))
//            {
//                string majestic = "majestic.yaml";

//                if (!File.Exists(majestic))
//                {
//                    MessageBox.Show("File " + majestic + " not found!" + Environment.NewLine + "Install the latest version of Putty and try again.");
//                    return;
//                }

//                string[] lines = File.ReadAllLines(majestic);

//                for (int x = 0; x < lines.Length; x++)
//                {
//                    if (lines[x].StartsWith("  contrast: "))
//                    {
//                        lines[x] = "  contrast: " + txtContrast.Text;
//                    }
//                    else if (lines[x].StartsWith("  hue: "))
//                    {
//                        lines[x] = "  hue: " + txtHue.Text;
//                    }
//                    else if (lines[x].StartsWith("  saturation:"))
//                    {
//                        lines[x] = "  saturation: " + txtSaturation.Text;
//                    }
//                    else if (lines[x].StartsWith("  luminance: "))
//                    {
//                        lines[x] = "  luminance: " + txtLuminance.Text;
//                    }
//                    else if (lines[x].StartsWith("  bitrate: "))
//                    {
//                        lines[x] = "  bitrate: " + txtBitrate.Text;
//                    }
//                    else if (lines[x].StartsWith("  codec: h26"))
//                    {
//                        lines[x] = "  codec: h26" + txtEncode.Text; // Убедитесь, что текст закодирования сохраняется корректно
//                    }
//                    else if (lines[x].StartsWith("  size: "))
//                    {
//                        lines[x] = "  size: " + txtResolution.Text;
//                    }
//                    else if (lines[x].StartsWith("  fps: "))
//                    {
//                        lines[x] = "  fps: " + txtFPS.Text;
//                    }
//                    else if (lines[x].StartsWith("  sensorConfig: "))
//                    {
//                        lines[x] = "  sensorConfig: " + txtSensor.Text;
//                    }
//                    else if (lines[x].StartsWith("  exposure: "))
//                    {
//                        lines[x] = "  exposure: " + txtExposure.Text;
//                    }
//                    else if (lines[x].StartsWith("  mirror: "))
//                    {
//                        lines[x] = "  mirror: " + txtMirror.Text;
//                    }
//                    else if (lines[x].StartsWith("  flip: "))
//                    {
//                        lines[x] = "  flip: " + txtFlip.Text;
//                    }
//                }

//                File.WriteAllLines(majestic, lines);
//                MessageBox.Show("Settings saved successfully", "OpenIPC", MessageBoxButtons.OK, MessageBoxIcon.Information);
//            }
//        }
//        private void btnRead_Click(object sender, EventArgs e)
//        {
//            string settingsconf = "settings.conf";
//            if (!File.Exists(settingsconf))
//            {
//                File.Create(settingsconf).Dispose();
//                MessageBox.Show("File " + settingsconf + " not found and default created!");
//                using (StreamWriter sw = new StreamWriter(File.Open(settingsconf, FileMode.OpenOrCreate)))
//                {
//                    sw.WriteLine("openipc:192.168.0.1");
//                    sw.WriteLine("nvr:192.168.0.1");
//                    sw.WriteLine("radxa:192.168.0.1");
//                }
//            }

//            string @extern = "extern.bat";
//            if (!File.Exists(@extern))
//            {
//                MessageBox.Show("File " + @extern + " not found!");
//                return;
//            }

//            if (IsValidIP(txtIP.Text))
//            {
//                using (Process process = new Process())
//                {
//                    process.StartInfo.UseShellExecute = false;
//                    process.StartInfo.FileName = @extern;
//                    string[] lines = File.ReadAllLines(settingsconf);

//                    for (int x = 0; x < lines.Length; x++)
//                    {
//                        if (rBtnNVR.Checked)
//                        {
//                            if (lines[x].StartsWith("nvr:"))
//                            {
//                                lines[x] = "nvr:" + txtIP.Text;
//                            }
//                            process.StartInfo.Arguments = "dlvrx " + txtIP.Text + " " + txtPassword.Text;
//                        }
//                        else if (rBtnRadxaZero3w.Checked)
//                        {
//                            if (lines[x].StartsWith("radxa:"))
//                            {
//                                lines[x] = "radxa:" + txtIP.Text;
//                            }
//                            process.StartInfo.Arguments = "dlwfbng " + txtIP.Text + " " + txtPassword.Text;
//                        }
//                        else
//                        {
//                            if (lines[x].StartsWith("openipc:"))
//                            {
//                                lines[x] = "openipc:" + txtIP.Text;
//                            }
//                            process.StartInfo.Arguments = "dl " + txtIP.Text + " " + txtPassword.Text;
//                        }
//                    }
//                    File.WriteAllLines(settingsconf, lines);
//                    process.StartInfo.RedirectStandardOutput = false;
//                    process.Start();
//                }
//            }
//            else
//            {
//                MessageBox.Show("Please enter a valid IP address");
//                return;
//            }

//            // Resetting the text fields
//            txtResolution.Text = "";
//            txtFPS.Text = "";
//            txtEncode.Text = "";
//            txtBitrate.Text = "";
//            txtExposure.Text = "";
//            txtContrast.Text = "";
//            txtSaturation.Text = "";
//            txtHue.Text = "";
//            txtLuminance.Text = "";
//            txtFlip.Text = "";
//            txtMirror.Text = "";
//            txtSensor.Text = "";
//            txtSerial.Text = "";
//            txtBaud.Text = "";
//            txtRouter.Text = "";
//            txtMCSTLM.Text = "";
//            txtAggregate.Text = "";
//            txtRC_CHANNEL.Text = "";
//            txtFrequency.Text = "";
//            txtPower.Text = "";
//            txtFreq24.Text = "";
//            txtPower24.Text = "";
//            txtMCS.Text = "";
//            txtSTBC.Text = "";
//            txtLDPC.Text = "";
//            txtFECK.Text = "";
//            txtFECN.Text = "";

//            Thread.Sleep(3000);

//            string wfbconf = "wfb.conf";
//            if (!File.Exists(wfbconf))
//            {
//                MessageBox.Show("File " + wfbconf + " not found!" + Environment.NewLine + "Install the latest version of Putty and try again.");
//                return;
//            }

//            var WFBallLines = new List<string>(File.ReadAllLines(wfbconf));

//            if (rBtnNVR.Checked)
//            {
//                txtFrequency.Text = ReadLine(7, WFBallLines);
//                txtPower.Text = ReadLine(10, WFBallLines);
//                txtFreq24.Text = ReadLine(8, WFBallLines);
//                txtPower24.Text = ReadLine(9, WFBallLines);
//                txtMCS.Text = ReadLine(14, WFBallLines);
//                txtSTBC.Text = ReadLine(15, WFBallLines);
//            }
//            else if (rBtnRadxaZero3w.Checked)
//            {
//                string wfbng = "wifibroadcast.cfg";
//                if (!File.Exists(wfbng))
//                {
//                    MessageBox.Show("File " + wfbng + " not found!" + Environment.NewLine + "Install the latest version of Putty and try again.");
//                    return;
//                }

//                var WFBngallLines = new List<string>(File.ReadAllLines(wfbng));
//                txtFrequency.Text = ReadLine(2, WFBngallLines);
//                if (rBtnCam.Checked) txtPower.Text = ReadLine(6, WFBallLines);

//                for (int x = 0; x < WFBallLines.Count; x++)
//                {
//                    if (WFBallLines[x].StartsWith("options 88XXau_wfb "))
//                    {
//                        txtPower.Text = ReadLine(x + 1, WFBallLines);
//                    }
//                }
//                txtMCS.Text = ReadLine(8, WFBngallLines);
//                txtSTBC.Text = ReadLine(12, WFBngallLines);
//            }
//            else
//            {
//                txtFrequency.Text = ReadLine(7, WFBallLines);
//                txtPower.Text = ReadLine(10, WFBallLines);
//                txtFreq24.Text = ReadLine(8, WFBallLines);
//                txtPower24.Text = ReadLine(9, WFBallLines);
//                txtMCS.Text = ReadLine(14, WFBallLines);
//                txtSTBC.Text = ReadLine(12, WFBallLines);
//                txtLDPC.Text = ReadLine(13, WFBallLines);
//                txtFECK.Text = ReadLine(20, WFBallLines);
//                txtFECN.Text = ReadLine(21, WFBallLines);
//            }

//            if (rBtnNVR.Checked || rBtnCam.Checked)
//            {
//                string telemetry = "telemetry.conf";
//                if (!File.Exists(telemetry))
//                {
//                    MessageBox.Show("File " + telemetry + " not found!" + Environment.NewLine + "Install the latest version of Putty and try again.");
//                    return;
//                }

//                var TLMallLines = new List<string>(File.ReadAllLines(telemetry));
//                txtSerial.Text = ReadLine(4, TLMallLines);
//                txtBaud.Text = ReadLine(5, TLMallLines);
//                txtRouter.Text = ReadLine(8, TLMallLines);
//                txtMCSTLM.Text = ReadLine(14, TLMallLines);
//                txtAggregate.Text = ReadLine(26, TLMallLines);
//                txtRC_CHANNEL.Text = ReadLine(29, TLMallLines);

//                if (rBtnNVR.Checked)
//                {
//                    string vdec = "vdec.conf";
//                    if (!File.Exists(vdec))
//                    {
//                        MessageBox.Show("File " + vdec + " not found!" + Environment.NewLine + "Install the latest version of Putty and try again.");
//                        return;
//                    }

//                    var VDECallLines = new List<string>(File.ReadAllLines(vdec));
//                    txtResolutionVRX.Text = ReadLine(22, VDECallLines);
//                    txtCodecVRX.Text = ReadLine(7, VDECallLines);
//                    txtFormat.Text = ReadLine(11, VDECallLines);
//                    txtPortVRX.Text = ReadLine(3, VDECallLines);
//                    txtMavlinkVRX.Text = ReadLine(26, VDECallLines);
//                    txtOSD.Text = ReadLine(30, VDECallLines);
//                    txtExtras.Text = ReadLine(52, VDECallLines);
//                    string line = ReadLine(53, VDECallLines);
//                    string value2 = line.Replace("\"", "");
//                    string[] separators = { "-osd_ele", "x", "y" };
//                    string[] result = value2.Split(separators, StringSplitOptions.RemoveEmptyEntries);

//                    for (int i = 0; i < result.Length; i += 2)
//                    {
//                        if (i / 2 < 18) // Adjusting positions for 18 RadioButtons
//                        {
//                            RadioButton radioButton = (RadioButton)this.Controls["RadioButton" + (i / 2 + 1)];
//                            radioButton.Left = int.Parse(result[i]) / 2.5;
//                            radioButton.Top = int.Parse(result[i + 1]) / 2.5;
//                        }
//                    }
//                }
//                else
//                {
//                    string majestic = "majestic.yaml";
//                    if (!File.Exists(majestic))
//                    {
//                        MessageBox.Show("File " + majestic + " not found!" + Environment.NewLine + "Install the latest version of Putty and try again.");
//                        return;
//                    }

//                    var CamallLines = new List<string>(File.ReadAllLines(majestic));
//                    for (int x = 0; x < CamallLines.Count; x++)
//                    {
//                        if (CamallLines[x].StartsWith("  size:") && string.IsNullOrEmpty(txtResolution.Text))
//                            txtResolution.Text = ReadLine(x + 1, CamallLines);
//                        if (CamallLines[x].StartsWith("  fps:") && string.IsNullOrEmpty(txtFPS.Text))
//                            txtFPS.Text = ReadLine(x + 1, CamallLines);
//                        if (CamallLines[x].StartsWith("  codec: h26"))
//                            txtEncode.Text = ReadLine(x + 1, CamallLines);
//                        if (CamallLines[x].StartsWith("  bitrate:"))
//                            txtBitrate.Text = ReadLine(x + 1, CamallLines);
//                        if (CamallLines[x].StartsWith("  exposure:"))
//                            txtExposure.Text = ReadLine(x + 1, CamallLines);
//                        if (CamallLines[x].StartsWith("  contrast:"))
//                            txtContrast.Text = ReadLine(x + 1, CamallLines);
//                        if (CamallLines[x].StartsWith("  saturation:"))
//                            txtSaturation.Text = ReadLine(x + 1, CamallLines);
//                        if (CamallLines[x].StartsWith("  hue:"))
//                            txtHue.Text = ReadLine(x + 1, CamallLines);
//                        if (CamallLines[x].StartsWith("  luminance:"))
//                            txtLuminance.Text = ReadLine(x + 1, CamallLines);
//                        if (CamallLines[x].StartsWith("  flip:"))
//                            txtFlip.Text = ReadLine(x + 1, CamallLines);
//                        if (CamallLines[x].StartsWith("  mirror:"))
//                            txtMirror.Text = ReadLine(x + 1, CamallLines);
//                        if (CamallLines[x].StartsWith("  sensor:"))
//                            txtSensor.Text = ReadLine(x + 1, CamallLines);
//                    }
//                }
//            }
//        }
//        private void Form1_Load(object sender, EventArgs e)
//        {
//            string IPsettings = "settings.conf";
//            if (!File.Exists(IPsettings))
//            {
//                File.Create(IPsettings).Dispose();
//                using (StreamWriter sw = new StreamWriter(IPsettings))
//                {
//                    sw.WriteLine("openipc:192.168.0.1");
//                    sw.WriteLine("nvr:192.168.0.1");
//                    sw.WriteLine("radxa:192.168.0.1");
//                }
//                MessageBox.Show($"File {IPsettings} not found and default created!");
//            }

//            var IPsettingsReaderallLines = new List<string>(File.ReadAllLines(IPsettings));

//            string value1 = ReadLine(1, IPsettingsReaderallLines);
//            string value2 = ReadLine(2, IPsettingsReaderallLines);
//            string value3 = ReadLine(3, IPsettingsReaderallLines);

//            OpenIPCIP = value1.Split(':')[1].Trim();
//            NVRIP = value2.Split(':')[1].Trim();
//            RadxaIP = value3.Split(':')[1].Trim();
//            txtIP.Text = OpenIPCIP;

//            // Populate ComboBox1
//            ComboBox1.Items.Clear();
//            string[] frequencies5GHz = {
//        "5180 MHz [36]", "5200 MHz [40]", "5220 MHz [44]", "5240 MHz [48]",
//        "5260 MHz [52]", "5280 MHz [56]", "5300 MHz [60]", "5320 MHz [64]",
//        "5500 MHz [100]", "5520 MHz [104]", "5540 MHz [108]", "5560 MHz [112]",
//        "5580 MHz [116]", "5600 MHz [120]", "5620 MHz [124]", "5640 MHz [128]",
//        "5660 MHz [132]", "5680 MHz [136]", "5700 MHz [140]", "5720 MHz [144]",
//        "5745 MHz [149]", "5765 MHz [153]", "5785 MHz [157]", "5805 MHz [161]",
//        "5825 MHz [165]", "5845 MHz [169]", "5865 MHz [173]", "5885 MHz [177]"
//    };
//            ComboBox1.Items.AddRange(frequencies5GHz);
//            ComboBox1.Text = "Select 5.8GHz Frequency";

//            // Populate ComboBox2
//            ComboBox2.Items.Clear();
//            for (int i = 1; i <= 20; i++)
//            {
//                ComboBox2.Items.Add(i.ToString());
//            }
//            ComboBox2.Items.Add("25");
//            ComboBox2.Items.Add("30");
//            ComboBox2.Items.Add("35");
//            ComboBox2.Items.Add("40");
//            ComboBox2.Items.Add("45");
//            ComboBox2.Items.Add("50");
//            ComboBox2.Items.Add("55");
//            ComboBox2.Items.Add("58");
//            ComboBox2.Items.Add("60");
//            ComboBox2.Items.Add("63");
//            ComboBox2.Text = "Select 5.8GHz TX Power";

//            // Populate ComboBox3
//            ComboBox3.Items.Clear();
//            string[] frequencies2_4GHz = {
//        "2412 MHz [1]", "2417 MHz [2]", "2422 MHz [3]", "2427 MHz [4]",
//        "2432 MHz [5]", "2437 MHz [6]", "2442 MHz [7]", "2447 MHz [8]",
//        "2452 MHz [9]", "2457 MHz [10]", "2462 MHz [11]", "2467 MHz [12]",
//        "2472 MHz [13]", "2484 MHz [14]"
//    };
//            ComboBox3.Items.AddRange(frequencies2_4GHz);
//            ComboBox3.Text = "Select 2.4GHz Frequency";

//            // Populate ComboBox4
//            ComboBox4.Items.Clear();
//            for (int i = 20; i <= 55; i += 5)
//            {
//                ComboBox4.Items.Add(i.ToString());
//            }
//            ComboBox4.Text = "Select 2.4GHz TX Power";

//            // Populate ComboBox5
//            ComboBox5.Items.Clear();
//            for (int i = 0; i <= 31; i++)
//            {
//                ComboBox5.Items.Add(i.ToString());
//            }
//            ComboBox5.Text = "Select MCS INDEX";

//            // Populate ComboBox6
//            ComboBox6.Items.Clear();
//            ComboBox6.Items.Add("0");
//            ComboBox6.Items.Add("1");
//            ComboBox6.Text = "Select STBC";

//            // Populate ComboBox7
//            ComboBox7.Items.Clear();
//            ComboBox7.Items.Add("0");
//            ComboBox7.Items.Add("1");
//            ComboBox7.Text = "Select LDPC";

//            // Populate ComboBox8
//            ComboBox8.Items.Clear();
//            for (int i = 1; i <= 15; i++)
//            {
//                ComboBox8.Items.Add(i.ToString());
//            }
//            ComboBox8.Text = "Select FEC K";

//            // Populate ComboBox9
//            ComboBox9.Items.Clear();
//            for (int i = 1; i <= 15; i++)
//            {
//                ComboBox9.Items.Add(i.ToString());
//            }
//            ComboBox9.Text = "Select FEC N";

//            // Populate cmbResolution
//            cmbResolution.Items.Clear();
//            string[] resolutions = {
//        "1280x720", "1456x816", "1920x1080", "2104x1184",
//        "2208x1248", "2240x1264", "2312x1304", "2512x1416",
//        "2560x1440", "2560x1920", "3200x1800", "3840x2160"
//    };
//            cmbResolution.Items.AddRange(resolutions);
//            cmbResolution.Text = "Select Resolution";

//            // Populate cmbOSD
//            cmbOSD.Items.Clear();
//            cmbOSD.Items.Add("simple");
//            cmbOSD.Items.Add("none");
//            cmbOSD.Text = "Select OSD";

//            // Populate cmbFormat
//            cmbFormat.Items.Clear();
//            cmbFormat.Items.Add("frame");
//            cmbFormat.Items.Add("stream");
//            cmbFormat.Text = "Select Format";

//            // Populate cmbResolutionVRX
//            cmbResolutionVRX.Items.Clear();
//            string[] resolutionsVRX = {
//        "720p60", "1080p60", "1024x768x60", "1366x768x60",
//        "1280x1024x60", "1600x1200x60", "2560x1440x30"
//    };
//            cmbResolutionVRX.Items.AddRange(resolutionsVRX);
//            cmbResolutionVRX.Text = "Select Resolution";

//            // Populate cmbFPS
//            cmbFPS.Items.Clear();
//            for (int i = 20; i <= 120; i += 10)
//            {
//                cmbFPS.Items.Add(i.ToString());
//            }
//            cmbFPS.Text = "Select FPS";

//            // Populate cmbCodec
//            cmbCodec.Items.Clear();
//            cmbCodec.Items.Add("h264");
//            cmbCodec.Items.Add("h265");
//            cmbCodec.Text = "Select Codec";

//            // Populate cmbCodecVRX
//            cmbCodecVRX.Items.Clear();
//            cmbCodecVRX.Items.Add("h264");
//            cmbCodecVRX.Items.Add("h265");
//            cmbCodecVRX.Text = "Select Codec";

//            // Populate cmbBitrate
//            cmbBitrate.Items.Clear();
//            for (int i = 1024; i <= 19456; i += 1024)
//            {
//                cmbBitrate.Items.Add(i.ToString());
//            }
//            cmbBitrate.Text = "Select Bitrate";

//            // Populate cmbExposure
//            cmbExposure.Items.Clear();
//            int[] exposureValues = { 5, 6, 8, 10, 11, 12, 14, 16, 33, 50 };
//            foreach (var value in exposureValues)
//            {
//                cmbExposure.Items.Add(value.ToString());
//            }
//            cmbExposure.Text = "Select Exposure";

//            // Populate cmbContrast
//            cmbContrast.Items.Clear();
//            for (int i = 1; i <= 100; i += 10)
//            {
//                cmbContrast.Items.Add(i.ToString());
//            }
//            cmbContrast.Text = "Select Contrast";

//            // Populate cmbHue
//            cmbHue.Items.Clear();
//            for (int i = 1; i <= 100; i += 10)
//            {
//                cmbHue.Items.Add(i.ToString());
//            }
//            cmbHue.Text = "Select Hue";

//            // Populate cmbSaturation
//            cmbSaturation.Items.Clear();
//            for (int i = 1; i <= 100; i += 10)
//            {
//                cmbSaturation.Items.Add(i.ToString());
//            }
//            cmbSaturation.Text = "Select Saturation";

//            // Populate cmbLuminance
//            cmbLuminance.Items.Clear();
//            for (int i = 1; i <= 100; i += 10)
//            {
//                cmbLuminance.Items.Add(i.ToString());
//            }
//            cmbLuminance.Text = "Select Luminance";

//            // Populate cmbFlip
//            cmbFlip.Items.Clear();
//            cmbFlip.Items.Add("true");
//            cmbFlip.Items.Add("false");
//            cmbFlip.Text = "Select Flip";

//            // Populate cmbMirror
//            cmbMirror.Items.Clear();
//            cmbMirror.Items.Add("true");
//            cmbMirror.Items.Add("false");
//            cmbMirror.Text = "Select Mirror";

//            // Populate cmbDenoise
//            cmbDenoise.Items.Clear();
//            cmbDenoise.Items.Add("true");
//            cmbDenoise.Items.Add("false");
//            cmbDenoise.Text = "Select Denoise";

//            // Populate cmbIR
//            cmbIR.Items.Clear();
//            cmbIR.Items.Add("true");
//            cmbIR.Items.Add("false");
//            cmbIR.Text = "Select IR";

//            // Populate cmbRegion
//            cmbRegion.Items.Clear();
//            string[] regions = { "region1", "region2", "region3" };
//            cmbRegion.Items.AddRange(regions);
//            cmbRegion.Text = "Select Region";

//            // Set default values
//            ComboBox1.SelectedIndex = 0;
//            ComboBox2.SelectedIndex = 0;
//            ComboBox3.SelectedIndex = 0;
//            ComboBox4.SelectedIndex = 0;
//            ComboBox5.SelectedIndex = 0;
//            ComboBox6.SelectedIndex = 0;
//            ComboBox7.SelectedIndex = 0;
//            ComboBox8.SelectedIndex = 0;
//            ComboBox9.SelectedIndex = 0;
//            cmbResolution.SelectedIndex = 0;
//            cmbOSD.SelectedIndex = 0;
//            cmbFormat.SelectedIndex = 0;
//            cmbResolutionVRX.SelectedIndex = 0;
//            cmbFPS.SelectedIndex = 0;
//            cmbCodec.SelectedIndex = 0;
//            cmbCodecVRX.SelectedIndex = 0;
//            cmbBitrate.SelectedIndex = 0;
//            cmbExposure.SelectedIndex = 0;
//            cmbContrast.SelectedIndex = 0;
//            cmbHue.SelectedIndex = 0;
//            cmbSaturation.SelectedIndex = 0;
//            cmbLuminance.SelectedIndex = 0;
//            cmbFlip.SelectedIndex = 0;
//            cmbMirror.SelectedIndex = 0;
//            cmbDenoise.SelectedIndex = 0;
//            cmbIR.SelectedIndex = 0;
//            cmbRegion.SelectedIndex = 0;
//        }
//        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
//        {
//            string sInput = ComboBox1.SelectedItem.ToString();
//            string last4Letter = sInput.Substring(sInput.Length - 4).Replace("]", "").Replace("[", "");

//            if (rBtnRadxaZero3w.Checked)
//            {
//                txtFrequency.Text = "wifi_channel = " + last4Letter;
//            }
//            else
//            {
//                txtFrequency.Text = "channel=" + last4Letter;
//                txtFreq24.Text = "frequency=";
//            }
//        }
//        private void ComboBox2_SelectedIndexChanged(object sender, EventArgs e)
//        {
//            if (rBtnRadxaZero3w.Checked)
//            {
//                txtPower.Text = "options 88XXau_wfb rtw_tx_pwr_idx_override=" + ComboBox2.SelectedItem.ToString();
//            }
//            else
//            {
//                txtPower.Text = "driver_txpower_override=" + ComboBox2.SelectedItem.ToString();
//            }
//        }

//        private void ComboBox3_SelectedIndexChanged(object sender, EventArgs e)
//        {
//            string sInput = ComboBox3.SelectedItem.ToString();
//            string last3Letter = sInput.Substring(sInput.Length - 3).Replace("]", "").Replace("[", "");
//            txtFrequency.Text = "channel=";
//            txtFreq24.Text = "frequency=" + last3Letter;
//        }

//        private void ComboBox4_SelectedIndexChanged(object sender, EventArgs e)
//        {
//            txtPower24.Text = "txpower=" + ComboBox4.SelectedItem.ToString();
//        }

//        private void ComboBox5_SelectedIndexChanged(object sender, EventArgs e)
//        {
//            txtMCS.Text = "mcs_index=" + ComboBox5.SelectedItem.ToString();
//        }

//        private void ComboBox6_SelectedIndexChanged(object sender, EventArgs e)
//        {
//            txtSTBC.Text = "stbc=" + ComboBox6.SelectedItem.ToString();
//        }

//        private void ComboBox7_SelectedIndexChanged(object sender, EventArgs e)
//        {
//            txtLDPC.Text = "ldpc=" + ComboBox7.SelectedItem.ToString();
//        }

//        private void ComboBox8_SelectedIndexChanged(object sender, EventArgs e)
//        {
//            txtFECK.Text = "fec_k=" + ComboBox8.SelectedItem.ToString();
//        }

//        private void ComboBox9_SelectedIndexChanged(object sender, EventArgs e)
//        {
//            txtFECN.Text = "fec_n=" + ComboBox9.SelectedItem.ToString();
//        }
//        private void cmbResolution_SelectedIndexChanged(object sender, EventArgs e)
//        {
//            txtResolution.Text = "  size: " + cmbResolution.SelectedItem.ToString();

//            switch (cmbResolution.SelectedItem.ToString())
//            {
//                case "1280x720":
//                case "1456x816":
//                    txtFPS.Text = "  fps: 120";
//                    cmbFPS.Text = "120";
//                    txtExposure.Text = "  exposure: 8";
//                    cmbExposure.Text = "8";
//                    break;

//                case "2208x1248":
//                case "1920x1080":
//                    txtFPS.Text = "  fps: 90";
//                    cmbFPS.Text = "90";
//                    txtExposure.Text = "  exposure: 11";
//                    cmbExposure.Text = "11";
//                    break;

//                case "2104x1184":
//                    txtFPS.Text = "  fps: 100";
//                    cmbFPS.Text = "100";
//                    txtExposure.Text = "  exposure: 10";
//                    cmbExposure.Text = "10";
//                    break;

//                case "2240x1264":
//                    txtFPS.Text = "  fps: 60";
//                    cmbFPS.Text = "60";
//                    txtExposure.Text = "  exposure: 16";
//                    cmbExposure.Text = "16";
//                    break;

//                case "2512x1416":
//                    txtFPS.Text = "  fps: 70";
//                    cmbFPS.Text = "70";
//                    txtExposure.Text = "  exposure: 14";
//                    cmbExposure.Text = "14";
//                    break;

//                case "2312x1304":
//                    txtFPS.Text = "  fps: 80";
//                    cmbFPS.Text = "80";
//                    txtExposure.Text = "  exposure: 12";
//                    cmbExposure.Text = "12";
//                    break;

//                case "2560x1440":
//                    txtFPS.Text = "  fps: 60";
//                    cmbFPS.Text = "60";
//                    txtExposure.Text = "  exposure: 16";
//                    cmbExposure.Text = "16";
//                    break;

//                case "2560x1920":
//                case "3200x1800":
//                    txtFPS.Text = "  fps: 30";
//                    cmbFPS.Text = "30";
//                    txtExposure.Text = "  exposure: 33";
//                    cmbExposure.Text = "33";
//                    break;

//                case "3840x2160":
//                    txtFPS.Text = "  fps: 20";
//                    cmbFPS.Text = "20";
//                    txtExposure.Text = "  exposure: 50";
//                    cmbExposure.Text = "50";
//                    break;
//            }
//        }
//        private void cmbFPS_SelectedIndexChanged(object sender, EventArgs e)
//        {
//            txtFPS.Text = "  fps: " + cmbFPS.SelectedItem.ToString();
//            txtExposure.Text = "  exposure: " + Math.Floor(1000.0 / Convert.ToInt32(cmbFPS.SelectedItem.ToString()));
//        }

//        private void cmbCodec_SelectedIndexChanged(object sender, EventArgs e)
//        {
//            txtEncode.Text = "  codec: " + cmbCodec.SelectedItem.ToString();
//        }

//        private void cmbBitrate_SelectedIndexChanged(object sender, EventArgs e)
//        {
//            txtBitrate.Text = "  bitrate: " + cmbBitrate.SelectedItem.ToString();
//        }

//        private void cmbExposure_SelectedIndexChanged(object sender, EventArgs e)
//        {
//            txtExposure.Text = "  exposure: " + cmbExposure.SelectedItem.ToString();
//        }

//        private void cmbContrast_SelectedIndexChanged(object sender, EventArgs e)
//        {
//            txtContrast.Text = "  contrast: " + cmbContrast.SelectedItem.ToString();
//        }

//        private void cmbHue_SelectedIndexChanged(object sender, EventArgs e)
//        {
//            txtHue.Text = "  hue: " + cmbHue.SelectedItem.ToString();
//        }

//        private void cmbSaturation_SelectedIndexChanged(object sender, EventArgs e)
//        {
//            txtSaturation.Text = "  saturation: " + cmbSaturation.SelectedItem.ToString();
//        }

//        private void cmbLuminance_SelectedIndexChanged(object sender, EventArgs e)
//        {
//            txtLuminance.Text = "  luminance: " + cmbLuminance.SelectedItem.ToString();
//        }

//        private void cmbFlip_SelectedIndexChanged(object sender, EventArgs e)
//        {
//            txtFlip.Text = "  flip: " + cmbFlip.SelectedItem.ToString();
//        }

//        private void cmbMirror_SelectedIndexChanged(object sender, EventArgs e)
//        {
//            txtMirror.Text = "  mirror: " + cmbMirror.SelectedItem.ToString();
//        }

//        private bool IsValidIP(string ipAddress)
//        {
//            return System.Text.RegularExpressions.Regex.IsMatch(ipAddress,
//                @"^(25[0-5]|2[0-4]\d|[0-1]?\d?\d)(\.(25[0-5]|2[0-4]\d|[0-1]?\d?\d)){3}$");
//        }
//        private void btnReboot_Click(object sender, EventArgs e)
//        {
//            string externFile = "extern.bat";
//            if (!File.Exists(externFile))
//            {
//                MessageBox.Show("File " + externFile + " not found!");
//                return;
//            }

//            if (IsValidIP(txtIP.Text))
//            {
//                using (Process process = new Process())
//                {
//                    process.StartInfo.UseShellExecute = false;
//                    process.StartInfo.FileName = externFile;
//                    process.StartInfo.Arguments = $"rb {txtIP.Text} {txtPassword.Text}";
//                    process.StartInfo.RedirectStandardOutput = false;
//                    process.Start();
//                }

//                btnSaveReboot.Enabled = false;
//                btnReboot.Enabled = false;

//                // Clear the text fields
//                txtResolution.Text = "";
//                txtFPS.Text = "";
//                txtEncode.Text = "";
//                txtBitrate.Text = "";
//                txtExposure.Text = "";
//                txtContrast.Text = "";
//                txtSaturation.Text = "";
//                txtHue.Text = "";
//                txtLuminance.Text = "";
//                txtFlip.Text = "";
//                txtMirror.Text = "";
//                txtSensor.Text = "";
//                txtSerial.Text = "";
//                txtBaud.Text = "";
//                txtRouter.Text = "";
//                txtMCSTLM.Text = "";
//                txtAggregate.Text = "";
//                txtRC_CHANNEL.Text = "";
//                txtFrequency.Text = "";
//                txtPower.Text = "";
//                txtFreq24.Text = "";
//                txtPower24.Text = "";
//                txtMCS.Text = "";
//                txtSTBC.Text = "";
//                txtLDPC.Text = "";
//                txtFECK.Text = "";
//                txtFECN.Text = "";
//            }
//            else
//            {
//                MessageBox.Show("Please enter a valid IP address");
//            }
//        }
//        private void cmbSerial_SelectedIndexChanged(object sender, EventArgs e)
//        {
//            txtSerial.Text = "serial=" + cmbSerial.SelectedItem.ToString();
//        }

//        private void cmbBaud_SelectedIndexChanged(object sender, EventArgs e)
//        {
//            txtBaud.Text = "baud=" + cmbBaud.SelectedItem.ToString();
//        }

//        private void cmbRouter_SelectedIndexChanged(object sender, EventArgs e)
//        {
//            if (cmbRouter.Text == "MAVFWD")
//            {
//                txtRouter.Text = "router=0";
//            }
//            else if (cmbRouter.Text == "MAVLINK-ROUTER")
//            {
//                txtRouter.Text = "router=1";
//            }
//            else if (cmbRouter.Text == "MSPOSD")
//            {
//                txtRouter.Text = "router=2";
//            }
//        }
//        private void cmbMCSTLM_SelectedIndexChanged(object sender, EventArgs e)
//        {
//            txtMCSTLM.Text = "mcs_index=" + cmbMCSTLM.SelectedItem.ToString();
//        }

//        private void txtSaveTLM_Click(object sender, EventArgs e)
//        {
//            if (!string.IsNullOrEmpty(txtSerial.Text))
//            {
//                string telemetry = "telemetry.conf";
//                if (!System.IO.File.Exists(telemetry))
//                {
//                    MessageBox.Show("File " + telemetry + " not found!" + Environment.NewLine + "Install the latest version of Putty and try again.");
//                    return;
//                }

//                string[] lines = System.IO.File.ReadAllLines(telemetry);

//                for (int x = 0; x < lines.Length; x++)
//                {
//                    if (lines[x].StartsWith("serial="))
//                    {
//                        lines[x] = txtSerial.Text;
//                    }
//                    if (lines[x].StartsWith("baud="))
//                    {
//                        lines[x] = txtBaud.Text;
//                    }
//                    if (lines[x].StartsWith("router="))
//                    {
//                        lines[x] = txtRouter.Text;
//                    }
//                    if (lines[x].StartsWith("mcs_index="))
//                    {
//                        lines[x] = txtMCSTLM.Text;
//                    }
//                    if (lines[x].StartsWith("aggregate="))
//                    {
//                        lines[x] = txtAggregate.Text;
//                    }
//                    if (lines[x].StartsWith("channels="))
//                    {
//                        lines[x] = txtRC_CHANNEL.Text;
//                    }
//                }

//                System.IO.File.WriteAllLines(telemetry, lines);
//                MessageBox.Show("Settings saved successfully", "OpenIPC", MessageBoxButtons.OK, MessageBoxIcon.Information);
//            }
//        }
//        private void txtSaveVRX_Click(object sender, EventArgs e)
//        {
//            if (!string.IsNullOrEmpty(txtResolutionVRX.Text))
//            {
//                if (rBtnRadxaZero3w.Checked)
//                {
//                    string setdisplay = "screen-mode";
//                    if (!System.IO.File.Exists(setdisplay))
//                    {
//                        MessageBox.Show("File " + setdisplay + " not found!" + Environment.NewLine + "Install the latest version of Putty and try again.");
//                        return;
//                    }

//                    string[] setdisplaylines = System.IO.File.ReadAllLines(setdisplay);
//                    for (int x = 0; x < setdisplaylines.Length; x++)
//                    {
//                        setdisplaylines[x] = txtResolutionVRX.Text + "@" + txtCodecVRX.Text;
//                    }
//                    System.IO.File.WriteAllLines(setdisplay, setdisplaylines);
//                }
//                else
//                {
//                    string vdec = "vdec.conf";
//                    if (!System.IO.File.Exists(vdec))
//                    {
//                        MessageBox.Show("File " + vdec + " not found!" + Environment.NewLine + "Install the latest version of Putty and try again.");
//                        return;
//                    }

//                    string[] lines = System.IO.File.ReadAllLines(vdec);
//                    for (int y = 0; y < lines.Length; y++)
//                    {
//                        if (lines[y].StartsWith("mode="))
//                        {
//                            lines[y] = txtResolutionVRX.Text;
//                        }
//                        if (lines[y].StartsWith("codec="))
//                        {
//                            lines[y] = txtCodecVRX.Text;
//                        }
//                        if (lines[y].StartsWith("format="))
//                        {
//                            lines[y] = txtFormat.Text;
//                        }
//                        if (lines[y].StartsWith("port="))
//                        {
//                            lines[y] = txtPortVRX.Text;
//                        }
//                        if (lines[y].StartsWith("mavlink_port="))
//                        {
//                            lines[y] = txtMavlinkVRX.Text;
//                        }
//                        if (lines[y].StartsWith("osd="))
//                        {
//                            lines[y] = txtOSD.Text;
//                        }
//                        if (lines[y].StartsWith("extra="))
//                        {
//                            lines[y] = txtExtras.Text;
//                        }
//                    }
//                    System.IO.File.WriteAllLines(vdec, lines);
//                }
//                MessageBox.Show("Settings saved successfully", "OpenIPC", MessageBoxButtons.OK, MessageBoxIcon.Information);
//            }
//        }
//        private void cmbResolutionVRX_SelectedIndexChanged(object sender, EventArgs e)
//        {
//            if (rBtnRadxaZero3w.Checked)
//            {
//                txtResolutionVRX.Text = cmbResolutionVRX.SelectedItem.ToString();
//            }
//            else
//            {
//                txtResolutionVRX.Text = "mode=" + cmbResolutionVRX.SelectedItem.ToString();
//            }
//        }

//        private void cmbCodecVRX_SelectedIndexChanged(object sender, EventArgs e)
//        {
//            if (rBtnRadxaZero3w.Checked)
//            {
//                txtCodecVRX.Text = cmbCodecVRX.SelectedItem.ToString();
//            }
//            else
//            {
//                txtCodecVRX.Text = "codec=" + cmbCodecVRX.SelectedItem.ToString();
//            }
//        }

//        private void cmbOSD_SelectedIndexChanged(object sender, EventArgs e)
//        {
//            txtOSD.Text = "osd=" + cmbOSD.SelectedItem.ToString();
//        }

//        private void cmbFormat_SelectedIndexChanged(object sender, EventArgs e)
//        {
//            txtFormat.Text = "format=" + cmbFormat.SelectedItem.ToString();
//        }

//        private void btnGenerateKeys_Click(object sender, EventArgs e)
//        {
//            string externFile = "extern.bat";
//            if (!System.IO.File.Exists(externFile))
//            {
//                MessageBox.Show("File " + externFile + " not found!");
//                return;
//            }

//            if (IsValidIP(txtIP.Text))
//            {
//                var process = new System.Diagnostics.Process();
//                process.StartInfo.UseShellExecute = false;
//                process.StartInfo.FileName = externFile;
//                process.StartInfo.Arguments = "keysgen " + txtIP.Text + " " + txtPassword.Text;
//                process.StartInfo.RedirectStandardOutput = false;
//                process.Start();
//            }
//            else
//            {
//                MessageBox.Show("Please enter a valid IP address");
//            }
//        }
//        private void btnReceiveKeys_Click(object sender, EventArgs e)
//        {
//            string externFile = "extern.bat";
//            if (!System.IO.File.Exists(externFile))
//            {
//                MessageBox.Show("File " + externFile + " not found!");
//                return;
//            }

//            if (IsValidIP(txtIP.Text))
//            {
//                var process = new System.Diagnostics.Process();
//                process.StartInfo.UseShellExecute = false;
//                process.StartInfo.FileName = externFile;
//                process.StartInfo.Arguments = rBtnCam.Checked
//                    ? "keysdlcam " + txtIP.Text + " " + txtPassword.Text
//                    : "keysdlgs " + txtIP.Text + " " + txtPassword.Text;
//                process.StartInfo.RedirectStandardOutput = false;
//                process.Start();
//            }
//            else
//            {
//                MessageBox.Show("Please enter a valid IP address");
//            }
//        }

//        private void btnSendKeys_Click(object sender, EventArgs e)
//        {
//            string externFile = "extern.bat";
//            if (!System.IO.File.Exists(externFile))
//            {
//                MessageBox.Show("File " + externFile + " not found!");
//                return;
//            }

//            if (IsValidIP(txtIP.Text))
//            {
//                var process = new System.Diagnostics.Process();
//                process.StartInfo.UseShellExecute = false;
//                process.StartInfo.FileName = externFile;
//                process.StartInfo.Arguments = rBtnCam.Checked
//                    ? "keysulcam " + txtIP.Text + " " + txtPassword.Text
//                    : "keysulgs " + txtIP.Text + " " + txtPassword.Text;
//                process.StartInfo.RedirectStandardOutput = false;
//                process.Start();
//            }
//            else
//            {
//                MessageBox.Show("Please enter a valid IP address");
//            }
//        }

//        private void btnUpdate_Click(object sender, EventArgs e)
//        {
//            string externFile = "extern.bat";
//            if (!System.IO.File.Exists(externFile))
//            {
//                MessageBox.Show("File " + externFile + " not found!");
//                return;
//            }

//            if (IsValidIP(txtIP.Text))
//            {
//                var process = new System.Diagnostics.Process();
//                process.StartInfo.UseShellExecute = false;
//                process.StartInfo.FileName = externFile;
//                process.StartInfo.Arguments = "sysup " + txtIP.Text + " " + txtPassword.Text;
//                process.StartInfo.RedirectStandardOutput = false;
//                process.Start();
//            }
//            else
//            {
//                MessageBox.Show("Please enter a valid IP address");
//            }
//        }

//        private void btnRestartWFB_Click(object sender, EventArgs e)
//        {
//            string externFile = "extern.bat";
//            if (!System.IO.File.Exists(externFile))
//            {
//                MessageBox.Show("File " + externFile + " not found!");
//                return;
//            }

//            if (IsValidIP(txtIP.Text))
//            {
//                var process = new System.Diagnostics.Process();
//                process.StartInfo.UseShellExecute = false;
//                process.StartInfo.FileName = externFile;
//                process.StartInfo.Arguments = "rswfb " + txtIP.Text + " " + txtPassword.Text;
//                process.StartInfo.RedirectStandardOutput = false;
//                process.Start();
//            }
//            else
//            {
//                MessageBox.Show("Please enter a valid IP address");
//            }
//        }
//        private void btnRestartMajestic_Click(object sender, EventArgs e)
//        {
//            string externFile = "extern.bat";
//            if (!System.IO.File.Exists(externFile))
//            {
//                MessageBox.Show("File " + externFile + " not found!");
//                return;
//            }

//            if (IsValidIP(txtIP.Text))
//            {
//                var process = new System.Diagnostics.Process();
//                process.StartInfo.UseShellExecute = false;
//                process.StartInfo.FileName = externFile;
//                process.StartInfo.Arguments = "rsmaj " + txtIP.Text + " " + txtPassword.Text;
//                process.StartInfo.RedirectStandardOutput = false;
//                process.Start();
//            }
//            else
//            {
//                MessageBox.Show("Please enter a valid IP address");
//            }
//        }

//        private void rBtnNVR_CheckedChanged(object sender, EventArgs e)
//        {
//            txtIP.Text = NVRIP; // Assuming NVRIP is a class variable
//            var rb = sender as RadioButton;

//            if (rb.Checked)
//            {
//                rb.BackColor = Color.Gold;
//                rb.ForeColor = Color.Black;
//            }
//            else
//            {
//                rb.BackColor = Color.FromArgb(45, 45, 45);
//                rb.ForeColor = Color.White;
//            }

//            // Reset all input fields
//            txtResolution.Text = "";
//            txtFPS.Text = "";
//            txtEncode.Text = "";
//            txtBitrate.Text = "";
//            txtExposure.Text = "";
//            txtContrast.Text = "";
//            txtSaturation.Text = "";
//            txtHue.Text = "";
//            txtLuminance.Text = "";
//            txtFlip.Text = "";
//            txtMirror.Text = "";
//            txtSensor.Text = "";
//            txtSerial.Text = "";
//            txtBaud.Text = "";
//            txtRouter.Text = "";
//            txtMCSTLM.Text = "";
//            txtAggregate.Text = "";
//            txtRC_CHANNEL.Text = "";
//            txtFrequency.Text = "";
//            txtPower.Text = "";
//            txtFreq24.Text = "";
//            txtPower24.Text = "";
//            txtMCS.Text = "";
//            txtSTBC.Text = "";
//            txtLDPC.Text = "";
//            txtFECK.Text = "";
//            txtFECN.Text = "";
//            txtResolutionVRX.Text = "";
//            txtCodecVRX.Text = "";
//            txtOSD.Text = "";
//            txtFormat.Text = "";
//            txtPortVRX.Text = "";
//            txtMavlinkVRX.Text = "";
//            txtExtras.Text = "";

//            // Update visibility and text of buttons and labels
//            btnResetCam.Visible = true;
//            btnResetCam.Text = "Reset NVR";
//            btnSaveReboot.Enabled = false;
//            btnReboot.Enabled = false;
//            Label8.Visible = true;
//            Label9.Visible = true;
//            Label10.Visible = false;
//            cmbVersion.Visible = false;
//            txtResX.Visible = true;
//            txtResY.Visible = true;
//            checkCustomRes.Visible = true;
//            btnSendKeys.Text = "Send gs.key";
//            btnGenerateKeys.Visible = true;
//            btnUpdate.Visible = false;
//            btnRuby.Visible = false;
//            btnWFB.Visible = false;
//            btnOfflinefw.Visible = true;
//            btnOfflinefw.Text = "Update NVR";
//            txtSOC.Visible = true;
//            cmbVersion.Visible = true;
//            Button2.Visible = false;
//            Button3.Visible = false;
//            btnSensor.Visible = false;
//            btnBinBackup.Visible = false;
//            btnDriver.Visible = false;
//            btnDriverBackup.Visible = false;
//            cmbSensor.Visible = false;
//            txtDriver.Visible = false;
//            btnMSPGS.Visible = false;
//            btnMAVGS.Visible = false;
//            rBtnMode1.Visible = false;
//            rBtnMode2.Visible = false;
//            btnReset.Visible = false;
//            btnAddButtons.Visible = false;
//            txtSaveVRX.Visible = false;
//            btnUART0.Visible = false;
//            btnUART0OFF.Visible = false;
//            btnExtra.Visible = false;
//            btnRestartWFB.Visible = true;
//            btnRestartMajestic.Visible = false;
//            txtSaveCam.Visible = false;
//            txtSaveTLM.Visible = false;
//            ComboBox3.Visible = true;
//            ComboBox4.Visible = true;
//            ComboBox5.Visible = false;
//            ComboBox6.Visible = false;
//            ComboBox7.Visible = false;
//            ComboBox8.Visible = false;
//            ComboBox9.Visible = false;
//            cmbOSD.Visible = true;
//            cmbFormat.Visible = true;
//            txtLDPC.Visible = false;
//            txtFECK.Visible = false;
//            txtFECN.Visible = false;
//            txtOSD.Visible = true;
//            txtFormat.Visible = true;
//            txtFreq24.Visible = true;
//            txtPower24.Visible = true;
//            txtPortVRX.Visible = true;
//            txtMavlinkVRX.Visible = true;
//            txtExtras.Visible = true;
//            btnMSP.Visible = false;
//            btnFontsINAV.Visible = false;
//            btnOnboardREC.Visible = false;
//            rBtnRECON.Visible = false;
//            rBtnRECOFF.Visible = false;
//            Label2.Visible = true;
//            txtMCS.ReadOnly = false;
//            txtSTBC.ReadOnly = false;

//            // Clear and populate ComboBox1 with frequency options
//            ComboBox1.Items.Clear();
//            ComboBox1.Items.AddRange(new object[]
//            {
//        "5180 MHz [36]",
//        "5200 MHz [40]",
//        "5220 MHz [44]",
//        "5240 MHz [48]",
//        "5260 MHz [52]",
//        "5280 MHz [56]",
//        "5300 MHz [60]",
//        "5320 MHz [64]",
//        "5500 MHz [100]",
//        "5520 MHz [104]",
//        "5540 MHz [108]",
//        "5560 MHz [112]",
//        "5580 MHz [116]",
//        "5600 MHz [120]",
//        "5620 MHz [124]",
//        "5640 MHz [128]",
//        "5660 MHz [132]",
//        "5680 MHz [136]",
//        "5700 MHz [140]",
//        "5720 MHz [144]",
//        "5745 MHz [149]",
//        "5765 MHz [153]",
//        "5785 MHz [157]",
//        "5805 MHz [161]",
//        "5825 MHz [165]",
//        "5845 MHz [169]",
//        "5865 MHz [173]",
//        "5885 MHz [177]"
//            });
//            ComboBox1.Text = "Select 5.8GHz Frequency";

//            // Clear and populate cmbResolutionVRX with resolution options
//            cmbResolutionVRX.Items.Clear();
//            cmbResolutionVRX.Items.AddRange(new object[]
//            {
//        "720p60",
//        "1080p60",
//        "1024x768x60",
//        "1366x768x60",
//        "1280x1024x60",
//        "1600x1200x60",
//        "2560x1440x30"
//            });
//            cmbResolutionVRX.Text = "Select Resolution";

//            // Clear and populate cmbCodecVRX with codec options
//            cmbCodecVRX.Items.Clear();
//            cmbCodecVRX.Items.AddRange(new object[]
//            {
//        "h264",
//        "h265"
//            });
//            cmbCodecVRX.Text = "Select Codec";

//            // Set default password
//            txtPassword.Text = "12345";
//        }
//        private void rBtnCam_CheckedChanged(object sender, EventArgs e)
//        {
//            txtIP.Text = OpenIPCIP; // Assuming OpenIPCIP is a class variable
//            RadioButton rb = sender as RadioButton;

//            if (rb.Checked)
//            {
//                rb.BackColor = Color.Gold;
//                rb.ForeColor = Color.Black;
//            }
//            else
//            {
//                rb.BackColor = Color.FromArgb(45, 45, 45);
//                rb.ForeColor = Color.White;
//            }

//            // Reset all input fields
//            txtResolution.Text = "";
//            txtFPS.Text = "";
//            txtEncode.Text = "";
//            txtBitrate.Text = "";
//            txtExposure.Text = "";
//            txtContrast.Text = "";
//            txtSaturation.Text = "";
//            txtHue.Text = "";
//            txtLuminance.Text = "";
//            txtFlip.Text = "";
//            txtMirror.Text = "";
//            txtSensor.Text = "";
//            txtSerial.Text = "";
//            txtBaud.Text = "";
//            txtRouter.Text = "";
//            txtMCSTLM.Text = "";
//            txtAggregate.Text = "";
//            txtRC_CHANNEL.Text = "";
//            txtFrequency.Text = "";
//            txtPower.Text = "";
//            txtFreq24.Text = "";
//            txtPower24.Text = "";
//            txtMCS.Text = "";
//            txtSTBC.Text = "";
//            txtLDPC.Text = "";
//            txtFECK.Text = "";
//            txtFECN.Text = "";
//            txtResolutionVRX.Text = "";
//            txtCodecVRX.Text = "";
//            txtOSD.Text = "";
//            txtFormat.Text = "";
//            txtPortVRX.Text = "";
//            txtMavlinkVRX.Text = "";
//            txtExtras.Text = "";

//            // Update visibility and text of buttons and labels
//            btnResetCam.Visible = true;
//            btnResetCam.Text = "Reset Camera";
//            btnSaveReboot.Enabled = false;
//            btnReboot.Enabled = false;
//            Label8.Visible = true;
//            Label9.Visible = true;
//            Label10.Visible = true;
//            cmbVersion.Visible = true;
//            txtResX.Visible = true;
//            txtResY.Visible = true;
//            checkCustomRes.Visible = true;
//            btnSendKeys.Text = "Send drone.key";
//            btnGenerateKeys.Visible = false;
//            btnUpdate.Visible = false;
//            btnRuby.Visible = false;
//            btnWFB.Visible = false;
//            btnOfflinefw.Visible = true;
//            btnOfflinefw.Text = "Update Camera";
//            txtSOC.Visible = true;
//            cmbVersion.Visible = true;
//            Button2.Visible = true;
//            Button3.Visible = true;
//            btnSensor.Visible = true;
//            btnBinBackup.Visible = true;
//            btnDriver.Visible = true;
//            btnDriverBackup.Visible = true;
//            cmbSensor.Visible = true;
//            txtDriver.Visible = true;
//            txtSaveVRX.Visible = false;
//            btnMSPGS.Visible = false;
//            btnMAVGS.Visible = false;
//            rBtnMode1.Visible = false;
//            rBtnMode2.Visible = false;
//            btnReset.Visible = false;
//            btnAddButtons.Visible = false;
//            txtSaveVRX.Visible = false;
//            btnUART0.Visible = true;
//            btnUART0OFF.Visible = true;
//            btnExtra.Visible = true;
//            btnRestartWFB.Visible = true;
//            btnRestartMajestic.Visible = true;
//            txtSaveCam.Visible = false;
//            txtSaveTLM.Visible = false;
//            ComboBox3.Visible = true;
//            ComboBox4.Visible = true;
//            ComboBox5.Visible = true;
//            ComboBox6.Visible = true;
//            ComboBox7.Visible = true;
//            ComboBox8.Visible = true;
//            ComboBox9.Visible = true;
//            cmbOSD.Visible = true;
//            cmbFormat.Visible = true;
//            txtLDPC.Visible = true;
//            txtFECK.Visible = true;
//            txtFECN.Visible = true;
//            txtOSD.Visible = true;
//            txtFormat.Visible = true;
//            txtFreq24.Visible = true;
//            txtPower24.Visible = true;
//            txtPortVRX.Visible = true;
//            txtMavlinkVRX.Visible = true;
//            txtExtras.Visible = true;
//            btnMSP.Visible = true;
//            btnFontsINAV.Visible = true;
//            btnOnboardREC.Visible = true;
//            rBtnRECON.Visible = true;
//            rBtnRECOFF.Visible = true;
//            Label2.Visible = true;
//            txtMCS.ReadOnly = true;
//            txtSTBC.ReadOnly = true;

//            // Clear and populate ComboBox1 with frequency options
//            ComboBox1.Items.Clear();
//            ComboBox1.Items.AddRange(new object[]
//            {
//        "5180 MHz [36]",
//        "5200 MHz [40]",
//        "5220 MHz [44]",
//        "5240 MHz [48]",
//        "5260 MHz [52]",
//        "5280 MHz [56]",
//        "5300 MHz [60]",
//        "5320 MHz [64]",
//        "5500 MHz [100]",
//        "5520 MHz [104]",
//        "5540 MHz [108]",
//        "5560 MHz [112]",
//        "5580 MHz [116]",
//        "5600 MHz [120]",
//        "5620 MHz [124]",
//        "5640 MHz [128]",
//        "5660 MHz [132]",
//        "5680 MHz [136]",
//        "5700 MHz [140]",
//        "5720 MHz [144]",
//        "5745 MHz [149]",
//        "5765 MHz [153]",
//        "5785 MHz [157]",
//        "5805 MHz [161]",
//        "5825 MHz [165]",
//        "5845 MHz [169]",
//        "5865 MHz [173]",
//        "5885 MHz [177]"
//            });
//            ComboBox1.Text = "Select 5.8GHz Frequency";

//            // Clear and populate cmbResolutionVRX with resolution options
//            cmbResolutionVRX.Items.Clear();
//            cmbResolutionVRX.Items.AddRange(new object[]
//            {
//        "720p60",
//        "1080p60",
//        "1024x768x60",
//        "1366x768x60",
//        "1280x1024x60",
//        "1600x1200x60",
//        "2560x1440x30"
//            });
//            cmbResolutionVRX.Text = "Select Resolution";

//            // Clear and populate cmbCodecVRX with codec options
//            cmbCodecVRX.Items.Clear();
//            cmbCodecVRX.Items.AddRange(new object[]
//            {
//        "h264",
//        "h265"
//            });
//            cmbCodecVRX.Text = "Select Codec";

//            // Set default password
//            txtPassword.Text = "12345";
//        }
//        private void rBtnRadxaZero3w_CheckedChanged(object sender, EventArgs e)
//        {
//            txtIP.Text = RadxaIP; // Assuming RadxaIP is a class variable
//            RadioButton rb = sender as RadioButton;

//            if (rb.Checked)
//            {
//                rb.BackColor = Color.Gold;
//                rb.ForeColor = Color.Black;
//            }
//            else
//            {
//                rb.BackColor = Color.FromArgb(45, 45, 45);
//                rb.ForeColor = Color.White;
//            }

//            // Reset all input fields
//            txtResolution.Text = "";
//            txtFPS.Text = "";
//            txtEncode.Text = "";
//            txtBitrate.Text = "";
//            txtExposure.Text = "";
//            txtContrast.Text = "";
//            txtSaturation.Text = "";
//            txtHue.Text = "";
//            txtLuminance.Text = "";
//            txtFlip.Text = "";
//            txtMirror.Text = "";
//            txtSensor.Text = "";
//            txtSerial.Text = "";
//            txtBaud.Text = "";
//            txtRouter.Text = "";
//            txtMCSTLM.Text = "";
//            txtAggregate.Text = "";
//            txtRC_CHANNEL.Text = "";
//            txtFrequency.Text = "";
//            txtPower.Text = "";
//            txtFreq24.Text = "";
//            txtPower24.Text = "";
//            txtMCS.Text = "";
//            txtSTBC.Text = "";
//            txtLDPC.Text = "";
//            txtFECK.Text = "";
//            txtFECN.Text = "";
//            txtResolutionVRX.Text = "";
//            txtCodecVRX.Text = "";
//            txtOSD.Text = "";
//            txtFormat.Text = "";
//            txtPortVRX.Text = "";
//            txtMavlinkVRX.Text = "";
//            txtExtras.Text = "";

//            // Update visibility and text of buttons and labels
//            btnResetCam.Visible = false;
//            btnSaveReboot.Enabled = false;
//            btnReboot.Enabled = false;
//            Label8.Visible = false;
//            Label9.Visible = false;
//            Label10.Visible = false;
//            cmbVersion.Visible = false;
//            txtResX.Visible = false;
//            txtResY.Visible = false;
//            checkCustomRes.Visible = false;
//            btnSendKeys.Text = "Send gs.key";
//            txtSOC.Visible = false;
//            btnGenerateKeys.Visible = true;
//            btnUpdate.Visible = false;
//            btnRuby.Visible = false;
//            btnWFB.Visible = false;
//            btnOfflinefw.Visible = false;
//            Button2.Visible = false;
//            Button3.Visible = false;
//            btnSensor.Visible = false;
//            btnBinBackup.Visible = false;
//            btnDriver.Visible = false;
//            btnDriverBackup.Visible = false;
//            cmbSensor.Visible = false;
//            txtDriver.Visible = false;
//            btnMSPGS.Visible = true;
//            btnMAVGS.Visible = true;
//            rBtnMode1.Visible = true;
//            rBtnMode2.Visible = true;
//            btnReset.Visible = true;
//            btnAddButtons.Visible = true;
//            txtSaveVRX.Visible = false;
//            btnUART0.Visible = false;
//            btnUART0OFF.Visible = false;
//            btnExtra.Visible = false;
//            btnRestartWFB.Visible = false;
//            btnRestartMajestic.Visible = false;
//            txtSaveCam.Visible = false;
//            txtSaveTLM.Visible = false;
//            ComboBox3.Visible = false;
//            ComboBox4.Visible = false;
//            ComboBox5.Visible = false;
//            ComboBox6.Visible = false;
//            ComboBox7.Visible = false;
//            ComboBox8.Visible = false;
//            ComboBox9.Visible = false;
//            cmbOSD.Visible = false;
//            cmbFormat.Visible = false;
//            txtLDPC.Visible = false;
//            txtFECK.Visible = false;
//            txtFECN.Visible = false;
//            txtOSD.Visible = false;
//            txtFormat.Visible = false;
//            txtFreq24.Visible = false;
//            txtPower24.Visible = false;
//            txtPortVRX.Visible = false;
//            txtMavlinkVRX.Visible = false;
//            txtExtras.Visible = true;
//            btnMSP.Visible = false;
//            btnFontsINAV.Visible = false;
//            btnOnboardREC.Visible = false;
//            rBtnRECON.Visible = false;
//            rBtnRECOFF.Visible = false;
//            Label2.Visible = true;
//            txtMCS.ReadOnly = false;
//            txtSTBC.ReadOnly = false;

//            // Clear and populate ComboBox1 with frequency options
//            ComboBox1.Items.Clear();
//            ComboBox1.Items.AddRange(new object[]
//            {
//        "2412 MHz [1]",
//        "2417 MHz [2]",
//        "2422 MHz [3]",
//        "2427 MHz [4]",
//        "2432 MHz [5]",
//        "2437 MHz [6]",
//        "2442 MHz [7]",
//        "2447 MHz [8]",
//        "2452 MHz [9]",
//        "2457 MHz [10]",
//        "2462 MHz [11]",
//        "2467 MHz [12]",
//        "2472 MHz [13]",
//        "2484 MHz [14]",
//        "5180 MHz [36]",
//        "5200 MHz [40]",
//        "5220 MHz [44]",
//        "5240 MHz [48]",
//        "5260 MHz [52]",
//        "5280 MHz [56]",
//        "5300 MHz [60]",
//        "5320 MHz [64]",
//        "5500 MHz [100]",
//        "5520 MHz [104]",
//        "5540 MHz [108]",
//        "5560 MHz [112]",
//        "5580 MHz [116]",
//        "5600 MHz [120]",
//        "5620 MHz [124]",
//        "5640 MHz [128]",
//        "5660 MHz [132]",
//        "5680 MHz [136]",
//        "5700 MHz [140]",
//        "5720 MHz [144]",
//        "5745 MHz [149]",
//        "5765 MHz [153]",
//        "5785 MHz [157]",
//        "5805 MHz [161]",
//        "5825 MHz [165]",
//        "5845 MHz [169]",
//        "5865 MHz [173]",
//        "5885 MHz [177]"
//            });
//            ComboBox1.Text = "Select 5.8GHz Frequency";

//            // Clear and populate cmbResolutionVRX with resolution options
//            cmbResolutionVRX.Items.Clear();
//            cmbResolutionVRX.Items.AddRange(new object[]
//            {
//        "1280x720",
//        "1920x1080"
//            });
//            cmbResolutionVRX.Text = "Select Resolution";

//            // Clear and populate cmbCodecVRX with codec options
//            cmbCodecVRX.Items.Clear();
//            cmbCodecVRX.Items.AddRange(new object[]
//            {
//        "20",
//        "30",
//        "50",
//        "60",
//        "90",
//        "100",
//        "110",
//        "120"
//            });
//            cmbCodecVRX.Text = "Select FPS";

//            // Set default password
//            txtPassword.Text = "root";
//        }
//        #region InterOP

//        [StructLayout(LayoutKind.Sequential)]
//        private struct NMHDR
//        {
//            public int HWND;
//            public int idFrom;
//            public int code;

//            public override string ToString()
//            {
//                return string.Format("Hwnd: {0}, ControlID: {1}, Code: {2}", HWND, idFrom, code);
//            }
//        }

//        private const int TCN_FIRST = unchecked((int)0xFFFFFFFFFFFFFDDA);
//        private const int TCN_SELCHANGING = (TCN_FIRST - 2);

//        private const int WM_USER = 0x400;
//        private const int WM_NOTIFY = 0x4E;
//        private const int WM_REFLECT = WM_USER + 0x1C00;

//        #endregion

//        private void btnUART0_Click(object sender, EventArgs e)
//        {
//            ExecuteUARTCommand("UART0on");
//        }

//        private void btnUART0OFF_Click(object sender, EventArgs e)
//        {
//            ExecuteUARTCommand("UART0off");
//        }

//        private void ExecuteUARTCommand(string command)
//        {
//            string externFile = "extern.bat";
//            if (!File.Exists(externFile))
//            {
//                MessageBox.Show($"File {externFile} not found!");
//                return;
//            }

//            if (IsValidIP(txtIP.Text))
//            {
//                using (Process process = new Process())
//                {
//                    process.StartInfo.UseShellExecute = false;
//                    process.StartInfo.FileName = externFile;
//                    process.StartInfo.Arguments = $"{command} {txtIP.Text} {txtPassword.Text}";
//                    process.StartInfo.RedirectStandardOutput = false;
//                    process.Start();
//                }
//            }
//            else
//            {
//                MessageBox.Show("Please enter a valid IP address");
//            }
//        }

//        private void MoveSelectedRadioButton(int deltaX, int deltaY)
//        {
//            RadioButton[] radioButtons = {
//            RadioButton1, RadioButton2, RadioButton3, RadioButton4, RadioButton5,
//            RadioButton6, RadioButton7, RadioButton8, RadioButton9, RadioButton10,
//            RadioButton11, RadioButton12, RadioButton13, RadioButton14, RadioButton15,
//            RadioButton16, RadioButton17, RadioButton18
//        };

//            foreach (var radioButton in radioButtons)
//            {
//                if (radioButton.Checked)
//                {
//                    radioButton.Left += deltaX;
//                    radioButton.Top += deltaY;
//                }
//            }
//        }

//        private void btnLEFT_Click(object sender, EventArgs e)
//        {
//            MoveSelectedRadioButton(-2, 0);
//        }

//        private void btnRIGHT_Click(object sender, EventArgs e)
//        {
//            MoveSelectedRadioButton(2, 0);
//        }

//        private void btnUP_Click(object sender, EventArgs e)
//        {
//            MoveSelectedRadioButton(0, -2);
//        }

//        private void btnDOWN_Click(object sender, EventArgs e)
//        {
//            MoveSelectedRadioButton(0, 2);
//        }

//        // Implement the IsValidIP method based on your requirements
//        private bool IsValidIP(string ip)
//        {
//            // Your logic to validate IP address
//            return true; // Example: return true or false based on validation
//        }
//        private void Button1_Click(object sender, EventArgs e)
//        {
//            string vdec = "vdec.conf";
//            if (!File.Exists(vdec))
//            {
//                MessageBox.Show($"File {vdec} not found!\nInstall the latest version of Putty and try again.");
//                return;
//            }

//            ResetRadioButtonPositions();

//            string[] lines = File.ReadAllLines(vdec);
//            for (int y = 0; y < lines.Length; y++)
//            {
//                if (lines[y].StartsWith("osd_elements="))
//                {
//                    lines[y] = BuildOsdElementsString();
//                }
//            }
//            File.WriteAllLines(vdec, lines);
//            MessageBox.Show("Settings saved successfully", "OpenIPC", MessageBoxButtons.OK, MessageBoxIcon.Information);
//        }

//        private void ResetRadioButtonPositions()
//        {
//            RadioButton[] radioButtons = {
//            RadioButton1, RadioButton2, RadioButton3, RadioButton4, RadioButton5,
//            RadioButton6, RadioButton7, RadioButton8, RadioButton9, RadioButton10,
//            RadioButton11, RadioButton12, RadioButton13, RadioButton14, RadioButton15,
//            RadioButton16, RadioButton17, RadioButton18
//        };

//            CheckBox[] checkBoxes = {
//            CheckBox1, CheckBox2, CheckBox3, CheckBox4, CheckBox5,
//            CheckBox6, CheckBox7, CheckBox8, CheckBox9, CheckBox10,
//            CheckBox11, CheckBox12, CheckBox13, CheckBox14, CheckBox15,
//            CheckBox16, CheckBox17, CheckBox18
//        };

//            for (int i = 0; i < radioButtons.Length; i++)
//            {
//                if (!checkBoxes[i].Checked)
//                {
//                    radioButtons[i].Left = 0;
//                }
//            }
//        }

//        private string BuildOsdElementsString()
//        {
//            string osdElements = "osd_elements=\"";
//            RadioButton[] radioButtons = {
//            RadioButton1, RadioButton2, RadioButton3, RadioButton4, RadioButton5,
//            RadioButton6, RadioButton7, RadioButton8, RadioButton9, RadioButton10,
//            RadioButton11, RadioButton12, RadioButton13, RadioButton14, RadioButton15,
//            RadioButton16, RadioButton17, RadioButton18
//        };

//            for (int i = 0; i < radioButtons.Length; i++)
//            {
//                osdElements += $"-osd_ele{i + 1}x {radioButtons[i].Left * 2.5} -osd_ele{i + 1}y {radioButtons[i].Top * 2.5} ";
//            }

//            // Adjust RadioButton18's position
//            osdElements += $"-osd_ele18x {(RadioButton18.Left - 128) * 2.5} -osd_ele18y {(RadioButton18.Top - 124) * 2.5}\"";
//            return osdElements;
//        }

//        private void btnScan_Click(object sender, EventArgs e)
//        {
//            lblScan.Text = "Available IP Addresses on your network:";
//            BackgroundWorker1.RunWorkerAsync();
//        }
//        private void BackgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
//        {
//            Ping ping;
//            PingReply pingReply;

//            Thread.Sleep(500); // Optional delay before starting the scan

//            Parallel.For(0, 254, i =>
//            {
//                ping = new Ping();
//                pingReply = ping.Send(txtScan.Text + i.ToString());

//                // Use Invoke to update UI from the background thread
//                if (pingReply.Status == IPStatus.Success)
//                {
//                    // Safely update the label on the UI thread
//                    this.Invoke(new Action(() =>
//                    {
//                        lblScan.Text += $"{Environment.NewLine}{txtScan.Text}{i}";
//                    }));
//                }
//            });

//            // Inform the user that the scan is complete
//            MessageBox.Show("Scan completed");
//        }
//        private void btnScan_Click(object sender, EventArgs e)
//        {
//            lblScan.Text = "Available IP Addresses on your network:";
//            BackgroundWorker1.RunWorkerAsync();
//        }
//        private void btnUART0_Click(object sender, EventArgs e)
//        {
//            string externFile = "extern.bat";
//            if (!File.Exists(externFile))
//            {
//                MessageBox.Show($"File {externFile} not found!");
//                return;
//            }

//            if (IsValidIP(txtIP.Text))
//            {
//                using (Process process = new Process())
//                {
//                    process.StartInfo.UseShellExecute = false;
//                    process.StartInfo.FileName = externFile;
//                    process.StartInfo.Arguments = $"UART0on {txtIP.Text} {txtPassword.Text}";
//                    process.StartInfo.RedirectStandardOutput = false;
//                    process.Start();
//                }
//            }
//            else
//            {
//                MessageBox.Show("Please enter a valid IP address");
//            }
//        }

//        private void btnUART0OFF_Click(object sender, EventArgs e)
//        {
//            string externFile = "extern.bat";
//            if (!File.Exists(externFile))
//            {
//                MessageBox.Show($"File {externFile} not found!");
//                return;
//            }

//            if (IsValidIP(txtIP.Text))
//            {
//                using (Process process = new Process())
//                {
//                    process.StartInfo.UseShellExecute = false;
//                    process.StartInfo.FileName = externFile;
//                    process.StartInfo.Arguments = $"UART0off {txtIP.Text} {txtPassword.Text}";
//                    process.StartInfo.RedirectStandardOutput = false;
//                    process.Start();
//                }
//            }
//            else
//            {
//                MessageBox.Show("Please enter a valid IP address");
//            }
//        }

//        private void MoveRadioButton(Control[] radioButtons, int offsetX, int offsetY)
//        {
//            foreach (var radioButton in radioButtons)
//            {
//                if (radioButton is RadioButton rb && rb.Checked)
//                {
//                    rb.Left += offsetX;
//                    rb.Top += offsetY;
//                }
//            }
//        }

//        private void btnLEFT_Click(object sender, EventArgs e)
//        {
//            MoveRadioButton(new Control[] { RadioButton1, RadioButton2, RadioButton3, RadioButton4, RadioButton5, RadioButton6, RadioButton7, RadioButton8, RadioButton9, RadioButton10, RadioButton11, RadioButton12, RadioButton13, RadioButton14, RadioButton15, RadioButton16, RadioButton17 }, -2, 0);
//        }

//        private void btnRIGHT_Click(object sender, EventArgs e)
//        {
//            MoveRadioButton(new Control[] { RadioButton1, RadioButton2, RadioButton3, RadioButton4, RadioButton5, RadioButton6, RadioButton7, RadioButton8, RadioButton9, RadioButton10, RadioButton11, RadioButton12, RadioButton13, RadioButton14, RadioButton15, RadioButton16, RadioButton17 }, 2, 0);
//        }

//        private void btnUP_Click(object sender, EventArgs e)
//        {
//            MoveRadioButton(new Control[] { RadioButton1, RadioButton2, RadioButton3, RadioButton4, RadioButton5, RadioButton6, RadioButton7, RadioButton8, RadioButton9, RadioButton10, RadioButton11, RadioButton12, RadioButton13, RadioButton14, RadioButton15, RadioButton16, RadioButton17, RadioButton18 }, 0, -2);
//        }

//        private void btnDOWN_Click(object sender, EventArgs e)
//        {
//            MoveRadioButton(new Control[] { RadioButton1, RadioButton2, RadioButton3, RadioButton4, RadioButton5, RadioButton6, RadioButton7, RadioButton8, RadioButton9, RadioButton10, RadioButton11, RadioButton12, RadioButton13, RadioButton14, RadioButton15, RadioButton16, RadioButton17, RadioButton18 }, 0, 2);
//        }

//        private void Button1_Click(object sender, EventArgs e)
//        {
//            string vdecFilePath = "vdec.conf";
//            if (!File.Exists(vdecFilePath))
//            {
//                MessageBox.Show($"File {vdecFilePath} not found!\nInstall the latest version of Putty and try again.");
//                return;
//            }

//            for (int i = 1; i <= 18; i++)
//            {
//                CheckBox cb = (CheckBox)this.Controls[$"CheckBox{i}"];
//                RadioButton rb = (RadioButton)this.Controls[$"RadioButton{i}"];
//                rb.Visible = cb.Checked;
//            }

//            var lines = File.ReadAllLines(vdecFilePath);
//            for (int y = 0; y < lines.Length; y++)
//            {
//                if (lines[y].StartsWith("osd_elements="))
//                {
//                    lines[y] = $"osd_elements=\"-osd_ele1x {RadioButton1.Left * 2.5} -osd_ele1y {RadioButton1.Top * 2.5} " +
//                                $"-osd_ele2x {RadioButton2.Left * 2.5} -osd_ele2y {RadioButton2.Top * 2.5} " +
//                                $"-osd_ele3x {RadioButton3.Left * 2.5} -osd_ele3y {RadioButton3.Top * 2.5} " +
//                                $"-osd_ele4x {RadioButton4.Left * 2.5} -osd_ele4y {RadioButton4.Top * 2.5} " +
//                                $"-osd_ele5x {RadioButton5.Left * 2.5} -osd_ele5y {RadioButton5.Top * 2.5} " +
//                                $"-osd_ele6x {RadioButton6.Left * 2.5} -osd_ele6y {RadioButton6.Top * 2.5} " +
//                                $"-osd_ele7x {RadioButton7.Left * 2.5} -osd_ele7y {RadioButton7.Top * 2.5} " +
//                                $"-osd_ele8x {RadioButton8.Left * 2.5} -osd_ele8y {RadioButton8.Top * 2.5} " +
//                                $"-osd_ele9x {RadioButton9.Left * 2.5} -osd_ele9y {RadioButton9.Top * 2.5} " +
//                                $"-osd_ele10x {RadioButton10.Left * 2.5} -osd_ele10y {RadioButton10.Top * 2.5} " +
//                                $"-osd_ele11x {RadioButton11.Left * 2.5} -osd_ele11y {RadioButton11.Top * 2.5} " +
//                                $"-osd_ele12x {RadioButton12.Left * 2.5} -osd_ele12y {RadioButton12.Top * 2.5} " +
//                                $"-osd_ele13x {RadioButton13.Left * 2.5} -osd_ele13y {RadioButton13.Top * 2.5} " +
//                                $"-osd_ele14x {RadioButton14.Left * 2.5} -osd_ele14y {RadioButton14.Top * 2.5} " +
//                                $"-osd_ele15x {RadioButton15.Left * 2.5} -osd_ele15y {RadioButton15.Top * 2.5} " +
//                                $"-osd_ele16x {RadioButton16.Left * 2.5} -osd_ele16y {RadioButton16.Top * 2.5} " +
//                                $"-osd_ele17x {RadioButton17.Left * 2.5} -osd_ele17y {RadioButton17.Top * 2.5} " +
//                                $"-osd_ele18x {(RadioButton18.Left - 128) * 2.5} -osd_ele18y {(RadioButton18.Top - 124) * 2.5}\"";
//                }
//            }

//            File.WriteAllLines(vdecFilePath, lines);
//            MessageBox.Show("Settings saved successfully", "OpenIPC", MessageBoxButtons.OK, MessageBoxIcon.Information);
//        }

//        private void btnScan_Click(object sender, EventArgs e)
//        {
//            lblScan.Text = "Available IP Addresses on your network:";
//            BackgroundWorker1.RunWorkerAsync();
//        }

//        private void BackgroundWorker1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
//        {
//            Ping ping = new Ping();
//            Thread.Sleep(500);

//            Parallel.For(0, 254, i =>
//            {
//                PingReply pingReply = ping.Send(txtScan.Text + i.ToString());
//                this.BeginInvoke(new Action(() =>
//                {
//                    if (pingReply.Status == IPStatus.Success)
//                    {
//                        lblScan.Text += $"{Environment.NewLine}{txtScan.Text}{i}";
//                    }
//                }));
//            });
//            MessageBox.Show("Scan completed");
//        }

//        private void CheckBox_CheckedChanged(object sender, EventArgs e)
//        {
//            CheckBox checkBox = sender as CheckBox;
//            int index = int.Parse(checkBox.Name.Substring(8)); // CheckBox1, CheckBox2, etc.
//            RadioButton radioButton = (RadioButton)this.Controls[$"RadioButton{index}"];
//            radioButton.Visible = checkBox.Checked;
//        }
//        private void btnSensor_Click(object sender, EventArgs e)
//        {
//            string externFile = "extern.bat";
//            if (!File.Exists(externFile))
//            {
//                MessageBox.Show($"File {externFile} not found!");
//                return;
//            }

//            if (!string.IsNullOrEmpty(txtResolution.Text) && cmbSensor.Text != "Select Sensor")
//            {
//                string majesticFile = "majestic.yaml";
//                if (!File.Exists(majesticFile))
//                {
//                    MessageBox.Show($"File {majesticFile} not found!\nInstall the latest version of Putty and try again.");
//                    return;
//                }

//                string[] lines = File.ReadAllLines(majesticFile);
//                for (int x = 0; x < lines.Length; x++)
//                {
//                    if (lines[x].StartsWith("  sensorConfig: "))
//                    {
//                        lines[x] = txtSensor.Text;
//                    }
//                }
//                File.WriteAllLines(majesticFile, lines);
//            }

//            if (IsValidIP(txtIP.Text) && cmbSensor.Text != "Select Sensor")
//            {
//                using (Process process = new Process())
//                {
//                    process.StartInfo.UseShellExecute = false;
//                    process.StartInfo.FileName = externFile;
//                    process.StartInfo.Arguments = $"binup {txtIP.Text} {txtPassword.Text} {cmbSensor.Text}.bin";
//                    process.StartInfo.RedirectStandardOutput = false;
//                    process.Start();
//                }
//            }
//            else
//            {
//                MessageBox.Show("Please enter a valid IP address and select a sensor calibration file to install");
//            }
//        }

//        private void btnDriver_Click(object sender, EventArgs e)
//        {
//            string externFile = "extern.bat";
//            if (!File.Exists(externFile))
//            {
//                MessageBox.Show($"File {externFile} not found!");
//                return;
//            }

//            if (IsValidIP(txtIP.Text))
//            {
//                using (Process process = new Process())
//                {
//                    process.StartInfo.UseShellExecute = false;
//                    process.StartInfo.FileName = externFile;
//                    process.StartInfo.Arguments = $"koup {txtIP.Text} {txtPassword.Text} {txtDriver.Text}";
//                    process.StartInfo.RedirectStandardOutput = false;
//                    process.Start();
//                }
//            }
//            else
//            {
//                MessageBox.Show("Please enter a valid IP address");
//            }
//        }
//        private void btnBinBackup_Click(object sender, EventArgs e)
//        {
//            string externFile = "extern.bat";
//            if (!File.Exists(externFile))
//            {
//                MessageBox.Show($"File {externFile} not found!");
//                return;
//            }

//            if (IsValidIP(txtIP.Text))
//            {
//                using (Process process = new Process())
//                {
//                    process.StartInfo.UseShellExecute = false;
//                    process.StartInfo.FileName = externFile;
//                    process.StartInfo.Arguments = $"bindl {txtIP.Text} {txtPassword.Text} {cmbSensor.Text}.bin";
//                    process.StartInfo.RedirectStandardOutput = false;
//                    process.Start();
//                }
//            }
//            else
//            {
//                MessageBox.Show("Please enter a valid IP address");
//            }
//        }

//        private void btnDriverBackup_Click(object sender, EventArgs e)
//        {
//            string externFile = "extern.bat";
//            if (!File.Exists(externFile))
//            {
//                MessageBox.Show($"File {externFile} not found!");
//                return;
//            }

//            if (IsValidIP(txtIP.Text))
//            {
//                using (Process process = new Process())
//                {
//                    process.StartInfo.UseShellExecute = false;
//                    process.StartInfo.FileName = externFile;
//                    process.StartInfo.Arguments = $"kodl {txtIP.Text} {txtPassword.Text} {txtDriver.Text}";
//                    process.StartInfo.RedirectStandardOutput = false;
//                    process.Start();
//                }
//            }
//            else
//            {
//                MessageBox.Show("Please enter a valid IP address");
//            }
//        }

//        private void Button2_Click(object sender, EventArgs e)
//        {
//            string externFile = "extern.bat";
//            if (!File.Exists(externFile))
//            {
//                MessageBox.Show($"File {externFile} not found!");
//                return;
//            }

//            if (IsValidIP(txtIP.Text))
//            {
//                using (Process process = new Process())
//                {
//                    process.StartInfo.UseShellExecute = false;
//                    process.StartInfo.FileName = externFile;
//                    process.StartInfo.Arguments = $"shdl {txtIP.Text} {txtPassword.Text}";
//                    process.StartInfo.RedirectStandardOutput = false;
//                    process.Start();
//                }
//            }
//            else
//            {
//                MessageBox.Show("Please enter a valid IP address");
//            }
//        }
//        private void Button3_Click(object sender, EventArgs e)
//        {
//            string externFile = "extern.bat";
//            if (!File.Exists(externFile))
//            {
//                MessageBox.Show($"File {externFile} not found!");
//                return;
//            }

//            if (IsValidIP(txtIP.Text))
//            {
//                using (Process process = new Process())
//                {
//                    process.StartInfo.UseShellExecute = false;
//                    process.StartInfo.FileName = externFile;
//                    process.StartInfo.Arguments = $"shup {txtIP.Text} {txtPassword.Text}";
//                    process.StartInfo.RedirectStandardOutput = false;
//                    process.Start();
//                }
//            }
//            else
//            {
//                MessageBox.Show("Please enter a valid IP address");
//            }
//        }

//        private void checkCustomRes_CheckedChanged(object sender, EventArgs e)
//        {
//            if (checkCustomRes.Checked)
//            {
//                txtExtras.Text = $"extra=\"--bg-r 0 --bg-g 0 --bg-b 50 --ar manual --ar-w {txtResX.Text} --ar-h {txtResX.Text}\"";
//            }
//            else
//            {
//                txtExtras.Text = "extra=\"--bg-r 0 --bg-g 0 --bg-b 50\"";
//            }
//        }

//        private void Button4_Click(object sender, EventArgs e)
//        {
//            string externFile = "extern.bat";
//            if (!File.Exists(externFile))
//            {
//                MessageBox.Show($"File {externFile} not found!");
//                return;
//            }

//            if (IsValidIP(txtIP.Text))
//            {
//                using (Process process = new Process())
//                {
//                    process.StartInfo.UseShellExecute = false;
//                    process.StartInfo.FileName = externFile;
//                    process.StartInfo.Arguments = $"temp {txtIP.Text} {txtPassword.Text}";
//                    process.StartInfo.RedirectStandardOutput = false;
//                    process.Start();
//                }
//            }
//            else
//            {
//                MessageBox.Show("Please enter a valid IP address");
//            }
//        }
//        private void cmbAggregate_SelectedIndexChanged(object sender, EventArgs e)
//        {
//            txtAggregate.Text = $"aggregate={cmbAggregate.SelectedItem.ToString()}";
//        }

//        private void cmbRC_Channel_SelectedIndexChanged(object sender, EventArgs e)
//        {
//            txtRC_CHANNEL.Text = $"channels={cmbRC_Channel.SelectedItem.ToString()}";
//        }

//        private void btnRuby_Click(object sender, EventArgs e)
//        {
//            string externFile = "extern.bat";
//            if (!File.Exists(externFile))
//            {
//                MessageBox.Show($"File {externFile} not found!");
//                return;
//            }

//            if (IsValidIP(txtIP.Text))
//            {
//                using (Process process = new Process())
//                {
//                    process.StartInfo.UseShellExecute = false;
//                    process.StartInfo.FileName = externFile;
//                    process.StartInfo.Arguments = $"rubyfw {txtIP.Text} {txtPassword.Text}";
//                    process.StartInfo.RedirectStandardOutput = false;
//                    process.Start();
//                }
//            }
//            else
//            {
//                MessageBox.Show("Please enter a valid IP address");
//            }
//        }

//        private void btnWFB_Click(object sender, EventArgs e)
//        {
//            string externFile = "extern.bat";
//            if (!File.Exists(externFile))
//            {
//                MessageBox.Show($"File {externFile} not found!");
//                return;
//            }

//            if (IsValidIP(txtIP.Text))
//            {
//                using (Process process = new Process())
//                {
//                    process.StartInfo.UseShellExecute = false;
//                    process.StartInfo.FileName = externFile;
//                    process.StartInfo.Arguments = $"wfbfw {txtIP.Text} {txtPassword.Text}";
//                    process.StartInfo.RedirectStandardOutput = false;
//                    process.Start();
//                }
//            }
//            else
//            {
//                MessageBox.Show("Please enter a valid IP address");
//            }
//        }
//        private void btnOfflinefw_Click(object sender, EventArgs e)
//        {
//            if (IsValidIP(txtIP.Text) && cmbVersion.Text != "Select OpenIPC Version")
//            {
//                DownloadStart();
//            }
//            else
//            {
//                MessageBox.Show("Please enter a valid IP address and select an OpenIPC version to flash");
//            }
//        }
//        private void btnSaveReboot_Click(object sender, EventArgs e)
//        {
//            string vdecconf = "vdec.conf";
//            if (!File.Exists(vdecconf))
//            {
//                using (File.Create(vdecconf)) { }
//                File.WriteAllText(vdecconf, "dummy file ignore it");
//            }

//            if (!string.IsNullOrEmpty(txtFrequency.Text))
//            {
//                string wfbconf = "wfb.conf";
//                if (!File.Exists(wfbconf))
//                {
//                    MessageBox.Show("File " + wfbconf + " not found!" + Environment.NewLine + "Install the latest version of Putty and try again.");
//                    return;
//                }

//                string WFBfilePath = wfbconf;
//                string[] lines = File.ReadAllLines(WFBfilePath);
//                for (int x = 0; x < lines.Length; x++)
//                {
//                    if (rBtnRadxaZero3w.Checked)
//                    {
//                        string wfbng = "wifibroadcast.cfg";
//                        if (!File.Exists(wfbng))
//                        {
//                            MessageBox.Show("File " + wfbng + " not found!" + Environment.NewLine + "Install the latest version of Putty and try again.");
//                            return;
//                        }

//                        string[] WFBlines = File.ReadAllLines(wfbng);
//                        if (WFBlines[1].StartsWith("wifi_channel = "))
//                        {
//                            WFBlines[1] = txtFrequency.Text;
//                        }
//                        if (WFBlines[7].StartsWith("peer = 'connect://"))
//                        {
//                            WFBlines[7] = txtMCS.Text;
//                        }
//                        if (WFBlines[11].StartsWith("peer = 'connect://"))
//                        {
//                            WFBlines[11] = txtSTBC.Text;
//                        }
//                        File.WriteAllLines(wfbng, WFBlines);

//                        if (lines[x].StartsWith("options 88XXau_wfb rtw_tx_pwr_idx_override="))
//                        {
//                            lines[x] = txtPower.Text;
//                        }
//                    }
//                    else
//                    {
//                        if (lines[x].StartsWith("channel="))
//                        {
//                            lines[x] = txtFrequency.Text;
//                        }
//                        if (lines[x].StartsWith("driver_txpower_override="))
//                        {
//                            lines[x] = txtPower.Text;
//                        }
//                        if (lines[x].StartsWith("frequency="))
//                        {
//                            lines[x] = txtFreq24.Text;
//                        }
//                        if (lines[x].StartsWith("txpower="))
//                        {
//                            lines[x] = txtPower24.Text;
//                        }
//                        if (rBtnNVR.Checked)
//                        {
//                            if (lines[x].StartsWith("udp_addr="))
//                            {
//                                lines[x] = txtMCS.Text;
//                            }
//                            if (lines[x].StartsWith("udp_port="))
//                            {
//                                lines[x] = txtSTBC.Text;
//                            }
//                        }
//                        else
//                        {
//                            if (lines[x].StartsWith("stbc="))
//                            {
//                                lines[x] = txtSTBC.Text;
//                            }
//                            if (lines[x].StartsWith("ldpc="))
//                            {
//                                lines[x] = txtLDPC.Text;
//                            }
//                            if (lines[x].StartsWith("mcs_index="))
//                            {
//                                lines[x] = txtMCS.Text;
//                            }
//                            if (lines[x].StartsWith("fec_k="))
//                            {
//                                lines[x] = txtFECK.Text;
//                            }
//                            if (lines[x].StartsWith("fec_n="))
//                            {
//                                lines[x] = txtFECN.Text;
//                            }
//                        }
//                    }
//                }
//                File.WriteAllLines(WFBfilePath, lines);
//            }

//            if (!string.IsNullOrEmpty(txtSerial.Text))
//            {
//                string telemetry = "telemetry.conf";
//                if (!File.Exists(telemetry))
//                {
//                    MessageBox.Show("File " + telemetry + " not found!" + Environment.NewLine + "Install the latest version of Putty and try again.");
//                    return;
//                }

//                string TLMfilePath = telemetry;
//                string[] lines = File.ReadAllLines(TLMfilePath);
//                for (int x = 0; x < lines.Length; x++)
//                {
//                    if (lines[x].StartsWith("serial="))
//                    {
//                        lines[x] = txtSerial.Text;
//                    }
//                    if (lines[x].StartsWith("baud="))
//                    {
//                        lines[x] = txtBaud.Text;
//                    }
//                    if (lines[x].StartsWith("router="))
//                    {
//                        lines[x] = txtRouter.Text;
//                    }
//                    if (lines[x].StartsWith("mcs_index="))
//                    {
//                        lines[x] = txtMCSTLM.Text;
//                    }
//                    if (lines[x].StartsWith("aggregate="))
//                    {
//                        lines[x] = txtAggregate.Text;
//                    }
//                    if (lines[x].StartsWith("channels="))
//                    {
//                        lines[x] = txtRC_CHANNEL.Text;
//                    }
//                }
//                File.WriteAllLines(TLMfilePath, lines);
//            }

//            if (!string.IsNullOrEmpty(txtResolution.Text))
//            {
//                string majestic = "majestic.yaml";
//                if (!File.Exists(majestic))
//                {
//                    MessageBox.Show("File " + majestic + " not found!" + Environment.NewLine + "Install the latest version of Putty and try again.");
//                    return;
//                }

//                string CamfilePath = majestic;
//                string[] lines = File.ReadAllLines(CamfilePath);
//                for (int x = 0; x < lines.Length; x++)
//                {
//                    if (lines[x].StartsWith("  contrast: "))
//                    {
//                        lines[x] = txtContrast.Text;
//                    }
//                    if (lines[x].StartsWith("  hue: "))
//                    {
//                        lines[x] = txtHue.Text;
//                    }
//                    if (lines[x].StartsWith("  saturation:"))
//                    {
//                        lines[x] = txtSaturation.Text;
//                    }
//                    if (lines[x].StartsWith("  luminance: "))
//                    {
//                        lines[x] = txtLuminance.Text;
//                    }
//                    if (lines[x].StartsWith("  bitrate: "))
//                    {
//                        lines[x] = txtBitrate.Text;
//                    }
//                    if (lines[x].StartsWith("  codec: h26"))
//                    {
//                        lines[x] = txtEncode.Text;
//                    }
//                    if (lines[x].StartsWith("  size: "))
//                    {
//                        lines[x] = txtResolution.Text;
//                    }
//                    if (lines[x].StartsWith("  fps: "))
//                    {
//                        lines[x] = txtFPS.Text;
//                    }
//                    if (lines[x].StartsWith("  sensorConfig: "))
//                    {
//                        lines[x] = txtSensor.Text;
//                    }
//                    if (lines[x].StartsWith("  exposure: "))
//                    {
//                        lines[x] = txtExposure.Text;
//                    }
//                    if (lines[x].StartsWith("  mirror: "))
//                    {
//                        lines[x] = txtMirror.Text;
//                    }
//                    if (lines[x].StartsWith("  flip: "))
//                    {
//                        lines[x] = txtFlip.Text;
//                    }
//                }
//                File.WriteAllLines(CamfilePath, lines);
//            }

//            if (!string.IsNullOrEmpty(txtResolutionVRX.Text))
//            {
//                if (rBtnRadxaZero3w.Checked)
//                {
//                    string setdisplay = "screen-mode";
//                    if (!File.Exists(setdisplay))
//                    {
//                        MessageBox.Show("File " + setdisplay + " not found!" + Environment.NewLine + "Install the latest version of Putty and try again.");
//                        return;
//                    }

//                    string[] setdisplaylines = File.ReadAllLines(setdisplay);
//                    for (int x = 0; x < setdisplaylines.Length; x++)
//                    {
//                        setdisplaylines[x] = txtResolutionVRX.Text + "@" + txtCodecVRX.Text;
//                    }
//                    File.WriteAllLines(setdisplay, setdisplaylines);
//                }
//                else
//                {
//                    string vdec = "vdec.conf";
//                    if (!File.Exists(vdec) && rBtnNVR.Checked)
//                    {
//                        MessageBox.Show("File " + vdec + " not found!" + Environment.NewLine + "Install the latest version of Putty and try again.");
//                        return;
//                    }

//                    string VDECfilePath = vdec;
//                    string[] lines = File.ReadAllLines(VDECfilePath);
//                    for (int y = 0; y < lines.Length; y++)
//                    {
//                        if (lines[y].StartsWith("mode="))
//                        {
//                            lines[y] = txtResolutionVRX.Text;
//                        }
//                        if (lines[y].StartsWith("codec="))
//                        {
//                            lines[y] = txtCodecVRX.Text;
//                        }
//                        if (lines[y].StartsWith("format="))
//                        {
//                            lines[y] = txtFormat.Text;
//                        }
//                        if (lines[y].StartsWith("port="))
//                        {
//                            lines[y] = txtPortVRX.Text;
//                        }
//                        if (lines[y].StartsWith("mavlink_port="))
//                        {
//                            lines[y] = txtMavlinkVRX.Text;
//                        }
//                        if (lines[y].StartsWith("osd="))
//                        {
//                            lines[y] = txtOSD.Text;
//                        }
//                        if (lines[y].StartsWith("extra="))
//                        {
//                            lines[y] = txtExtras.Text;
//                        }
//                    }
//                    File.WriteAllLines(VDECfilePath, lines);
//                }
//            }
//            string @extern = "extern.bat";
//            if (!File.Exists(@extern))
//            {
//                MessageBox.Show("File " + @extern + " not found!");
//                return;
//            }

//            if (IsValidIP(txtIP.Text))
//            {
//                MessageBox.Show("All settings are saved and will now be uploaded." + Environment.NewLine + "Click [Enter] multiple times in the script." + Environment.NewLine + "OpenIPC will reboot after uploading is complete.", MessageBoxIcon.Information, "OpenIPC");

//                using (Process process = new Process())
//                {
//                    process.StartInfo.UseShellExecute = false;
//                    process.StartInfo.FileName = @extern;
//                    if (rBtnNVR.Checked)
//                    {
//                        process.StartInfo.Arguments = "ulvrxr " + txtIP.Text + " " + txtPassword.Text;
//                    }
//                    else if (rBtnRadxaZero3w.Checked)
//                    {
//                        process.StartInfo.Arguments = "ulwfbngr " + txtIP.Text + " " + txtPassword.Text;
//                    }
//                    else
//                    {
//                        process.StartInfo.Arguments = "ulr " + txtIP.Text + " " + txtPassword.Text;
//                    }
//                    process.StartInfo.RedirectStandardOutput = false;
//                    process.Start();
//                }
//            }
//            else
//            {
//                MessageBox.Show("Please enter a valid IP address");
//            }

//        }
//        private void btnMSPGS_Click(object sender, EventArgs e)
//        {
//            string @extern = "extern.bat";
//            if (!File.Exists(@extern))
//            {
//                MessageBox.Show("File " + @extern + " not found!");
//                return;
//            }

//            if (IsValidIP(txtIP.Text))
//            {
//                using (Process process = new Process())
//                {
//                    process.StartInfo.UseShellExecute = false;
//                    process.StartInfo.FileName = @extern;
//                    process.StartInfo.Arguments = "mspgs " + txtIP.Text + " " + txtPassword.Text;
//                    process.StartInfo.RedirectStandardOutput = false;
//                    process.Start();
//                }
//            }
//            else
//            {
//                MessageBox.Show("Please enter a valid IP address");
//            }
//        }
//        private void btnMAVGS_Click(object sender, EventArgs e)
//        {
//            string @extern = "extern.bat";
//            if (!File.Exists(@extern))
//            {
//                MessageBox.Show("File " + @extern + " not found!");
//                return;
//            }

//            if (IsValidIP(txtIP.Text))
//            {
//                using (Process process = new Process())
//                {
//                    process.StartInfo.UseShellExecute = false;
//                    process.StartInfo.FileName = @extern;

//                    if (rBtnMode1.Checked)
//                    {
//                        process.StartInfo.Arguments = "mavgs " + txtIP.Text + " " + txtPassword.Text;
//                    }
//                    else
//                    {
//                        process.StartInfo.Arguments = "mavgs2 " + txtIP.Text + " " + txtPassword.Text;
//                    }

//                    process.StartInfo.RedirectStandardOutput = false;
//                    process.Start();
//                }
//            }
//            else
//            {
//                MessageBox.Show("Please enter a valid IP address");
//            }
//        }
//        private void Timer1_Tick(object sender, EventArgs e)
//        {
//            using (Ping ping = new Ping())
//            {
//                PingReply pingReply = ping.Send(txtIP.Text);
//                if (pingReply.Status == IPStatus.Success)
//                {
//                    connected.BackColor = Color.FromArgb(30, 255, 30); // Green color for success
//                }
//                else
//                {
//                    connected.BackColor = Color.FromArgb(255, 30, 30); // Red color for failure
//                }
//            }

//            Timer1.Enabled = false; // Stop the timer after one tick
//        }

//        private void txtIP_TextChanged(object sender, EventArgs e)
//        {
//            if (IsValidIP(txtIP.Text))
//            {
//                Timer1.Enabled = true; // Enable the timer when the IP is valid
//            }
//        }

//        private void btnExtra_Click(object sender, EventArgs e)
//        {
//            string @extern = "extern.bat";
//            if (!File.Exists(@extern))
//            {
//                MessageBox.Show("File " + @extern + " not found!");
//                return;
//            }

//            if (IsValidIP(txtIP.Text))
//            {
//                using (Process process = new Process())
//                {
//                    process.StartInfo.UseShellExecute = false;
//                    process.StartInfo.FileName = @extern;
//                    process.StartInfo.Arguments = "extra " + txtIP.Text + " " + txtPassword.Text;
//                    process.StartInfo.RedirectStandardOutput = false;
//                    process.Start();
//                }
//            }
//            else
//            {
//                MessageBox.Show("Please enter a valid IP address");
//            }
//        }
//        private void btnReset_Click(object sender, EventArgs e)
//        {
//            string @extern = "extern.bat";
//            if (!File.Exists(@extern))
//    {
//                MessageBox.Show("File " + @extern +" not found!");
//                return;
//            }

//            if (IsValidIP(txtIP.Text))
//            {
//                using (Process process = new Process())
//                {
//                    process.StartInfo.UseShellExecute = false;
//                    process.StartInfo.FileName = @extern;
//                    process.StartInfo.Arguments = "resetradxa " + txtIP.Text + " " + txtPassword.Text;
//                    process.StartInfo.RedirectStandardOutput = false;
//                    process.Start();
//                }
//            }
//            else
//            {
//                MessageBox.Show("Please enter a valid IP address");
//            }
//        }

//        private void btnResetCam_Click(object sender, EventArgs e)
//        {
//            DialogResult result = MessageBox.Show("All OpenIPC camera settings will be restored to default.", "Warning!!!", MessageBoxButtons.YesNo);
//            if (result == DialogResult.No)
//            {
//                MessageBox.Show("No changes have been done.");
//            }
//            else if (result == DialogResult.Yes)
//            {
//                string extern = "extern.bat";
//                if (!File.Exists(extern))
//        {
//                    MessageBox.Show("File " + extern +" not found!");
//                    return;
//                }

//                if (IsValidIP(txtIP.Text))
//                {
//                    using (Process process = new Process())
//                    {
//                        process.StartInfo.UseShellExecute = false;
//                        process.StartInfo.FileName = extern;
//                        process.StartInfo.Arguments = "resetcam " + txtIP.Text + " " + txtPassword.Text;
//                        process.StartInfo.RedirectStandardOutput = false;
//                        process.Start();
//                    }
//                }
//                else
//                {
//                    MessageBox.Show("Please enter a valid IP address");
//                }
//            }
//        }

//        private void btnAddButtons_Click(object sender, EventArgs e)
//        {
//            if (!string.IsNullOrEmpty(txtFrequency.Text))
//            {
//                string stream = "stream.sh";
//                if (!File.Exists(stream))
//                {
//                    MessageBox.Show("File " + stream + " not found!" + Environment.NewLine + "Install the latest version of Putty and try again.");
//                    return;
//                }

//                string[] lines = File.ReadAllLines(stream);
//                for (int x = 0; x < lines.Length; x++)
//                {
//                    if (lines[x].StartsWith("        iw"))
//                    {
//                        lines[x] = "        iw " + txtExtras.Text + " set freq $Freq";
//                    }
//                    else if (lines[x].StartsWith("           iw"))
//                    {
//                        lines[x] = "           iw " + txtExtras.Text + " set freq $Freq";
//                    }
//                }
//                File.WriteAllLines(stream, lines);
//            }

//            string extern = "extern.bat";
//            if (!File.Exists(extern))
//    {
//                MessageBox.Show("File " + extern +" not found!");
//                return;
//            }

//            if (IsValidIP(txtIP.Text))
//            {
//                using (Process process = new Process())
//                {
//                    process.StartInfo.UseShellExecute = false;
//                    process.StartInfo.FileName = extern;
//                    process.StartInfo.Arguments = "addbuttons " + txtIP.Text + " " + txtPassword.Text;
//                    process.StartInfo.RedirectStandardOutput = false;
//                    process.Start();
//                }
//            }
//            else
//            {
//                MessageBox.Show("Please enter a valid IP address");
//            }
//        }
//        private void btnMSP_Click(object sender, EventArgs e)
//        {
//            string externFile = "extern.bat";
//            string mspFile = "msposd";

//            if (!File.Exists(externFile))
//            {
//                MessageBox.Show("File " + externFile + " not found!");
//                return;
//            }

//            if (!File.Exists(mspFile))
//            {
//                MessageBox.Show("File " + mspFile + " not found!");
//                return;
//            }

//            if (IsValidIP(txtIP.Text))
//            {
//                using (Process process = new Process())
//                {
//                    process.StartInfo.UseShellExecute = false;
//                    process.StartInfo.FileName = externFile;
//                    process.StartInfo.Arguments = "msp " + txtIP.Text + " " + txtPassword.Text;
//                    process.StartInfo.RedirectStandardOutput = false;
//                    process.Start();
//                }
//            }
//            else
//            {
//                MessageBox.Show("Please enter a valid IP address");
//            }
//        }

//        private void btnFontsINAV_Click(object sender, EventArgs e)
//        {
//            string externFile = "extern.bat";
//            string fontFile1 = "inav/font.png";
//            string fontFile2 = "inav/font_hd.png";

//            if (!File.Exists(externFile))
//            {
//                MessageBox.Show("File " + externFile + " not found!");
//                return;
//            }

//            if (!File.Exists(fontFile1))
//            {
//                MessageBox.Show("File " + fontFile1 + " not found!");
//                return;
//            }

//            if (!File.Exists(fontFile2))
//            {
//                MessageBox.Show("File " + fontFile2 + " not found!");
//                return;
//            }

//            if (IsValidIP(txtIP.Text))
//            {
//                using (Process process = new Process())
//                {
//                    process.StartInfo.UseShellExecute = false;
//                    process.StartInfo.FileName = externFile;
//                    process.StartInfo.Arguments = "fontsINAV " + txtIP.Text + " " + txtPassword.Text;
//                    process.StartInfo.RedirectStandardOutput = false;
//                    process.Start();
//                }
//            }
//            else
//            {
//                MessageBox.Show("Please enter a valid IP address");
//            }
//        }
//        private void btnOnboardREC_Click(object sender, EventArgs e)
//        {
//            string externFile = "extern.bat";

//            if (!File.Exists(externFile))
//            {
//                MessageBox.Show("File " + externFile + " not found!");
//                return;
//            }

//            if (IsValidIP(txtIP.Text))
//            {
//                using (Process process = new Process())
//                {
//                    process.StartInfo.UseShellExecute = false;
//                    process.StartInfo.FileName = externFile;

//                    if (rBtnRECON.Checked)
//                    {
//                        process.StartInfo.Arguments = "onboardrecon " + txtIP.Text + " " + txtPassword.Text;
//                    }
//                    else
//                    {
//                        process.StartInfo.Arguments = "onboardrecoff " + txtIP.Text + " " + txtPassword.Text;
//                    }

//                    process.StartInfo.RedirectStandardOutput = false;
//                    process.Start();
//                }
//            }
//            else
//            {
//                MessageBox.Show("Please enter a valid IP address");
//            }
//        }

//        private void btnDualOSD_Click(object sender, EventArgs e)
//        {
//            string externFile = "extern.bat";

//            if (!File.Exists(externFile))
//            {
//                MessageBox.Show("File " + externFile + " not found!");
//                return;
//            }

//            if (IsValidIP(txtIP.Text))
//            {
//                using (Process process = new Process())
//                {
//                    process.StartInfo.UseShellExecute = false;
//                    process.StartInfo.FileName = externFile;
//                    process.StartInfo.Arguments = "dualosd " + txtIP.Text + " " + txtPassword.Text;
//                    process.StartInfo.RedirectStandardOutput = false;
//                    process.Start();
//                }
//            }
//            else
//            {
//                MessageBox.Show("Please enter a valid IP address");
//            }
//        }

//    }
//}
