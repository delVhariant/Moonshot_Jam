using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

[CustomEditor(typeof(GameState))]
public class WinStateEditor : Editor 
{
    Rect menuRect;
    public override void OnInspectorGUI()
    {
        GameState myTarget = (GameState)target;
        base.OnInspectorGUI();
        bool selected = EditorGUILayout.DropdownButton(new GUIContent("Set Next Level"),FocusType.Passive);
        if (Event.current.type == EventType.Repaint) {
            menuRect = GUILayoutUtility.GetLastRect();
        }
        if(selected)
        {
            GenericMenu menu = new GenericMenu();
            EditorBuildSettingsScene[] scenes = EditorBuildSettings.scenes;
            void handleItemClicked(object parameter)
            {
                Debug.Log(parameter);
                myTarget.SetNextLevel(parameter.ToString());
                EditorUtility.SetDirty(myTarget);
            }

            for(int i=0; i < scenes.Length; i++)
            {
                // string name=SceneUtility.GetBuildIndexByScenePath(scenes[i].path).ToString();
                var arr =scenes[i].path.Split('/');
                string name=arr[arr.Length-1].Split('.')[0];
                //Debug.Log($"Found: {name} at: {scenes[i].path}");
                menu.AddItem(new GUIContent(name), false, handleItemClicked, name);
            }
            menu.DropDown(menuRect);
        }
    }


    // public static void DrawDropdown(Rect position, GUIContent label)
    // {
    //     if (!EditorGUI.DropdownButton(position, label, FocusType.Passive))
    //     {
    //         return;
    //     }
    
    //     void handleItemClicked(object parameter)
    //     {
    //         Debug.Log(parameter);
    //     }
    
    //     GenericMenu menu = new GenericMenu();
    //     EditorBuildSettingsScene[] scenes = EditorBuildSettings.scenes;
    //     for(int i=0; i < scenes.Length; i++)
    //     {
    //         string name=SceneManager.GetSceneByPath(scenes[i].path).name;
    //         menu.AddItem(new GUIContent(name), false, handleItemClicked, name);
    //     }
    //     menu.DropDown(position);
    // }
}
