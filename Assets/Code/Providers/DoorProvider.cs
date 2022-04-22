using Code.Components;
using Leopotam.EcsLite;
using UnityEngine;

namespace Code.Providers
{
	public class DoorProvider : MonoBehaviour
	{
		[SerializeField] private Transform _doorModelTransform;
		[SerializeField] private ButtonProvider _buttonProvider;
		
		private EcsWorld _world;
		private int _entity;
		
		private void Start()
		{
			_buttonProvider.Start();
			
			_world = WorldInstance.GetWorld();
			_entity = _world.NewEntity();

			AddDoorComponent(_entity);
			AddMovementComponent(_entity);
			AddMovementLimitationComponent(_entity);
		}

		private void AddMovementLimitationComponent(int entity)
		{
			var movementLimitationPool = _world.GetPool<MovementLimitationComponent>();
			ref var movementLimitationComponent = ref movementLimitationPool.Add(entity);
			
			movementLimitationComponent.StartPosition = new Vector3(0f, 2.5f, 0f);
			movementLimitationComponent.TargetPosition = new Vector3(0f, -2.5f, 0f);
		}

		private void AddMovementComponent(int entity)
		{
			var movementPool = _world.GetPool<TransformMovementComponent>();
			ref var movementComponent = ref movementPool.Add(entity);

			movementComponent.SpeedInitial = 2f;
			movementComponent.Transform = _doorModelTransform;
		}

		private void AddDoorComponent(int entity)
		{
			var doorPool = _world.GetPool<DoorComponent>();
			ref var doorComponent = ref doorPool.Add(entity);

			doorComponent.ButtonEntity = _buttonProvider.Entity;
		}
	}
}