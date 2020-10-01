﻿using UnityEditor;
using UnityEngine;

public class LevelEditor : EditorWindow
{
    private bool[,] _levelMatrix = new bool[0, 0];

    private int _width = 1;
    private int _height = 1;
    private int _level = 1;

    [MenuItem("Find a pair/Level Editor")]
    private static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(LevelEditor));
    }

    private void OnGUI()
    {
        GUILayout.Label("LEVEL NUMBER", EditorStyles.boldLabel);
        _level = EditorGUILayout.IntField("Level Number", _level);

        GUILayout.Label("Array width/height", EditorStyles.boldLabel);
        _width = EditorGUILayout.IntField("Width", _width);
        _height = EditorGUILayout.IntField("Height", _height);

        if (_width != _levelMatrix.GetLength(0) || _height != _levelMatrix.GetLength(1))
        {
            _levelMatrix = new bool[_width, _height];
        }

        ChangeArrayWidthAndHeight();

        if (GUILayout.Button("Create"))
        {
            var asset = ScriptableObject.CreateInstance<LevelConfig>();
            asset.SetLevel(_levelMatrix, _level);

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