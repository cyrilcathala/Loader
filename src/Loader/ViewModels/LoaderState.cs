using System;
using System.Collections.Generic;
using System.Text;

namespace Loader.ViewModels
{
    public enum LoaderState
    {
        NotStarted,
        Loading,
        Completed,
        Faulted,
        Empty
    }
}
