using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Wokarol.Input;

namespace Wokarol.Player
{
	public class PlayerInputController : MonoBehaviour
	{
		private PlayerActions actions;

		private void Awake()
		{
			actions = new PlayerActions();

			actions.Enable();
		}
	} 
}
