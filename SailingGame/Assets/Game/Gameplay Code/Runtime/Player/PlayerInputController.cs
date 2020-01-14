using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Wokarol.Input;

namespace Wokarol.Player
{
	public class PlayerInputController : MonoBehaviour
	{
		[SerializeField, Range(0, 0.5f)] private float directionDeadzone = 0.25f;

		private PlayerActions actions;

		private Vector2 direction;

		private void Awake()
		{
			actions = new PlayerActions();
			actions.Enable();

			actions.Main.Direction.performed += Direction_performed;
			actions.Main.DirectionByPointer.performed += DirectionByPointer_performed;
		}

		private void Direction_performed(InputAction.CallbackContext ctx)
		{
			Vector2 v = ctx.ReadValue<Vector2>();
			if(v.sqrMagnitude > directionDeadzone * directionDeadzone)
				direction = v.normalized;
		}

		private void DirectionByPointer_performed(InputAction.CallbackContext ctx)
		{
			// TODO: Implements mouse direction
		}

		private void OnDrawGizmos()
		{
			if (!Application.isPlaying)
				return;

			Gizmos.DrawRay(transform.position, direction * 2);
			Gizmos.DrawWireSphere((Vector2)transform.position + direction * 2, 0.1f);
		}
	} 
}
