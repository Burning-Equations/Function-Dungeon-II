using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace CannonPlatform
{
    /// <summary>
    /// Controls the vertical movement of the cannon platform.
    /// </summary>
    public class CannonPlatformController : MonoBehaviour
    {
        [SerializeField] private float maxHeight = 10f; // Maximum height the platform can reach.
        [SerializeField] private float minHeight = 0f; // Minimum height the platform can descend to.
        [SerializeField] private float movementSmoothing = 5f; // Smoothing factor for the platform movement.

        [SerializeField] private UnityEvent onMovingUp = new (); // Event invoked when the platform is moving up.
        [SerializeField] private UnityEvent onMovingDown = new (); // Event invoked when the platform is moving down.

        private Vector3 _wantedPosition;

        private void Start()
        {
            _wantedPosition = transform.position;
        }

        private void FixedUpdate()
        {
            // Smoothly move the platform towards the wanted position.
            if (Vector3.Distance(transform.position, _wantedPosition) > 0.01f)
            {
                transform.position = Vector3.Lerp(transform.position, _wantedPosition, Time.deltaTime * movementSmoothing);
            }
            else
            {
                transform.position = _wantedPosition;
            }

        }

        /// <summary>
        /// Move the platform vertically based on the input.
        /// </summary>
        /// <param name="input">Vertical input for moving the platform, typically from -1 to 1.</param>
        public void Move(float input)
        {
            var translationY = input;
            var newPosition = _wantedPosition; //+ new Vector3(0, translationY, 0);

            // Clamping the position to ensure the platform stays within defined bounds.
            newPosition.y = Mathf.Clamp(input, minHeight, maxHeight);

            _wantedPosition = newPosition;
        }

        /// <summary>
        /// Moves the platform upward by a specified amount.
        /// </summary>
        /// <param name="amount">The amount to move up, should be a positive number.</param>
        public void MoveUp(float amount)
        {
            Move(amount);
            onMovingUp.Invoke();
        }

        /// <summary>
        /// Moves the platform downward by a specified amount.
        /// </summary>
        /// <param name="amount">The amount to move down, should be a positive number.</param>
        public void MoveDown(float amount)
        {
            Move(-amount);
            onMovingDown.Invoke();
        }
    }
}