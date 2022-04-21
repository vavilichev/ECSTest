using System;
using UnityEngine;

namespace Components
{
    [Serializable]
    public struct CharacterMovementComponent
    {
        public CharacterController CharacterController;
        public float SpeedInitial;
        
        [HideInInspector] public float Speed;
        [HideInInspector] public Vector3 DirectionNormalized;
    }
}