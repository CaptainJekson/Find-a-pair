using CJ.FindAPair.Constants;
using CJ.FindAPair.Modules.Meta.Configs;
using Code.Features.LevelFeature.Components;
using Code.Features.ThemesFeature.Components;
using Code.GlobalUtils;
using Scellecs.Morpeh;
using UnityEngine;

namespace Code.Features.ThemesFeature.Systems
{
    public class ThemeInitSystem : SimpleSystem<ThemeLevelInit, Level>, ISystem
    {
        [Injectable] private Stash<ThemeLevelInit> _themeLevelInit;
        [Injectable] private Stash<Level> _level;
        
        [Injectable] private ThemeConfigCollection _themesConfig;
        [Injectable] private SpecialCardImageConfig _specialCardImageConfig;
        [Injectable] private Locator _locator;
        
        private const int IndexTheme = 0; //TODO выбранная тема
        
        public void OnUpdate(float deltaTime)
        {
            foreach (var entity in _filter)
            {
                var selectedThemeConfig = _themesConfig.Themes[IndexTheme]; //TODO выбранная тема
                
                ref var level = ref _level.Get(entity);
                
                var cards = level.enableCards;
                var quantityOfCardOfPair = (int) level.levelConfig.QuantityOfCardOfPair;
                cards.Sort();
                var sortedCards = cards;
                
                _locator.levelBackground.SetSprite(selectedThemeConfig.BackGroundSprite);
                
                foreach (var card in sortedCards)
                {
                    card.SetShirt(selectedThemeConfig.ShirtSprite);
                }
                
                var pairCounter = 0;
                var index = 0;
                
                for (var i = selectedThemeConfig.FacesSprites.Count - 1; i > 0; i--)
                {
                    var j = Random.Range(0, i);
                
                    (selectedThemeConfig.FacesSprites[i], selectedThemeConfig.FacesSprites[j]) 
                        = (selectedThemeConfig.FacesSprites[j], selectedThemeConfig.FacesSprites[i]);
                }

                foreach (var card in sortedCards)
                {
                    if (card.NumberPair < ConstantsCard.NUMBER_SPECIAL)
                    {
                        card.SetFace(selectedThemeConfig.FacesSprites[index]);
                    }
                    else
                    {
                        card.SetFace(selectedThemeConfig.SpecialCardFaceSprite);
                        card.SetSpecialIcon(GetFaceSpecialCard(card.NumberPair));
                    }

                    pairCounter++;

                    if (pairCounter >= quantityOfCardOfPair)
                    {
                        index++;
                        pairCounter = 0;
                    }
                }

                _themeLevelInit.Remove(entity);
            }
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