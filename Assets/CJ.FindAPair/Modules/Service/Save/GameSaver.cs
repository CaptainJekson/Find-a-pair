using System;
using CI.QuickSave;
using CJ.FindAPair.Modules.CoreGames.Booster;

namespace CJ.FindAPair.Modules.Service.Save
{
    public class GameSaver
    {
        private QuickSaveWriter _writer;
        private QuickSaveReader _reader;

        public QuickSaveReader Reader => _reader;

        private GameSaver()
        {
            _writer = QuickSaveWriter.Create("GameSave");
            
            try
            {
                _reader = QuickSaveReader.Create("GameSave");
            }
            catch
            {
                var player = new Player();
                
                _writer.Write("Player", player);
                _writer.Commit();
                _reader = QuickSaveReader.Create("GameSave");
            }
        }

        public void AddPlayerCoin(int value)
        {
            var player = _reader.Read<Player>("Player");
            player.Coin += value;
            _writer.Write("Player", player);
            _writer.Commit();
        }

        public bool RemoveCoinsIfPossible(int value)
        {
            var player = _reader.Read<Player>("Player");

            if (player.Coin < value)
                return false;

            player.Coin -= value;
            _writer.Write("Player", player);
            _writer.Commit();
            
            return true;
        }

        public void AddBooster(int value, BoosterType boosterType)
        {
            var player = _reader.Read<Player>("Player");
            player.Boosters ??= new Boosters();

            switch (boosterType)
            {
                case BoosterType.Electroshock:
                    player.Boosters.Electroshock += value;
                    break;
                case BoosterType.MagicEye:
                    player.Boosters.MagicEye += value;
                    break;
                case BoosterType.Sapper:
                    player.Boosters.Sapper += value;
                    break;
            }

            _writer.Write("Player", player);
            _writer.Commit();
        }

        public bool RemoveBoosterIfPossible(int value, BoosterType boosterType)
        {
            var player = _reader.Read<Player>("Player");
            player.Boosters ??= new Boosters();

            switch (boosterType)
            {
                case BoosterType.Electroshock:
                    if (player.Boosters.Electroshock < value)
                        return false;
                    player.Boosters.Electroshock -= value;
                    break;
                case BoosterType.MagicEye:
                    if (player.Boosters.MagicEye < value)
                        return false;
                    player.Boosters.MagicEye -= value;
                    break;
                case BoosterType.Sapper:
                    if (player.Boosters.Sapper < value)
                        return false;
                    player.Boosters.Sapper -= value;
                    break;
            }

            _writer.Write("Player", player);
            _writer.Commit();
            
            return true;
        }
    }
}