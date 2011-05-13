using System.ComponentModel;
using Vlc.DotNet.Core;

#if WPF
namespace Vlc.DotNet.Wpf
#else
namespace Vlc.DotNet.Forms
#endif
{
    /// <summary>
    /// VlcAudioProperties class
    /// </summary>
    public sealed class VlcAudioProperties
    {
        private readonly IVlcControl myHostVlcControl;

        /// <summary>
        /// Check / Set mute
        /// </summary>
        [Category(CommonStrings.VLC_DOTNET_PROPERTIES_CATEGORY)]
        public bool IsMute
        {
            get
            {
                if (VlcContext.InteropManager != null &&
                    VlcContext.InteropManager.MediaPlayerInterops != null &&
                    VlcContext.InteropManager.MediaPlayerInterops.AudioInterops != null &&
                    VlcContext.InteropManager.MediaPlayerInterops.AudioInterops.GetMute.IsAvailable &&
                    VlcContext.HandleManager != null &&
                    VlcContext.HandleManager.MediaPlayerHandles != null &&
                    VlcContext.HandleManager.MediaPlayerHandles.ContainsKey(myHostVlcControl))
                {
                    return VlcContext.InteropManager.MediaPlayerInterops.AudioInterops.GetMute.Invoke(VlcContext.HandleManager.MediaPlayerHandles[myHostVlcControl]) == 1;
                }
                return false;
            }
            set
            {
                if (VlcContext.InteropManager != null &&
                    VlcContext.InteropManager.MediaPlayerInterops != null &&
                    VlcContext.InteropManager.MediaPlayerInterops.AudioInterops != null &&
                    VlcContext.InteropManager.MediaPlayerInterops.AudioInterops.SetMute.IsAvailable &&
                    VlcContext.HandleManager != null &&
                    VlcContext.HandleManager.MediaPlayerHandles != null &&
                    VlcContext.HandleManager.MediaPlayerHandles.ContainsKey(myHostVlcControl))
                {
                    VlcContext.InteropManager.MediaPlayerInterops.AudioInterops.SetMute.Invoke(VlcContext.HandleManager.MediaPlayerHandles[myHostVlcControl], value ? 1 : 0);
                }
            }
        }
        /// <summary>
        /// Get / Set volume
        /// </summary>
        [Category(CommonStrings.VLC_DOTNET_PROPERTIES_CATEGORY)]
        public int Volume
        {
            get
            {
                if (VlcContext.InteropManager != null &&
                    VlcContext.InteropManager.MediaPlayerInterops != null &&
                    VlcContext.InteropManager.MediaPlayerInterops.AudioInterops != null &&
                    VlcContext.InteropManager.MediaPlayerInterops.AudioInterops.GetVolume.IsAvailable &&
                    VlcContext.HandleManager != null &&
                    VlcContext.HandleManager.MediaPlayerHandles != null &&
                    VlcContext.HandleManager.MediaPlayerHandles.ContainsKey(myHostVlcControl))
                {
                    return VlcContext.InteropManager.MediaPlayerInterops.AudioInterops.GetVolume.Invoke(VlcContext.HandleManager.MediaPlayerHandles[myHostVlcControl]);
                }
                return -1;
            }
            set
            {
                if (VlcContext.InteropManager != null &&
                    VlcContext.InteropManager.MediaPlayerInterops != null &&
                    VlcContext.InteropManager.MediaPlayerInterops.AudioInterops != null &&
                    VlcContext.InteropManager.MediaPlayerInterops.AudioInterops.SetVolume.IsAvailable &&
                    VlcContext.HandleManager != null &&
                    VlcContext.HandleManager.MediaPlayerHandles != null &&
                    VlcContext.HandleManager.MediaPlayerHandles.ContainsKey(myHostVlcControl))
                {
                    VlcContext.InteropManager.MediaPlayerInterops.AudioInterops.SetVolume.Invoke(VlcContext.HandleManager.MediaPlayerHandles[myHostVlcControl], value);
                }
            }
        }
        /// <summary>
        /// Get / Set delay
        /// </summary>
        [Category(CommonStrings.VLC_DOTNET_PROPERTIES_CATEGORY)]
        public long Delay
        {
            get
            {
                if (VlcContext.InteropManager != null &&
                    VlcContext.InteropManager.MediaPlayerInterops != null &&
                    VlcContext.InteropManager.MediaPlayerInterops.AudioInterops != null &&
                    VlcContext.InteropManager.MediaPlayerInterops.AudioInterops.GetDelay.IsAvailable &&
                    VlcContext.HandleManager != null &&
                    VlcContext.HandleManager.MediaPlayerHandles != null &&
                    VlcContext.HandleManager.MediaPlayerHandles.ContainsKey(myHostVlcControl))
                {
                    return VlcContext.InteropManager.MediaPlayerInterops.AudioInterops.GetDelay.Invoke(VlcContext.HandleManager.MediaPlayerHandles[myHostVlcControl]);
                }
                return 0;
            }
            set
            {
                if (VlcContext.InteropManager != null &&
                    VlcContext.InteropManager.MediaPlayerInterops != null &&
                    VlcContext.InteropManager.MediaPlayerInterops.AudioInterops != null &&
                    VlcContext.InteropManager.MediaPlayerInterops.AudioInterops.SetDelay.IsAvailable &&
                    VlcContext.HandleManager != null &&
                    VlcContext.HandleManager.MediaPlayerHandles != null &&
                    VlcContext.HandleManager.MediaPlayerHandles.ContainsKey(myHostVlcControl))
                {
                    VlcContext.InteropManager.MediaPlayerInterops.AudioInterops.SetDelay.Invoke(VlcContext.HandleManager.MediaPlayerHandles[myHostVlcControl], value);
                }
            }
        }

        internal VlcAudioProperties(IVlcControl vlcControl)
        {
            myHostVlcControl = vlcControl;
        }
    }
}