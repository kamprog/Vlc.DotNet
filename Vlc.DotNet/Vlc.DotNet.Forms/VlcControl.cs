using System;
using System.ComponentModel;
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
    }
}
