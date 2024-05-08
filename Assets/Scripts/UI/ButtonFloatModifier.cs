using System;
using TypedUnityEvent;
using UnityEngine;

namespace UI
{
    // Docstring for the class
    public class ButtonFloatModifier : MonoBehaviour
    {
        [Header("Settings")] 
        [SerializeField] private float startValue;
        [SerializeField] private Vector2 range = new(-10f, 10f);
        [SerializeField] private float step = 0.1f;
        // Unclear naming, what is roundings? (decimal points)
        [SerializeField] private int roundings = 1;

        [Header("Events")] 
        [SerializeField] private FloatEvent onFloatChange = new();

        private float _value;

        /// <summary>
        /// The float value of this modifier
        /// </summary>
        public float Value
        {
            get => _value;

            private set
            {
                _value = Mathf.Clamp(MathF.Round(value, roundings), range.x, range.y);

                onFloatChange.Invoke(_value);
            }
        }

        private void Start()
        {
            Value = startValue;
        }

        /// <summary>
        /// Increase the value by the step
        /// </summary>
        public void Increase()
        {
            Value += step;
        }

        /// <summary>
        /// Decrease the value by the step
        /// </summary>
        public void Decrease()
        {
            Value -= step;
        }
    }
}