using System;
using System.Collections.Generic;

namespace FantasyProject.Models
{
    public class FantasyTeam
    {
        public int FantasyTeamId { get; set; }
        public double Wallet { get; set; }
        public string Password { get; set; }
        public string TeamName { get; set; }
        public List<Player> Players { get; set; }
        public FantasyTeam()
        {
            Players = new List<Player>();
        }
    }
}