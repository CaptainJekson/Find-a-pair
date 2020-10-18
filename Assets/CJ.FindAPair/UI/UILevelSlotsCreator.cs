using CJ.FindAPair.CardTable;
using CJ.FindAPair.Configurations;
using System.Collections.Generic;
using UnityEngine;

namespace CJ.FindAPair.UI
{
    class UILevelSlotsCreator : MonoBehaviour
    {        
        [SerializeField] private LevelConfigCollection _levelConfigCollection;
        [SerializeField] private LevelCreator _levelCreator;
        [SerializeField] private UILevelPage _uILevelPage;
        [SerializeField] private UILevelSlot _uILevelSlot;
        [SerializeField] private int _slotsPerPage;

        private List<UILevelPage> _pages;
        private List<UILevelSlot> _slots;
        private int _requiredQuantityPages;

        private bool IsNotEven =>_levelConfigCollection.Levels.Count % _slotsPerPage > 0;

        private void Awake()
        {
            _pages = new List<UILevelPage>();
            _slots = new List<UILevelSlot>();

            InitLevelsSlot();
        }

        private void InitLevelsSlot()
        {
            CreatePages();
            CreateSlots();
            SetLevelData();
        }

        private void CreatePages()
        {
            int quantityPages = _levelConfigCollection.Levels.Count / _slotsPerPage;
            _requiredQuantityPages = IsNotEven ? ++quantityPages : quantityPages;

            for (int i = 0; i < _requiredQuantityPages; i++)
            {
                UILevelPage newPage = Instantiate(_uILevelPage, transform.position, Quaternion.identity);
                newPage.transform.SetParent(transform, false);
                _pages.Add(newPage);
            }
        }

        private void CreateSlots()
        {
            for (int i = 0; i < (IsNotEven ? _pages.Count - 1 : _pages.Count) ; i++)
            {
                for (int j = 0; j < _slotsPerPage; j++)
                {
                    UILevelSlot newSlot = Instantiate(_uILevelSlot, transform.position, Quaternion.identity);
                    newSlot.transform.SetParent(_pages[i].transform, false);
                    _slots.Add(newSlot);
                }
            }

            if (IsNotEven)
            {
                for (int i = 0; i < _levelConfigCollection.Levels.Count % _slotsPerPage; i++)
                {
                    UILevelSlot newSlot = Instantiate(_uILevelSlot, transform.position, Quaternion.identity);
                    newSlot.transform.SetParent(_pages[_pages.Count - 1].transform, false);
                    _slots.Add(newSlot);
                }
            }
        }

        private void SetLevelData()
        {
            for (int i = 0; i < _slots.Count; i++)
            {
                _slots[i].Level = _levelConfigCollection.Levels[i];
                _slots[i].LevelCreator = _levelCreator;
                _slots[i].SetLevelNumberText(i + 1);
            }
        }
    }
}
