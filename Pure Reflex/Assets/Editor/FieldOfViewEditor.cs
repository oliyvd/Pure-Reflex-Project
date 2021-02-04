using System.Collections;
using UnityEngine;
using UnityEditor;


/// <summary>
/// <Author> Sebastian Lague </Author>
/// <URL> https://www.youtube.com/watch?v=rQG9aUWarwE&t </URL>
/// </summary>
[CustomEditor (typeof (FieldOfView))]
public class FieldOfViewEditor : Editor
{
    void OnSceneGUI() {
        FieldOfView fow = (FieldOfView)target;
        Handles.DrawWireArc(fow.transform.position, Vector3.up, Vector3.forward, 360, fow.viewRadius);
        Vector3 viewAngleA = fow.DirFromAngle(-fow.viewAngle / 2, false);
        Vector3 viewAngleB = fow.DirFromAngle(fow.viewAngle / 2, false);

        Handles.DrawLine(fow.transform.position, fow.transform.position + viewAngleA * fow.viewRadius);
        Handles.DrawLine(fow.transform.position, fow.transform.position + viewAngleB * fow.viewRadius);

        Handles.color = Color.red;
        foreach (Transform visibleTarget in fow.visibleTargets)
        {
            Handles.DrawLine(fow.transform.position, visibleTarget.position);

        }
    }
}
