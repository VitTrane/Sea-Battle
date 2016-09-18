using Infrastructure;
using SeaBattle.BattleShipServiceCallback;
using SeaBattle.GameService;
using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace SeaBattle.Managers
{
    public class ClientManager : IDisposable
    {
        private static ClientManager _instance = null;
        private Dictionary<Type, BaseResponse> _responses;
        private ServiceClient _client;
        private BattleShipCallback _callback;

        public ILogger Logger { get; set; }
        public Guid ClientId { get; set; }
        public string PlayerNickname { get; set; }

        public BattleShipCallback Callback
        {
            get { return _callback; }
            set { _callback = value; }
        }

        /// <summary>
        /// Возвращает клиент
        /// </summary>
        public ServiceClient Client
        {
            get { return _client; }
            private set { _client = value; }
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
            Logger = new FileLogger();
        }

        /// <summary>
        /// Создает клиента
        /// </summary>
        public void CreateClient()
        {
            if (_client == null)
            {
                try
                {
                    Callback = new BattleShipCallback();
                    _client = new ServiceClient(new InstanceContext(Callback), "NetTcpBinding_IService");
                    _client.Open();
                }
                catch (Exception)
                {
                    Dispose();
                }
                
            }
        }

        public void Dispose()
        {
            try
            { 
                _client.Close();
            }
            catch (Exception ex)
            {
                string message = string.Format("{0} \n {1},\n {2}", ex.Message,
                    ex.ToString(), ex.StackTrace);
                Logger.WriteLineError(message);

                try
                {
                    _client.Abort(); 
                }
                catch (Exception e)
                {
                    string m = string.Format("{0} \n {1},\n {2}", e.Message,
                    e.ToString(), e.StackTrace);
                    Logger.WriteLineError(message);
                }                               
            }
            _client = null;
        }
    }
}
