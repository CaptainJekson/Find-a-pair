using System.Collections.Generic;
using CJ.FindAPair.Modules.CoreGames;
using CJ.FindAPair.Modules.CoreGames.Configs;
using UnityEngine;
using Zenject;

namespace CJ.FindAPair.UI
{
    public class LevelSelectWindow : MonoBehaviour
    {
        [SerializeField] private PreviewLevelWindow _previewLevelWindow;
        [SerializeField] private LevelPagePanel _levelPagePanel;
        [SerializeField] private LevelSlot _levelSlot;
        [SerializeField] private int _slotsPerPage;
        
        private LevelConfigCollection _levelConfigCollection;
        private LevelCreator _levelCreator;
        private List<LevelPagePanel> _pages;
        private List<LevelSlot> _slots;
        private int _requiredQuantityPages;

        private bool IsNotEven =>_levelConfigCollection.Levels.Count % _slotsPerPage > 0;

        [Inject]
        private void Construct(LevelConfigCollection levelConfigCollection, LevelCreator levelCreator)
        {
            Debug.LogError("Construct LevelSelectWindow");
            _levelConfigCollection = levelConfigCollection;
            _levelCreator = levelCreator;
            
            _pages = new List<LevelPagePanel>();
            _slots = new List<LevelSlot>();

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
                var newPage = Instantiate(_levelPagePanel, transform.position, Quaternion.identity);
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
                    var newSlot = Instantiate(_levelSlot, transform.position, Quaternion.identity);
                    newSlot.transform.SetParent(_pages[i].transform, false);
                    _slots.Add(newSlot);
                }
            }

            if (IsNotEven)
            {
                for (int i = 0; i < _levelConfigCollection.Levels.Count % _slotsPerPage; i++)
                {
                    var newSlot = Instantiate(_levelSlot, transform.position, Quaternion.identity);
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
                _slots[i].PreviewLevelWindow = _previewLevelWindow;
                _slots[i].SetLevelNumberText(i + 1);
            }
        }
    }
}
