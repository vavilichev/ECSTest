using Code.Components;
using Leopotam.EcsLite;

namespace Code.Systems
{
	public class PlayerFollowToPointSystem : IEcsRunSystem, IEcsInitSystem
	{
		public void Init(EcsSystems systems)
		{
			var playerFilter = systems.GetWorld()
				.Filter<PlayerComponent>()
				.Inc<PlayerFollowToPointComponent>()
				.End();

			var playerPool = systems.GetWorld().GetPool<PlayerComponent>();
			var followPool = systems.GetWorld().GetPool<PlayerFollowToPointComponent>();
			
			foreach (var entity in playerFilter)
			{
				var playerComponent = playerPool.Get(entity);
				ref var followComponent = ref followPool.Get(entity);

				followComponent.TargetPosition = playerComponent.Transform.position - followComponent.Offset;
			}
		}
		
		public void Run(EcsSystems systems)
		{
			var playerFilter = systems.GetWorld()
				.Filter<PlayerComponent>()
				.Inc<CharacterMovementComponent>()
				.Inc<PlayerFollowToPointComponent>()
				.Inc<PointAndClickInputComponent>()
				.End();
			
			var playerPool = systems.GetWorld().GetPool<PlayerComponent>();
			var movementPool = systems.GetWorld().GetPool<CharacterMovementComponent>();
			var inputPool = systems.GetWorld().GetPool<PointAndClickInputComponent>();
			var playerFollowToPointPool = systems.GetWorld().GetPool<PlayerFollowToPointComponent>();

			foreach (var entity in playerFilter)
			{
				ref var playerComponent = ref playerPool.Get(entity);
				ref var movementComponent = ref movementPool.Get(entity);
				ref var playerFollowToPointComponent = ref playerFollowToPointPool.Get(entity);
				ref var inputComponent = ref inputPool.Get(entity);

				playerFollowToPointComponent.TargetPosition = inputComponent.TargetPosition;

				var stopDistanceSqrt = playerFollowToPointComponent.StopDistance * playerFollowToPointComponent.StopDistance;
				var targetPosition = playerFollowToPointComponent.TargetPosition + playerFollowToPointComponent.Offset;
				var transformPosition = playerComponent.Transform.position;
				var direction = targetPosition - transformPosition;
				var distanceSqrt = direction.sqrMagnitude;

				var speed = distanceSqrt <= stopDistanceSqrt ? 0 : movementComponent.SpeedInitial;
				
				movementComponent.DirectionNormalized = direction.normalized;
				movementComponent.Speed = speed;
			}
		}
	}
}