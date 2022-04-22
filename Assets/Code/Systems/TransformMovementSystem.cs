using Code.Components;
using Leopotam.EcsLite;
using UnityEngine;

namespace Code.Systems
{
	public class TransformMovementSystem : IEcsRunSystem
	{
		public void Run(EcsSystems systems)
		{
			var movementFilter = systems.GetWorld().Filter<TransformMovementComponent>().End();
			var movementPool = systems.GetWorld().GetPool<TransformMovementComponent>();
			
			foreach (var entity in movementFilter)
			{
				ref var transformMovementComponent = ref movementPool.Get(entity);

				ref var transform = ref transformMovementComponent.Transform;
				ref var speed = ref transformMovementComponent.Speed;
				ref var direction = ref transformMovementComponent.DirectionNormalized;

				var newPosition = transform.position + direction * speed * Time.deltaTime;

				transform.position = newPosition;
			}
		}
	}
}