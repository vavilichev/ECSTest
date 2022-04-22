using Code.Components;
using Code.Mono.Extensions;
using Leopotam.EcsLite;
using UnityEngine;

namespace Code.Providers
{
	public class DoorProvider : MonoBehaviour
	{
		[SerializeField] private Transform _doorModelTransform;
		[SerializeField] private ButtonProvider _buttonProvider;
		[SerializeField] private float _speed = 2f;
		[SerializeField] private Vector3 _doorBottomPosition = new Vector3(0f, -2.5f, 0f);
		
		public int Entity { get; private set; }
		
		private EcsWorld _world;
		
		private void Start()
		{
			_buttonProvider.Start();
			
			_world = WorldInstance.GetWorld();
			Entity = _world.NewEntity();

			AddDoorComponent(Entity, _buttonProvider.Entity);
			AddMovementLimitationComponent(Entity);
			
			this.AddPositionComponent(_world, Entity, _doorModelTransform.localPosition);
			this.AddMovementComponent(_world, Entity, _speed);
			this.AddDirectionComponent(_world, Entity);
			this.AddTransformComponent(_world, Entity, _doorModelTransform);
		}

		private void AddMovementLimitationComponent(int entity)
		{
			var movementLimitationPool = _world.GetPool<MovementLimitationComponent>();
			ref var movementLimitationComponent = ref movementLimitationPool.Add(entity);

			movementLimitationComponent.StartPosition = _doorModelTransform.localPosition;
			movementLimitationComponent.TargetPosition = _doorBottomPosition;
		}

		private void AddDoorComponent(int entity, int buttonEntity)
		{
			var doorPool = _world.GetPool<DoorComponent>();
			ref var doorComponent = ref doorPool.Add(entity);

			doorComponent.ButtonEntity = buttonEntity;
		}
	}
}