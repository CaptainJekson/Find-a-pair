#if UNITY_EDITOR
using CJ.FindAPair.Modules.Meta.Configs;
using CJ.FindAPair.Modules.Service;
using UnityEditor;
using UnityEngine;

namespace CJ.FindAPair.CustomEditor
{
    public class ThemeEditor : EditorWindow
    {
        private const int COUNT_FACES = 15;
        
        private ThemeConfig _levelConfig;
        
        private string _id;
        private string _name;
        private string _description;

        private CurrencyType _currencyType;
        private int _price;

        private Sprite _shirtSprite;
        private Sprite[] _facesSprites = new Sprite[COUNT_FACES];
        private Sprite _backGroundSprite;
        private AudioClip _music;
    
        [MenuItem("Find a pair/Theme Editor")]
        private static void ShowWindow()
        {
            GetWindow(typeof(ThemeEditor));
        }
    
        private void OnGUI()
        {
            GUILayout.Label("РЕДАКТОР ТЕМЫ", EditorStyles.boldLabel);
            _levelConfig = EditorGUILayout.ObjectField(_levelConfig, typeof(ThemeConfig), true)
                as ThemeConfig;
            if (GUILayout.Button("Прочитать файл темы"))
            {
                ReadThemeConfig();
            }
            
            _id = EditorGUILayout.TextField("Идентификатор темы", _id);
            _name = EditorGUILayout.TextField("Название темы", _name);
            _description = EditorGUILayout.TextField("Описание темы", _description);
            
            _currencyType = (CurrencyType)EditorGUILayout.EnumPopup(new GUIContent("Тип валюты"),
                    _currencyType);
            _price = EditorGUILayout.IntField("Стоимость", _price);
            _shirtSprite = EditorGUILayout.ObjectField("Рубашка карты:",_shirtSprite, typeof(Sprite),
                true) as Sprite;

            GUILayout.Label("Лицевые стороны карт:", EditorStyles.boldLabel);
            var index = 0;
            for (var i = 0; i < COUNT_FACES / 3; i++)
            {
                EditorGUILayout.BeginHorizontal();
                
                for (var j = 0; j < 3; j++)
                {
                    _facesSprites[index] = EditorGUILayout.ObjectField($"Лицо карты {index + 1}:",
                        _facesSprites[index], typeof(Sprite), true) as Sprite;
                    index++;
                }
                EditorGUILayout.EndHorizontal();
            }
            
            GUILayout.Label("Фон на уровне:", EditorStyles.boldLabel);
            _backGroundSprite = EditorGUILayout.ObjectField("Фон:",_backGroundSprite, typeof(Sprite),
                true) as Sprite;
            
            _music = EditorGUILayout.ObjectField("Фоновая музыка:",_music, typeof(AudioClip),
                true) as AudioClip;

            if (GUILayout.Button("Создать/перезаписать"))
            {
                CreateTheme();
            }
        }

        private void CreateTheme()
        {
            var asset = CreateInstance<ThemeConfig>();
            asset.SetData(_id, _name, _description);
            asset.SetPrice(_currencyType, _price);
            asset.SetSprites(_shirtSprite, _facesSprites, _backGroundSprite);
            asset.SetAudio(_music);
            
            AssetDatabase.CreateAsset(asset,$"Assets/CJ.FindAPair/Resources/Configs/Themes/Theme {_id}.asset");
            AssetDatabase.SaveAssets();
            EditorUtility.FocusProjectWindow();
            Selection.activeObject = asset;
        }

        private void ReadThemeConfig() //TODO реализовать 
        {
            if (_levelConfig == null)
            {
                Debug.LogError("[Error: Выберите файл с темой]");
                return;
            }

            _id = _levelConfig.Id;
            _name = _levelConfig.Name;
            _description = _levelConfig.Description;

            _currencyType = _levelConfig.CurrencyType;
            _price = _levelConfig.Price;

            _shirtSprite = _levelConfig.ShirtSprite;
            _facesSprites = _levelConfig.FacesSprites.ToArray();
            _backGroundSprite = _levelConfig.BackGroundSprite;
            _music = _levelConfig.Music;
        }
    }
}

#endif