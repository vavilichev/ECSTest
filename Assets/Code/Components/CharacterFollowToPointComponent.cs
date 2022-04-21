using System;
using UnityEngine;

namespace Components
{
	[Serializable]
	public struct CharacterFollowToPointComponent
	{
		public Vector3 Offset;
		public float StopDistance;

		[HideInInspector]public Vector3 TargetPosition;
	}
}