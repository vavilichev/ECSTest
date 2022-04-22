using Code.Components;
using Code.Mono.Extensions;
using Leopotam.EcsLite;
using UnityEngine;

namespace Code.Providers
{
	public class ButtonProvider : MonoBehaviour
	{
		[SerializeField] private Transform _buttonModelTransform;
		[SerializeField] private Vector3 _pressedButtonOffset = new Vector3(0f, -0.45f, 0f);
		[SerializeField] private Vector3 _topLocalPosition = new Vector3(0f, 0.3f, 0f);
		[SerializeField] private Vector3 _bottomLocalPosition = new Vector3(0f, -0.15f, 0f);
		
		public int Entity { get; private set; }
		
		private EcsWorld _world;
		private bool _isReady;
		
		public void Start()
		{
			if (!_isReady)
			{
				_world = WorldInstance.GetWorld();
				Entity = _world.NewEntity();

				AddButtonComponent(Entity);
				AddMovementLimitationComponent(Entity);
				AddPositionComponent(Entity);
				AddRadiusComponent(Entity);
				
				this.AddTransformComponent(_world, Entity, _buttonModelTransform);

				_isReady = true;
			}
		}

		private void AddButtonComponent(int entity)
		{
			var pool = _world.GetPool<ButtonComponent>();
			ref var buttonComponent = ref pool.Add(entity);
			
			buttonComponent.IsPressed = false;
		}

		private void AddMovementLimitationComponent(int entity)
		{
			var movementLimitationPool = _world.GetPool<MovementLimitationComponent>();
			ref var movementLimitationComponent = ref movementLimitationPool.Add(entity);

			movementLimitationComponent.StartPosition = _topLocalPosition;
			movementLimitationComponent.TargetPosition = _bottomLocalPosition;
		}

		private void AddPositionComponent(int entity)
		{
			var positionPool = _world.GetPool<PositionComponent>();
			ref var positionComponent = ref positionPool.Add(entity);

			positionComponent.Position = transform.position;
		}

		private void AddRadiusComponent(int entity)
		{
			var radiusPool = _world.GetPool<RadiusComponent>();
			ref var radiusComponent = ref radiusPool.Add(entity);

			radiusComponent.Raduis = _buttonModelTransform.localScale.x;
		}
	}
}