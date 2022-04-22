using Code.Components;
using Leopotam.EcsLite;
using UnityEngine;

namespace Code.Systems
{
	public class CameraFollowSystem : IEcsRunSystem
	{
		public void Run(EcsSystems systems)
		{
			var cameraFilter = systems.GetWorld().Filter<CameraComponent>().End();
			var cameraPool = systems.GetWorld().GetPool<CameraComponent>();
			
			var playerFilter = systems.GetWorld().Filter<PlayerComponent>().End();
			var playerPool = systems.GetWorld().GetPool<PlayerComponent>();

			foreach (var cameraEntity in cameraFilter)
			{
				ref var cameraComponent = ref cameraPool.Get(cameraEntity);

				if (cameraComponent.IsMain)
				{
					foreach(var entity in playerFilter)
					{
						ref var playerComponent = ref playerPool.Get(entity);

						var currentPosition = cameraComponent.Transform.position;
						var targetPoint = playerComponent.Transform.position + cameraComponent.Offset;

						cameraComponent.Transform.position = Vector3.SmoothDamp(currentPosition, targetPoint, ref cameraComponent.Velocity, cameraComponent.Smoothness);
					}    
				}
			}
		}
	}
}