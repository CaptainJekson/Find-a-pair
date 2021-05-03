using TigerForge;

namespace CJ.FindAPair.Modules.Service.Save
{
    public class GameSaver
    {
        private string _password = "hohoho5417828";
        private string _saveFileName = "SaveData";
        
        public void IncreaseNumberValue(int value, string keyValue)
        {
            var saveDataFile = new EasyFileSave(_saveFileName);
            
            if (saveDataFile.Load(_password))
            {
                var currentCoin = saveDataFile.GetInt(keyValue);
                saveDataFile.Add(keyValue, currentCoin + value);
            }
            else
            {
                saveDataFile.Add(keyValue, value);
            }

            saveDataFile.Save(_password);
            saveDataFile.Dispose();
        }

        public bool DecreaseNumberValueIfPossible(int value, string keyValue)
        {
            var saveDataFile = new EasyFileSave(_saveFileName);
            
            if (saveDataFile.Load(_password))
            {
                var currentCoin = saveDataFile.GetInt(keyValue);

                if (currentCoin < value)
                    return false;
                
                saveDataFile.Add(keyValue, currentCoin - value);
            }
            else
            {
                return false;
            }

            saveDataFile.Save(_password);
            saveDataFile.Dispose();

            return true;
        }

        public int ReadNumberValue(string keyValue)
        {
            var saveDataFile = new EasyFileSave(_saveFileName);
            return saveDataFile.Load(_password) ? saveDataFile.GetInt(keyValue) : 0;
        }
    }
}