using Code.Components;
using Code.Mono.Extensions;
using Leopotam.EcsLite;
using UnityEngine;

namespace Code.Providers
{
	public class PlayerProvider : MonoBehaviour
	{
		[SerializeField] private float _speed;
		[SerializeField] private float _stopDistance = 0.1f;
		[SerializeField] private Vector3 _walkingOffset = new Vector3(0f, 1.2f, 0f);
		
		public int Entity { get; private set; }
		
		private EcsWorld _world;
		
		private void Start()
		{
			_world = WorldInstance.GetWorld();
			Entity = _world.NewEntity();
			
			AddPlayerComponent(Entity);
			AddInputComponent(Entity);

			this.AddMovementComponent(_world, Entity, _speed);
			this.AddPositionComponent(_world, Entity, transform.position);
			this.AddDirectionComponent(_world, Entity);
			this.AddTransformComponent(_world, Entity, transform);
		}

		private void AddPlayerComponent(int entity)
		{
			var playerPool = _world.GetPool<PlayerComponent>();
			playerPool.Add(entity);
		}

		private void AddInputComponent(int entity)
		{
			var playerInputPool = _world.GetPool<PointAndClickInputComponent>();
			ref var playerInputComponent = ref playerInputPool.Add(entity);

			playerInputComponent.TargetPosition = transform.position;
		}
	}
}