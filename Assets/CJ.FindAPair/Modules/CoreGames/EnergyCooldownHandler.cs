using System;
using CJ.FindAPair.Modules.CoreGames.Configs;
using Zenject;

public class EnergyCooldownHandler
{
    private ISaver _gameSaver;
    private GameSettingsConfig _gameSettingsConfig;

    private DateTime _cooldownTime;
    
    [Inject]
    private void Construct(ISaver gameSaver, GameSettingsConfig gameSettingsConfig)
    {
        _gameSaver = gameSaver;
        _gameSettingsConfig = gameSettingsConfig;
        _cooldownTime = new DateTime();
    }

    public void TryDecreaseScore()
    {
        var saveData = _gameSaver.LoadData();

        if (saveData.ItemsData.Energy > 0)
        {
            saveData.ItemsData.Energy--;

            saveData.ItemsData.EnergyCooldownTime = DateTime.Now
                .AddSeconds(_gameSettingsConfig.EnergyScoreCooldownInSeconds).ToString();
            
            _gameSaver.SaveData(saveData);
        }
    }

    public void TryIncreaseScore()
    {
        var saveData = _gameSaver.LoadData();

        if (DateTime.Parse(saveData.ItemsData.EnergyCooldownTime) <= DateTime.Now)
        {
            saveData.ItemsData.Energy++;

            _gameSaver.SaveData(saveData);
        }
    }

    public string ShowEnergyCooldownTimeInterval()
    {
        var saveData = _gameSaver.LoadData();
        
        return (DateTime.Parse(saveData.ItemsData.EnergyCooldownTime) - DateTime.Now).ToString(@"mm\:ss");
    }
}