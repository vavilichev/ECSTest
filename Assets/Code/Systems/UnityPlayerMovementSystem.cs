using Code.Components;
using Leopotam.EcsLite;
using UnityEngine;

namespace Code.Systems
{
	public class UnityPlayerMovementSystem : IEcsRunSystem
	{
		public void Run(EcsSystems systems)
		{
			var playerFilter = systems.GetWorld()
				.Filter<PlayerComponent>()
				.End();
			var transformPool = systems.GetWorld().GetPool<TransformComponent>();
			var positionPool = systems.GetWorld().GetPool<PositionComponent>();

			foreach (var playerEntity in playerFilter)
			{
				ref var positionComponent = ref positionPool.Get(playerEntity);
				ref var transformComponent = ref transformPool.Get(playerEntity);

				transformComponent.Transform.position = Vector3.Lerp(positionComponent.Position, transformComponent.Transform.position, Time.deltaTime);
			}
		}
	}
}