using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Management;
using System.Windows.Forms;

namespace Unlocker
{
    public partial class UnlockerForm : Form
    {
        private Label infoLabel;
        private ComboBox actionComboBox;
        private Button okButton;
        private Button quitButton;
        private string filePath;
        private Process[] blockingProcesses;

        public UnlockerForm(string file)
        {
            filePath = file;
            blockingProcesses = GetLockingProcesses(filePath);

            this.Text = "Unlocker";
            this.Size = new System.Drawing.Size(250, 175);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            infoLabel = new Label()
            {
                Text = blockingProcesses.Length > 0 ? 
                    $"Blocking processes: {blockingProcesses.Length}" : 
                    "No blocks were found.\nHowever, you can take action.",
                Left = 10, Top = 10, Width = 360, Height = 50
            };
            this.Controls.Add(infoLabel);

            actionComboBox = new ComboBox() { Left = 10, Top = 65, Width = 200 };
            actionComboBox.Items.AddRange(new string[] { "No action", "Delete", "Rename", "Move" });
            actionComboBox.SelectedIndex = 0;
            this.Controls.Add(actionComboBox);

            okButton = new Button() { Text = "OK", Left = 10, Top = 100, Width = 70 };
            okButton.Click += new EventHandler(PerformAction);
            this.Controls.Add(okButton);

            quitButton = new Button() { Text = "Quit", Left = 100, Top = 100, Width = 70 };
            quitButton.Click += (s, e) => Application.Exit();
            this.Controls.Add(quitButton);
        }

        private void PerformAction(object? sender, EventArgs e)
        {
            string action = actionComboBox.SelectedItem?.ToString() ?? "No action";

            if (blockingProcesses.Length > 0)
            {
                foreach (var proc in blockingProcesses)
                {
                    proc.Kill();
                }
            }

            switch (action)
            {
                case "Delete":
                    File.Delete(filePath);
                    MessageBox.Show("File deleted.");
                    break;

                case "Rename":
                    string directory = Path.GetDirectoryName(filePath) ?? string.Empty;
                    string newName = Path.Combine(directory, "Renamed_" + Path.GetFileName(filePath));    
                    File.Move(filePath, newName);
                    MessageBox.Show($"File renamed to {newName}");
                    break;

                case "Move":
                    using (OpenFileDialog dialog = new OpenFileDialog())
                    {
                        dialog.CheckFileExists = false;
                        dialog.FileName = Path.GetFileName(filePath);
                        if (dialog.ShowDialog() == DialogResult.OK)
                        {
                            File.Move(filePath, dialog.FileName);
                            MessageBox.Show($"File moved to {dialog.FileName}");
                        }
                    }
                    break;

                default:
                    MessageBox.Show("No action was taken.");
                    break;
            }
            Application.Exit();
        }

        private Process[] GetLockingProcesses(string filePath)
        {
            var processes = new System.Collections.Generic.List<Process>();
            string query = "SELECT * FROM Win32_Process";
            using (ManagementObjectSearcher searcher = new ManagementObjectSearcher(query))
            {
                foreach (ManagementObject obj in searcher.Get())
                {
                    try
                    {
                        int processId = Convert.ToInt32(obj["ProcessId"]);
                        Process process = Process.GetProcessById(processId);
                        if (process.Modules.Cast<ProcessModule>().Any(m => m.FileName == filePath))
                        {
                            processes.Add(process);
                        }
                    }
                    catch { }
                }
            }
            return processes.ToArray();
        }
    }
}
