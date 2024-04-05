using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(AISensor))]
public class SensorView : Editor
{
    void OnSceneGUI()
    {
        AISensor sensor = (AISensor)target;
        Handles.color = Color.white;
        Handles.DrawWireArc(sensor.transform.position, Vector3.forward,
            Vector2.up, 360, sensor.Distance);
        Vector3 viewAngleA = sensor.DirFromAngle(-sensor.Angle / 2, false);
        Vector3 viewAngleB = sensor.DirFromAngle(sensor.Angle / 2, false);

        Handles.DrawLine(sensor.transform.position, sensor.transform.position
            + viewAngleA * sensor.Distance);
        Handles.DrawLine(sensor.transform.position, sensor.transform.position
            + viewAngleB * sensor.Distance);

        Handles.color = Color.red;
        foreach (var visibleTarget in sensor.Targets)
        {
            Handles.DrawLine(sensor.transform.position, visibleTarget.position);
        }
    }
}