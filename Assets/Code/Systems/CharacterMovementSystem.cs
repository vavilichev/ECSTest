using Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Systems
{
	sealed class CharacterMovementSystem : IEcsRunSystem
	{
		private readonly EcsFilter<CharacterMovementComponent> _movableFilter = null;

		public void Run()
		{
			foreach (var entity in _movableFilter)
			{
				ref var movementComponent = ref _movableFilter.Get1(entity);
				
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