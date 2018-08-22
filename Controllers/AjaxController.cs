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
    public class AjaxController : Controller
    {   
        private FantasyProjectContext _context;
        public AjaxController(FantasyProjectContext context)
        {
            _context = context;
        }


        [HttpGet]
        public PartialViewResult AllOfTeam(string team)
        {
            System.Console.WriteLine(team);
            List<Player> ListPlayers = _context.Players.Where(p => p.TeamName == team).ToList();
            System.Console.WriteLine(ListPlayers);
            return PartialView("~/Views/Ajax/PlayerData.cshtml", ListPlayers);
        }

        [HttpGet]
        public PartialViewResult AllOfPos(string position)
        {
            System.Console.WriteLine(position);
            System.Console.WriteLine("I'm here!");
            List<Player> ListPlayers = _context.Players.Where(p => p.Position == position).ToList();
            return PartialView("~/Views/Ajax/PlayerData.cshtml", ListPlayers);
        }

        [HttpGet]
        public PartialViewResult AllOfAvail()
        {
            List<Player> ListPlayers = _context.Players.Where(p => p.FantasyTeamId == -1).ToList();
            return PartialView("~/Views/Ajax/PlayerData.cshtml", ListPlayers);
        }
    }
}