using System;
using CJ.FindAPair.Modules.CoreGames.Configs;
using Zenject;

public class EnergyHandler
{
    private ISaver _gameSaver;
    private GameSettingsConfig _gameSettingsConfig;

    [Inject]
    public void Construct(ISaver gameSaver, GameSettingsConfig gameSettingsConfig)
    {
        _gameSaver = gameSaver;
        _gameSettingsConfig = gameSettingsConfig;
    }
    
    public void DecreaseScore()
    {
        var saveData = _gameSaver.LoadData();

        saveData.ItemsData.Energy--;
        saveData.ItemsData.EnergyCooldowEndTimePoints
            .Add(DateTime.Now.AddSeconds(_gameSettingsConfig.EnergyScoreCooldownInSeconds).ToString());

        _gameSaver.SaveData(saveData);
    }
    
    private void TryIncreaseScore()
    {
        var saveData = _gameSaver.LoadData();

        foreach (var timePoint in saveData.ItemsData.EnergyCooldowEndTimePoints)
        {
            if (DateTime.Parse(timePoint) > DateTime.Now)
            {
                saveData.ItemsData.Energy++;
                saveData.ItemsData.EnergyCooldowEndTimePoints.Remove(timePoint);
            }
        }
        
        _gameSaver.SaveData(saveData);
    }
}