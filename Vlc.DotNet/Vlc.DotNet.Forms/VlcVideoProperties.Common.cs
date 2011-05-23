using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using Vlc.DotNet.Core;

#if WPF
using System.Windows;
using Vlc.DotNet.Core.Interops.Signatures.LibVlc.MediaPlayer;

namespace Vlc.DotNet.Wpf
#else
using System.Drawing;

namespace Vlc.DotNet.Forms
#endif
{
    /// <summary>
    /// VlcVideoProperties class
    /// </summary>
    public sealed class VlcVideoProperties : IDisposable
    {
        private readonly IVlcControl myHostVlcControl;

        /// <summary>
        /// Get video size
        /// </summary>
        [Category(CommonStrings.VLC_DOTNET_PROPERTIES_CATEGORY)]
        public Size Size
        {
            get
            {
                uint width = 0;
                uint height = 0;
                if (VlcContext.InteropManager != null &&
                    VlcContext.InteropManager.MediaPlayerInterops != null &&
                    VlcContext.InteropManager.MediaPlayerInterops.VideoInterops != null &&
                    VlcContext.InteropManager.MediaPlayerInterops.VideoInterops.GetSize.IsAvailable &&
                    VlcContext.HandleManager != null &&
                    VlcContext.HandleManager.MediaPlayerHandles != null &&
                    VlcContext.HandleManager.MediaPlayerHandles.ContainsKey(myHostVlcControl))
                {
                    VlcContext.InteropManager.MediaPlayerInterops.VideoInterops.GetSize.Invoke(VlcContext.HandleManager.MediaPlayerHandles[myHostVlcControl], 0, out width, out height);
                    if (width <= 0 && height <= 0)
                        return Size.Empty;
#if WPF
                    return new Size(width, height);
#else
                    return new Size((int)width, (int)height);
#endif
                }
                return Size.Empty;
            }
        }
        /// <summary>
        /// Get / Set video scale
        /// </summary>
        [Category(CommonStrings.VLC_DOTNET_PROPERTIES_CATEGORY)]
        public int Scale
        {
            get
            {
                if (VlcContext.InteropManager != null &&
                    VlcContext.InteropManager.MediaPlayerInterops != null &&
                    VlcContext.InteropManager.MediaPlayerInterops.VideoInterops != null &&
                    VlcContext.InteropManager.MediaPlayerInterops.VideoInterops.GetScale.IsAvailable &&
                    VlcContext.HandleManager != null &&
                    VlcContext.HandleManager.MediaPlayerHandles != null &&
                    VlcContext.HandleManager.MediaPlayerHandles.ContainsKey(myHostVlcControl))
                {
                    return VlcContext.InteropManager.MediaPlayerInterops.VideoInterops.GetScale.Invoke(VlcContext.HandleManager.MediaPlayerHandles[myHostVlcControl]);
                }
                return 0;
            }
            set
            {
                if (VlcContext.InteropManager != null &&
                    VlcContext.InteropManager.MediaPlayerInterops != null &&
                    VlcContext.InteropManager.MediaPlayerInterops.VideoInterops != null &&
                    VlcContext.InteropManager.MediaPlayerInterops.VideoInterops.SetScale.IsAvailable &&
                    VlcContext.HandleManager != null &&
                    VlcContext.HandleManager.MediaPlayerHandles != null &&
                    VlcContext.HandleManager.MediaPlayerHandles.ContainsKey(myHostVlcControl))
                {
                    VlcContext.InteropManager.MediaPlayerInterops.AudioInterops.SetDelay.Invoke(VlcContext.HandleManager.MediaPlayerHandles[myHostVlcControl], value);
                }
            }
        }

        /// <summary>
        /// Set subtitle file
        /// </summary>
        /// <param name="subtitleFile">The subtitle file</param>
        public void SetSubtitleFile(string subtitleFile)
        {
            if (!string.IsNullOrEmpty(subtitleFile) &&
                File.Exists(subtitleFile) &&
                VlcContext.InteropManager != null &&
                VlcContext.InteropManager.MediaPlayerInterops != null &&
                VlcContext.InteropManager.MediaPlayerInterops.VideoInterops != null &&
                VlcContext.InteropManager.MediaPlayerInterops.VideoInterops.SetSubtitleFile.IsAvailable &&
                VlcContext.HandleManager.MediaPlayerHandles != null &&
                VlcContext.HandleManager.MediaPlayerHandles.ContainsKey(myHostVlcControl))
            {
                VlcContext.InteropManager.MediaPlayerInterops.VideoInterops.SetSubtitleFile.Invoke(VlcContext.HandleManager.MediaPlayerHandles[myHostVlcControl], subtitleFile);
            }
        }
        /// <summary>
        /// Get / Set video subtitle.
        /// </summary>
        public int CurrentSpuIndex
        {
            get
            {
                if (VlcContext.InteropManager != null &&
                    VlcContext.InteropManager.MediaPlayerInterops != null &&
                    VlcContext.InteropManager.MediaPlayerInterops.VideoInterops != null &&
                    VlcContext.InteropManager.MediaPlayerInterops.VideoInterops.GetSpu.IsAvailable &&
                    VlcContext.HandleManager.MediaPlayerHandles != null &&
                    VlcContext.HandleManager.MediaPlayerHandles.ContainsKey(myHostVlcControl))
                {
                    return VlcContext.InteropManager.MediaPlayerInterops.VideoInterops.GetSpu.Invoke(VlcContext.HandleManager.MediaPlayerHandles[myHostVlcControl]);
                }
                return -1;
            }
            set
            {
                if (VlcContext.InteropManager != null &&
                    VlcContext.InteropManager.MediaPlayerInterops != null &&
                    VlcContext.InteropManager.MediaPlayerInterops.VideoInterops != null &&
                    VlcContext.InteropManager.MediaPlayerInterops.VideoInterops.SetSpu.IsAvailable &&
                    VlcContext.HandleManager.MediaPlayerHandles != null &&
                    VlcContext.HandleManager.MediaPlayerHandles.ContainsKey(myHostVlcControl))
                {
                    VlcContext.InteropManager.MediaPlayerInterops.VideoInterops.SetSpu.Invoke(VlcContext.HandleManager.MediaPlayerHandles[myHostVlcControl], value);
                }
            }
        }
        /// <summary>
        /// Get the number of available video subtitles.
        /// </summary>
        public int SpuCount
        {
            get
            {
                if (VlcContext.InteropManager != null &&
                    VlcContext.InteropManager.MediaPlayerInterops != null &&
                    VlcContext.InteropManager.MediaPlayerInterops.VideoInterops != null &&
                    VlcContext.InteropManager.MediaPlayerInterops.VideoInterops.GetSpuCount.IsAvailable &&
                    VlcContext.HandleManager.MediaPlayerHandles != null &&
                    VlcContext.HandleManager.MediaPlayerHandles.ContainsKey(myHostVlcControl))
                {
                    VlcContext.InteropManager.MediaPlayerInterops.VideoInterops.GetSpuCount.Invoke(VlcContext.HandleManager.MediaPlayerHandles[myHostVlcControl]);
                }
                return 0;
            }
        }
        /// <summary>
        /// Get the description of available video subtitles.
        /// </summary>
        public VlcTrackDescription SpuDescription
        {
            get
            {
                if (VlcContext.InteropManager != null &&
                    VlcContext.InteropManager.MediaPlayerInterops != null &&
                    VlcContext.InteropManager.MediaPlayerInterops.VideoInterops != null &&
                    VlcContext.InteropManager.MediaPlayerInterops.VideoInterops.GetSpuDescription.IsAvailable &&
                    VlcContext.HandleManager.MediaPlayerHandles != null &&
                    VlcContext.HandleManager.MediaPlayerHandles.ContainsKey(myHostVlcControl))
                {
                    var ptr = VlcContext.InteropManager.MediaPlayerInterops.VideoInterops.GetSpuDescription.Invoke(VlcContext.HandleManager.MediaPlayerHandles[myHostVlcControl]);
                    if(ptr != IntPtr.Zero)
                    {
                        var td = (TrackDescription)Marshal.PtrToStructure(ptr, typeof(TrackDescription));
                        return new VlcTrackDescription(td);
                    }
                }
                return null;
            }
        }

        /// <summary>
        /// Set deinterlace mode
        /// </summary>
        /// <param name="mode">Mode of deinterlace</param>
        public void SetDeinterlaceMode(VlcDeinterlaceModes mode)
        {
            if (VlcContext.HandleManager.LibVlcHandle != IntPtr.Zero &&
                VlcContext.InteropManager != null &&
                VlcContext.InteropManager.MediaPlayerInterops != null &&
                VlcContext.InteropManager.MediaPlayerInterops.VideoInterops != null &&
                VlcContext.InteropManager.MediaPlayerInterops.VideoInterops.SetDeinterlace.IsAvailable)
            {
                if (mode != VlcDeinterlaceModes.None)
                    VlcContext.InteropManager.MediaPlayerInterops.VideoInterops.SetDeinterlace.Invoke(VlcContext.HandleManager.MediaPlayerHandles[myHostVlcControl], mode.ToString().ToLower());
                else
                    VlcContext.InteropManager.MediaPlayerInterops.VideoInterops.SetDeinterlace.Invoke(VlcContext.HandleManager.MediaPlayerHandles[myHostVlcControl], null);
            }
        }
        /// <summary>
        /// Set aspect ration
        /// </summary>
        /// <param name="aspect">Aspect to define</param>
        public void SetAspectRatio(string aspect)
        {
            if (!string.IsNullOrEmpty(aspect) &&
                VlcContext.InteropManager != null &&
                VlcContext.InteropManager.MediaPlayerInterops != null &&
                VlcContext.InteropManager.MediaPlayerInterops.VideoInterops != null &&
                VlcContext.InteropManager.MediaPlayerInterops.VideoInterops.SetAspectRatio.IsAvailable &&
                VlcContext.HandleManager != null &&
                VlcContext.HandleManager.MediaPlayerHandles != null &&
                VlcContext.HandleManager.EventManagerHandles.ContainsKey(myHostVlcControl))
            {
                VlcContext.InteropManager.MediaPlayerInterops.VideoInterops.SetAspectRatio.Invoke(VlcContext.HandleManager.MediaPlayerHandles[myHostVlcControl], aspect);
            }
        }

        internal VlcVideoProperties(IVlcControl vlcControl)
        {
            myHostVlcControl = vlcControl;
        }

        public void Dispose()
        {
        }
    }
}