using DataObjects;
using LogicLayer;
using System.Collections.Generic;
using System.Windows;

namespace RollerDerby
{
    /// <summary>
    /// Interaction logic for Add_Skater.xaml
    /// </summary>
    public partial class Add_Skater : Window
    {
        Skater _skt;
        SkaterManager _sm;
        LeagueManager _lm;

        public Add_Skater()
        {
            InitializeComponent();
            _skt = new Skater();
            _sm = new SkaterManager();
            _lm = new LeagueManager();
            List<League> _leagues = _lm.getAllLeague();
            List<string> _leagueNames = new List<string>();
            foreach (League l in _leagues)
            {
                _leagueNames.Add(l.LeagueID);

            }
            cboTeamID.Items.Clear();
            cboTeamID.ItemsSource = _leagueNames;


        }

        private void btnCreateSkater_Click(object sender, RoutedEventArgs e)
        {
            _skt.Email = txtEmail.Text;
            _skt.SkaterID = txtSkaterID.Text;
            _skt.FamilyName = txtFamilyName.Text;
            _skt.GivenName = txtGivenName.Text;
            _skt.Phone = txtPhone.Text;
            _skt.TeamID = cboTeamID.Text;
            _sm.createSkater(_skt);


        }
    }
}
