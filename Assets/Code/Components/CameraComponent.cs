using UnityEngine;

namespace Code.Components
{
	public struct CameraComponent
	{
		public Camera Camera;
		public Transform Transform;
		public Vector3 Offset;
		public float Smoothness;
		public Vector3 Velocity;
		public bool IsMain;
	}
}