using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(MoveTrain))]
public class MoveTrainEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        MoveTrain myTarget = (MoveTrain)target;

        if (GUILayout.Button("Take Off"))
        {
            myTarget.TakeOff();
        }
    }
}
