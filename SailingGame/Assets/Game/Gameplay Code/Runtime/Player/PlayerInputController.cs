using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Wokarol.Global;
using Wokarol.Input;

namespace Wokarol.Player
{
	public class PlayerInputController : MonoBehaviour, IMotorInput
	{
		[SerializeField, Range(0, 0.5f)] private float directionDeadzone = 0.25f;

		public Vector2 Direction { get; private set; }
		public float SailPower { get; private set; }

		private PlayerActions actions;

		private void Awake()
		{
			actions = new PlayerActions();
			actions.Enable();

			actions.Main.Direction.performed += Direction_performed;
			actions.Main.DirectionByPointer.performed += DirectionByPointer_performed;

			actions.Main.SailPower.performed += SailPower_performed;
		}

		private void Direction_performed(InputAction.CallbackContext ctx)
		{
			Vector2 v = ctx.ReadValue<Vector2>();
			if (v.sqrMagnitude > directionDeadzone * directionDeadzone)
				Direction = v.normalized;
		}

		private void DirectionByPointer_performed(InputAction.CallbackContext ctx)
		{
			Camera camera = Game.World.MainCamera;
			Vector2 v = ctx.ReadValue<Vector2>();

			Vector2 worldPos = camera.ScreenToWorldPoint(new Vector3(v.x, v.y, -camera.transform.position.z));
			Direction = ((Vector3)worldPos - transform.position).normalized;
		}

		private void SailPower_performed(InputAction.CallbackContext ctx)
		{
			SailPower = ctx.ReadValue<float>();
		}

		private void OnDrawGizmos()
		{
			if (!Application.isPlaying)
				return;

			Gizmos.DrawRay(transform.position, Direction * 2);
			Gizmos.DrawWireSphere((Vector2)transform.position + Direction * 2 * SailPower, 0.1f);
		}
	}
}
