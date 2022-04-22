using Code.Components;
using Leopotam.EcsLite;
using UnityEngine;

namespace Code.Systems
{
	sealed class CharacterMovementSystem : IEcsRunSystem
	{
		public void Run(EcsSystems systems)
		{
			var filter = systems.GetWorld().Filter<CharacterMovementComponent>().End();
			var pool = systems.GetWorld().GetPool<CharacterMovementComponent>();
			
			foreach (var index in filter)
			{
				ref var movementComponent = ref pool.Get(index);
				
				ref var direction = ref movementComponent.DirectionNormalized;
				ref var characterController = ref movementComponent.CharacterController;
				ref var speed = ref movementComponent.Speed;

				var transform = movementComponent.CharacterController.transform;
				var rawDirection =  transform.right * direction.x + transform.forward * direction.z;

				characterController.Move(rawDirection * speed * Time.deltaTime);
			}
		}
	}
}