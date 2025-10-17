using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Unity.Plastic.Newtonsoft.Json;

namespace UnityAIGenerator
{
    public class UEchoWindow : EditorWindow
    {
        private string userRequest = "";
        private string generatedCode = "";
        private bool isGenerating = false;
        private bool scriptCreated = false;
        private string createdScriptPath = "";
        private Vector2 scrollPosition;
        private string backendUrl = "http://localhost:5001";
        
        [MenuItem("Tools/Unity AI Code Generator")]
        public static void ShowWindow()
        {
            GetWindow<UEchoWindow>("Unity AI Generator");
        }
        
        void OnGUI()
        {
            GUILayout.Label("Unity AI Code Generator", EditorStyles.boldLabel);
            GUILayout.Space(10);
            
            // Backend URL configuration
            GUILayout.Label("Backend Configuration:", EditorStyles.label);
            backendUrl = EditorGUILayout.TextField("Backend URL:", backendUrl);
            GUILayout.Space(5);
            
            // User request input
            GUILayout.Label("What Unity script do you want to create?", EditorStyles.label);
            userRequest = EditorGUILayout.TextArea(userRequest, GUILayout.Height(60));
            GUILayout.Space(5);
            
            // Generate button
            GUI.enabled = !isGenerating && !string.IsNullOrEmpty(userRequest);
            if (GUILayout.Button(isGenerating ? "Generating..." : "Generate & Create Unity Script", GUILayout.Height(30)))
            {
                scriptCreated = false;
                createdScriptPath = "";
                _ = GenerateCode();
            }
            GUI.enabled = true;
            
            GUILayout.Space(10);
            
            // Generated code display
            if (!string.IsNullOrEmpty(generatedCode))
            {
                GUILayout.Label("Generated Unity Script:", EditorStyles.boldLabel);
                scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition, GUILayout.Height(300));
                EditorGUILayout.TextArea(generatedCode, GUILayout.ExpandHeight(true));
                EditorGUILayout.EndScrollView();
                
                GUILayout.Space(5);
                
                // Show script creation status
                if (scriptCreated)
                {
                    EditorGUILayout.HelpBox($"✅ Script created successfully!\nPath: {createdScriptPath}", MessageType.Info);
                }
                
                // Action buttons
                GUILayout.BeginHorizontal();
                if (GUILayout.Button("Copy to Clipboard"))
                {
                    EditorGUIUtility.systemCopyBuffer = generatedCode;
                    Debug.Log("Code copied to clipboard!");
                }
                if (GUILayout.Button("Clear"))
                {
                    generatedCode = "";
                    scriptCreated = false;
                    createdScriptPath = "";
                }
                GUILayout.EndHorizontal();
            }
            
            // Status messages
            if (isGenerating)
            {
                EditorGUILayout.HelpBox("Generating Unity script... Please wait.", MessageType.Info);
            }
        }
        
        private async Task GenerateCode()
        {
            isGenerating = true;
            generatedCode = "";
            Repaint();
            
            try
            {
                using (var client = new HttpClient())
                {
                    var requestData = new
                    {
                        prompt = userRequest,
                        type = "unity_script"
                    };
                    
                    var json = JsonConvert.SerializeObject(requestData);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    
                    var response = await client.PostAsync($"{backendUrl}/api/generate-unity-script", content);
                    
                    if (response.IsSuccessStatusCode)
                    {
                        var responseContent = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<GenerationResponse>(responseContent);
                        generatedCode = result.code;
                        
                        // Automatically create the script file
                        if (!string.IsNullOrEmpty(generatedCode))
                        {
                            CreateScriptAsset();
                        }
                    }
                    else
                    {
                        generatedCode = $"Error: {response.StatusCode} - {response.ReasonPhrase}";
                    }
                }
            }
            catch (System.Exception e)
            {
                generatedCode = $"Error connecting to backend: {e.Message}";
                Debug.LogError($"Unity AI Generator Error: {e.Message}");
            }
            finally
            {
                isGenerating = false;
                Repaint();
            }
        }
        
        private void CreateScriptAsset()
        {
            if (string.IsNullOrEmpty(generatedCode))
                return;
                
            // Extract class name from generated code
            string className = ExtractClassName(generatedCode);
            if (string.IsNullOrEmpty(className))
            {
                EditorUtility.DisplayDialog("Error", "Could not extract class name from generated code.", "OK");
                return;
            }
            
            // Create script file
            string scriptPath = $"Assets/{className}.cs";
            System.IO.File.WriteAllText(scriptPath, generatedCode);
            
            // Refresh asset database
            AssetDatabase.Refresh();
            
            // Select the created script
            var scriptAsset = AssetDatabase.LoadAssetAtPath<MonoScript>(scriptPath);
            if (scriptAsset != null)
            {
                Selection.activeObject = scriptAsset;
                EditorGUIUtility.PingObject(scriptAsset);
            }
            
            // Set status variables
            scriptCreated = true;
            createdScriptPath = scriptPath;
            
            Debug.Log($"Unity script created: {scriptPath}");
        }
        
        private string ExtractClassName(string code)
        {
            // Simple regex to extract class name from C# code
            var match = System.Text.RegularExpressions.Regex.Match(code, @"public\s+class\s+(\w+)");
            return match.Success ? match.Groups[1].Value : "";
        }
        
        [System.Serializable]
        private class GenerationResponse
        {
            public string code;
            public string prompt;
            public string script_type;
            public string[] components;
        }
    }
}
