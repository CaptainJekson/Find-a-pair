using System;
using System.Collections.Generic;
using CJ.FindAPair.Modules.CoreGames;
using CJ.FindAPair.Modules.CoreGames.Configs;
using CJ.FindAPair.Modules.UI.Installer;
using CJ.FindAPair.Modules.UI.Slots;
using CJ.FindAPair.Utility;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace CJ.FindAPair.Modules.UI.Windows
{
    public class LevelSelectWindow : Window
    {
        [SerializeField] private LevelPagePanel _levelPagePanel;
        [SerializeField] private RectTransform _contentPosition;
        [SerializeField] private int _slotsPerPage;
        [SerializeField] private Scrollbar _scrollbar;
        
        private LevelConfigCollection _levelConfigCollection;
        private LevelCreator _levelCreator;
        private UIRoot _uiRoot;
        private List<LevelPagePanel> _pages;
        private List<LevelSlot> _slots;
        private int _requiredQuantityPages;

        private bool IsNotEven =>_levelConfigCollection.Levels.Count % _slotsPerPage > 0;

        [Inject]
        private void Construct(LevelConfigCollection levelConfigCollection, LevelCreator levelCreator, UIRoot uiRoot)
        {
            _levelConfigCollection = levelConfigCollection;
            _levelCreator = levelCreator;
            _uiRoot = uiRoot;
            _pages = new List<LevelPagePanel>();
            _slots = new List<LevelSlot>();
        }

        protected override void Init()
        {
            CreatePages();
            SetSlots();
            SetLevelData();
            
            DOTween.To(()=> _scrollbar.value, x=> _scrollbar.value = x, 0.0f, 2.0f);
        }

        private void CreatePages()
        {
            var quantityPages = _levelConfigCollection.Levels.Count / _slotsPerPage;
            _requiredQuantityPages = IsNotEven ? ++quantityPages : quantityPages;

            for (var i = 0; i < _requiredQuantityPages; i++)
            {
                var newPage = Instantiate(_levelPagePanel, transform.position, Quaternion.identity);
                newPage.transform.SetParent(_contentPosition, false);
                newPage.transform.SetAsFirstSibling();
                _pages.Add(newPage);
            }
        }

        private void SetSlots()
        {
            for (var i = 0; i < (IsNotEven ? _pages.Count - 1 : _pages.Count) ; i++)
            {
                for (var j = 0; j < _slotsPerPage; j++)
                {
                    _slots.Add(_pages[i].LevelSlots[j]);
                }
            }
            
            if (IsNotEven)
            {
                for (var i = 0; i < _levelConfigCollection.Levels.Count % _slotsPerPage; i++)
                {
                    _slots.Add(_pages[_pages.Count - 1].LevelSlots[i]);
                }
            }
        }

        private void SetLevelData()
        {
            for (var i = 0; i < _slots.Count; i++)
            {
                _slots[i].SetData(_levelConfigCollection.Levels[i], _levelCreator, _uiRoot);
            }
        }
    }
}
