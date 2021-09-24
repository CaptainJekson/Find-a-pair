using System.Collections.Generic;
using CJ.FindAPair.Constants;
using CJ.FindAPair.Modules.CoreGames;
using CJ.FindAPair.Modules.Meta.Configs;
using CJ.FindAPair.Modules.Service.Save;
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
        private GameSaver _gameSaver;

        public ThemesSelector(ThemeConfigCollection themeConfigCollection, SpecialCardImageConfig specialCardImageConfig,
            LevelCreator levelCreator, LevelBackground
            levelBackground, GameSaver gameSaver)
        {
            _themeConfigCollection = themeConfigCollection;
            _specialCardImageConfig = specialCardImageConfig;
            _levelCreator = levelCreator;
            _levelBackground = levelBackground;
            _gameSaver = gameSaver;
            _selectedThemeConfig = themeConfigCollection.GetThemeConfig(ReadSelectedTheme());
            _levelCreator.OnLevelCreated += InitTheme;
        }

        public void ChangeTheme(string themeId)
        {
            _gameSaver.WriteStringValue(SaveKeys.SelectedTheme, themeId);
            _selectedThemeConfig = _themeConfigCollection.GetThemeConfig(ReadSelectedTheme());
        }
        
        private void InitTheme()
        {
            _quantityOfCardOfPair = (int) _levelCreator.LevelConfig.QuantityOfCardOfPair;
            
            SortCards();
            SetBackground();
            SetCards();
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
            
            foreach (var card in _sortedCards)
            {
                card.SetFace(_selectedThemeConfig.FacesSprites[index]);
                
                if (card.NumberPair >= ConstantsCard.NUMBER_SPECIAL)
                {
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
            var themeId = _gameSaver.ReadStringValue(SaveKeys.SelectedTheme);

            if (themeId == null)
            {
                _gameSaver.WriteStringValue(SaveKeys.SelectedTheme, _themeConfigCollection.DefaultThemeId);
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