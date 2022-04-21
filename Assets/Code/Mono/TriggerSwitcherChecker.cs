using Components;
using Leopotam.Ecs;
using UnityEngine;
using Voody.UniLeo;

namespace Mono
{
	public class TriggerSwitcherChecker : MonoBehaviour
	{
		private EcsWorld World
		{
			get
			{
				if (_world == null)
				{
					_world = WorldHandler.GetWorld();
				}

				return _world;
			}
		}

		private EcsWorld _world;
		
		private readonly EcsFilter<TriggerSwitcherComponent> _triggerFilter = null;

		private void Start()
		{
			_world = WorldHandler.GetWorld();
		}

		private void OnTriggerEnter(Collider other)
		{
			var triggerFilter = World.GetFilter(typeof(EcsFilter<TriggerSwitcherComponent>));
			var triggerPool = World.GetPool<TriggerSwitcherComponent>();
			var playerFilter = World.GetFilter(typeof(EcsFilter<PlayerTagComponent>));
			var playerPool = World.GetPool<PlayerTagComponent>();

			foreach (var index in triggerFilter)
			{
				ref var triggerComponent = ref triggerPool.GetItem(index);
				
				if (triggerComponent.Checker != this)
				{
					continue;
				}

				foreach (var playerIndex in playerFilter)
				{
					ref var playerComponent = ref playerPool.GetItem(playerIndex);

					if (other.gameObject == playerComponent.GameObject)
					{
						triggerComponent.IsOn = true;
					}
				}
			}
		}

		private void OnTriggerExit(Collider other)
		{
			var triggerFilter = World.GetFilter(typeof(EcsFilter<TriggerSwitcherComponent>));
			var triggerPool = World.GetPool<TriggerSwitcherComponent>();
			var playerFilter = World.GetFilter(typeof(EcsFilter<PlayerTagComponent>));
			var playerPool = World.GetPool<PlayerTagComponent>();

			foreach (var index in triggerFilter)
			{
				ref var triggerComponent = ref triggerPool.GetItem(index);

				if (triggerComponent.Checker != this)
				{
					continue;
				}

				foreach (var playerIndex in playerFilter)
				{
					ref var playerComponent = ref playerPool.GetItem(playerIndex);

					if (other.gameObject == playerComponent.GameObject)
					{
						triggerComponent.IsOn = false;
					}
				}
			}
		}
	}
}