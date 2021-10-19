using System.Collections.Generic;
using CJ.FindAPair.Modules.Meta.Configs;
using CJ.FindAPair.Modules.Meta.Themes;
using CJ.FindAPair.Modules.Service.Store;
using CJ.FindAPair.Modules.UI.Installer;
using CJ.FindAPair.Modules.UI.Slots;
using UnityEngine;
using Zenject;

namespace CJ.FindAPair.Modules.UI.Windows
{
    public class ThemeWindow : Window
    {
        [SerializeField] private ThemeSlot _themeSlotPrefab;
        [SerializeField] private RectTransform _selectedSlotParent;
        [SerializeField] private RectTransform _contentSlotParent;

        private BlockWindow _blockWindow;
        private ThemesSelector _themesSelector;
        private ThemeConfigCollection _themeConfigCollection;
        private IStoreDriver _storeDriver;
        private ISaver _gameSaver;

        private ThemeSlot _selectedThemeSlot;
        private List<ThemeSlot> _themeSlots;

        [Inject]
        private void Construct(UIRoot uiRoot, ThemesSelector themesSelector,
            ThemeConfigCollection themeConfigCollection, ISaver gameSaver, IStoreDriver storeDriver)
        {
            _blockWindow = uiRoot.GetWindow<BlockWindow>();
            _themesSelector = themesSelector;
            _themeConfigCollection = themeConfigCollection;
            _gameSaver = gameSaver;
            _storeDriver = storeDriver;
            _themeSlots = new List<ThemeSlot>();
        }

        protected override void Init()
        {
            CreateThemeSlots();
        }

        protected override void OnOpen()
        {
            RefreshStateSlots();
        }

        protected override void OnCloseButtonClick()
        {
            _blockWindow.Close();
            base.OnCloseButtonClick();
        }

        private void CreateThemeSlots()
        {
            _selectedThemeSlot = Instantiate(_themeSlotPrefab, _selectedSlotParent);
            _selectedThemeSlot.EnableChangeThemeToggle();

            foreach (var themeConfig in _themeConfigCollection.Themes)
            {
                var spawnedSlot = Instantiate(_themeSlotPrefab, _contentSlotParent);
                _themeSlots.Add(spawnedSlot);
                spawnedSlot.Init(_themesSelector, _storeDriver, RefreshSlotData);
                spawnedSlot.SetData(themeConfig);
            }

            RefreshSlotData();
        }

        private void RefreshSlotData()
        {
            RefreshSelectedThemes();
            RefreshReceivedThemes();
            RefreshStateSlots();
        }

        private void RefreshSelectedThemes()
        {
            var saveData = _gameSaver.LoadData();

            foreach (var slot in _themeSlots)
            {
                slot.gameObject.SetActive((slot.ThemeId == saveData.ThemesData.SelectedTheme) == false);
            }

            var selectedThemeConfig = _themeConfigCollection.GetThemeConfig(_gameSaver.LoadData()
                .ThemesData.SelectedTheme);
            _selectedThemeSlot.Init(_themesSelector, _storeDriver, RefreshSlotData);
            _selectedThemeSlot.SetData(selectedThemeConfig);
        }

        private void RefreshReceivedThemes()
        {
            var saveData = _gameSaver.LoadData();

            foreach (var openedTheme in saveData.ThemesData.OpenedThemes)
            {
                foreach (var slot in _themeSlots)
                {
                    if (slot.ThemeId == openedTheme)
                    {
                        slot.EnableSelectButton(true);
                    }
                }
            }
        }

        private void RefreshStateSlots()
        {
            foreach (var slot in _themeSlots)
            {
                slot.RefreshBuyButton();
            }
        }
    }
}