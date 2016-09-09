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
    public class ClientManager
    {
        private static ClientManager _instance = null;
        private Dictionary<Type, BaseResponse> _responses;
        private ServiceClient _client;

        public BattleShipCallback Callback { get; private set; }

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
        }

        public T GetResponses<T>()
            where T : BaseResponse
        {
            if (_responses.ContainsKey(typeof(T)))
                return (T)_responses[typeof(T)];

            else return null;
        }

        public void AddResponses<T>(BaseResponse response)
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
        }

        private void CreateClient()
        {
            if (_client == null)
            {
                Callback = new BattleShipCallback();
                _client = new ServiceClient(new InstanceContext(Callback));
            }
        }
    }
}
