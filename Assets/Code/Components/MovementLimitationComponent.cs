using System;
using UnityEngine;

namespace Code.Components
{
	[Serializable]
	public struct MovementLimitationComponent
	{
		public Vector3 StartPosition;
		public Vector3 TargetPosition;
	}
}