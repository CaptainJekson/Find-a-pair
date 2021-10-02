using System.Collections.Generic;
using System.Linq;
using CJ.FindAPair.Modules.Service;
using UnityEngine;

namespace CJ.FindAPair.Modules.Meta.Configs
{
    [CreateAssetMenu(fileName = "Theme", menuName = "Find a pair/Theme")]
    public class ThemeConfig : ScriptableObject
    {
        [SerializeField] private string _id;
        [SerializeField] private string _name;
        [SerializeField] private string _description;
        [SerializeField] private CurrencyType _currencyType;
        [SerializeField] private bool _isOpensLevel;
        [SerializeField] private int _price;
        [SerializeField] private int _requiredLevel;
        [SerializeField] private Sprite _shirtSprite;
        [SerializeField] private List<Sprite> _facesSprites;
        [SerializeField] private Sprite _backGroundSprite;
        [SerializeField] private AudioClip _music;
        
        public string Id => _id;
        public string Name => _name;
        public string Description => _description;
        public CurrencyType CurrencyType => _currencyType;
        public bool IsOpensLevel => _isOpensLevel;
        public int Price => _price;
        public int RequiredLevel => _requiredLevel;
        public Sprite ShirtSprite => _shirtSprite;
        public List<Sprite> FacesSprites => _facesSprites;
        public Sprite BackGroundSprite => _backGroundSprite;
        public AudioClip Music => _music;
        
        public void SetData(string id, string name, string description)
        {
            _id = id;
            _name = name;
            _description = description;
        }

        public void SetPrice(bool isOpensLevel, int requiredLevel, CurrencyType currencyType, int price)
        {
            _isOpensLevel = isOpensLevel;
            _requiredLevel = requiredLevel;
            _currencyType = currencyType;
            _price = price;
        }

        public void SetSprites(Sprite shirtSprite, Sprite[] facesSprites, Sprite backGroundSprite)
        {
            _shirtSprite = shirtSprite;
            _facesSprites = facesSprites.ToList();
            _backGroundSprite = backGroundSprite;
        }

        public void SetAudio(AudioClip music)
        {
            _music = music;
        }
    }
}