// --------------------------------------------------------------------------------------------------------------------
// <copyright file="VlcPlayer.cs" company="VideoLan DotNet">
//   See http://vlcdotnet.codeplex.com/license
// </copyright>
// <summary>
//   Example application for Windows Forms
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Vlc.DotNet.Forms.SampleApplication
{
    using System;
    using System.Windows.Forms;

    using Vlc.DotNet.Core.Medias;

    /// <summary>
    /// Application logic for the VlcPlayer Form
    /// </summary>
    public partial class VlcPlayer : Form
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VlcPlayer"/> class.
        /// </summary>
        /// <remarks>
        /// Making changes to the Form sometimes adds the following line of code to VlcPlayer.Designer.cs:
        ///    this.vlcControl.Media = null;
        /// As the Media property may not be set to null in the current release, this will raise an exception at 
        /// runtime. So, if you get an <see cref="ArgumentNullException"/> in VlcControl.Common.cs, check for this
        /// line of code and remove it. 
        /// </remarks>
        public VlcPlayer()
        {
            InitializeComponent();

            labelPlaybackPosition.Text = string.Empty;
            this.vlcControl.PositionChanged += this.VlcControlOnPositionChanged;

            /* 
            var media = new LocationMedia("screen://");
            vlcControl1.SetMedia(media);
            */

            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                var media = new PathMedia(openFileDialog.FileName);
                vlcControl.Media = media;
            }
        }

        /// <summary>
        /// Called if the Play button is clicked; starts the VLC playback. 
        /// </summary>
        /// <param name="sender">Event sender. </param>
        /// <param name="e">Event arguments. </param>
        private void ButtonPlayClick(object sender, EventArgs e)
        {
            vlcControl.Play();
        }

        /// <summary>
        /// Called if the Pause button is clicked; pauses the VLC playback. 
        /// </summary>
        /// <param name="sender">Event sender. </param>
        /// <param name="e">Event arguments. </param>
        private void ButtonPauseClick(object sender, EventArgs e)
        {
            vlcControl.Pause();
        }

        /// <summary>
        /// Called if the Stop button is clicked; stops the VLC playback. 
        /// </summary>
        /// <param name="sender">Event sender. </param>
        /// <param name="e">Event arguments. </param>
        private void ButtonStopClick(object sender, EventArgs e)
        {
            this.vlcControl.Stop();
        }

        /// <summary>
        /// Event handler for the <see cref="VlcControl.PositionChanged"/> event. 
        /// Updates the label containing the playback position. 
        /// </summary>
        /// <param name="sender">Event sending <see cref="VlcControl"/>. </param>
        /// <param name="e">Event arguments, containing the current position. </param>
        private void VlcControlOnPositionChanged(VlcControl sender, Core.VlcEventArgs<float> e)
        {
            labelPlaybackPosition.Text = (e.Data * 100).ToString("000") + " %";
        }
    }
}
