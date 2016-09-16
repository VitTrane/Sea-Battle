using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SeaBattle.GameServiceReference;
using SeaBattle.Common;

namespace SeaBattle.Controllers
{
    public class GameController : Controller
    {
        //
        // GET: /Game/

        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Метод запроса соперника игры.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetOpponent()
        {
            bool status = false;
            string message = "";
            ClientManager.Instance.Client.CreateGame(new CreateGameRequest());
            var resp = new CreateGameResponse();
            ClientManager.Instance.Callback.CreateGameCallBack(resp);
            if (resp.IsSuccess)
            {
                status = true;
                message = "Ok";
            }
            else
            {
                message = resp.Error + resp.ExtensionData;
            }
            return new JsonResult { Data = new { status = status, message = message } };
        }

    }
}
