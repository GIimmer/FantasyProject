using System;
using System.Collections.Generic;

namespace FantasyProject.Models
{
    public class Player
    {
        public int PlayerId { get; set; }
        public double Rank { get; set; }
        public string Name { get; set; }
        public string TeamName { get; set; }
        public string Position { get; set; }
        public double Age { get; set; }
        public double Games { get; set; }
        public double GamesStarted { get; set; }
        public double PassCompleted { get; set; }
        public double PassAttempted { get; set; }
        public double PassYards { get; set; }
        public double PassTouchdowns { get; set; }
        public double PassInterfered { get; set; }
        public double RushAttempts { get; set; }
        public double RushYards { get; set; }
        public double RushYA { get; set; }
        public double RushTouchdowns { get; set; }
        public double RecTargets { get; set; }
        public double RecReceptions { get; set; }
        public double RecYards { get; set; }
        public double RecYR { get; set; }
        public double RecTD { get; set; }
        public double TwoPtConvMade { get; set; }
        public double TwoPtConvPass { get; set; }
        public double FPointsNFL { get; set; }
        public double FPointsPPR { get; set; }
        public double FPointsDraftKing { get; set; }
        public double FPointsFanDuel { get; set; }
        public double FPointsVBD { get; set; }
        public double PosRank { get; set; }
        public double OvRank { get; set; }
        public int TeamId { get; set; }
        public Team Team { get; set; }
    }
}