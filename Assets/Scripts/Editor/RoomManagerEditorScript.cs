using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(RoomManager))]
public class RoomManagerEditorScript : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        EditorGUILayout.HelpBox("This script is responsbile for creating and joining rooms", MessageType.Info);

        RoomManager roomManager = (RoomManager)target;

       
        if (GUILayout.Button("Join VR Lab"))
        {
            roomManager.OnEnterButtonClicked_VRLAB();
        }

    }
}
