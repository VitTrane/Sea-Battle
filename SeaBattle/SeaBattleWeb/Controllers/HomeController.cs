using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SeaBattle.Models;
using React;

namespace SeaBattle.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Index/
        private User tempUser;

        public ActionResult Index()
        { 

            return View();
        }

        [HttpPost]
        public JsonResult UserLogin(User u)
        {
           // make u logged in.
            tempUser = u;
            bool status = false;
            string message = "";
            if (ModelState.IsValid)
            {
                  
                    status = true;
                    message = "";
            }
            else
            {
                message = "Failed! Please try again";
            }
            return new JsonResult { Data = new { status = status, message = message } };
        }

        [HttpPost]
        public JsonResult UserRegister(User u)
        {
            // register u and log in.
            tempUser = u;
            bool status = false;
            string message = "";
            if (ModelState.IsValid)
            {

                status = true;
                message = "ok";
            }
            else
            {
                message = "Failed! Please try again";
            }
            return new JsonResult { Data = new { status = status, message = message } };
        }

        [HttpPost]
        public JsonResult SendMessage(Message mess)
        {
            bool status = false;
            string message;
            string sendingMess = mess.Text;
            //to send message from Game
            if (ModelState.IsValid)
            {
                //to send mees to server
                status = true;
                message = "ok";
            }
            else
            {
                message = "Failed! Please try again";
            }
            return new JsonResult { Data = new { status = status, message = message } };
        }
    }
}
