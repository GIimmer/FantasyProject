using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Web.Script.Serialization;
using FantasyProject.Models;
using Microsoft.EntityFrameworkCore;

namespace FantasyProject.Controllers
{
    public class AjaxController : Controller
    {   
        private FantasyProjectContext _context;
        public AjaxController(FantasyProjectContext context)
        {
            _context = context;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Ajax.Utility.RegisterTypeForAjax
        }

        [HttpPost("/Sort/PlayerName")]
        public PartialViewResult SortPlayerName()
        {
            List<Player> ListPlayers = _context.Players.Where(p => p.TeamName == "SEA").OrderBy(p => p.Name).ToList();
            var serializer = new JavaScriptSerializer();
            var serializedResult = serializer.Serialize(ListPlayers);
            return View("DisplayPlayers", serializedResult);
        }
    }
}