using System.Collections;
using System.IO;
using UnityEngine;

public class SaveGameDataToServerTest : MonoBehaviour
{
    private string _registerPath = "https://c91780.hostru10.fornex.host/orange-games777.ru/Find-a-pair/commander.php";
    private string _savePath;
    private static string _jsonSaveFile;
    private void Start()
    {
        _savePath = Path.Combine(Application.dataPath, "SaveData.json");
        _jsonSaveFile = File.ReadAllText(_savePath);
        
        StartCoroutine(Test());
    }
    
    private IEnumerator Test()
    {
        var form = new WWWForm();
        form.AddField("SaveData", _jsonSaveFile);
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
