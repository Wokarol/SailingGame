using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wokarol.Ship
{
	[RequireComponent(typeof(Rigidbody2D))]
	public class ShipMotor : MonoBehaviour
	{
		[SerializeField] private float MaxVelocity = 1f;

		public IMotorInput Input { get; set; }

		private Rigidbody2D body;

		private void Awake()
		{
			body = GetComponent<Rigidbody2D>();
		}

		private void Update()
		{
			Vector2 direction = Input.Direction;
			float velocity = Input.SailPower * MaxVelocity;


			body.rotation = Vector2.SignedAngle(Vector2.down, direction);
			body.velocity = direction * velocity;
		}
	}
}
