﻿#if UNITY_EDITOR

using CJ.FindAPair.Configuration;
using UnityEngine;
using UnityEditor;
using UnityEngine.SocialPlatforms;

namespace CJ.FindAPair.CustomEditor
{
    public class LevelEditor : EditorWindow
    {
        private bool[,] _levelMatrix = new bool[0, 0];

        private int _level = 0;
        private int _width = 0;
        private int _height = 0;
        private float _scale = 0.9f;

        private QuantityOfCardOfPair _quantityOfCardOfPair;
        private int _tries = 0;
        private int _time = 0;
        private bool _isFortune = false;
        private bool _isEntanglement = false;
        private bool _isReset = false;
        private bool _isBomb = false;

        private int _quantityPairOfFortune = 0;
        private int _quantityPairOfEntanglement = 0;
        private int _quantityPairOfReset = 0;
        private int _quantityPairOfBombs = 0;


        private string _errorMessage;

        [MenuItem("Find a pair/Level Editor")]
        private static void ShowWindow()
        {
            EditorWindow.GetWindow(typeof(LevelEditor));
        }

        private void OnGUI()
        {
            GUILayout.Label("РЕДАКТОР УРОВНЯ", EditorStyles.boldLabel);
            _level = EditorGUILayout.IntField("Номер уровня", _level);

            GUILayout.Label("Игровое поле - ширина/длина", EditorStyles.boldLabel);
            _width = EditorGUILayout.IntField("Ширина", _width);
            _height = EditorGUILayout.IntField("Длина", _height);
            _scale = EditorGUILayout.Slider("Масштаб", _scale, 0.0f, 1.0f);

            GUILayout.Label("Условия уровня", EditorStyles.boldLabel);
            _quantityOfCardOfPair =
                (QuantityOfCardOfPair) EditorGUILayout.EnumPopup(new GUIContent("Количество карт в паре"),
                    _quantityOfCardOfPair);
            _tries = EditorGUILayout.IntField("Попытки", _tries);
            _time = EditorGUILayout.IntField("Время", _time);

            _isFortune = EditorGUILayout.Toggle("Карты удачи", _isFortune);
            if (_isFortune)
            {
                _quantityPairOfFortune = EditorGUILayout.IntField("Количесво пар удачи", _quantityPairOfFortune);
            }

            _isEntanglement = EditorGUILayout.Toggle("Карты запутывания", _isEntanglement);
            if (_isEntanglement)
            {
                _quantityPairOfEntanglement = EditorGUILayout.IntField("Количесво пар запутывания",
                    _quantityPairOfEntanglement);
            }

            _isReset = EditorGUILayout.Toggle("Карты сброса", _isReset);
            if (_isReset)
            {
                _quantityPairOfReset = EditorGUILayout.IntField("Количесво пар сброса", _quantityPairOfReset);
            }

            _isBomb = EditorGUILayout.Toggle("Карты бомбы", _isBomb);
            if (_isBomb)
            {
                _quantityPairOfBombs = EditorGUILayout.IntField("Количесво пар с бомбами", _quantityPairOfBombs);
            }

            if (_width != _levelMatrix.GetLength(0) || _height != _levelMatrix.GetLength(1))
            {
                _levelMatrix = new bool[_width, _height];
            }

            ChangeArrayWidthAndHeight();

            if (GUILayout.Button("Создать"))
            {
                if (LevelValidation(ref _errorMessage))
                {
                    CreateLevel();
                }
                else
                {
                    Debug.LogError(_errorMessage);
                }
            }

            if (GUILayout.Button("Заполнить все"))
            {
                WriteToAllMatrix(true);
            }

            if (GUILayout.Button("Снять все"))
            {
                WriteToAllMatrix(false);
            }
        }

        private void CreateLevel()
        {
            var asset = ScriptableObject.CreateInstance<LevelConfig>();
            asset.SetSizeLevel(_levelMatrix, _level, _scale);
            asset.SetConditionsLevel(_quantityOfCardOfPair, _tries, _time, _quantityPairOfFortune,
                _quantityPairOfEntanglement, _quantityPairOfReset, _quantityPairOfBombs);

            AssetDatabase.CreateAsset(asset, $"Assets/CJ.FindAPair/Resources/Levels/Level {_level}.asset");
            AssetDatabase.SaveAssets();

            EditorUtility.FocusProjectWindow();

            Selection.activeObject = asset;
        }

        private void WriteToAllMatrix(bool value)
        {
            for (var i = 0; i < _levelMatrix.GetLength(0); i++)
            {
                for (var j = 0; j < _levelMatrix.GetLength(1); j++)
                {
                    _levelMatrix[i, j] = value;
                }
            }
        }

        private void ChangeArrayWidthAndHeight()
        {
            for (var j = 0; j < _height; j++)
            {
                EditorGUILayout.BeginHorizontal();

                for (var i = 0; i < _width; i++)
                {
                    _levelMatrix[i, j] = EditorGUILayout.Toggle(_levelMatrix[i, j]);
                }

                EditorGUILayout.EndHorizontal();
            }
        }

        private bool LevelValidation(ref string errorMessage)
        {
            var isValid = false;
            var quantityCard = 0;
            var quantitySpecialCard = (_quantityPairOfFortune + _quantityPairOfEntanglement +
                                       _quantityPairOfReset + _quantityPairOfBombs)  * (int) _quantityOfCardOfPair;

            foreach (var item in _levelMatrix)
            {
                if (item)
                {
                    quantityCard++;
                }
            }

            switch (_quantityOfCardOfPair)
            {
                case QuantityOfCardOfPair.TwoCards:
                {
                    isValid = quantityCard >= 4 && quantityCard % 2 == 0;
                    errorMessage = "[Error: Кол-во карт должно быть более 4 и делится на 2 без остатка]";
                }
                    break;
                case QuantityOfCardOfPair.ThreeCards:
                {
                    isValid = quantityCard >= 6 && quantityCard % 3 == 0;
                    errorMessage = "[Error: Кол-во карт должно быть более 6 и делится на 3 без остатка]";
                }
                    break;
                case QuantityOfCardOfPair.FourCards:
                {
                    isValid = quantityCard >= 8 && quantityCard % 4 == 0;
                    errorMessage = "[Error: Кол-во карт должно быть более 8 и делится на 4 без остатка]";
                }
                    break;
                default:
                {
                    isValid = false;
                    errorMessage = "[Error: Выберите кол-во карт в паре]";
                }
                    break;
            }


            if (quantitySpecialCard >= quantityCard)
            {
                isValid = false;
                errorMessage = "[Error: Кол-во специальных карт не должно равнятся кол-ву обычных карт]";
            }

            return isValid;
        }
    }
}

#endif