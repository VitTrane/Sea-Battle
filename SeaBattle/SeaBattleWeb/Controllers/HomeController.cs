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
        public JsonResult Shot(XYCoordinate shot)
        {
            var t = shot;
            bool status = false;
            string message = "";
            string shotSt = "";//{damaged, killed, miss}
            

            return new JsonResult { Data = new { status = status, message = message, shotStatus = shotSt} };
        }

        [HttpPost]
        public JsonResult TakeShot()
        {
            bool status = false;
            string message = "";
            string shotSt = "miss";//{damaged, killed, miss}
            int xs = 0;
            int ys = 0;
            status = true;
            

            return new JsonResult { Data = new { status = status, message = message, x = xs, y = ys, shotStatus = shotSt } };
        }

        [HttpPost]
        public JsonResult StartGame(ShipMap map)
        {
           
            bool status = false;
            string message = "";
            string enemyName = "";
            bool isplayerturn = false;
            //to do to send player shot

            return new JsonResult { Data = new { status = status, message = message, enemy = enemyName, isPlayerTurn = isplayerturn } };
        }

        [HttpPost]
        public JsonResult SendMessage(Message mess)
        {
            bool status = false;
            string message;
            string sendingMess = mess.Text;
            if (ModelState.IsValid)
            {
                //to send mess to server
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
        public JsonResult GetMessage()
        {
            bool status = false;
            bool isended = false;//The end of the game for some reason
            string message = "";
            string newMess = "";
            //insert new message into the newMess, the message is for the info about some error, status = true - there's message for player.

            return new JsonResult { Data = new { status = status, message = message, newMessage = newMess, isEnded = isended } };
        }
        //Exit game
        [HttpPost]
        public JsonResult ExitGame(Message m)
        {

            bool status = true;//true - everything is ok
            string message = "";
            string isEnded = m.Text;//false - the user left game before the end
            //
            return new JsonResult { Data = new { status = status, message = message} };
        }
        //Create game
        [HttpPost]
        public JsonResult CreateGame()
        {
            bool status = false;
            string message = "";
            string userName = "";//The name of the person who wanna play with you
            bool founded = false;//true - somebody wanna play with you
            //
            return new JsonResult { Data = new { status = status, message = message, isFounded = founded, userName = userName } };
        }
        [HttpPost]
        public JsonResult StopWaitForPlayer()
        {
            bool status = true;
            string message = "";

            //remove the game from actual list
            return new JsonResult { Data = new { status = status, message = message} };
        }
    }
}
