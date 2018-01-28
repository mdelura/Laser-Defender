using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class PreferenceChangedEventArgs
{
    public PreferenceChangedEventArgs(string preferenceName, object value)
    {
        PreferenceName = preferenceName;
        Value = value;
    }

    public string PreferenceName { get; private set; }

    public object Value { get; private set; }
}
