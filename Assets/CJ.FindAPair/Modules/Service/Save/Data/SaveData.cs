using System;
using System.Collections.Generic;

[Serializable]
public class SaveData
{
    public int UserId;
    public int CurrentLevel = 1;
    public List<int> CompletedLevels;
    public ItemsData ItemsData;
    public ThemesData ThemesData;
    public AdsData AdsData;
    
    public SaveData()
    {
        CompletedLevels = new List<int>();
        ItemsData = new ItemsData();
        ThemesData = new ThemesData();
        AdsData = new AdsData();
    }
    
    public bool DecreaseDetectorBoosterIfPossible(int value)
    {
        if (value > ItemsData.DetectorBooster)
            return false;
        
        ItemsData.DetectorBooster -= value;
        
        return true;
    }

    public bool DecreaseMagnetBoosterIfPossible(int value)
    {
        if (value > ItemsData.MagnetBooster)
            return false;
        
        ItemsData.MagnetBooster -= value;
        
        return true;
    }
    
    public bool DecreaseSapperBoosterIfPossible(int value)
    {
        if (value > ItemsData.SapperBooster)
            return false;
        
        ItemsData.SapperBooster -= value;
        
        return true;
    }
}

[Serializable]
public class ItemsData
{
    public int DetectorBooster;
    public int MagnetBooster;
    public int SapperBooster;
    public int Coins;
    public int Diamond;
    public int Energy = 5;
    public string EnergyCooldownTime;
}

[Serializable]
public class ThemesData
{
    public string SelectedTheme;
    public List<string> OpenedThemes;

    public ThemesData()
    {
        OpenedThemes = new List<string>();
    }
}

[Serializable]
public class AdsData
{
    public string EndCooldownForContinueGame;
}