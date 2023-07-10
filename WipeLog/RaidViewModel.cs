using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Epoxy;
using Epoxy.Supplemental;

namespace WipeLog
{
    [ViewModel]
    internal class RaidViewModel
    {
        public string Name { get; set; }
        public ObservableCollection<PhaseViewModel> Phases { get; set; } = new ObservableCollection<PhaseViewModel>();
        public RaidViewModel(string name, PhaseViewModel[] phases) 
        {
            Name = name;
            Phases.AddRange(phases);
        }
    }
}
