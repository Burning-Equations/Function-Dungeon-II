// Unused using directives
using System;
using UnityEngine;
// Correct the namespace
namespace LineController
{
    // Use require component, change application to actual view object, instead of the controller
    [ExecuteInEditMode]
    public class FunctionLineController : MonoBehaviour
    {
        [Header("Function settings")]
        // Bools are already false by default
        public bool isQuadratic = false;

        [Header("Function coefficients")]
        [Tooltip("This variable defines the slope of the function.")]
        [SerializeField] private float a = 1f;
        [Tooltip("This variable defines the y-intercept of the function.")]
        // Floats are already 0 by default
        [SerializeField] private float b = 0f;
        [Tooltip("This variable defines the quadratic power of the function.")]
        [Min(0.001f)]
        [SerializeField] private float c = 1f;

        [Header("Visual settings")]
        [SerializeField] private float lineLength = 10f;
        // Missing space
        // Properties must be after class fields and before methods
        public float LineLength
        {
            get { return lineLength; }
            private set
            {
                lineLength = Mathf.Max(0, value);  // Ensure lineLength never goes negative.
                DrawFunction();  // Update the line renderer whenever the value changes.
            }
        }

        [SerializeField] private int segments = 10;
        [SerializeField] private LineRenderer lineRenderer;
        // Double spaces
        
        
        private void Start()
        {
            DrawFunction();
        }

        private void Update()
        {
            DrawFunction();
        }
        /// <summary>
        /// Evaluates the function at a given 'x' value.
        /// </summary>
        /// <param name="x">The 'x' value at which to evaluate the function.</param>
        /// <returns>The 'y' value of the function at 'x'.</returns>
        public float EvaluateFunction(float x)
        {
            return a * Mathf.Pow(x, c) + b;
        }
        /// <summary>
        /// Computes the velocity at a point along the function.
        /// </summary>
        /// <param name="startPos">The starting position on the function.</param>
        /// <param name="followDistance">The distance along the function to calculate the velocity.</param>
        /// <returns>A normalized vector representing the velocity.</returns>
        public Vector3 GetVelocityAtPoint(Vector3 startPos, float followDistance)
        {
            var startX = startPos.x;
            var endX = startX + followDistance;
            var startY = EvaluateFunction(startX);
            var endY = EvaluateFunction(endX);

            return new Vector3(endX - startX, endY - startY, 0).normalized;
        }
        /// <summary>
        /// Draws the function line based on the current coefficients and settings.
        /// </summary>
        // Unclear naming, is not drawing the line...
        private void DrawFunction()
        {
            if (!isQuadratic)
            {
                c = 1; // Forcing 'c' to be 1 if not quadratic
            }
            // Remove this when using the require component
            if (lineRenderer == null)
            {
                Debug.LogError("LineRenderer is missing.");
                return;
            }
            // Why are we doing this?
            lineRenderer.positionCount = segments + 1;
            // Remove this, not needed, debugging stuff
            lineRenderer.startWidth = 0.1f;
            lineRenderer.endWidth = 0.1f;

            var step = lineLength / segments;
            for (var i = 0; i <= segments; i++)
            {
                // Add comments to explain logarithm
                var x = i * step;
                var y = EvaluateFunction(x);
                lineRenderer.SetPosition(i, new Vector3(x, y, 0));
            }
        }

        private void OnValidate()
        {
            DrawFunction();
        }
    }
}
