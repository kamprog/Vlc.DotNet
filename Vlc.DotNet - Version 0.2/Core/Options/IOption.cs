namespace Vlc.DotNet.Core.Options
{
    internal interface IOption
    {
        bool IsEnabled { get; set; }
        void SetValue(object value);
        string GetOptionString(string prefix);
    }
}