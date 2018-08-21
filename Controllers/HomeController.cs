using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FantasyProject.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;

namespace FantasyProject.Controllers
{
    public class HomeController : Controller
    {
        private FantasyProjectContext _context;
        public HomeController(FantasyProjectContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            bool Changed = false;
            if(Changed){
                return RedirectToAction("LoadTeams");
            }
            else{
                return RedirectToAction("DisplayPlayers");
            }
        }

        [HttpGet("LoadTeams")]
        public IActionResult LoadTeams()
        {
            List<Team> TheseTeams = _context.Teams.ToList();
            foreach(Team ThisTeam in TheseTeams){
                _context.Teams.Remove(ThisTeam);
                _context.SaveChanges();
            }
            var TeamList = new List<(string, string)>
            {
                ("ARI", "Arizona Cardinals"),
                ("ATL", "Atlanta Falcons"),
                ("BAL", "Baltimore Ravens"),
                ("BUF", "Buffalo Bills"),
                ("CAR", "Carolina Panthers"),
                ("CHI", "Chicago Bears"),
                ("CIN", "Cincinatti Bengals"),
                ("CLE", "Cleveland Browns"),
                ("DAL", "Dallas Cowboys"),
                ("DEN", "Denver Broncos"),
                ("DET", "Detroit Lions"),
                ("GNB", "Greenbay Packers"),
                ("HOU", "Houston Texans"),
                ("IND", "Indianapolis Colts"),
                ("JAX", "Jacksonville Jaguars"),
                ("KAN", "Kansas City Chiefs"),
                ("LAC", "Los Angeles Chargers"),
                ("LAR", "Los Angeles Rams"),
                ("MIA", "Miami Dolphins"),
                ("MIN", "Minnesota Vikings"),
                ("NOR", "New Orleans Saints"),
                ("NEW", "New England Patriots"),
                ("NYG", "New York Giants"),
                ("NYJ", "New York Jets"),
                ("OAK", "Oakland Raiders"),
                ("PHI", "Philadelphia Eagles"),
                ("PIT", "Pittsburgh Steelers"),
                ("SEA", "Seattle Seahawks"),
                ("SFO", "San Fransisco 49ers"),
                ("TAM", "Tampa Bay Buccaneers"),
                ("TEN", "Tennessee Titans"),
                ("WAS", "Washington Redskins"),
                ("2TM", "Multiple Teams")
            };
            for(int i = 0; i < TeamList.Count; i++)
            {
                Team NewTeam = new Team
                {
                    Abbr = TeamList[i].Item1,
                    FullName = TeamList[i].Item2
                };
                _context.Add(NewTeam);
                _context.SaveChanges();
            }
            return RedirectToAction("LoadSeasonStats");
        }
        
        [HttpGet("LoadSeasonStats")]
        public IActionResult LoadSeasonStats()
        {
            StreamReader sr = new StreamReader("CSVPlayerFile.txt");

            string CurrentLine;
            while((CurrentLine = sr.ReadLine()) != null)
            {
                List<string> LineValues = CurrentLine.Split(',').ToList<string>();
                try
                {
                    double PlayerRank = Convert.ToDouble(LineValues[0]);
                    string PlayerName;
                    if (LineValues[1].Contains('*'))
                    {
                        int index = LineValues[1].IndexOf('*');
                        string result = LineValues[1].Substring(0, index);
                        PlayerName = result;
                    } else {
                        int index = LineValues[1].IndexOf('\\');
                        string result = LineValues[1].Substring(0, index);
                        PlayerName = result;
                    }
                    List<double> Result = new List<double>();
                    foreach(string item in LineValues.Skip(4)){
                        if(item != ""){
                            Result.Add(double.Parse(item));
                        } else {
                            Result.Add(0.0);
                        }
                    }

                    Player ThisPlayer = new Player()
                    {
                        Rank = PlayerRank,
                        TeamName = LineValues[2],
                        Position = LineValues[3],
                        Age = Result[0],
                        Games = Result[1],
                        GamesStarted = Result[2],
                        PassCompleted = Result[3],
                        PassAttempted = Result[4],
                        PassYards = Result[5],
                        PassTouchdowns = Result[6],
                        PassInterfered = Result[7],
                        RushAttempts = Result[8],
                        RushYards = Result[9],
                        RushYA = Result[10],
                        RushTouchdowns = Result[11],
                        RecTargets = Result[12],
                        RecReceptions = Result[13],
                        RecYards = Result[14],
                        RecYR = Result[15],
                        RecTD = Result[16],
                        TwoPtConvMade = Result[17],
                        TwoPtConvPass = Result[18],
                        FPointsNFL = Result[19],
                        FPointsPPR = Result[20],
                        FPointsDraftKing = Result[21],
                        FPointsFanDuel = Result[22],
                        FPointsVBD = Result[23],
                        PosRank = Result[24],
                        OvRank = Result[25],
                    };
                    if(_context.Players.Where(p => p.Name == PlayerName).FirstOrDefault() != null){
                        PlayerName += '2';
                        ThisPlayer.Name = PlayerName;
                        _context.Add(ThisPlayer);
                    } else {
                        ThisPlayer.Name = PlayerName;
                    }
                    Team TeamOfPlayer = _context.Teams.SingleOrDefault(t => t.Abbr == LineValues[2]);
                    TeamOfPlayer.Players.Add(ThisPlayer);
                    _context.SaveChanges();
                } catch(Exception e) {
                    System.Console.WriteLine("An error occurred: '{0}'", e);
                }
            }
            return RedirectToAction("DisplayPlayers");
        }

        public IActionResult DisplayPlayers()
        {
            
            List<Player> ListPlayers = _context.Players.Where(p => p.TeamName == "SEA").ToList();
            return View("DisplayPlayers", ListPlayers);
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
