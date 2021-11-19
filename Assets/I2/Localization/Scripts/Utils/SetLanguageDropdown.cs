using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace I2.Loc
{
	[AddComponentMenu("I2/Localization/SetLanguage Dropdown")]
	public class SetLanguageDropdown : MonoBehaviour
	{
		[SerializeField] private List<Sprite> _itemImages;
		
        #if UNITY_5_2 || UNITY_5_3 || UNITY_5_4_OR_NEWER
        void OnEnable()
		{
			var dropdown = GetComponent<TMP_Dropdown>();
			if (dropdown==null)
				return;

			var currentLanguage = LocalizationManager.CurrentLanguage;
			if (LocalizationManager.Sources.Count==0) LocalizationManager.UpdateSources();
			var languages = LocalizationManager.GetAllLanguages();

			// Fill the dropdown elements
			dropdown.ClearOptions();
			dropdown.AddOptions(SetOptions(languages));

			dropdown.value = languages.IndexOf( currentLanguage );
			dropdown.onValueChanged.RemoveListener( OnValueChanged );
			dropdown.onValueChanged.AddListener( OnValueChanged );
		}

        private List<TMP_Dropdown.OptionData> SetOptions(List<string> languages)
        {
	        List<TMP_Dropdown.OptionData> options = new List<TMP_Dropdown.OptionData>();
	        
	        for (int i = 0; i < _itemImages.Count; i++)
	        {
		        TMP_Dropdown.OptionData optionData = new TMP_Dropdown.OptionData();

		        optionData.text = languages[i];
		        optionData.image = _itemImages[i];
				
		        options.Add(optionData);
	        }

	        return options;
        }

        void OnValueChanged( int index )
		{
			var dropdown = GetComponent<TMP_Dropdown>();
			if (index<0)
			{
				index = 0;
				dropdown.value = index;
			}

			LocalizationManager.CurrentLanguage = dropdown.options[index].text;
        }
        #endif
    }
}