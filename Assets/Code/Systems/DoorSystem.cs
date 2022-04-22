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
				.Inc<TransformMovementComponent>()
				.Inc<MovementLimitationComponent>()
				.End();
			var doorPool = systems.GetWorld().GetPool<DoorComponent>();
			var buttonPool = systems.GetWorld().GetPool<ButtonComponent>();
			var movementPool = systems.GetWorld().GetPool<TransformMovementComponent>();
			var movementLimitationPool = systems.GetWorld().GetPool<MovementLimitationComponent>();

			foreach (var doorEntity in doorFilter)
			{
				ref var doorComponent = ref doorPool.Get(doorEntity);
				var buttonEntity = doorComponent.ButtonEntity;
				ref var buttonComponent = ref buttonPool.Get(buttonEntity);
				ref var movementComponent = ref movementPool.Get(doorEntity);
				ref var movementLimitationComponent = ref movementLimitationPool.Get(doorEntity);

				var doorTransform = movementComponent.Transform;

				if (buttonComponent.IsPressed)
				{
					if (doorTransform.position.y > movementLimitationComponent.TargetPosition.y)
					{
						movementComponent.Speed = movementComponent.SpeedInitial;
						movementComponent.DirectionNormalized = Vector3.down;
					}
					else
					{
						movementComponent.Speed = 0;
					}
				}
				else
				{
					movementComponent.Speed = 0;
				}
			}
		}
	}
}