using UnityEngine;

namespace Code.Components
{
	public struct TransformMovementComponent
	{
		public Transform Transform;
		public float SpeedInitial;
		public float Speed;
		public Vector3 DirectionNormalized;
	}
}