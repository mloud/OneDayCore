using System;
using System.Collections;

namespace OneDay.Core.Ui
{
    public interface IShowable
    {
        IEnumerator Show(KeyValueData data, Action hide);
        IEnumerator Hide();
    }
}
