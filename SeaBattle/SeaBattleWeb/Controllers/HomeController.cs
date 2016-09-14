using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SeaBattle.Models;
using React;
using SeaBattle.GameServiceReference;
using SeaBattle.Common;

namespace SeaBattle.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Index/

        public ActionResult Index()
        { 
            return View();
        }

        /// <summary>
        /// Метод авторизации пользователя.
        /// </summary>
        /// <param name="u">Экземпляр пользователя с указанным логином и паролем.</param>
        /// <returns>Json ответ о состоянии авторизации.</returns>
        [HttpPost]
        public JsonResult UserLogin(User u)
        {
            bool status = false;
            string message = "";

            if (ModelState.IsValid)
            {
                try
                {
                    ClientManager.Instance.CreateClient();
                    var authrequest = new AuthorizeRequest { Login = u.Login, Password = u.Password };
                    AuthorizeResponse authresponse = ClientManager.Instance.Client.Authorize(authrequest);
                    if (authresponse.IsSuccess)
                    {
                        status = true;
                        message = "Ok";
                    }
                    else
                    {
                        message = "Error: " + authresponse.Error;
                    }
                }
                catch (Exception e)
                {
                    message = "Exception: " + e.Message;
                    ClientManager.Instance.Dispose();
                }
            }
            else
            {
                message = "Error: Login or password are uncorrect";
            }

            return new JsonResult { Data = new { status = status, message = message } };
        }

        /// <summary>
        /// Метод регистрации пользователя.
        /// </summary>
        /// <param name="u">Экземпляр пользователя с логином, паролем и почтой.</param>
        /// <returns>Json ответ о состоянии регистрации.</returns>
        [HttpPost]
        public JsonResult UserRegister(User u)
        {
            bool status = false;
            string message = "";
            try
            {
                if (ModelState.IsValid)
                {
                    ClientManager.Instance.CreateClient();
                    var registerrequeest = new RegisterRequest() { Login = u.Login, Password = u.Password, Email = u.Email };
                    RegisterResponse registerresponse = ClientManager.Instance.Client.Register(registerrequeest);
                    if (registerresponse.IsSuccess)
                    {
                        status = true;
                        message = "Ok";
                    }
                    else
                    {
                        message = "Error: " + registerresponse.Error;
                    }
                }
                else
                {
                    message = "Error: Login or password are uncorrect";
                }
            }
            catch (Exception e)
            {
                message = "Exception: " + e.Message;
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
