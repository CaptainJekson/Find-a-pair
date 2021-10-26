using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TestConnection : MonoBehaviour
{
    [SerializeField] private Toggle _isRegToggle;
    [SerializeField] private TMP_InputField _nameText;
    [SerializeField] private TMP_InputField _passwordText;
    [SerializeField] private TextMeshProUGUI _logText;
    [SerializeField] private Button _button;
    
    private string _registerPath = "https://c91780.hostru10.fornex.host/orange-games777.ru/Find-a-pair/register.php";
    private string _loginPath = "https://c91780.hostru10.fornex.host/orange-games777.ru/Find-a-pair/login.php";
    
    private void Start()
    {
        _button.onClick.AddListener(() =>
        {
            if(_isRegToggle.isOn)
                StartCoroutine(RegisterUser());
            else
                StartCoroutine(LoginUser());
        });
    }

    private IEnumerator RegisterUser()
    {
        var form = new WWWForm();
        form.AddField("Name", _nameText.text);
        form.AddField("Password", _passwordText.text);
        var www = new WWW(_registerPath, form);    
        yield return www;
        if (www.error != null)
        {
            _logText.text = "Ошибка: " + www.error;
            yield break;
        }
        _logText.text = "Сервер ответил: " + www.text;
    }
    
    private IEnumerator LoginUser()
    {
        var form = new WWWForm();
        form.AddField("User", _nameText.text);
        form.AddField("Password", _passwordText.text);
        var www = new WWW(_loginPath, form);    
        yield return www;
        if (www.error != null)
        {
            _logText.text = "Ошибка: " + www.error;
            yield break;
        }
        _logText.text = "Сервер ответил: " + www.text;
    }
}
