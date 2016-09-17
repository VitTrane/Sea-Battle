using SeaBattle.GameService;
using System;

namespace SeaBattle.BattleShipServiceCallback
{
    public class ResponseEventArgs : EventArgs
    {
        public BaseResponse Response { get; set; }
    }
}
