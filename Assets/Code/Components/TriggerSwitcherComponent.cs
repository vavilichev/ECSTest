using System;
using Mono;
using UnityEngine;

namespace Components
{
	[Serializable]
	public struct TriggerSwitcherComponent
	{
		public GameObject GameObject;
		public TriggerSwitcherChecker Checker;
		public bool IsOn;
	}
}