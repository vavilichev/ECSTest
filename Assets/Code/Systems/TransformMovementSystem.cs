using Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Systems
{
	public class TransformMovementSystem : IEcsRunSystem
	{
		private readonly EcsFilter<TransformMovementComponent> _filter = null;
		
		public void Run()
		{
			foreach (var entity in _filter)
			{
				ref var transformMovementComponent = ref _filter.Get1(entity);

				ref var transform = ref transformMovementComponent.Transform;
				ref var speed = ref transformMovementComponent.Speed;
				ref var direction = ref transformMovementComponent.DirectionNormalized;

				var newPosition = transform.position + direction * speed * Time.deltaTime;

				transform.position = newPosition;
			}
		}
	}
}