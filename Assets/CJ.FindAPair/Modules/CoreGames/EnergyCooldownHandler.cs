using System;
using CJ.FindAPair.Modules.CoreGames.Configs;
using Zenject;

public class EnergyCooldownHandler
{
    private ISaver _gameSaver;
    private GameSettingsConfig _gameSettingsConfig;

    [Inject]
    private void Construct(ISaver gameSaver, GameSettingsConfig gameSettingsConfig)
    {
        _gameSaver = gameSaver;
        _gameSettingsConfig = gameSettingsConfig;
    }

    public void TryDecreaseScore()
    {
        var saveData = _gameSaver.LoadData();

        if (saveData.ItemsData.Energy > 0)
        {
            saveData.ItemsData.Energy--;

            if (saveData.ItemsData.EnergyCooldowTimePoints.Count <= 0)
            {
                saveData.ItemsData.EnergyCooldowTimePoints
                    .Add(DateTime.Now.AddSeconds(_gameSettingsConfig.EnergyScoreCooldownInSeconds).ToString());
            }
            else
            {
                var lastPointTimeInterval = DateTime.Parse(_gameSaver.LoadData().ItemsData
                    .EnergyCooldowTimePoints[saveData.ItemsData.EnergyCooldowTimePoints.Count - 1]) - DateTime.Now;
                
                saveData.ItemsData.EnergyCooldowTimePoints
                    .Add(DateTime.Now.AddSeconds(_gameSettingsConfig.EnergyScoreCooldownInSeconds)
                        .Add(lastPointTimeInterval).ToString());
            }

            _gameSaver.SaveData(saveData);
        }
    }

    public void TryIncreaseScore()
    {
        var saveData = _gameSaver.LoadData();

        if (DateTime.Now >= DateTime.Parse(saveData.ItemsData.EnergyCooldowTimePoints[0]))
        {
            saveData.ItemsData.Energy++;
            saveData.ItemsData.EnergyCooldowTimePoints.RemoveAt(0);
            
            _gameSaver.SaveData(saveData);
        }
    }

    public string ShowEnergyCooldownTimeInterval()
    {
        return (DateTime.Parse(_gameSaver.LoadData().ItemsData.EnergyCooldowTimePoints[0]) - DateTime.Now)
            .ToString(@"mm\:ss");
    }
}