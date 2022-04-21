using System;
using UnityEngine;

namespace Components
{
	[Serializable]
	public struct FollowToTransformComponent
	{
		public Transform TargetTransform;
		public Vector3 Offset;
		public float StopDistance;
	}
}