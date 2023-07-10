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
    internal class PhaseViewModel
    {
        public string Name { get; set; }
        public bool IsExpanded { get; set; }
        public ObservableCollection<ActionViewModel> Actions { get; set; } = new ObservableCollection<ActionViewModel>();

        public PhaseViewModel(string name, ActionViewModel[] actions, bool expanded = true) 
        {
            Name = name;
            Actions.AddRange(actions);
            Actions.Add(new ActionViewModel("その他"));
            IsExpanded = expanded;
        }

        public PhaseViewModel(string name, string[] actions, bool expanded = true)
        {
            Name = name;
            Actions.AddRange(actions.Select((a) => new ActionViewModel(a)));
            Actions.Add(new ActionViewModel("その他"));
            IsExpanded = expanded;
        }
    }
}
