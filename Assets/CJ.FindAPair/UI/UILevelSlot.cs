using CJ.FindAPair.CardTable;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class UILevelSlot : MonoBehaviour
{
    [SerializeField] private LevelCreator _levelCreator;

    //TODO Будет использоватся для конфига уровня. Будет передаватся из списка конфигов уровня
    [SerializeField] private Level _level;

    private Button _button;

    //TODO Будет использоватся для конфига уровня. Будет передаватся из списка конфигов уровня
    public Level Level { get => _level; set => _level = value; }

    private void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(CreateLevel);
    }

    private void CreateLevel()
    {
        _levelCreator.CreateLevel(_level);
    }
}
