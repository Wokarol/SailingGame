using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wokarol.Ship
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class ShipMotor : MonoBehaviour
    {
        [SerializeField, Tooltip("m/s")] private float MaxVelocity = 1f;
        [Space]
        [SerializeField, Tooltip("m/s^2")] private float AccelerationSpeed = 0.5f;
        [SerializeField, Tooltip("m/s^2")] private float DecelerationSpeed = 0.5f;
        [Space]
        [SerializeField, Tooltip("degrees/s")] private float RotationSpeed = 90;
        [Tooltip("x{0, max velocity}  y{0, rotation speed}")]
        [SerializeField] private AnimationCurve velocityToRotationSpeed = AnimationCurve.Linear(0, 0.25f, 1, 1);

        public IMotorInput Input { get; set; }

        private Rigidbody2D body;

        private float velocity = 0;
        private Quaternion rotation = Quaternion.identity;

        private void Awake()
        {
            body = GetComponent<Rigidbody2D>();
            if (!body) Debug.LogError($"No Rigidbody2D found on {nameof(ShipMotor)}!", this);
        }

        private void FixedUpdate()
        {

            // Calculates new target velocity
            float targetVelocity = Input.SailPower * MaxVelocity;
            float velocityChangeSpeed = targetVelocity > velocity ? AccelerationSpeed : DecelerationSpeed;

            // Calculates new velocity
            velocity = Mathf.MoveTowards(velocity, targetVelocity, velocityChangeSpeed * Time.deltaTime);

            // Calculates new direction
            Quaternion targetRotation = Quaternion.FromToRotation(Vector2.up, Input.Direction);
            float currentRotationSpeed = RotationSpeed * velocityToRotationSpeed.Evaluate(velocity / MaxVelocity);
            rotation = Quaternion.RotateTowards(rotation, targetRotation, currentRotationSpeed * Time.deltaTime);

            // Uses calculated data to move ship via rigidbody
            if (body)
            {
                transform.rotation = rotation;
                body.velocity = transform.up * velocity;
            }
            else
            {
                Debug.LogError($"Rigidbody is null on {nameof(ShipMotor)}", this);
            }
        }
    }
}
