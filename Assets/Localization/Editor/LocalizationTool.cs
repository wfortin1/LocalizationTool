using System.IO;
using UnityEditor;
using UnityEngine;

namespace Localization
{
    // CLASS RESPONSIBILITY
    // - Adding key's and giving them a label --> DONE
    // - Sending that data down to the scriptable objects and adding it to there key dictionary 
    //   - LanguageSO object's should not be able to edit their keys 
    // - Adding new Language SO is done from here as well. 
    // Actual key editing is done on the ScriptableObject.
    public class LocalizationTool : EditorWindow
    {
        [SerializeField] private StringStringDictionary _translationKeys;
        
        // Serialized Properties
        private SerializedObject _serializedObject;
        private SerializedProperty _translationKeysProperty;
        
        [MenuItem("Tools/Localization")] 
        public static void OpenWindow() => GetWindow<LocalizationTool>("Localization");

        private void OnGUI()
        {
            _serializedObject.Update();
            
            EditorGUILayout.PropertyField(_translationKeysProperty, true);
            
            _serializedObject.ApplyModifiedProperties();

            if (GUI.changed)
            {
                SyncLanguageAssets();
            }

            using (var layout = new GUILayout.HorizontalScope())
            {
                if (GUILayout.Button("Create New Language"))
                {
                    CreateNewLanguage();
                }
                
                if (GUILayout.Button("Force Synchronization"))
                {
                    SyncLanguageAssets();
                }
            }
        }

        private void SyncLanguageAssets()
        {
            string[] result = AssetDatabase.FindAssets("t:LanguageSO");
                
            foreach (string languageAssetGUID in result)
            {
                string path = AssetDatabase.GUIDToAssetPath(languageAssetGUID);
                    
                LanguageSO languageObject = (LanguageSO)AssetDatabase.LoadAssetAtPath(path, typeof(LanguageSO));

                if (languageObject)
                {
                    languageObject.SetTranslationKeyDictionary(_translationKeys);
                }
                    
                EditorUtility.SetDirty(languageObject);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
            }
        }

        private void CreateNewLanguage()
        {
            if (!AssetDatabase.IsValidFolder("Assets/Resources"))
            {
                AssetDatabase.CreateFolder("Assets", "Resources");
            }
            
            if (!AssetDatabase.IsValidFolder("Assets/Resources/Localization"))
            {
                AssetDatabase.CreateFolder("Resources", "Localization");
            }

            string path = Path.Combine("Assets", "Resources", "Localization", "NewLanguageSO.asset");

            LanguageSO languageSO = CreateInstance<LanguageSO>();
            AssetDatabase.CreateAsset(languageSO, path);
            
            SyncLanguageAssets();
        }

        private void OnEnable()
        {
            string data = EditorPrefs.GetString("LocalizationTool", JsonUtility.ToJson(this, false));
            JsonUtility.FromJsonOverwrite(data, this);
            
            _serializedObject = new SerializedObject(this);
            _translationKeysProperty = _serializedObject.FindProperty("_translationKeys");
        }

        private void OnDisable()
        {
            string data = JsonUtility.ToJson(this, false); 
            EditorPrefs.SetString("LocalizationTool", data);
        }
    }
}
