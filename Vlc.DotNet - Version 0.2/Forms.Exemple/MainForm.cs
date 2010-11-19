using System;
using System.IO;
using System.Windows.Forms;
using Vlc.DotNet.Core.Medias;
using System.ComponentModel;

namespace Vlc.DotNet.Forms.Exemple
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            myVlcControl.Manager.PropertyChanged += OnVlcControlPropertyChanged;
        }

        private void CheckVlcPath()
        {
            if (!File.Exists(Path.Combine(myVlcPathFolderBrowserDialog.SelectedPath, "libvlc.dll")))
                throw new FileNotFoundException("Could not find libvlc.dll in VideoLan Client path.");
            if (!File.Exists(Path.Combine(myVlcPathFolderBrowserDialog.SelectedPath, "libvlccore.dll")))
                throw new FileNotFoundException("Could not find libvlccore.dll in VideoLan Client path.");
            if (!Directory.Exists(Path.Combine(myVlcPathFolderBrowserDialog.SelectedPath, "plugins")))
                throw new DirectoryNotFoundException("Could not find plugins directory in VideoLan Client path.");
        }

        private void OnAddMediaButtonClick(object sender, EventArgs e)
        {
            if (myAddMediaOpenFileDialog.ShowDialog() == DialogResult.Cancel)
                return;
            var media = new FileMedia(myAddMediaOpenFileDialog.FileName);
            myVlcControl.Manager.MediaLibrary.MediaItems.Add(media);
            myMediasListBox.Items.Add(media);
        }
        private void OnVlcPathButtonClick(object sender, EventArgs e)
        {
            if (myVlcPathFolderBrowserDialog.ShowDialog() == DialogResult.Cancel)
                return;
            try
            {
                CheckVlcPath();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            myVlcPathTextBox.Text = myVlcPathFolderBrowserDialog.SelectedPath;
            myVlcControl.Manager.VlcLibPath = myVlcPathFolderBrowserDialog.SelectedPath;
        }

        private void OnMediasListBoxSelectedIndexChanged(object sender, EventArgs e)
        {
            myStartButton.Enabled = myRemoveMediaButton.Enabled = myMediasListBox.SelectedIndex > -1;
        }

        private void OnRemoveMediaButtonClick(object sender, EventArgs e)
        {
            myVlcControl.Manager.MediaLibrary.MediaItems.Remove((MediaBase)myMediasListBox.SelectedItem);
        }

        private void OnStartButtonClick(object sender, EventArgs e)
        {
            myVlcControl.Manager.MediaLibrary.SelectedMedia = (MediaBase)myMediasListBox.SelectedItem;
            myVlcControl.Manager.Play();
            myStopButton.Enabled = true;
        }

        private void OnStopButtonClick(object sender, EventArgs e)
        {
            myVlcControl.Manager.Stop();
            myStartButton.Enabled = myMediasListBox.SelectedIndex > -1;
            myStopButton.Enabled = false;
        }

        private void OnVlcControlPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            switch (args.PropertyName)
            {
                case "Position":
                    {
                        //todo
                        //myPositionTrackBar.Value = (int) myVlcControl.Manager.Position;
                        //myPositionTrackBar.BeginInvoke(
                        //    new MethodInvoker(
                        //        delegate()
                        //        {
                        //            Console.WriteLine(myVlcControl.Manager.Position);
                        //            myPositionTrackBar.Value = (int)myVlcControl.Manager.Position;
                        //        }));
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
