using UnityEngine;
using UnityEditor;
// Missing space
// Incorrect namespace
namespace Shake
{
    [CustomEditor(typeof(ShakeBehavior))]
    public class ShakeBehaviorEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            var shakeScript = (ShakeBehavior)target;

            if (GUILayout.Button("Test Shake"))
            {
                // Trigger the shake using the current settings in the inspector
                shakeScript.Shake(shakeScript.ShakeDuration, shakeScript.ShakeIntensity);
            }
        }
    }
}