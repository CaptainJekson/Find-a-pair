#if UNITY_EDITOR
using CJ.FindAPair.Modules.Meta.Configs;
using CJ.FindAPair.Modules.Service;
using UnityEditor;
using UnityEngine;

namespace CJ.FindAPair.CustomEditor
{
    public class ThemeEditor : EditorWindow
    {
        private const int COUNT_FACES = 14;
        
        private ThemeConfig _themeConfig;
        
        private string _id;
        private string _name;
        private string _description;

        private CurrencyType _currencyType;
        private bool _isOpensLevel;
        private int _price;
        private int _requiredLevel;

        private Sprite _shirtSprite;
        private Sprite _specialCardFaceSprite;
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
            _themeConfig = EditorGUILayout.ObjectField(_themeConfig, typeof(ThemeConfig), true)
                as ThemeConfig;
            if (GUILayout.Button("Прочитать файл темы"))
            {
                ReadThemeConfig();
            }
            
            _id = EditorGUILayout.TextField("Идентификатор темы", _id);
            _name = EditorGUILayout.TextField("Название темы", _name);
            _description = EditorGUILayout.TextField("Описание темы", _description);
            
            _isOpensLevel = EditorGUILayout.Toggle("Открывается за уровень", _isOpensLevel);
            
            if (_isOpensLevel)
            {
                _requiredLevel = EditorGUILayout.IntField("Требуемый уровень", _requiredLevel);
            }
            else
            {
                _currencyType = (CurrencyType)EditorGUILayout.EnumPopup(new GUIContent("Тип валюты"),
                    _currencyType);
                _price = EditorGUILayout.IntField("Стоимость", _price);
            }
            
            _shirtSprite = EditorGUILayout.ObjectField("Рубашка карты:",_shirtSprite, typeof(Sprite),
                true) as Sprite;
            
            _specialCardFaceSprite = EditorGUILayout.ObjectField("Лицевая сторона специальной карты:",
                _specialCardFaceSprite, typeof(Sprite), true) as Sprite;

            GUILayout.Label("Лицевые стороны карт:", EditorStyles.boldLabel);
        
            ShowFaceCardFields(0,5);
            ShowFaceCardFields(5,10);
            ShowFaceCardFields(10,14);
            
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

        private void ShowFaceCardFields(int startIndex, int endIndex)
        {
            EditorGUILayout.BeginHorizontal();
            for (var j = startIndex; j < endIndex; j++)
            {
                _facesSprites[j] = EditorGUILayout.ObjectField($"Лицо пары {j + 1}:",
                    _facesSprites[j], typeof(Sprite), true) as Sprite;
            }
            EditorGUILayout.EndHorizontal();
        }

        private void CreateTheme()
        {
            var asset = CreateInstance<ThemeConfig>();
            asset.SetData(_id, _name, _description);
            asset.SetPrice(_isOpensLevel, _requiredLevel, _currencyType, _price);
            asset.SetSprites(_shirtSprite, _specialCardFaceSprite, _facesSprites, _backGroundSprite);
            asset.SetAudio(_music);
            
            AssetDatabase.CreateAsset(asset,$"Assets/CJ.FindAPair/Resources/Configs/Themes/Theme {_id}.asset");
            AssetDatabase.SaveAssets();
            EditorUtility.FocusProjectWindow();
            Selection.activeObject = asset;
        }

        private void ReadThemeConfig()
        {
            if (_themeConfig == null)
            {
                Debug.LogError("[Error: Выберите файл с темой]");
                return;
            }

            _id = _themeConfig.Id;
            _name = _themeConfig.Name;
            _description = _themeConfig.Description;

            _isOpensLevel = _themeConfig.IsOpensLevel;
            _currencyType = _themeConfig.CurrencyType;
            _price = _themeConfig.Price;
            _requiredLevel = _themeConfig.RequiredLevel;

            _shirtSprite = _themeConfig.ShirtSprite;
            _specialCardFaceSprite = _themeConfig.SpecialCardFaceSprite;
            _facesSprites = _themeConfig.FacesSprites.ToArray();
            _backGroundSprite = _themeConfig.BackGroundSprite;
            _music = _themeConfig.Music;
        }
    }
}

#endif