using SeaBattle.BattleShipServiceCallback;
using SeaBattle.GameService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace SeaBattle.Managers
{
    /// <summary>
    /// Делегат обработки уведомлений
    /// </summary>
    public delegate void NotificationHandler();

    public class ClientManager : IDisposable
    {
        private static ClientManager _instance = null;
        private Dictionary<Type, NotificationHandler> _events;   
        private Dictionary<Type, BaseResponse> _responses;
        private ServiceClient _client;

        public BattleShipCallback Callback { get; private set; }

        /// <summary>
        /// Возвращает клиент
        /// </summary>
        public ServiceClient Client
        {
            get
            {
                if (_client != null)
                    return _client;

                CreateClient();
                return _client;
            }
            private set
            {
                if (_client == null)
                    _client = value;
            }
        }

        public static ClientManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ClientManager();
                return _instance;
            }
        }

        private ClientManager()
        {
            _responses = new Dictionary<Type, BaseResponse>();
            _events = new Dictionary<Type, NotificationHandler>();
        }

        /// <summary>
        /// Получает ответ от сервера
        /// </summary>
        /// <typeparam name="T">Тип ответа</typeparam>
        public T GetResponse<T>()
            where T : BaseResponse
        {
            if (!_responses.ContainsKey(typeof(T)))
                return null;

           return (T)_responses[typeof(T)];;
        }

        /// <summary>
        /// Сохраняет ответ от сервера
        /// </summary>
        /// <typeparam name="T">Тип ответа</typeparam>
        /// <param name="response">Ответ, который нужно сохранить</param>
        public void AddResponse<T>(BaseResponse response)
            where T : BaseResponse
        {
            if (!_responses.ContainsKey(typeof(T)))
            {
                _responses.Add(typeof(T), response);
            }
            else
            {
                _responses[typeof(T)] = response;
            }
            OnGetResponse<T>();
        }

        /// <summary>
        /// Создает клиента
        /// </summary>
        private void CreateClient()
        {
            if (_client == null)
            {
                Callback = new BattleShipCallback();
                _client = new ServiceClient(new InstanceContext(Callback), "NetTcpBinding_IService");
            }
        }

        public void Dispose()
        {
            try
            {
                _client.Close();
            }
            catch (Exception)
            {
                _client.Abort();
            }            
        }

        /// <summary>
        /// Добавляет метод обработки ответа от сервера
        /// </summary>
        /// <typeparam name="T">Тип ответа который нужно обработать</typeparam>
        /// <param name="handler">Метод обработки который нужно выполнить при получении ответа</param>
        public void SubscribeToResponse<T>(NotificationHandler handler)
        {
            if (!_events.ContainsKey(typeof(T)))
                _events.Add(typeof(T), handler);
            else
                _events[typeof(T)] = handler;
        }

        /// <summary>
        /// Удаляет метод обработки ответа
        /// </summary>
        /// <typeparam name="T">Тип ответа который больше не нужно обрабатывать</typeparam>
        public void UnsubscribeFromResponse<T>()
        {
            if (_events.ContainsKey(typeof(T)))
                _events.Remove(typeof(T));
        }

        /// <summary>
        /// Выполняет метод
        /// </summary>
        /// <typeparam name="T">Тип ответа, для которого нужно выполнить метод</typeparam>
        private void OnGetResponse<T>()
        {
            NotificationHandler handler = _events[typeof(T)];
            if (handler != null)
                handler();
        }
    }
}
