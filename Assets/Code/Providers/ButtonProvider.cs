using Code.Components;
using Leopotam.EcsLite;
using UnityEngine;

namespace Code.Providers
{
	public class ButtonProvider : MonoBehaviour
	{
		[SerializeField] private Transform _buttonModelTransform;
		
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

				_isReady = true;
			}
		}

		private void AddButtonComponent(int entity)
		{
			var pool = _world.GetPool<ButtonComponent>();
			ref var buttonComponent = ref pool.Add(entity);
			
			buttonComponent.Transform = _buttonModelTransform;
			buttonComponent.IsPressed = false;
		}

		private void AddMovementLimitationComponent(int entity)
		{
			var movementLimitationPool = _world.GetPool<MovementLimitationComponent>();
			ref var movementLimitationComponent = ref movementLimitationPool.Add(entity);

			movementLimitationComponent.StartPosition = new Vector3(0f, 0.3f, 0f);
			movementLimitationComponent.TargetPosition = new Vector3(0f, -0.15f, 0f);
		}
		
		private void OnTriggerEnter(Collider other)
		{
			var playerFilter = _world.Filter<PlayerComponent>().End();
			var playerPool = _world.GetPool<PlayerComponent>();
			var buttonPool = _world.GetPool<ButtonComponent>();
			
			ref var buttonComponent = ref buttonPool.Get(Entity);

			foreach (var playerEntity in playerFilter)
			{
				ref var playerComponent = ref playerPool.Get(playerEntity);

				if (playerComponent.GameObject == other.gameObject)
				{
					buttonComponent.IsPressed = true;
				}
			}			
		}

		private void OnTriggerExit(Collider other)
		{
			var playerFilter = _world.Filter<PlayerComponent>().End();
			var playerPool = _world.GetPool<PlayerComponent>();
			var buttonPool = _world.GetPool<ButtonComponent>();
			
			ref var buttonComponent = ref buttonPool.Get(Entity);

			foreach (var playerEntity in playerFilter)
			{
				ref var playerComponent = ref playerPool.Get(playerEntity);

				if (playerComponent.GameObject == other.gameObject)
				{
					buttonComponent.IsPressed = false;
				}
			}	
		}
	}
}