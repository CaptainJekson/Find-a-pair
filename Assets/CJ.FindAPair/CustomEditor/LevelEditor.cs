using CJ.FindAPair.Configuration;
using UnityEditor;
using UnityEngine;

namespace CJ.FindAPair.CustomEditor
{
    public class LevelEditor : EditorWindow
    {
        private bool[,] _levelMatrix = new bool[0, 0];

        private int _level = 0;
        private int _width = 0;
        private int _height = 0;

        private QuantityOfCardOfPair _quantityOfCardOfPair;
        private int _tries = 0;
        private int _time = 0;
        private bool _isBomb = false;
        private int _quantityPairOfBombs = 0;

        [MenuItem("Find a pair/Level Editor")]
        private static void ShowWindow()
        {
            EditorWindow.GetWindow(typeof(LevelEditor));
        }

        private void OnGUI()
        {
            GUILayout.Label("LEVEL NUMBER", EditorStyles.boldLabel);
            _level = EditorGUILayout.IntField("Level number", _level);

            GUILayout.Label("Game field - width/height", EditorStyles.boldLabel);
            _width = EditorGUILayout.IntField("Width", _width);
            _height = EditorGUILayout.IntField("Height", _height);

            GUILayout.Label("Level conditions", EditorStyles.boldLabel);
            _quantityOfCardOfPair = (QuantityOfCardOfPair)EditorGUILayout.EnumPopup(new GUIContent("Quantity of card of pair"), _quantityOfCardOfPair);
            _tries = EditorGUILayout.IntField("Tries", _width);
            _time = EditorGUILayout.IntField("Time", _time);
            _isBomb = EditorGUILayout.Toggle("With bombs", _isBomb);

            if(_isBomb)
            {
                _quantityPairOfBombs = EditorGUILayout.IntField("Quantity pair of bombs", _quantityPairOfBombs);
            }

            if (_width != _levelMatrix.GetLength(0) || _height != _levelMatrix.GetLength(1))
            {
                _levelMatrix = new bool[_width, _height];
            }

            ChangeArrayWidthAndHeight();

            if (GUILayout.Button("Create"))
            {
                var asset = ScriptableObject.CreateInstance<LevelConfig>();             
                asset.SetSizeLevel(_levelMatrix, _level);
                asset.SetConditionsLevel(_quantityOfCardOfPair, _tries, _time, _quantityPairOfBombs);

                AssetDatabase.CreateAsset(asset, $"Assets/CJ.FindAPair/Resources/Levels/Level {_level}.asset");
                AssetDatabase.SaveAssets();

                EditorUtility.FocusProjectWindow();

                Selection.activeObject = asset;
            }

            if (GUILayout.Button("All true"))
            {
                WriteToAllMatrix(true);
            }

            if (GUILayout.Button("All false"))
            {
                WriteToAllMatrix(false);
            }
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
    }
}

