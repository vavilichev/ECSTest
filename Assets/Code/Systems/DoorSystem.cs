using Code.Components;
using Leopotam.EcsLite;
using UnityEngine;

namespace Code.Systems
{
	public class DoorSystem : IEcsRunSystem
	{
		public void Run(EcsSystems systems)
		{
			var doorFilter = systems.GetWorld()
				.Filter<DoorComponent>()
				.Inc<MovementComponent>()
				.Inc<PositionComponent>()
				.Inc<DirectionComponent>()
				.Inc<MovementLimitationComponent>()
				.End();
			
			var doorPool = systems.GetWorld().GetPool<DoorComponent>();
			var buttonPool = systems.GetWorld().GetPool<ButtonComponent>();
			var movementPool = systems.GetWorld().GetPool<MovementComponent>();
			var movementLimitationPool = systems.GetWorld().GetPool<MovementLimitationComponent>();
			var positionPool = systems.GetWorld().GetPool<PositionComponent>();
			var directionPool = systems.GetWorld().GetPool<DirectionComponent>();
			
			foreach (var doorEntity in doorFilter)
			{
				ref var doorComponent = ref doorPool.Get(doorEntity);
				var buttonEntity = doorComponent.ButtonEntity;
				ref var buttonComponent = ref buttonPool.Get(buttonEntity);
				ref var movementComponent = ref movementPool.Get(doorEntity);
				ref var movementLimitationComponent = ref movementLimitationPool.Get(doorEntity);
				ref var positionComponent = ref positionPool.Get(doorEntity);
				ref var directionComponent = ref directionPool.Get(doorEntity);

				directionComponent.Direction = buttonComponent.IsPressed ? Vector3.down : Vector3.zero;
				
				var shouldMove = buttonComponent.IsPressed && positionComponent.Position.y > movementLimitationComponent.TargetPosition.y;
				var speed = shouldMove ? movementComponent.Speed : 0f;

				positionComponent.Position += directionComponent.Direction * speed;
			}
		}
	}
}