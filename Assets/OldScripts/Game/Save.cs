using System;
using UnityEngine.Serialization;

namespace CJ.FindAPair.Game
{
    [Serializable]
    public class Save
    {
        public int Gold;
        public int Energy;

        public int MagicEye;
        public int Electroshock;
        public int Sapper;
    }
}