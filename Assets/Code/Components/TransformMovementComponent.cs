using System;
using UnityEngine;

namespace Components
{
	[Serializable]
	public struct TransformMovementComponent
	{
		public Transform Transform;
		public float SpeedInitial;

		[HideInInspector] public float Speed;
		[HideInInspector] public Vector3 DirectionNormalized;
	}
}