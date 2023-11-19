using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

namespace Localization
{
    public class LocalizedTextField : MonoBehaviour
    {
        [SerializeField] private bool _overrideTranslation;
        [SerializeField] private string _localizationKey;
        
        private TextMeshProUGUI _textField;

        private void Awake()
        {
            _textField = GetComponent<TextMeshProUGUI>();
        }

        public void Translate()
        {
            if (_overrideTranslation)
                return;

            if (string.IsNullOrEmpty(LocalizationManager.Instance.Localize(_localizationKey)))
                return;
            
            _textField.SetText(LocalizationManager.Instance.Localize(_localizationKey));
        }
    }
}
