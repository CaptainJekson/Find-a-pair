using System;
using System.Collections;
using CJ.FindAPair.Modules.Service.Server.Configs;
using UnityEngine;
using Zenject;

namespace CJ.FindAPair.Modules.Service.Server
{
    public class ServerConnector : MonoBehaviour
    {
        private ServerConfig _serverConfig;
        private Action<string> _completeAction;
        
        [Inject]
        public void Construct(ServerConfig serverConfig)
        {
            _serverConfig = serverConfig;
        }
        
        public void CreateSave(string jsonSaveFile, Action<string> completeAction = null)
        {
            _completeAction = completeAction;
            
            var form = new WWWForm();
            form.AddField("Command", "CreateSave");
            form.AddField("SetSaveDataJson", jsonSaveFile);
            StartCoroutine(Connect(form));
        }

        public void UpdateSave(string jsonSaveFile, Action<string> completeAction = null)
        {
            _completeAction = completeAction;
            
            var form = new WWWForm();
            form.AddField("Command", "UpdateSave");
            form.AddField("SetSaveDataJson", jsonSaveFile);
            StartCoroutine(Connect(form));
        }

        public void LoadSave(int userId, Action<string> completeAction = null) //TODO Реализация на серве ещё не сделана
        {
            _completeAction = completeAction;
            
            var form = new WWWForm();
            form.AddField("Command", "LoadSave");
            form.AddField("SetUserId", userId);
            StartCoroutine(Connect(form));
        }
    
        private IEnumerator Connect(WWWForm form)
        {    
            if(_serverConfig.IsConnected == false)
                yield break;
            
            var www = new WWW(_serverConfig.RegisterPath, form);
            yield return www;
            if (www.error != null)
            {
                Debug.LogError("Error: " + www.error);
                yield break;
            }
            _completeAction?.Invoke(www.text);
            _completeAction = null;
            Debug.Log("Server response: " + www.text);
        }
    }
}
