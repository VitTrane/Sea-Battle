using SeaBattle.GameService;
using SeaBattle.Managers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SeaBattle.BattleShipServiceCallback
{
    public class ResponseEventArgs : EventArgs
    {
        public BaseResponse Response { get; set; }
    }

    public class BattleShipCallback : IServiceCallback
    {
        private SynchronizationContext syncContext;
        private Dictionary<Type, EventHandler<ResponseEventArgs>> _handlers;

        public BattleShipCallback()
        {
            syncContext = AsyncOperationManager.SynchronizationContext;
            _handlers = new Dictionary<Type, EventHandler<ResponseEventArgs>>();
        }

        /// <summary>
        /// Добавляет обработчик ответа от сервера
        /// </summary>
        /// <typeparam name="T">Тип ответа, для которого нужен обработчик</typeparam>
        /// <param name="handler">Метод обработки ответа</param>
        public void SetHandler<T>(EventHandler<ResponseEventArgs> handler)
        {
            if (!_handlers.ContainsKey(typeof(T)))
                _handlers.Add(typeof(T), handler);
            else
                _handlers[typeof(T)] = handler;
        }

        /// <summary>
        /// Удаляет обработчик ответа
        /// </summary>
        /// <typeparam name="T">Тип ответа, который больше н нужно обрабатывать</typeparam>
        public void RemoveHandler<T>()
        {
            if (_handlers.ContainsKey(typeof(T)))
                _handlers.Remove(typeof(T));
        }

        /// <summary>
        /// Вызывает обработчик ответа
        /// </summary>
        /// <typeparam name="T">Тип ответа, который нужно обработать</typeparam>
        /// <param name="eventData">Данные для обработчика</param>
        private void OnBroadcast<T>(object eventData)
        {
            ResponseEventArgs eventArgs = new ResponseEventArgs() { Response = (AuthorizeResponse)eventData };

            if (_handlers.ContainsKey(typeof(T)) && _handlers[typeof(T)] != null)
            {
                _handlers[typeof(T)].Invoke(this, eventArgs);
            }
        }

        public void DoShotCallback(ShotResponse response)
        {
            syncContext.Post(new SendOrPostCallback(OnBroadcast<ShotResponse>), response);
        }

        public void GiveConnectedOpponentInfo(CurentGameResponse response)
        {
            syncContext.Post(new SendOrPostCallback(OnBroadcast<CurentGameResponse>), response);
        }

        public void ConnectToGameCallback(CurentGameResponse response)
        {
            syncContext.Post(new SendOrPostCallback(OnBroadcast<CurentGameResponse>), response);
        }

        public void SendReadyCallback(SendReadyResponse response)
        {
            syncContext.Post(new SendOrPostCallback(OnBroadcast<SendReadyResponse>), response);
        }

        public void EndGame(EndGameResponse response)
        {
            syncContext.Post(new SendOrPostCallback(OnBroadcast<EndGameResponse>), response);
        }

        public void AbortGame(AbortGameResponse response)
        {
            syncContext.Post(new SendOrPostCallback(OnBroadcast<AbortGameResponse>), response);
        }

        public void StartChat(StartChatResponse response)
        {
            syncContext.Post(new SendOrPostCallback(OnBroadcast<StartChatResponse>), response);
        }

        public void RecieveMessage(RecieveMessageResponse response)
        {
            syncContext.Post(new SendOrPostCallback(OnBroadcast<RecieveMessageResponse>), response);
        }

        public void GetTopPlayersCallback(GetTopPlayersResponse response)
        {
            syncContext.Post(new SendOrPostCallback(OnBroadcast<GetTopPlayersResponse>), response);
        }

        public void GetStatisticLastGamesCallBack(GetLastGamesResponse response)
        {
            syncContext.Post(new SendOrPostCallback(OnBroadcast<GetLastGamesResponse>), response);
        }

        public void CreateGameCallBack(CreateGameResponse response)
        {
            syncContext.Post(new SendOrPostCallback(OnBroadcast<CreateGameResponse>), response);
        }

        public void StartGame(StartGameResponse response)
        {
            syncContext.Post(new SendOrPostCallback(OnBroadcast<StartGameResponse>), response);
        }

        public void SendOpponentIsReady(SendOpponentIsReadyResponse response)
        {
            syncContext.Post(new SendOrPostCallback(OnBroadcast<SendOpponentIsReadyResponse>), response);
        }

        public void GetListAvailableGamesCallback(GetListGamesResponse response)
        {
            throw new NotImplementedException();
        }
    }
}
