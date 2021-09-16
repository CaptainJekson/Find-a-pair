#if UNITY_EDITOR
using CJ.FindAPair.Modules.CoreGames;
using CJ.FindAPair.Modules.CoreGames.Configs;
using UnityEditor;
using UnityEngine;

namespace CJ.FindAPair.CustomEditor
{
    public class LevelEditor : EditorWindow
    {
        private LevelConfig _levelConfig;

        private bool[,] _levelMatrix = new bool[0, 0];

        private int _level = 0;
        private int _width = 0;
        private int _height = 0;
        private float _heightOffset = 0.0f;

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

        private int _quantityCards;
        private string _errorMessage;

        [MenuItem("Find a pair/Level Editor")]
        private static void ShowWindow()
        {
            GetWindow(typeof(LevelEditor));
        }

        private void OnGUI()
        {
            GUILayout.Label("РЕДАКТОР УРОВНЯ", EditorStyles.boldLabel);
            _levelConfig = EditorGUILayout.ObjectField(_levelConfig, typeof(LevelConfig), true)
                as LevelConfig;
            if (GUILayout.Button("Прочитать файл уровня"))
            {
                ReadLevelConfig();
            }

            _level = EditorGUILayout.IntField("Номер уровня", _level);

            GUILayout.Label("Игровое поле - ширина/длина", EditorStyles.boldLabel);
            _width = EditorGUILayout.IntField("Ширина", _width);
            _height = EditorGUILayout.IntField("Длина", _height);
            _heightOffset = EditorGUILayout.FloatField("Сдвиг по высоте", _heightOffset);

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

            if (GUILayout.Button("Создать/перезаписать"))
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

        private void ReadLevelConfig()
        {
            if (_levelConfig == null)
            {
                Debug.LogError("[Error: Выберите файл с уровнем]");
                return;
            }

            _level = _levelConfig.LevelNumber;
            _heightOffset = _levelConfig.HeightOffset;
            _quantityOfCardOfPair = _levelConfig.QuantityOfCardOfPair;
            _tries = _levelConfig.Tries;
            _time = _levelConfig.Time;
            _isFortune = _levelConfig.QuantityPairOfFortune > 0;
            _quantityPairOfFortune = _levelConfig.QuantityPairOfFortune;
            _isEntanglement = _levelConfig.QuantityPairOfEntanglement > 0;
            _quantityPairOfEntanglement = _levelConfig.QuantityPairOfEntanglement;
            _isReset = _levelConfig.QuantityPairOfReset > 0;
            _quantityPairOfReset = _levelConfig.QuantityPairOfReset;
            _isBomb = _levelConfig.QuantityPairOfBombs > 0;
            _quantityPairOfBombs = _levelConfig.QuantityPairOfBombs;

            _width = _levelConfig.Width;
            _height = _levelConfig.Height;

            _levelMatrix = new bool[_width,_height];
            var array = _levelConfig.LevelField;
            var countWidth = 0;
            var countHeight = 0;

            for (var i = 0; i < array.Count; i++)
            {
                if (countHeight >= _height)
                {
                    countHeight = 0;
                    countWidth++;
                }

                _levelMatrix[countWidth, countHeight] = array[i];

                countHeight++;
            }
        }

        private void CreateLevel()
        {
            var asset = CreateInstance<LevelConfig>();
            asset.SetSizeLevel(_levelMatrix, _level, _heightOffset);
            asset.SetConditionsLevel(_quantityOfCardOfPair, _tries, _time, _quantityPairOfFortune,
                _quantityPairOfEntanglement, _quantityPairOfReset, _quantityPairOfBombs);
            
            AssetDatabase.CreateAsset(asset,$"Assets/CJ.FindAPair/Resources/Configs/Levels/Level {_level}.asset");
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
                                       _quantityPairOfReset + _quantityPairOfBombs) * (int) _quantityOfCardOfPair;

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