using SeaBattle.GameService;
using SeaBattle.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeaBattle.BattleShipServiceCallback
{
    public class BattleShipCallback : IServiceCallback
    {
        public void AuthorizeCallback(AuthorizeResponse response)
        {
            ClientManager.Instance.AddResponse<AuthorizeResponse>(response);
        }

        public void RegisterCallback(RegisterResponse response)
        {
            ClientManager.Instance.AddResponse<RegisterResponse>(response);
        }

        public void DoShotCallback(ShotResponse response)
        {
            ClientManager.Instance.AddResponse<ShotResponse>(response);
        }

        public void GetListAvailableGames(GetListGamesResponse response)
        {
            ClientManager.Instance.AddResponse<GetListGamesResponse>(response);
        }

        public void GiveConnectedOpponentInfo(CurentGameResponse response)
        {
            ClientManager.Instance.AddResponse<CurentGameResponse>(response);
        }

        public void ConnectToGameCallback(CurentGameResponse response)
        {
            ClientManager.Instance.AddResponse<CurentGameResponse>(response);
        }

        public void SendReadyCallback(SendReadyResponse response)
        {
            ClientManager.Instance.AddResponse<SendReadyResponse>(response);
        }

        public void StartGame()
        {
        }

        public void EndGame(EndGameResponse response)
        {
            ClientManager.Instance.AddResponse<EndGameResponse>(response);
        }

        public void AbortGame(AbortGameResponse response)
        {
            ClientManager.Instance.AddResponse<AbortGameResponse>(response);
        }

        public void StartChat(StartChatResponse response)
        {
            ClientManager.Instance.AddResponse<StartChatResponse>(response);
        }

        public void RecieveMessage(RecieveMessageResponse response)
        {
            ClientManager.Instance.AddResponse<RecieveMessageResponse>(response);
        }

        public void GetTopPlayersCallback(GetTopPlayersResponse response)
        {
            ClientManager.Instance.AddResponse<GetTopPlayersResponse>(response);
        }

        public void GetStatisticLastGamesCallBack(GetLastGamesResponse response)
        {
            ClientManager.Instance.AddResponse<GetLastGamesResponse>(response);
        }

        public void CreateGameCallBack(CreateGameResponse response)
        {
            ClientManager.Instance.AddResponse<CreateGameResponse>(response);
        }

        public void SendOpponentIsReady()
        {
        }
    }
}
