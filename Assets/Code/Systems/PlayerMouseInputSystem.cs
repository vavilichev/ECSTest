using Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Systems
{
	sealed class PlayerMouseInputSystem : IEcsRunSystem
	{
		private readonly EcsFilter<PlayerTagComponent, CharacterMovementComponent> _playerFilter = null;

		private float _posX;
		private float _posZ;
		private bool _wasGroundClicked;
		private LayerMask _groundLayer;

		public PlayerMouseInputSystem()
		{
			_groundLayer = 1 << LayerMask.NameToLayer("Ground");
		}

		public void Run()
		{
			ReadClick();
			
			if (_wasGroundClicked)
			{
				foreach (var index in _playerFilter)
				{
					ref var movableComponent = ref _playerFilter.Get2(index);

					// movableComponent.TargetPosition.x = _posX;
					// movableComponent.TargetPosition.z = _posZ;
				}
			}
		}

		private void ReadClick()
		{
			_wasGroundClicked = false;

			if (Input.GetMouseButtonDown(0))
			{
				var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
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