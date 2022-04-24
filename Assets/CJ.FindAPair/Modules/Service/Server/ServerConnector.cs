using System;
using System.Threading.Tasks;
using CJ.FindAPair.Modules.Service.Server.Configs;
using UnityEngine;
using UnityEngine.Networking;

namespace CJ.FindAPair.Modules.Service.Server
{
    public class ServerConnector
    {
        private ServerConfig _serverConfig;
        private Action<string> _completeAction;
        private Action<string> _errorAction;

        public ServerConnector(ServerConfig serverConfig)
        {
            _serverConfig = serverConfig;
        }

        public void CreateSave(string jsonSaveFile, Action<string> completeAction = null)
        {
            _completeAction = completeAction;

            var form = new WWWForm();
            form.AddField("Command", "CreateSave");
            form.AddField("SetSaveDataJson", jsonSaveFile);

            Connect(form);
        }

        public void UpdateSave(string jsonSaveFile, Action<string> completeAction = null)
        {
            _completeAction = completeAction;

            var form = new WWWForm();
            form.AddField("Command", "UpdateSave");
            form.AddField("SetSaveDataJson", jsonSaveFile);
            
            Connect(form);
        }

        public void LoadSave(int userId, Action<string> completeAction = null, Action<string> errorAction = null)
        {
            _completeAction = completeAction;
            _errorAction = errorAction;

            var form = new WWWForm();
            form.AddField("Command", "LoadSave");
            form.AddField("SetUserId", userId);
            
            Connect(form);
        }

        private async void Connect(WWWForm form)
        {
            if (_serverConfig.IsConnected == false)
                return;
            
            using var request = UnityWebRequest.Post(_serverConfig.RegisterPath, form);
            
            var operation = request.SendWebRequest();

            while (operation.isDone == false)
            {
                await Task.Yield();
            }

            if (request.error == null)
            {
                _completeAction?.Invoke(request.downloadHandler.text);
                _completeAction = null;
                Debug.Log("Server response: " + request.downloadHandler.text);
            }
            else
            {
                _errorAction?.Invoke(request.error);
                _errorAction = null;
                Debug.LogError("Error: " + request.error);
            }
        }
    }
}