using UnityEngine;

namespace CJ.FindAPair.CardTable
{
    public class Card : MonoBehaviour
    {
        public bool IsEmpty;

        private void Awake()
        {
            IsEmpty = false;
        }
    }
}

