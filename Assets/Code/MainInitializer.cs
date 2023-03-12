using Code.GlobalUtils;
using Scellecs.Morpeh;
using UnityEngine;

namespace Code
{
    public class MainInitializer : MonoBehaviour
    {
        [SerializeField] private SerializedSimpleDImple configs;
        [SerializeField] private Locator locator;
        
        private SimpleDImple _container;

        private void Start()
        {
            _container = new SimpleDImple();
            _container.CopyRegistrationsFrom(configs);
            
            _container.Register(locator);
            _container.Register(World.Default);
            
            MorpehInitializer.Initialize(World.Default, _container);
        }
    }
}