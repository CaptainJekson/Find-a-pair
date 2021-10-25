using System.Collections;
using UnityEngine;

public class TestConnection : MonoBehaviour
{
    private string _path = "https://c91780.hostru10.fornex.host/orange-games777.ru/Find-a-pair/";
    
    private void Start()
    {
        StartCoroutine(Send());
    }

    private IEnumerator Send()
    {
        var form = new WWWForm();
        form.AddField("HelloServer", "привет сервер!");
        var www = new WWW(_path, form);    
        yield return www;
        if (www.error != null)
        {
            Debug.LogError("Ошибка: " + www.error);
            yield break;
        }
        Debug.Log("Сервер ответил: " + www.text);
    }
}
