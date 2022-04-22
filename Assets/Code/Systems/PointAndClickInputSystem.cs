using Code.Components;
using Leopotam.EcsLite;
using UnityEngine;

namespace Code.Systems
{
	sealed class PointAndClickInputSystem : IEcsRunSystem, IEcsInitSystem
	{
		private float _posX;
		private float _posZ;
		private bool _wasGroundClicked;
		private LayerMask _groundLayer;

		public void Init(EcsSystems systems)
		{
			_groundLayer = 1 << LayerMask.NameToLayer("Ground");
		}
		
		public void Run(EcsSystems systems)
		{
			ReadClick(systems);

			if (_wasGroundClicked)
			{
				var filter = systems.GetWorld().Filter<PointAndClickInputComponent>().End();
				var playerInputPool = systems.GetWorld().GetPool<PointAndClickInputComponent>();
				
				foreach (var entity in filter)
				{
					ref var playerInputComponent = ref playerInputPool.Get(entity);

					playerInputComponent.TargetPosition.x = _posX;
					playerInputComponent.TargetPosition.z = _posZ;
				}
			}
		}

		private void ReadClick(EcsSystems systems)
		{
			_wasGroundClicked = false;

			if (Input.GetMouseButtonDown(0))
			{
				var filter = systems.GetWorld().Filter<CameraComponent>().End();
				var camerasPool = systems.GetWorld().GetPool<CameraComponent>();

				foreach (var entity in filter)
				{
					ref var cameraComponent = ref camerasPool.Get(entity);
					var ray = cameraComponent.Camera.ScreenPointToRay(Input.mousePosition);
					RaycastHit hit;

					if(Physics.Raycast(ray, out hit, Mathf.Infinity, _groundLayer))
					{
						_wasGroundClicked = true;

						_posX = hit.point.x;
						_posZ = hit.point.z;
					}
				}
			}
		}
	}
}