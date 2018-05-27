using Competition.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Competition.ViewModels
{
    class MatchesVM
    {
        private static MatchesVM matchesVM = null;

        public static MatchesVM GetMatchesVM()
        {
            if (matchesVM == null)
                matchesVM = new MatchesVM();
            return matchesVM;
        }
        private MatchesVM() { }

        private Matches selectedMatch = null;

        public Matches SelectedMatch
        {
            get { return selectedMatch; }
            set { this.selectedMatch = value; }
        }
        private ObservableCollection<Matches> allMatches = new ObservableCollection<Matches>();
        public ObservableCollection<Matches> AllMatches { get { return this.allMatches; } }
    }
}
