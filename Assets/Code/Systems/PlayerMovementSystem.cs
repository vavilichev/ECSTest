using Code.Components;
using Leopotam.EcsLite;

namespace Code.Systems
{
	sealed class PlayerMovementSystem : IEcsRunSystem
	{
		public void Run(EcsSystems systems)
		{
			var playerPool = systems.GetWorld()
				.Filter<PlayerComponent>()
				.End();
			var movementPool = systems.GetWorld().GetPool<MovementComponent>();
			var positionPool = systems.GetWorld().GetPool<PositionComponent>();
			var playerInputPool = systems.GetWorld().GetPool<PointAndClickInputComponent>();
			var directionPool = systems.GetWorld().GetPool<DirectionComponent>();
			
			foreach (var playerEntity in playerPool)
			{
				ref var movementComponent = ref movementPool.Get(playerEntity);
				ref var positionComponent = ref positionPool.Get(playerEntity);
				ref var inputComponent = ref playerInputPool.Get(playerEntity);
				ref var directionComponent = ref directionPool.Get(playerEntity);

				directionComponent.Direction = (inputComponent.TargetPosition - positionComponent.Position);

				var distanceSqr = directionComponent.Direction.sqrMagnitude;
				var stopDistanceSqr = 0.02f;
				var shouldStop = distanceSqr <= stopDistanceSqr;
				var speed = shouldStop ? 0f : movementComponent.Speed;

				positionComponent.Position += directionComponent.Direction.normalized * speed;
			}
		}
	}
}