using UnityEngine;
using UnityEngine.Events;

namespace Shake
{
    /// <summary>
    /// Adds shake behavior to a GameObject in Unity. Can be configured to shake with or without decay.
    /// </summary>
    [ExecuteInEditMode]
    public class ShakeBehavior : MonoBehaviour
    {
        // Inconsistent access modifier
        // Inconsistent object creation
        // Serialized fields should start with a lowercase letter
        [SerializeField] UnityEvent OnShakeStart = new UnityEvent();

        [SerializeField] UnityEvent OnShakeEnd = new UnityEvent();
        // Remove, user experience nightmare
        [SerializeField] private float shakeDuration = 0.5f;

        /// <summary>
        /// Gets or sets the duration of the shake.
        /// </summary>
        public float ShakeDuration
        {
            get { return shakeDuration; }
            set { shakeDuration = value; }
        } 
        // Inline class fields, properties need to be after the fields, before methods
        // Inconsistent object creation, should not declare type
        [SerializeField]
        private Vector2 shakeIntensity = new Vector2(0.5f, 0.5f);

        /// <summary>
        /// Gets or sets the intensity of the shake on both axes.
        /// </summary>
        public Vector2 ShakeIntensity
        {
            get { return shakeIntensity; }
            set { shakeIntensity = value; }
        }

        [SerializeField]
        private bool enableDecay = true;

        /// <summary>
        /// Gets or sets a value indicating whether the shake intensity should decay over time.
        /// </summary>
        public bool EnableDecay
        {
            get { return enableDecay; }
            set { enableDecay = value; }
        }
        // Inconsistent naming, missing underscore
        private Vector3 originalPosition;
        private float currentShakeDuration;
        private Vector2 initialShakeIntensity;

        /// <summary>
        /// Initializes shake parameters when the component awakes.
        /// </summary>
        private void Awake()
        {
            // Move to start, use awake for object creation or retrieving references
            originalPosition = transform.localPosition;
        }

        /// <summary>
        /// Updates the shake behavior each frame, applying decay if enabled.
        /// </summary>
        private void Update()
        {
            // Make it coroutine based to fire in the different shake methods
            if (currentShakeDuration > 0)
            {
                // Make it a class field over initialising new object every frame
                Vector3 shakeAmount;

                if (enableDecay)
                {
                    // Type can be inferred, implicit over explicit typing
                    // Move to method to avoid duplicity
                    float decayRate = currentShakeDuration / shakeDuration;
                    shakeAmount = new Vector3(
                        initialShakeIntensity.x * Random.insideUnitSphere.x * decayRate,
                        initialShakeIntensity.y * Random.insideUnitSphere.y * decayRate,
                        0);
                }
                else
                {
                    // Move to method to avoid duplicity
                    shakeAmount = new Vector3(
                        initialShakeIntensity.x * Random.insideUnitSphere.x,
                        initialShakeIntensity.y * Random.insideUnitSphere.y,
                        0);
                }

                transform.localPosition = originalPosition + shakeAmount;
                // Currently shake duration is defined by amount of frames instead of time value, make thid clear
                currentShakeDuration -= Time.deltaTime;
                OnShakeStart.Invoke();
            }
            else
            {
                transform.localPosition = originalPosition;
                currentShakeDuration = 0;
                OnShakeEnd.Invoke();
            }
        }

        /// <summary>
        /// Starts a new shake with a specified duration and vector intensity.
        /// </summary>
        /// <param name="duration">Duration of the shake.</param>
        /// <param name="intensity">Intensity of the shake on both axes.</param>
        public void Shake(float duration, Vector2 intensity)
        {
            currentShakeDuration = duration;
            initialShakeIntensity = intensity;
            shakeIntensity = intensity; // This can be removed if not used elsewhere
            // Fire coroutine here
        }

        /// <summary>
        /// Starts a new shake with a specified duration and uniform intensity.
        /// </summary>
        /// <param name="duration">Duration of the shake.</param>
        /// <param name="intensity">Uniform intensity of the shake on both axes.</param>
        public void Shake(float duration, float intensity)
        {
            Shake(duration, new Vector2(intensity, intensity));
        }

        /// <summary>
        /// Starts a new shake with the specified duration using the default intensity.
        /// </summary>
        /// <param name="duration">Duration of the shake.</param>
        public void Shake(float duration)
        {
            Shake(duration, Vector2.one);
        }
    }
}