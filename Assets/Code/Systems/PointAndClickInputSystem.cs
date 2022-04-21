using Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Systems
{
	public class PointAndClickInputSystem : IEcsRunSystem, IEcsInitSystem
	{
		private readonly EcsFilter<PointAndClickInputComponent, CharacterFollowToPointComponent> _filter = null;

		private bool _wasGroundClicked;
		private float _posX;
		private float _posZ;
		private LayerMask _groundLayer;
		
		public void Init()
		{
			_groundLayer = 1 << LayerMask.NameToLayer("Ground");
		}

		public void Run()
		{
			foreach (var index in _filter)
			{
				ref var inputComponent = ref _filter.Get1(index);
				ref var followComponent = ref _filter.Get2(index);
				
				ReadClick(inputComponent.Camera);

				if (_wasGroundClicked)
				{
					var newTargetPosition = new Vector3(_posX, 0f, _posZ);

					followComponent.TargetPosition = newTargetPosition;
				}
			}
		}
		
		private void ReadClick(Camera camera)
		{
			_wasGroundClicked = false;

			if (Input.GetMouseButtonDown(0))
			{
				var ray = camera.ScreenPointToRay(Input.mousePosition);
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