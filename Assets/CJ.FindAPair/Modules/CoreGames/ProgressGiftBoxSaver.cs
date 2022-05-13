using CJ.FindAPair.Modules.UI.Installer;
using CJ.FindAPair.Modules.UI.Windows;

namespace CJ.FindAPair.Modules.CoreGames
{
    public class ProgressGiftBoxSaver
    {
        private GameWatcher _gameWatcher;
        private LevelCreator _levelCreator;
        private ISaver _gameSaver;

        public bool IsSaveProgress { get; set; }

        public ProgressGiftBoxSaver(GameWatcher gameWatcher, LevelCreator levelCreator, UIRoot uiRoot, ISaver gameSaver)
        {
            _gameWatcher = gameWatcher;
            _levelCreator = levelCreator;
            _gameSaver = gameSaver;
        }

        public void SaveProgress()
        {
            var saveData = _gameSaver.LoadData();

            saveData.ItemsData.Coins += _gameWatcher.Score;

            if (saveData.CompletedLevels.Contains(_levelCreator.LevelConfig.LevelNumber) == false)
            {
                TrySaveExtraRewardItems();
                saveData.CompletedLevels.Add(_levelCreator.LevelConfig.LevelNumber);
            }

            if (saveData.CurrentLevel == _levelCreator.LevelConfig.LevelNumber)
            {
                saveData.CurrentLevel++;
            }

            _gameSaver.SaveData(saveData);
        }
        
        private void TrySaveExtraRewardItems()
        {
            if (_levelCreator.LevelConfig.RewardItemsCollection)
            {
                var saveData = _gameSaver.LoadData();
                var itemsCollection = _levelCreator.LevelConfig.RewardItemsCollection.Items;

                foreach (var item in itemsCollection)
                {
                    switch (item.Type)
                    {
                        case ItemTypes.Coin:
                            saveData.ItemsData.Coins += item.Count;
                            break;
                        case ItemTypes.Diamond:
                            saveData.ItemsData.Diamond += item.Count;
                            break;
                        case ItemTypes.Energy:
                            saveData.ItemsData.Energy += item.Count;
                            break;
                        case ItemTypes.DetectorBooster:
                            saveData.ItemsData.DetectorBooster += item.Count;
                            break;
                        case ItemTypes.MagnetBooster:
                            saveData.ItemsData.MagnetBooster += item.Count;
                            break;
                        case ItemTypes.SapperBooster:
                            saveData.ItemsData.SapperBooster += item.Count;
                            break;
                    }
                }

                _gameSaver.SaveData(saveData);
                IsSaveProgress = true;
            }
        }
    }
}