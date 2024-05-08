// Remove unused namespaces
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

// Missing namespaces
[CustomEditor(typeof(CannonProjectileController))]
public class ShakeBehaviorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        // Explicit typing when implicitly typed variable can be used
        CannonProjectileController cannonScript = (CannonProjectileController)target;

        if (GUILayout.Button("Test Shot"))
        {
            // Trigger the shot using the current settings in the inspector
            cannonScript.ShootProjectile();
        }
    }
}
