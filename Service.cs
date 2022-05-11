using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soccer
{
    public class Service
    {
        public static string FileUrl = "players.csv";
        public static string CurrentTeam = "";
        public List<Player> AllPlayer { get; set; }
        public List<Player> Players { get; set; }
        public Dictionary<string, List<Player>> Teams { get; set; }
        public List<string> AllTeams { get; set; }

        public Service()
        {
            LoadData();
        }
        private async Task<bool> LoadData()
        {
            Players = new List<Player>();
            Teams = new Dictionary<string, List<Player>>();
            AllPlayer = new List<Player>();
            string[] lines = File.ReadAllLines(FileUrl);
            for (int i = 1; i < lines.Length; i++)
            {
                string[] line = lines[i].Split(";");
                Player tmp = new Player()
                {
                    Nationality = line[0],
                    Image = line[1],
                    Name = line[2],
                    Team = line[3],
                    Gender = line[4],
                };
                AllPlayer.Add(tmp);
            }
            AllTeams = new List<string>();
            AllTeams = AllPlayer.Select(player => player.Team.Trim()).Distinct().OrderBy(q => q).ToList();
            var res = (from t in AllTeams join p in Players on t equals p.Team select t).ToList();
            foreach (string team in AllTeams)
            {
                Teams[team] = AllPlayer.Where(p => p.Team.Trim() == team).ToList();
            }
            Players = Teams[AllTeams[0]];
            CurrentTeam = AllTeams[0];
            return true;
        }

        public void UpdatingData(Player newPlayer)
        {
            AllPlayer.Add(newPlayer);
            if (AllTeams.Contains(newPlayer.Team))
            {
                if (CurrentTeam == newPlayer.Team) Players.Add(newPlayer);
                Teams[newPlayer.Team].Add(newPlayer);
            }
            else
            {
                AllTeams.Add(newPlayer.Team);
                Teams.Add(newPlayer.Team, new List<Player> { newPlayer });
            }


        }
    }
}
