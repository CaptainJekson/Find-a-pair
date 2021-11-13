using UnityEngine;

namespace CJ.FindAPair.Modules.Service.Server.Configs
{
    [CreateAssetMenu(fileName = "ServerConfig", menuName = "Find a pair/Service/ServerConfig")]
    public class ServerConfig : ScriptableObject
    {
        [SerializeField] private string _registerPath;
        [SerializeField] private bool _isConnected; 
        
        public string RegisterPath => _registerPath;
        public bool IsConnected => _isConnected;
    }
}
