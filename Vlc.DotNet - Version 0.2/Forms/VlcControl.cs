using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Security.Permissions;
using System.Windows.Forms;
using Vlc.DotNet.Core;

namespace Vlc.DotNet.Forms
{
    /// <summary>
    /// Vlc WinForms Control
    /// </summary>
    public sealed class VlcControl : Control, IVlcControl
    {
        private const int WM_NCPAINT = 0x85;
        private bool isFirstWM_NCPAINT = true;

        /// <summary>
        /// Manager of the VlcControl
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public VlcManager Manager { get; private set; }

        /// <summary>
        /// Constructor of VlcControl
        /// </summary>
        public VlcControl()
        {
            BackColor = Color.Black;
            if(DesignMode)
                return;
            Manager = new VlcManager();
            try
            {
                CreateHandle();
                Manager.ControlHandle = Handle;
                Manager.VlcLibPath = Path.GetDirectoryName(Application.ExecutablePath);
            }
            catch (Exception)
            {
                return;
            }
        }

        /// <summary>
        /// Disposes of the resources (other than memory) used by the Vlc.DotNet.Forms.VlcControl.
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            Manager.Dispose();

            if (disposing)
            {
            }
        }

        /// <summary>
        /// Destructor of VlcControl
        /// </summary>
        ~VlcControl()
        {
            Dispose(false);
        }

        [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case WM_NCPAINT:
                    if (isFirstWM_NCPAINT)
                    {
                        if (Manager != null && Manager.AutoStart)
                            Manager.Play();
                        isFirstWM_NCPAINT = false;
                    }
                    break;
            }
            base.WndProc(ref m);
        }
    }
}
