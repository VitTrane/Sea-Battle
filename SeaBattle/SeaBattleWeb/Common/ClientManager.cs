using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SeaBattle.GameServiceReference;
using System.ServiceModel;

namespace SeaBattle.Common
{
    public class ClientManager : IDisposable
    {
        private static ClientManager _instance = null;
        private Dictionary<Type, BaseResponse> _responses;
        private ServiceClient _client;
        private BattleShipCallback _callback;

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
        }

        /// <summary>
        /// Создает клиента
        /// </summary>
        public void CreateClient()
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
            _client = null;
        }
    }
}