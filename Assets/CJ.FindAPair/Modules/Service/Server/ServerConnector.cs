using System;
using System.Collections;
using UnityEngine;

namespace CJ.FindAPair.Modules.Service.Server
{
    public class ServerConnector : MonoBehaviour
    {
        private string _registerPath = "https://c91780.hostru10.fornex.host/orange-games777.ru/Find-a-pair/commander.php";

        private Action<string> _completeAction;
        
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
            var www = new WWW(_registerPath, form);
            yield return www;
            if (www.error != null)
            {
                Debug.LogError("Ошибка: " + www.error);
                yield break;
            }
            _completeAction?.Invoke(www.text);
            _completeAction = null;
            Debug.Log("Сервер ответил: " + www.text);
        }
    }
}
