using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LoginManagers))]
public class LoginManagerEditorScript : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        EditorGUILayout.HelpBox("This script is responsbile for connecting to Photon Server.", MessageType.Info);

        LoginManagers login = (LoginManagers)target;
        if(GUILayout.Button("Connect Anonymously"))
        {
            login.ConnectAnonymously();
        }
    }
}
