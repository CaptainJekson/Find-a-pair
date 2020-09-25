using UnityEditor;
using UnityEngine;

public class LevelEditor : EditorWindow
{
    private bool[,] _fieldsArray = new bool[0, 0];

    private int _width = 1;
    private int _height = 1;
    private int _levelNumber = 1;

    [MenuItem("Find a pair/Level Editor")]
    private static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(LevelEditor));
    }

    private void OnGUI()
    {
        GUILayout.Label("LEVEL NUMBER", EditorStyles.boldLabel);
        _levelNumber = EditorGUILayout.IntField("Level Number", _levelNumber);

        GUILayout.Label("Array width/height", EditorStyles.boldLabel);
        _width = EditorGUILayout.IntField("Width", _width);
        _height = EditorGUILayout.IntField("Width", _height);

        if (_width != _fieldsArray.GetLength(0) || _height != _fieldsArray.GetLength(1))
        {
            _fieldsArray = new bool[_width, _height];
        }

        ChangeArrayWidthAndHeight();

        if (GUILayout.Button("Create"))
        {
            var asset = ScriptableObject.CreateInstance<LevelConfig>();
            asset.SetLevel(_fieldsArray, _levelNumber);

            AssetDatabase.CreateAsset(asset, $"Assets/CJ.FindAPair/Resources/Levels/Level {_levelNumber}.asset");
            AssetDatabase.SaveAssets();

            EditorUtility.FocusProjectWindow();

            Selection.activeObject = asset;
        }
    }

    private void ChangeArrayWidthAndHeight()
    {
        for (int j = 0; j < _height; j++)
        {
            EditorGUILayout.BeginHorizontal();

            for (int i = 0; i < _width; i++)
            {
                _fieldsArray[i, j] = EditorGUILayout.Toggle(_fieldsArray[i, j]);
            }

            EditorGUILayout.EndHorizontal();
        }
    }

}