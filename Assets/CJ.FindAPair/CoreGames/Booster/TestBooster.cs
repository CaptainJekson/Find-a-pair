using System;
using System.Collections;
using System.Collections.Generic;
using CJ.FindAPair.Game;
using CJ.FindAPair.Game.Booster;
using UnityEngine;
using UnityEngine.UI;

//TODO TEST CLASS!!!
public class TestBooster : MonoBehaviour
{
    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(AddBoosters);
    }

    private void AddBoosters()
    {
        GameSaver.SaveBooster(BoosterType.Electroshock, 1);
        GameSaver.SaveBooster(BoosterType.Sapper, 1);
        GameSaver.SaveBooster(BoosterType.MagicEye, 1);
    }
}
