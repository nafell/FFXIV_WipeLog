using Epoxy;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WipeLog
{
    [ViewModel]
    internal class SessionViewModel
    {
        public DateTime Date { get; set; }
        public ObservableCollection<WipeViewModel> Wipes { get; set; } = new ObservableCollection<WipeViewModel>();

        public SessionViewModel()
        {
            Date = DateTime.Now;
        }
    }
}
