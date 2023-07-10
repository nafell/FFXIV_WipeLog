using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Epoxy;

namespace WipeLog
{
    [ViewModel]
    internal class ActionViewModel : CheckViewModelBase
    {
        public ActionViewModel(string name) : base(name)
        {
        }
    }
}
