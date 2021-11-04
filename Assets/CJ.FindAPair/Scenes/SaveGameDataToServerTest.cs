using System;
using System.Collections;
using System.IO;
using UnityEngine;

enum Test
{
    CreateSave,
    UpdateSave,
    LoadSave,
}

public class SaveGameDataToServerTest : MonoBehaviour
{
    [SerializeField] private Test _typeSaveMethod;
    
    private string _registerPath = "https://c91780.hostru10.fornex.host/orange-games777.ru/Find-a-pair/commander.php";
    private string _savePath;
    private static string _jsonSaveFile;
    
    private void Start()
    {
        _savePath = Path.Combine(Application.dataPath, "SaveData.json");
        _jsonSaveFile = File.ReadAllText(_savePath);

        switch (_typeSaveMethod)
        {
            case Test.CreateSave:
                CreateSave(_jsonSaveFile);
                break;
            case Test.UpdateSave:
                UpdateSave(_jsonSaveFile);
                break;
            case Test.LoadSave:
                LoadSave(10003);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void CreateSave(string jsonSaveFile)
    {
        var form = new WWWForm();
        form.AddField("Command", "CreateSave");
        form.AddField("SetSaveDataJson", jsonSaveFile);
        StartCoroutine(Connect(form));
    }

    private void UpdateSave(string jsonSaveFile)
    {
        var form = new WWWForm();
        form.AddField("Command", "UpdateSave");
        form.AddField("SetSaveDataJson", jsonSaveFile);
        StartCoroutine(Connect(form));
    }

    private void LoadSave(int userId) //TODO Реализация на серве ещё не сделана
    {
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
        Debug.Log("Сервер ответил: " + www.text);
    }
}
