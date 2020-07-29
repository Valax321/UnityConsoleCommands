using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Valax321.ConsoleCommands.Editor
{
    internal class EditorConsole : EditorWindow
    {
        [MenuItem("Window/Valax321/Command Console")]
        static void Open()
        {
            var wnd = GetWindow<EditorConsole>();
            
            wnd.titleContent = new GUIContent("Command Console");

            var uiAsset =
                AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(
                    "Packages/com.valax321.consolecommands/Editor/Windows/CommandConsole.uxml");

            var ui = uiAsset.CloneTree();

            wnd.rootVisualElement.Add(ui);
        }
    }
}