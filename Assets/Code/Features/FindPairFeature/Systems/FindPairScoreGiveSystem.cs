using CJ.FindAPair.Modules.CoreGames;
using CJ.FindAPair.Modules.CoreGames.Configs;
using Code.Features.FindPairFeature.Components;
using Code.Features.LevelFeature.Interfaces;
using Code.GlobalUtils;
using Scellecs.Morpeh;

namespace Code.Features.FindPairFeature.Systems
{
    public class FindPairScoreGiveSystem : SimpleSystem<FindPairScore, FindPairScoreGive>, ISystem
    {
        [Injectable] private Stash<FindPairScore> _findPairScore;
        [Injectable] private Stash<FindPairScoreGive> _findPairScoreGive;

        [Injectable] private ILevelStorage _levelStorage;
        [Injectable] private GameSettingsConfig _gameSettingsConfig;
        
        public void OnUpdate(float deltaTime)
        {
            foreach (var entity in _filter)
            {
                ref var findPairScore = ref _findPairScore.Get(entity);
                
                //TODO dev проверка на IsIncomeLevel

                if (_levelStorage.TryGetCurrentLevel(out var level))
                {
                    switch (level.QuantityOfCardOfPair)
                    {
                        case QuantityOfCardOfPair.TwoCards:
                            findPairScore.score += _gameSettingsConfig.PointsTwoCards;
                            findPairScore.accruedScore = _gameSettingsConfig.PointsTwoCards;
                            break;
                        case QuantityOfCardOfPair.ThreeCards:
                            findPairScore.score += _gameSettingsConfig.PointsThreeCards;
                            findPairScore.accruedScore = _gameSettingsConfig.PointsThreeCards;
                            break;
                        case QuantityOfCardOfPair.FourCards:
                            findPairScore.score += _gameSettingsConfig.PointsFourCards;
                            findPairScore.accruedScore = _gameSettingsConfig.PointsThreeCards;
                            break;
                    }

                    if (findPairScore.comboCounter >= 1)
                    {
                        var scoreCombo = _gameSettingsConfig.ScoreCombo.Count > findPairScore.comboCounter ? 
                            _gameSettingsConfig.ScoreCombo[findPairScore.comboCounter - 1] : 
                            _gameSettingsConfig.ScoreCombo[_gameSettingsConfig.ScoreCombo.Count - 1];

                        findPairScore.comboCounter = scoreCombo;
                        findPairScore.score += scoreCombo;
                    }

                    findPairScore.comboCounter++;
                }
            }
        }
    }
}