using Code.Components;
using Leopotam.EcsLite;
using UnityEngine;

namespace Code.Systems
{
	public class UnityDoorSystem : IEcsRunSystem
	{
		public void Run(EcsSystems systems)
		{
			var doorFilter = systems.GetWorld()
				.Filter<DoorComponent>()
				.Inc<PositionComponent>()
				.Inc<TransformComponent>()
				.End();
			
			var positionPool = systems.GetWorld().GetPool<PositionComponent>();
			var transformPool = systems.GetWorld().GetPool<TransformComponent>();
			
			foreach (var doorEntity in doorFilter)
			{
				ref var positionComponent = ref positionPool.Get(doorEntity);
				ref var transformComponent = ref transformPool.Get(doorEntity);

				transformComponent.Transform.localPosition = Vector3.Lerp(positionComponent.Position, transformComponent.Transform.position, Time.deltaTime);
			}
		}
	}
}