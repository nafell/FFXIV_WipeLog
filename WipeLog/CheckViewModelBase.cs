using Epoxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WipeLog
{
    [ViewModel]
    internal class CheckViewModelBase
    {
        public string Name { get; set; }
        public bool IsChecked { get; set; }

        public CheckViewModelBase(string name)
        {
            Name = name;
        }
    }
}
