using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Vlc.DotNet.Core;

namespace Vlc.DotNet.Silverlight.SampleApplication
{
    public class VlcContextParameterModel : INotifyDataErrorInfo, INotifyPropertyChanged
    {
        private readonly IDictionary<string, string> myErrors = new Dictionary<string, string>();

        private string myVlcDirectory;

        public string VlcLibDirectory
        {
            get { return myVlcDirectory; }
            set
            {
                if (!IsVlcLibDirectoryValid(value) || myVlcDirectory == value) 
                    return;
                myVlcDirectory = value;
                OnPropertyChanged("VlcLibDirectory");
            }
        }

        public bool IsVlcLibDirectoryValid(string vlcLibDirectory)
        {
            var oldValue = VlcContext.LibVlcDllsPath;
            var result = true;
            VlcContext.LibVlcDllsPath = vlcLibDirectory;
            try
            {
                VlcContext.Initialize();
            }
            catch(Exception ex)
            {
                myErrors["VlcLibDirectory"] = ex.Message;
                result = false;
            }
            finally
            {
                VlcContext.LibVlcDllsPath = oldValue;
            }
            return result;
        }

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        public System.Collections.IEnumerable GetErrors(string propertyName)
        {
            return myErrors[propertyName];
        }

        public bool HasErrors
        {
            get { return myErrors.Count > 0; }  
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if(PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
