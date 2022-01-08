using System.Collections.Generic;
using CJ.FindAPair.Constants;
using CJ.FindAPair.Modules.CoreGames;
using CJ.FindAPair.Modules.Meta.Configs;
using CJ.FindAPair.Utility;
using UnityEngine;

namespace CJ.FindAPair.Modules.Meta.Themes
{
    public class ThemesSelector
    {
        private ThemeConfigCollection _themeConfigCollection;
        private SpecialCardImageConfig _specialCardImageConfig;
        private ThemeConfig _selectedThemeConfig;
        private LevelBackground _levelBackground;
        private LevelCreator _levelCreator;
        private int _quantityOfCardOfPair;
        private List<Card> _sortedCards;
        private ISaver _gameSaver;

        public ThemesSelector(ThemeConfigCollection themeConfigCollection, SpecialCardImageConfig specialCardImageConfig,
            LevelCreator levelCreator, LevelBackground levelBackground, ISaver gameSaver)
        {
            _themeConfigCollection = themeConfigCollection;
            _specialCardImageConfig = specialCardImageConfig;
            _levelCreator = levelCreator;
            _levelBackground = levelBackground;
            _gameSaver = gameSaver;
            _selectedThemeConfig = themeConfigCollection.GetThemeConfig(ReadSelectedTheme());
            _levelCreator.LevelCreated += InitTheme;
        }

        public void ChangeTheme(string themeId)
        {
            var saveData = _gameSaver.LoadData();
            saveData.ThemesData.SelectedTheme = themeId;
            _gameSaver.SaveData(saveData);
            
            _selectedThemeConfig = _themeConfigCollection.GetThemeConfig(ReadSelectedTheme());
        }

        public void AddOpenedTheme(string themeId)
        {
            var saveData = _gameSaver.LoadData();
            saveData.ThemesData.OpenedThemes.Add(themeId);
            _gameSaver.SaveData(saveData);
        }

        public void RandomSelectTheme(bool isRandom)
        {
            PlayerPrefs.SetString(PlayerPrefsKeys.IsRandomChangeTheme, isRandom ? "On" : "Off");
        }

        private void InitTheme()
        {   
            var isRandomChangeTheme = PlayerPrefs.GetString(PlayerPrefsKeys.IsRandomChangeTheme);

            if (isRandomChangeTheme == "On")
                RandomChangeTheme();
            
            _quantityOfCardOfPair = (int) _levelCreator.LevelConfig.QuantityOfCardOfPair;
            
            SortCards();
            SetBackground();
            SetCards();
        }
        
        private void RandomChangeTheme()
        {
            var saveData = _gameSaver.LoadData();
            var openedThemes = saveData.ThemesData.OpenedThemes;
            var randomThemeId = openedThemes[Random.Range(0, openedThemes.Count)];

            ChangeTheme(randomThemeId);
        }

        private void SortCards()
        {
            _levelCreator.Cards.Sort();
            _sortedCards = _levelCreator.Cards;
        }
        
        private void SetBackground()
        {
            _levelBackground.SetSprite(_selectedThemeConfig.BackGroundSprite);
        }

        private void SetCards()
        {
            foreach (var card in _sortedCards)
            {
                card.SetShirt(_selectedThemeConfig.ShirtSprite);
            }
            
            var pairCounter = 0;
            var index = 0;
            
            for (var i = _selectedThemeConfig.FacesSprites.Count - 1; i > 0; i--)
            {
                var j = Random.Range(0, i);
                
                var temp = _selectedThemeConfig.FacesSprites[i];
                _selectedThemeConfig.FacesSprites[i] = _selectedThemeConfig.FacesSprites[j];
                _selectedThemeConfig.FacesSprites[j] = temp;
            }

            foreach (var card in _sortedCards)
            {
                if (card.NumberPair < ConstantsCard.NUMBER_SPECIAL)
                {
                    card.SetFace(_selectedThemeConfig.FacesSprites[index]);
                }
                else
                {
                    card.SetFace(_selectedThemeConfig.SpecialCardFaceSprite);
                    card.SetSpecialIcon(GetFaceSpecialCard(card.NumberPair));
                }

                pairCounter++;

                if (pairCounter >= _quantityOfCardOfPair)
                {
                    index++;
                    pairCounter = 0;
                }
            }
        }

        private string ReadSelectedTheme()
        {
            var saveData = _gameSaver.LoadData();
            var themeId = saveData.ThemesData.SelectedTheme;

            if (saveData.ThemesData.OpenedThemes.Count < 1)
            {
                saveData.ThemesData.OpenedThemes.Add(_themeConfigCollection.DefaultThemeId);
                _gameSaver.SaveData(saveData);
            }
            
            if (themeId == null)
            {
                saveData.ThemesData.SelectedTheme = _themeConfigCollection.DefaultThemeId;
                _gameSaver.SaveData(saveData);
                return _themeConfigCollection.DefaultThemeId;
            }

            return themeId;
        }

        private Sprite GetFaceSpecialCard(int numberCard)
        {
            return numberCard switch
            {
                ConstantsCard.NUMBER_FORTUNE => _specialCardImageConfig.FortuneSprite,
                ConstantsCard.NUMBER_ENTANGLEMENT => _specialCardImageConfig.EntanglementSprite,
                ConstantsCard.NUMBER_RESET => _specialCardImageConfig.ResetSprite,
                ConstantsCard.NUMBER_BOMB => _specialCardImageConfig.BombSprite,
                _ => null
            };
        }
    }
}