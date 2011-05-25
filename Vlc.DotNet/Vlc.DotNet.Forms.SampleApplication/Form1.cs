using System;
using System.Windows.Forms;
using Vlc.DotNet.Core.Medias;

namespace Vlc.DotNet.Forms.SampleApplication
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            label1.Text = "";
            vlcControl1.PositionChanged += vlcControl1_PositionChanged;
            //var media = new LocationMedia("screen://");
            //vlcControl1.SetMedia(media);
            if (openFileDialog1.ShowDialog(this) == DialogResult.OK)
            {
                var media = new PathMedia(openFileDialog1.FileName);
                vlcControl1.Media = media;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            vlcControl1.Play();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            vlcControl1.Pause();
        }

        private void vlcControl1_PositionChanged(VlcControl sender, Vlc.DotNet.Core.VlcEventArgs<float> e)
        {
            label1.Text = (e.Data * 100).ToString("000") + " %";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            vlcControl1.Stop();
        }
    }
}
