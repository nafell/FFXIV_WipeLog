using Epoxy;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;

namespace WipeLog
{
    [ViewModel]
    internal class MainViewModel
    {
        public Dictionary<string, Func<RaidViewModel>> Raids { get; set; } = new Dictionary<string, Func<RaidViewModel>>();
        private KeyValuePair<string, Func<RaidViewModel>> _selectedRaid;
        public KeyValuePair<string, Func<RaidViewModel>> SelectedRaid 
        { 
            get { return _selectedRaid; }
            set
            {
                _selectedRaid = value;
                CurrentRaid = value.Value();
            }
        }
        public void SetSelectedRaid(KeyValuePair<string, Func<RaidViewModel>> value)
        {
            _selectedRaid = value;
            CurrentRaid = value.Value();
        }
        public ObservableCollection<ProblemViewModel> Problems { get; set; } = new ObservableCollection<ProblemViewModel>();
        public ObservableCollection<RoleViewModel> Roles { get; set; } = new ObservableCollection<RoleViewModel>();

        public SessionViewModel Session { get; set; } = new SessionViewModel();
        public WipeViewModel? SelectedWipe { get; set; }

        public RaidViewModel? CurrentRaid { get; set; }
        public MainViewModel()
        {
            var raid1 = new KeyValuePair<string, Func<RaidViewModel>>("天獄4層前半", CreateTengoku4a);
            Raids[raid1.Key] = raid1.Value;
            SetSelectedRaid(raid1);
            CreateProblems();
            CreateRoles();
        }

        private void CreateRaidInstance()
        {
            CurrentRaid = SelectedRaid.Value();
            CreateProblems();
            CreateRoles();
        }

        private void CreateProblems()
        {
            Problems = new ObservableCollection<ProblemViewModel>
            {
                new ProblemViewModel("集中切れ"),
                new ProblemViewModel("ど忘れ"),
                new ProblemViewModel("見間違い"),
                new ProblemViewModel("コールミス"),
                new ProblemViewModel("崩壊直後"),
                new ProblemViewModel("雑談"),
                new ProblemViewModel("譲り合い"),
                new ProblemViewModel("取り決め不足"),
                new ProblemViewModel("その他凡ミス"),
            };
        }
        private void CreateRoles()
        {
            Roles = new ObservableCollection<RoleViewModel>() 
            {
                new RoleViewModel("MT"),
                new RoleViewModel("ST"),
                new RoleViewModel("PH"),
                new RoleViewModel("BH"),
                new RoleViewModel("D1"),
                new RoleViewModel("D2"),
                new RoleViewModel("D3"),
                new RoleViewModel("D4"),
            };
        }

        private RaidViewModel CreateTengoku4a()
        {
            return new RaidViewModel
            (
                "天獄4層前半",
                new PhaseViewModel[]
                {
                    new PhaseViewModel
                    (
                        "パラ１",
                        new ActionViewModel[]
                        {
                            new ActionViewModel("ビーム")
                        }
                    ),
                    new PhaseViewModel
                    (
                        "パラ２",
                        new []
                        {
                            "塔設置","ビーム","塔踏み"
                        }
                    ),
                    new PhaseViewModel
                    (
                        "チェイン1",
                        new[]
                        {
                            "初期散開","4:4散開",
                            "内外","塔設置","塔踏み","範囲被さり"
                        }
                    ),
                    new PhaseViewModel
                    (
                        "パラ３",
                        new []
                        {
                            "TH塔踏み", "ビーム位置",
                            "ビーム誘導", "塔設置", "塔踏み", "全体攻撃"
                        }
                    ),
                    new PhaseViewModel
                    (
                        "サイコロ",
                        new []
                        {
                            "ビーム誘導","外周踏み",
                            "突進避け遅い","突進入るの早すぎ",
                            "1/3","2/4"
                        }
                    ),
                    new PhaseViewModel
                    (
                        "チェイン2A",
                        new []
                        {
                            "初期散開", "2個目駆け込み", "直進・折返し", "半面焼き", "最終散開", "アポ・ペリ"
                        }
                    ),
                    new PhaseViewModel
                    (
                        "チェイン2B",
                        new []
                        {
                            "ボス直線","4/8散開","雑魚直線","最終散開"
                        }
                    ),
                    new PhaseViewModel
                    (
                        "その他",
                        new [] {"その他"}
                    )
                }
            );
        }

        private void ExportCsv()
        {
            var str = "timestamp,phase,role,problem,reasons";
            foreach ( var wipe in Session.Wipes )
            {
                str += Environment.NewLine;
                str += wipe.Date.ToString("HH:mm:ss") + ",";
                str += wipe.WipePhaseName + ",";
                str += wipe.Roles + ",";
                str += wipe.Problems + ",";
                str += wipe.WipeActions;
            }
            File.WriteAllText($"session_{Session.Date.Year}_{Session.Date.Month}_{Session.Date.Day}.csv", str, Encoding.UTF8);
        }

        public Command AddWipeCommand => Command.Factory.Create<EventArgs>(async _ =>
        {
            if (CurrentRaid == null)
            {
                return;
            }

            Session.Wipes.Add(new WipeViewModel
                (
                    CurrentRaid, 
                    string.Join("/", Roles.Where(r => r.IsChecked).Select(rl => rl.Name)), 
                    string.Join("/", Problems.Where(p => p.IsChecked).Select(pb => pb.Name))
                ));
            CreateRaidInstance();
            ExportCsv();
        });

        public Command DeleteWipeCommand => Command.Factory.Create<EventArgs>(async _ =>
        {
            if (SelectedWipe == null) return;

            Session.Wipes.Remove(SelectedWipe);
        });

        public Command SetCurrentRaidTengoku4a => Command.Factory.Create<EventArgs>(async _ =>
        {
            var raid1 = new KeyValuePair<string, Func<RaidViewModel>>("天獄4層前半", CreateTengoku4a);
            Raids[raid1.Key] = raid1.Value;

            SetSelectedRaid(raid1);
        });

        public Command SetCurrentRaidTengoku4b => Command.Factory.Create<EventArgs>(async _ =>
        {
            var raid1 = new KeyValuePair<string, Func<RaidViewModel>>("天獄4層後半", CreateTengoku4a);
            Raids[raid1.Key] = raid1.Value;

            SetSelectedRaid(raid1);
        });
    }
}
