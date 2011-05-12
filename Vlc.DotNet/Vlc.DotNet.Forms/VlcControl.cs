using System;
using System.ComponentModel;
using System.Text;
using System.Windows.Forms;
using Vlc.DotNet.Core;

namespace Vlc.DotNet.Forms
{
    public sealed partial class VlcControl : Control
    {
        /// <summary>
        /// Constructor of VlcControl
        /// </summary>
        public VlcControl()
        {
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime)
                return;
            if (!VlcContext.IsInitialized)
                VlcContext.Initialize();
            VlcContext.HandleManager.MediaPlayerHandles[this] =
                VlcContext.InteropManager.MediaPlayerInterops.NewInstance.Invoke(
                    VlcContext.HandleManager.LibVlcHandle);
            AudioProperties = new VlcAudioProperties(this);
            VideoProperties = new VlcVideoProperties(this);
            InitEvents();
            HandleCreated += OnHandleCreated;
        }

        void OnHandleCreated(object sender, EventArgs e)
        {
            VlcContext.InteropManager.MediaPlayerInterops.SetHwnd.Invoke(VlcContext.HandleManager.MediaPlayerHandles[this], Handle);
            HandleCreated -= OnHandleCreated;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (IsPlaying)
                    Stop();

                FreeEvents();
                VlcContext.InteropManager.MediaPlayerInterops.ReleaseInstance.Invoke(VlcContext.HandleManager.MediaPlayerHandles[this]);
                VlcContext.HandleManager.MediaPlayerHandles.Remove(this);
            }
            base.Dispose(disposing);
        }

        /// <summary>
        /// Take snapshot
        /// </summary>
        /// <param name="filePath">The file path</param>
        /// <param name="width">The width of the snapshot</param>
        /// <param name="height">The height of the snapshot</param>
        public void TakeSnapshot(string filePath, uint width, uint height)
        {
            if (!string.IsNullOrEmpty(filePath) &&
                VlcContext.InteropManager != null &&
                VlcContext.InteropManager.MediaPlayerInterops != null &&
                VlcContext.InteropManager.MediaPlayerInterops.VideoInterops.TakeSnapshot.IsAvailable)
            {
                if(InvokeRequired)
                {
                    Invoke((MethodInvoker) (() => VlcContext.InteropManager.MediaPlayerInterops.VideoInterops.TakeSnapshot.Invoke(VlcContext.HandleManager.MediaPlayerHandles[this], 0, Encoding.UTF8.GetBytes(filePath), width, height)));
                    return;
                }
                VlcContext.InteropManager.MediaPlayerInterops.VideoInterops.TakeSnapshot.Invoke(VlcContext.HandleManager.MediaPlayerHandles[this], 0, Encoding.UTF8.GetBytes(filePath), width, height);
            }
        }
    }
}
