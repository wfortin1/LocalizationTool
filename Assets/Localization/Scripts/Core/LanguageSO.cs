using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Localization
{
    [CreateAssetMenu(menuName = "Scriptable Objects/LanguageSO", fileName = "LanguageSO", order = 1)]
    public class LanguageSO : ScriptableObject
    {
        // Serialized Fields
        [SerializeField] private string _languageDisplayName = ""; 
        [SerializeField] private StringStringDictionary _translationKeys = new StringStringDictionary();
        [SerializeField] private StringStringDictionary _translationValues = new StringStringDictionary();
        
        // Public Properties
        public StringStringDictionary TranslationValues => _translationValues;
        public string LanguageName => _languageDisplayName;
        
        // Unique Identifier
        [SerializeField, HideInInspector] private string _uniqueID;

        public string UniqueID
        {
            get
            {
                if (string.IsNullOrEmpty(_uniqueID))
                {
                    _uniqueID = Guid.NewGuid().ToString();
                }

                return _uniqueID;
            }
        }

        public void SetTranslationKeyDictionary(StringStringDictionary newDictionary)
        {
            _translationKeys = DeepCopy(newDictionary);
            
            foreach (string key in _translationKeys.Keys)
            {
                if (_translationValues.Keys.Contains(key) || string.IsNullOrEmpty(key))
                    continue; 
                
                _translationValues.Add(key, String.Empty);
            }
            
            List<string> keysToRemove = new List<string>();
            
            foreach (var pair in _translationValues)
            {
                if (string.IsNullOrEmpty(pair.Value) && !_translationKeys.Keys.Contains(pair.Key))
                {
                    keysToRemove.Add(pair.Key);
                }
            }
            
            foreach (string key in keysToRemove)
            {
                _translationValues.Remove(key);
            }
        }

        private StringStringDictionary DeepCopy(StringStringDictionary original)
        {
            StringStringDictionary copy = new StringStringDictionary();

            foreach (var pair in original)
            {
                copy.Add(pair.Key, pair.Value);
            }

            return copy;
        }
    }
}
