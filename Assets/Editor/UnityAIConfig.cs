using UnityEngine;
using UnityEditor;
using System.IO;

namespace UnityAIGenerator
{
    public class UnityAIConfig : ScriptableObject
    {
        [Header("Backend Configuration")]
        public string backendUrl = "http://localhost:5001";
        public bool autoConnect = true;
        public float connectionTimeout = 10f;
        
        [Header("Code Generation Settings")]
        public bool includeComments = true;
        public bool includeExplanations = true;
        public bool autoFormatCode = true;
        public string defaultNamespace = "";
        
        [Header("File Settings")]
        public string defaultScriptPath = "Assets/Scripts/Generated/";
        public bool createFolderStructure = true;
        public bool overwriteExisting = false;
        
        private static UnityAIConfig _instance;
        public static UnityAIConfig Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = LoadOrCreateConfig();
                }
                return _instance;
            }
        }
        
        private static UnityAIConfig LoadOrCreateConfig()
        {
            string configPath = "Assets/UnityAIGenerator/Config/UnityAIConfig.asset";
            
            // Try to load existing config
            UnityAIConfig config = AssetDatabase.LoadAssetAtPath<UnityAIConfig>(configPath);
            
            if (config == null)
            {
                // Create new config
                config = CreateInstance<UnityAIConfig>();
                
                // Create directory if it doesn't exist
                string directory = Path.GetDirectoryName(configPath);
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }
                
                // Save the config
                AssetDatabase.CreateAsset(config, configPath);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
            }
            
            return config;
        }
        
        [MenuItem("Tools/Unity AI Generator/Open Settings")]
        public static void OpenSettings()
        {
            Selection.activeObject = Instance;
            EditorGUIUtility.PingObject(Instance);
        }
        
        [MenuItem("Tools/Unity AI Generator/Reset Settings")]
        public static void ResetSettings()
        {
            if (EditorUtility.DisplayDialog("Reset Settings", 
                "Are you sure you want to reset all Unity AI Generator settings?", 
                "Yes", "No"))
            {
                Instance.backendUrl = "http://localhost:5000";
                Instance.autoConnect = true;
                Instance.connectionTimeout = 10f;
                Instance.includeComments = true;
                Instance.includeExplanations = true;
                Instance.autoFormatCode = true;
                Instance.defaultNamespace = "";
                Instance.defaultScriptPath = "Assets/Scripts/Generated/";
                Instance.createFolderStructure = true;
                Instance.overwriteExisting = false;
                
                EditorUtility.SetDirty(Instance);
                AssetDatabase.SaveAssets();
            }
        }
    }
}
