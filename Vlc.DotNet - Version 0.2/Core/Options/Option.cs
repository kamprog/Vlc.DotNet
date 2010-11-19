using System;

namespace Vlc.DotNet.Core.Options
{
    internal class Option<T> : IOption
    {
        private readonly Func<T, string> myGetOptionString;
        private readonly Func<T, bool> myValidator;

        private bool isEnabled;
        private T myValue;

        public Option(T defaultValue)
        {
            myValue = defaultValue;
        }

        public Option(Func<T, string> getOptionString)
        {
            myGetOptionString = getOptionString;
        }

        public Option(T defaultValue, Func<T, string> getOptionString)
            : this(defaultValue)
        {
            myGetOptionString = getOptionString;
        }

        public Option(Func<T, string> getOptionString, Func<T, bool> validator)
            : this(getOptionString)
        {
            myValidator = validator;
        }

        public Option(T defaultValue, Func<T, string> getOptionString, Func<T, bool> validator)
            : this(getOptionString)
        {
            myValidator = validator;
            myValue = defaultValue;
        }

        public T Value
        {
            get
            {
                return myValue;
            }
            set
            {
                if (myValidator != null)
                {
                    if (myValidator.Invoke(value))
                    {
                        myValue = value;
                    }
                    return;
                }
                myValue = value;
            }
        }

        #region IOption Members

        bool IOption.IsEnabled
        {
            get
            {
                return isEnabled;
            }
            set
            {
                isEnabled = value;
            }
        }

        void IOption.SetValue(object value)
        {
            Value = (T) value;
            isEnabled = true;
        }

        string IOption.GetOptionString(string prefix)
        {
            return prefix + myGetOptionString.Invoke(Value);
        }

        #endregion
    }
}