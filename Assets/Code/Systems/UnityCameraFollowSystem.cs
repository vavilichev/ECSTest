using Code.Components;
using Leopotam.EcsLite;
using UnityEngine;

namespace Code.Systems
{
	public class UnityCameraFollowSystem : IEcsRunSystem
	{
		public void Run(EcsSystems systems)
		{
			var cameraFollowFilter = systems.GetWorld().Filter<CameraFollowComponent>().End();
			var transfiormPool = systems.GetWorld().GetPool<TransformComponent>();
			var posisionPool = systems.GetWorld().GetPool<PositionComponent>();
			
			foreach (var cameraEntity in cameraFollowFilter)
			{
				ref var positionComponent = ref posisionPool.Get(cameraEntity);
				ref var transformComponent = ref transfiormPool.Get(cameraEntity);

				transformComponent.Transform.position = Vector3.Lerp(positionComponent.Position, transformComponent.Transform.position, Time.deltaTime);
			}
		}
	}
}