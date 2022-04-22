using Code.Components;
using Leopotam.EcsLite;
using UnityEngine;

namespace Code.Systems
{
	public class CameraFollowSystem : IEcsRunSystem
	{
		public void Run(EcsSystems systems)
		{
			var cameraFollowFilter = systems.GetWorld().Filter<CameraFollowComponent>().End();
			var cameraFollowPool = systems.GetWorld().GetPool<CameraFollowComponent>();
			var posisionPool = systems.GetWorld().GetPool<PositionComponent>();
			
			foreach (var cameraEntity in cameraFollowFilter)
			{
				ref var cameraFollowComponent = ref cameraFollowPool.Get(cameraEntity);
				ref var positionComponent = ref posisionPool.Get(cameraEntity);
				
				var currentPosition = positionComponent.Position;
				var targetPosition = cameraFollowComponent.Target.position + cameraFollowComponent.Offset;

				positionComponent.Position = Vector3.SmoothDamp(currentPosition, targetPosition, ref cameraFollowComponent.Velocity, cameraFollowComponent.Smoothness);
			}
		}
	}
}