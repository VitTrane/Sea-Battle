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
            ClientManager.Instance.AddResponses<AuthorizeResponse>(response);
        }

        public void RegisterCallback(RegisterResponse response)
        {
            ClientManager.Instance.AddResponses<RegisterResponse>(response);
        }

        public void DoShotCallback(ShotResponse response)
        {
            ClientManager.Instance.AddResponses<ShotResponse>(response);
        }

        public void GetListAvailableGames(GetListGamesResponse response)
        {
            ClientManager.Instance.AddResponses<GetListGamesResponse>(response);
        }

        public void GiveConnectedOpponentInfo(CurentGameResponse response)
        {
            ClientManager.Instance.AddResponses<CurentGameResponse>(response);
        }

        public void ConnectToGameCallback(CurentGameResponse response)
        {
            ClientManager.Instance.AddResponses<CurentGameResponse>(response);
        }

        public void SendReadyCallback(SendReadyResponse response)
        {
            ClientManager.Instance.AddResponses<SendReadyResponse>(response);
        }

        public void StartGame()
        {
        }

        public void EndGame(EndGameResponse response)
        {
            ClientManager.Instance.AddResponses<EndGameResponse>(response);
        }

        public void AbortGame(AbortGameResponse response)
        {
            ClientManager.Instance.AddResponses<AbortGameResponse>(response);
        }

        public void StartChat(StartChatResponse response)
        {
            ClientManager.Instance.AddResponses<StartChatResponse>(response);
        }

        public void RecieveMessage(RecieveMessageResponse response)
        {
            ClientManager.Instance.AddResponses<RecieveMessageResponse>(response);
        }

        public void GetTopPlayersCallback(GetTopPlayersResponse response)
        {
            ClientManager.Instance.AddResponses<GetTopPlayersResponse>(response);
        }

        public void GetStatisticLastGamesCallBack(GetLastGamesResponse response)
        {
            ClientManager.Instance.AddResponses<GetLastGamesResponse>(response);
        }

        public void CreateGameCallBack(CreateGameResponse response)
        {
            ClientManager.Instance.AddResponses<CreateGameResponse>(response);
        }

        public void SendOpponentIsReady()
        {
        }
    }
}
