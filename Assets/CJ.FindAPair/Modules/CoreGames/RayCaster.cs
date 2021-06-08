using UnityEngine;

namespace CJ.FindAPair.Modules.CoreGames
{
    public class RayCaster : MonoBehaviour
    {
        private Camera _gameCamera;

        private void Awake()
        {
            _gameCamera = FindObjectOfType<Camera>();
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                var ray = _gameCamera.ScreenPointToRay(Input.mousePosition);
                
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit) && hit.collider.GetComponent<Card>())
                {
                    var card = hit.collider.GetComponent<Card>();
                    card.Show();
                }
            }
        }
    }
}