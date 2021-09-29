using UnityEngine;

namespace CJ.FindAPair.Utility
{
    public class Rotator : MonoBehaviour
    {
        [SerializeField] private float _speedRotateZ;
            
        private void Update()
        {
            transform.Rotate(0,0, _speedRotateZ);
        }
    }
}