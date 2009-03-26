using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms.Integration;
using Vlc.DotNet.Core;
using Panel = System.Windows.Forms.Panel;

namespace Vlc.DotNet.Wpf
{
    public sealed class VlcControl : Control, IDisposable, IVlcControl
    {
        #region Manager Property

        private static readonly DependencyProperty ManagerProperty;

        public VlcManager Manager
        {
            get
            {
                return GetValue(ManagerProperty) as VlcManager;
            }
            set
            {
                SetValue(ManagerProperty, value);
            }
        }

        private static void VlcManagerChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            var ctrl = o as VlcControl;
            var manager = e.NewValue as VlcManager;
            var ctrlTemplate = DependencyPropertyDescriptor.FromProperty(TemplateProperty, typeof(VlcControl)).GetValue(ctrl) as ControlTemplate;
            if (ctrlTemplate == null || ctrl == null)
                return;
            var box = ctrlTemplate.FindName("PnlBox", ctrl) as Panel;
            if (manager != null && box != null)
                manager.ControlHandle = box.Handle;
        }

        #endregion

        static VlcControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(
                typeof(VlcControl),
                new FrameworkPropertyMetadata(typeof(VlcControl)));

            ManagerProperty = DependencyProperty.Register(
                "Manager",
                typeof(VlcManager),
                typeof(VlcControl),
                new PropertyMetadata(new VlcManager(), VlcManagerChanged));
        }

        public VlcControl()
        {
            var isDesignMode = ((bool)DependencyPropertyDescriptor.FromProperty(DesignerProperties.IsInDesignModeProperty, typeof(DependencyObject)).Metadata.DefaultValue);
            if (!isDesignMode)
            {
                Loaded += ((sender, e) => OnLoad());
            }
        }

        #region IDisposable Members

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion

        ~VlcControl()
        {
            Dispose(false);
        }

        private void Dispose(bool disposing)
        {
            Manager.Dispose();

            if (disposing)
            {
                var host = Template.FindName("WFH", this) as WindowsFormsHost;
                if (host != null)
                {
                    if (host.Child != null)
                    {
                        host.Child.Dispose();
                        host.Child = null;
                    }
                    if (host.Handle != IntPtr.Zero)
                    {
                        host.Dispose();
                    }
                }
            }
        }

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);

            Window window = Window.GetWindow(this);
            if (window != null)
                window.Closing += ((sender, arg) => Dispose());

        }

        private void OnLoad()
        {
            if (Manager != null && Manager.AutoStart)
            {
                Manager.Play();
            }
        }

        public override void OnApplyTemplate()
        {
            var host = Template.FindName("WFH", this) as WindowsFormsHost;
            var box = Template.FindName("PnlBox", this) as Panel;

            if (host != null && box != null)
            {
                if (Manager == null)
                {
                    Manager = new VlcManager();
                }
                Manager.ControlHandle = box.Handle;
            }
        }
    }
}