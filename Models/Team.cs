using System;
using System.Collections.Generic;

namespace FantasyProject.Models
{
    public class Team
    {
        public int TeamId { get; set; }
        public string Abbr { get; set; }
        public String FullName { get; set; }
        public List<Player> Players { get; set; }
        public Team()
        {
            Players = new List<Player>();
        }
    }
}