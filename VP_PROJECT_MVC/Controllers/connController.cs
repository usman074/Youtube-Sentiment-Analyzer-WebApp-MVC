using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using VP_PROJECT_MVC.Models;

namespace VP_PROJECT_MVC.Controllers
{
    public class connController : Controller
    {
       
        //
        // GET: /conn/
        public ActionResult Index()
        {
            return View();
        }
        public async Task<ActionResult> conn()
        {
            List<String> con = null;
            String id = Request.Form["value"];
            conn c = new conn();
            
            if (id != null)
            {
                var x = c.Fetch_Data(id);
                con = await x;
                
            }
            if (con != null)
            {
                ViewBag.newData = con;
                ViewBag.score = con[0]+"%";
                ViewBag.sad = con[1] + "%";
                ViewBag.joy = con[2] + "%";
                ViewBag.fear = con[3] + "%";
                ViewBag.disgust = con[4] + "%";
                ViewBag.anger = con[5] + "%";
                ViewBag.positive = con[6];
                ViewBag.negative = con[7];
                ViewBag.neutral = con[8];
            }
            
            return View();
        }
	}
}