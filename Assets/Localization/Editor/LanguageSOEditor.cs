using UnityEditor;

namespace Localization
{
    [CustomEditor(typeof(LanguageSO))]
    public class LanguageSOEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            EditorGUILayout.HelpBox("Changes to TranslationKeys should be done through the Localization Tool Editor Window. " +
                                    "They will be automatically synchronized between the languages." +
                                    " Any changes you make here will not affect other languages and may cause unnecessary effects", MessageType.Warning);
            base.OnInspectorGUI();
        }
    }
}
