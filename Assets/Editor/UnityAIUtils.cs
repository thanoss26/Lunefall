using UnityEngine;
using UnityEditor;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace UnityAIGenerator
{
    public static class UnityAIUtils
    {
        /// <summary>
        /// Extract class name from C# code
        /// </summary>
        public static string ExtractClassName(string code)
        {
            var match = Regex.Match(code, @"public\s+class\s+(\w+)");
            return match.Success ? match.Groups[1].Value : "";
        }
        
        /// <summary>
        /// Extract namespace from C# code
        /// </summary>
        public static string ExtractNamespace(string code)
        {
            var match = Regex.Match(code, @"namespace\s+(\w+(?:\.\w+)*)");
            return match.Success ? match.Groups[1].Value : "";
        }
        
        /// <summary>
        /// Format C# code for better readability
        /// </summary>
        public static string FormatCSharpCode(string code)
        {
            // Basic formatting - in a real implementation, you might want to use a proper C# formatter
            var lines = code.Split('\n');
            var formattedLines = new System.Collections.Generic.List<string>();
            int indentLevel = 0;
            
            foreach (var line in lines)
            {
                var trimmedLine = line.Trim();
                
                // Decrease indent for closing braces
                if (trimmedLine.StartsWith("}"))
                {
                    indentLevel = Mathf.Max(0, indentLevel - 1);
                }
                
                // Add indentation
                var indentedLine = new string(' ', indentLevel * 4) + trimmedLine;
                formattedLines.Add(indentedLine);
                
                // Increase indent for opening braces
                if (trimmedLine.EndsWith("{"))
                {
                    indentLevel++;
                }
            }
            
            return string.Join("\n", formattedLines);
        }
        
        /// <summary>
        /// Create folder structure for generated scripts
        /// </summary>
        public static void CreateFolderStructure(string basePath)
        {
            if (!Directory.Exists(basePath))
            {
                Directory.CreateDirectory(basePath);
            }
            
            // Create common subfolders
            string[] subfolders = {
                "Player",
                "Camera", 
                "Audio",
                "UI",
                "Managers",
                "Utilities"
            };
            
            foreach (var folder in subfolders)
            {
                string folderPath = Path.Combine(basePath, folder);
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }
            }
        }
        
        /// <summary>
        /// Get appropriate folder for script type
        /// </summary>
        public static string GetScriptFolder(string scriptType, string basePath)
        {
            string folder = scriptType switch
            {
                "movement" => "Player",
                "jumping" => "Player",
                "camera_control" => "Camera",
                "audio" => "Audio",
                "ui_controller" => "UI",
                "game_manager" => "Managers",
                "input_handler" => "Player",
                "collision" => "Player",
                _ => "Utilities"
            };
            
            return Path.Combine(basePath, folder);
        }
        
        /// <summary>
        /// Validate C# code syntax
        /// </summary>
        public static bool ValidateCSharpCode(string code)
        {
            // Basic validation - check for common syntax issues
            if (string.IsNullOrEmpty(code))
                return false;
                
            // Check for basic C# structure
            if (!code.Contains("using UnityEngine;") && !code.Contains("using System;"))
                return false;
                
            if (!code.Contains("public class"))
                return false;
                
            if (!code.Contains("MonoBehaviour"))
                return false;
                
            // Check for balanced braces
            int openBraces = code.Count(c => c == '{');
            int closeBraces = code.Count(c => c == '}');
            
            return openBraces == closeBraces;
        }
        
        /// <summary>
        /// Add namespace to C# code if not present
        /// </summary>
        public static string AddNamespace(string code, string namespaceName)
        {
            if (string.IsNullOrEmpty(namespaceName))
                return code;
                
            if (code.Contains("namespace"))
                return code;
                
            var lines = code.Split('\n');
            var result = new System.Collections.Generic.List<string>();
            
            // Add using statements
            result.Add("using UnityEngine;");
            if (!code.Contains("using System;"))
                result.Add("using System;");
            result.Add("");
            
            // Add namespace
            result.Add($"namespace {namespaceName}");
            result.Add("{");
            
            // Add code with indentation
            foreach (var line in lines)
            {
                if (!string.IsNullOrEmpty(line.Trim()))
                {
                    result.Add("    " + line);
                }
            }
            
            result.Add("}");
            
            return string.Join("\n", result);
        }
        
        /// <summary>
        /// Show success message with created file path
        /// </summary>
        public static void ShowSuccessMessage(string filePath, string className)
        {
            EditorUtility.DisplayDialog("Script Created Successfully!", 
                $"Unity script '{className}' has been created at:\n{filePath}\n\nYou can now attach it to a GameObject in your scene.", 
                "OK");
        }
        
        /// <summary>
        /// Show error message
        /// </summary>
        public static void ShowErrorMessage(string title, string message)
        {
            EditorUtility.DisplayDialog(title, message, "OK");
        }
    }
}
