using UnityEngine;

namespace Code.Components
{
    public struct CharacterMovementComponent
    {
        public CharacterController CharacterController;
        public float SpeedInitial;
	    public float Speed;
        public Vector3 DirectionNormalized;
    }
}