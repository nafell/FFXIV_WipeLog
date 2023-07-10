using Epoxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WipeLog
{
    [ViewModel]
    internal class WipeViewModel
    {
        public DateTime Date { get; set; }
        public RaidViewModel Raid { get; set; }
        public string WipePhaseName { get; set; }
        public string WipeActions { get; set; }
        public string Roles { get; set; }
        public string Problems { get; set; }
        public WipeViewModel(RaidViewModel raid, string roles, string problems) 
        {
            Date = DateTime.Now;
            Raid = raid;

            Problems = problems;
            Roles = roles;
            var wipePhase = raid.Phases.First(ph => ph.Actions.Any(ac => ac.IsChecked));
            if (wipePhase == null)
            {
                return;
            }
            WipePhaseName = wipePhase.Name;
            WipeActions = string.Join(", ",wipePhase.Actions.Where(ac => ac.IsChecked).Select(a => a.Name));
        }
    }
}
