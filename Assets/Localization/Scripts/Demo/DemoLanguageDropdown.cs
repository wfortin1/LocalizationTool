using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Localization
{
    public class DemoLanguageDropdown : MonoBehaviour
    {
        private TMPro.TMP_Dropdown _languageDropdown;
        private Dictionary<string, string> _languageOptions;

        private List<string> _keysList;
        
        private void Awake()
        {
            _languageDropdown = GetComponent<TMPro.TMP_Dropdown>();
        }

        private void Start()
        {
            // Populate dropdown with values from manager
            _languageOptions = LocalizationManager.Instance.GetLanguageDictionary();

            _languageDropdown.ClearOptions();
            _languageDropdown.AddOptions(_languageOptions.Values.ToList());
            _keysList = _languageOptions.Keys.ToList();
            _languageDropdown.onValueChanged.AddListener(OnDropdownValueChanged);
        }

        public void OnDropdownValueChanged(int index)
        {
            string languageKey = _keysList[index];
            LocalizationManager.Instance.SetCurrentLanguage(languageKey);
        }
    }
}
