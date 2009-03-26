using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Security.Permissions;
using System.Windows.Forms;
using Vlc.DotNet.Core;

namespace Vlc.DotNet.Forms
{
    public sealed class VlcControl : Control, IVlcControl
    {
        private const int WM_NCPAINT = 0x85;
        private bool isFirstWM_NCPAINT = true;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public VlcManager Manager { get; private set; }

        public VlcControl()
        {
            Manager = new VlcManager();
            BackColor = Color.Black;
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

        protected override void Dispose(bool disposing)
        {
            Manager.Dispose();

            if (disposing)
            {
            }
        }

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
