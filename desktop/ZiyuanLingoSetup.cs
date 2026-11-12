using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.IO.Compression;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Microsoft.Win32;

namespace ZiyuanLingoInstaller
{
    static class SetupProgram
    {
        [DllImport("user32.dll")]
        private static extern bool SetProcessDPIAware();

        [STAThread]
        static void Main(string[] args)
        {
            try { SetProcessDPIAware(); } catch { }
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (args.Length > 0 && args[0] == "--uninstall")
            {
                RunUninstall();
                return;
            }
            Application.Run(new CyberSetupForm());
        }

        private static void RunUninstall()
        {
            string installDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Programs", "ZiyuanLingo");
            if (MessageBox.Show("Uninstall ZiyuanLingo Studio from your computer?", "ZiyuanLingo Uninstaller", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;
            try
            {
                string desktopLnk = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory), "ZiyuanLingo Studio.lnk");
                if (File.Exists(desktopLnk)) File.Delete(desktopLnk);
                string startMenuLnk = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Programs), "ZiyuanLingo Studio.lnk");
                if (File.Exists(startMenuLnk)) File.Delete(startMenuLnk);
                Registry.CurrentUser.DeleteSubKeyTree(@"Software\Microsoft\Windows\CurrentVersion\Uninstall\ZiyuanLingo", false);
                MessageBox.Show("Removed shortcuts and registry entries.", "Uninstall Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Uninstall warning: " + ex.Message);
            }
        }
    }

    public class CyberSetupForm : Form
    {
        private TextBox txtDir;
        private CheckBox chkDesktop, chkLaunch;
        private Panel barTrack, barFill;
        private Button btnInstall;

        public CyberSetupForm()
        {
            this.Text = "ZiyuanLingo Studio v1.1.0 Setup (x64)";
            this.Size = new Size(590, 415);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(7, 11, 20);
            this.ForeColor = Color.FromArgb(226, 232, 240);
            this.Font = new Font("Segoe UI", 9.5f);

            var header = new Panel { Dock = DockStyle.Top, Height = 92 };
            header.Paint += (s, e) =>
            {
                using (var br = new LinearGradientBrush(header.ClientRectangle, Color.FromArgb(15, 23, 42), Color.FromArgb(30, 27, 75), LinearGradientMode.Horizontal))
                {
                    e.Graphics.FillRectangle(br, header.ClientRectangle);
                }
                using (var pen = new Pen(Color.FromArgb(56, 189, 248), 2f))
                {
                    e.Graphics.DrawLine(pen, 0, header.Height - 1, header.Width, header.Height - 1);
                }
            };

            var lblHead = new Label
            {
                Text = "ZIYUANLINGO STUDIO v1.1.0 (OPEN SOURCE)",
                ForeColor = Color.White,
                BackColor = Color.Transparent,
                Font = new Font("Segoe UI", 14f, FontStyle.Bold),
                Location = new Point(24, 18),
                AutoSize = true
            };
            var lblSub = new Label
            {
                Text = "★ Free & Open Source (Apache-2.0) · EN / 中文 / 日本語 · Local BYOK + Cloud Ready",
                ForeColor = Color.FromArgb(56, 189, 248),
                BackColor = Color.Transparent,
                Font = new Font("Segoe UI Semibold", 9.2f),
                Location = new Point(26, 52),
                AutoSize = true
            };
            header.Controls.AddRange(new Control[] { lblHead, lblSub });

            var lblDir = new Label
            {
                Text = "INSTALLATION DIRECTORY",
                ForeColor = Color.FromArgb(148, 163, 184),
                Font = new Font("Consolas", 8.8f, FontStyle.Bold),
                Location = new Point(26, 112),
                AutoSize = true
            };

            txtDir = new TextBox
            {
                Location = new Point(26, 136),
                Width = 420,
                BackColor = Color.FromArgb(15, 23, 42),
                ForeColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Font = new Font("Consolas", 9.5f),
                Text = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Programs", "ZiyuanLingo")
            };

            var btnBrowse = new Button
            {
                Text = "Browse...",
                Location = new Point(456, 134),
                Width = 92,
                Height = 27,
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(30, 41, 59),
                ForeColor = Color.White
            };
            btnBrowse.FlatAppearance.BorderColor = Color.FromArgb(51, 65, 85);
            btnBrowse.Click += (s, e) =>
            {
                using (var fbd = new FolderBrowserDialog { SelectedPath = txtDir.Text })
                {
                    if (fbd.ShowDialog(this) == DialogResult.OK) txtDir.Text = fbd.SelectedPath;
                }
            };

            chkDesktop = new CheckBox
            {
                Text = "Create Desktop & Start Menu shortcuts",
                Checked = true,
                ForeColor = Color.FromArgb(203, 213, 225),
                Location = new Point(26, 182),
                AutoSize = true
            };

            chkLaunch = new CheckBox
            {
                Text = "Launch ZiyuanLingo Studio v1.1.0 immediately after setup",
                Checked = true,
                ForeColor = Color.FromArgb(167, 243, 208),
                Location = new Point(26, 212),
                AutoSize = true
            };

            barTrack = new Panel
            {
                Location = new Point(26, 258),
                Width = 522,
                Height = 14,
                BackColor = Color.FromArgb(15, 23, 42)
            };
            barFill = new Panel
            {
                Location = new Point(0, 0),
                Width = 0,
                Height = 14,
                BackColor = Color.FromArgb(56, 189, 248)
            };
            barTrack.Controls.Add(barFill);

            btnInstall = new Button
            {
                Text = "⚡ Install Studio Now",
                Location = new Point(368, 302),
                Width = 180,
                Height = 40,
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(37, 99, 235),
                ForeColor = Color.White,
                Font = new Font("Segoe UI Semibold", 10f, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnInstall.FlatAppearance.BorderSize = 0;
            btnInstall.Click += (s, e) => PerformInstall();

            var btnCancel = new Button
            {
                Text = "Cancel",
                Location = new Point(256, 302),
                Width = 98,
                Height = 40,
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(15, 23, 42),
                ForeColor = Color.FromArgb(148, 163, 184)
            };
            btnCancel.FlatAppearance.BorderColor = Color.FromArgb(30, 41, 59);
            btnCancel.Click += (s, e) => this.Close();

            this.Controls.AddRange(new Control[] { header, lblDir, txtDir, btnBrowse, chkDesktop, chkLaunch, barTrack, btnInstall, btnCancel });
        }

        private void SetProgress(int pct)
        {
            barFill.Width = (int)(barTrack.Width * (pct / 100.0));
            Application.DoEvents();
        }

        private static void CreateShortcut(string shortcutPath, string targetExe, string workDir)
        {
            Type shellType = Type.GetTypeFromProgID("WScript.Shell");
            if (shellType == null) return;
            dynamic shell = Activator.CreateInstance(shellType);
            dynamic sc = shell.CreateShortcut(shortcutPath);
            sc.TargetPath = targetExe;
            sc.WorkingDirectory = workDir;
            sc.Description = "ZiyuanLingo AI Document Translation Studio";
            sc.Save();
        }

        private void PerformInstall()
        {
            btnInstall.Enabled = false;
            try
            {
                string targetDir = txtDir.Text.Trim();
                Directory.CreateDirectory(targetDir);
                SetProgress(25);

                string tempZip = Path.Combine(Path.GetTempPath(), "ziyuanlingo_payload_" + Guid.NewGuid().ToString("N") + ".zip");
                using (Stream resStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("payload.zip"))
                {
                    if (resStream == null) throw new Exception("Embedded payload.zip missing.");
                    using (FileStream fs = File.Create(tempZip)) resStream.CopyTo(fs);
                }
                SetProgress(55);

                using (ZipArchive archive = ZipFile.OpenRead(tempZip))
                {
                    foreach (ZipArchiveEntry entry in archive.Entries)
                    {
                        string destPath = Path.Combine(targetDir, entry.FullName);
                        string destFolder = Path.GetDirectoryName(destPath);
                        if (!string.IsNullOrEmpty(destFolder)) Directory.CreateDirectory(destFolder);
                        if (!string.IsNullOrEmpty(entry.Name)) entry.ExtractToFile(destPath, true);
                    }
                }
                File.Delete(tempZip);
                SetProgress(85);

                string selfExe = Assembly.GetExecutingAssembly().Location;
                string uninstExe = Path.Combine(targetDir, "Uninstall.exe");
                File.Copy(selfExe, uninstExe, true);

                string mainExe = Path.Combine(targetDir, "ZiyuanLingo.exe");
                if (chkDesktop.Checked)
                {
                    CreateShortcut(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory), "ZiyuanLingo Studio.lnk"), mainExe, targetDir);
                    CreateShortcut(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Programs), "ZiyuanLingo Studio.lnk"), mainExe, targetDir);
                }

                using (RegistryKey key = Registry.CurrentUser.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Uninstall\ZiyuanLingo"))
                {
                    if (key != null)
                    {
                        key.SetValue("DisplayName", "ZiyuanLingo Studio");
                        key.SetValue("DisplayVersion", "1.1.0");
                        key.SetValue("Publisher", "ZiyuanLingo Studio (ziyuanlingo.com)");
                        key.SetValue("InstallLocation", targetDir);
                        key.SetValue("DisplayIcon", mainExe);
                        key.SetValue("UninstallString", "\"" + uninstExe + "\" --uninstall");
                    }
                }

                SetProgress(100);
                if (chkLaunch.Checked && File.Exists(mainExe))
                {
                    Process.Start(new ProcessStartInfo { FileName = mainExe, WorkingDirectory = targetDir });
                }
                this.Close();
            }
            catch (Exception ex)
            {
                btnInstall.Enabled = true;
                MessageBox.Show(this, "Installation error:\n" + ex.Message, "Setup Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
