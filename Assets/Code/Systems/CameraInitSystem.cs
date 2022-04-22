using Leopotam.EcsLite;
using UnityEngine;

namespace Code.Systems
{
	public class CameraInitSystem : IEcsInitSystem
	{
		public void Init(EcsSystems systems)
		{
			var cameraEntity = systems.GetWorld().NewEntity();
			
			AddCameraComponent(systems, cameraEntity);
		}

		private void AddCameraComponent(EcsSystems systems, int cameraEntity)
		{
			var cameraPool = systems.GetWorld().GetPool<Components.CameraComponent>();
			ref var cameraComponent = ref cameraPool.Add(cameraEntity);
			
			cameraComponent.Camera = Camera.main;
			cameraComponent.Transform = cameraComponent.Camera.transform;
			cameraComponent.Offset = new Vector3(22.5f, 38.4f, -25.47f);

			cameraComponent.IsMain = true;
		}
	}
}