using Code.Components;
using Leopotam.EcsLite;
using UnityEngine;

namespace Code.Systems
{
	public class PlayerInitSystem : IEcsInitSystem
	{
		public void Init(EcsSystems systems)
		{
			var playerEntity = systems.GetWorld().NewEntity();
			var playerGo = GameObject.FindGameObjectWithTag("Player");

			AddPlayerComponent(systems, playerEntity, playerGo);
			AddCharacterMovementComponent(systems, playerEntity, playerGo);
			AddInputComponent(systems, playerEntity);
			AddPlayerFollowToPointComponent(systems, playerEntity);
		}

		private void AddPlayerComponent(EcsSystems systems, int entity, GameObject playerGo)
		{
			var playerPool = systems.GetWorld().GetPool<PlayerComponent>();
			
			ref var playerComponent = ref playerPool.Add(entity);
			
			playerComponent.GameObject = playerGo;
			playerComponent.Transform = playerGo.transform;
		}

		private void AddCharacterMovementComponent(EcsSystems systems, int entity, GameObject playerGo)
		{
			var characterMovementPool = systems.GetWorld().GetPool<CharacterMovementComponent>();
			ref var movementComponent = ref characterMovementPool.Add(entity);
			
			movementComponent.CharacterController = playerGo.GetComponent<CharacterController>();
			movementComponent.SpeedInitial = 10f;
		}

		private void AddInputComponent(EcsSystems systems, int entity)
		{
			var playerInputPool = systems.GetWorld().GetPool<PointAndClickInputComponent>();
			playerInputPool.Add(entity);
		}

		private void AddPlayerFollowToPointComponent(EcsSystems systems, int entity)
		{
			var playerFollowToPointPool = systems.GetWorld().GetPool<PlayerFollowToPointComponent>();
			ref var playerFollowToPointComponent = ref playerFollowToPointPool.Add(entity);
			
			playerFollowToPointComponent.Offset = new Vector3(0f, 1.2f, 0f);
			playerFollowToPointComponent.StopDistance = 0.1f;
		}
	}
}