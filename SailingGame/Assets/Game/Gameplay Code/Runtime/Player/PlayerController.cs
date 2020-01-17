using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Wokarol.Ship;

namespace Wokarol.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private PlayerInputController input = null;
        [SerializeField] private ShipMotor motor = null;

        private void OnValidate()
        {
            if (motor == null) motor = GetComponent<ShipMotor>();
            if (input == null) input = GetComponent<PlayerInputController>();

            if (Application.isPlaying)
                Awake();
        }

        private void Awake()
        {
            motor.Input = input;
        }
    }
}
