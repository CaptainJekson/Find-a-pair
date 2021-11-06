using System;
using System.Collections.Generic;

[Serializable]
public class SaveData
{
    public int UserId;
    public int CurrentLevel;
    public ItemsData ItemsData;
    public ThemesData ThemesData;
    public AdsData AdsData;
    
    public SaveData()
    {
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
    public int Energy;
    public int Coins;
    public int Diamond;
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
