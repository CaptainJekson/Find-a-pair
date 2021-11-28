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

    public void DecreaseScore()
    {
        var saveData = _gameSaver.LoadData();

        if (saveData.ItemsData.Energy > 0)
        {
            if (saveData.ItemsData.Energy >= _gameSettingsConfig.MaxEnergyValue)
            {
                saveData.ItemsData.EnergyCooldownTime = DateTime.Now
                    .AddSeconds(_gameSettingsConfig.EnergyScoreCooldownInSeconds).ToString();
            }

            saveData.ItemsData.Energy--;
            _gameSaver.SaveData(saveData);
        }
    }

    public void TryIncreaseScore()
    {
        var saveData = _gameSaver.LoadData();
        var lastCooldownEnd = DateTime.Parse(saveData.ItemsData.EnergyCooldownTime);

        if (lastCooldownEnd <= DateTime.Now)
        {
            saveData.ItemsData.Energy++;
            
            saveData.ItemsData.EnergyCooldownTime = lastCooldownEnd
                .AddSeconds(_gameSettingsConfig.EnergyScoreCooldownInSeconds).ToString();
            
            _gameSaver.SaveData(saveData);
        }
    }

    public string ShowEnergyCooldownTimeInterval()
    {
        var saveData = _gameSaver.LoadData();
        
        return (DateTime.Parse(saveData.ItemsData.EnergyCooldownTime) - DateTime.Now).ToString(@"mm\:ss");
    }
}