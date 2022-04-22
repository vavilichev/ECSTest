using UnityEngine;

namespace Code.Components
{
	public struct CameraFollowComponent
	{
		public Vector3 Offset;
		public float Smoothness;
		public Vector3 Velocity;
		public Transform Target;
	}
}