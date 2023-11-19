using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace Localization
{
    public class LocalizationManager : MonoBehaviour
    {
        public static LocalizationManager Instance { get; private set; }
        
        private Dictionary<string, LanguageSO> languageMap = new Dictionary<string, LanguageSO>();
        private LanguageSO _currentLanguage;
        private StringStringDictionary _currentTranslationDictionary; 
        
        private void Awake()
        {
            if (Instance != null)
            {
                Debug.LogError("More than one LocalizationManager found in the scene.");
            }

            Instance = this; 
            
            List<LanguageSO> languageObjects = Resources.LoadAll<LanguageSO>("Localization").ToList();
            languageObjects.ForEach(language => languageMap.Add(language.UniqueID, language));
            
            _currentLanguage = languageMap.Values.FirstOrDefault();
            if (_currentLanguage)
            {
                _currentTranslationDictionary = _currentLanguage.TranslationValues;
            }
        }

        private void Start()
        {
            TranslateAllTextFields();
        }

        public void SetCurrentLanguage(string languageID)
        {
            languageMap.TryGetValue(languageID, out LanguageSO language);
            
            _currentLanguage = language;

            if (_currentLanguage)
            {
                _currentTranslationDictionary = _currentLanguage.TranslationValues;
            }
            
            TranslateAllTextFields();
        }
        
        // Returns a list of (UniqueID, DisplayName)
        public Dictionary<string, string> GetLanguageDictionary()
        {
            Dictionary<string, string> languageDictionary = new Dictionary<string, string>();

            foreach (LanguageSO language in languageMap.Values)
            {
                languageDictionary.Add(language.UniqueID, language.LanguageName);
            }

            return languageDictionary;
        }

        public void TranslateAllTextFields()
        {
            IEnumerable<LocalizedTextField> textFields = FindObjectsOfType<MonoBehaviour>(true).OfType<LocalizedTextField>();

            foreach (LocalizedTextField textField in textFields)
            {
               textField.Translate();
            }
        }
        
        // Given a Key/ID return the localized string for that key in the current language
        public string Localize(string translationKey)
        {
            if (_currentTranslationDictionary.TryGetValue(translationKey, out string localize))
            {
                return localize;
            }
            else
            {
                Debug.LogWarning($"[LOCALIZATION]: No translation found for key: {translationKey} in language: {_currentLanguage.LanguageName}");
                return translationKey;
            }
        }
    }
}
