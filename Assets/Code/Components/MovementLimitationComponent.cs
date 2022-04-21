using System;
using UnityEngine;

namespace Components
{
	[Serializable]
	public struct MovementLimitationComponent
	{
		public Vector3 StartPosition;
		public Vector3 TargetPosition;
	}
}