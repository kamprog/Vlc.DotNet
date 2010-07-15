using System;
using System.Collections.Generic;
using Vlc.DotNet.Core.Interop.Vlc;

namespace Vlc.DotNet.Core.Options
{
    public class OptionsBase
    {
        internal readonly Dictionary<Enum, IOption> myDicOptions = new Dictionary<Enum, IOption>();

        internal OptionsBase()
        {
        }

        internal void SetOptions(string optionPrefix, VlcMedia media, Type optionEnumType)
        {
            foreach (string element in Enum.GetNames(optionEnumType))
            {
                if (myDicOptions[(Enum) Enum.Parse(optionEnumType, element)].IsEnabled)
                    media.AddOption(myDicOptions[(Enum) Enum.Parse(optionEnumType, element)].GetOptionString(optionPrefix));
            }
        }

        internal string[] RetreiveOptions(string optionPrefix, Type optionEnumType)
        {
            var lst = new List<string>();
            foreach (string element in Enum.GetNames(optionEnumType))
            {
                if (myDicOptions[(Enum) Enum.Parse(optionEnumType, element)].IsEnabled)
                    lst.Add(myDicOptions[(Enum) Enum.Parse(optionEnumType, element)].GetOptionString(optionPrefix));
            }
            return lst.ToArray();
        }
    }
}