using Components;
using Leopotam.Ecs;

namespace Systems
{
	public class FollowToTransformSystem : IEcsRunSystem
	{
		private readonly EcsFilter<TransformMovementComponent, FollowToTransformComponent> _filter = null;
		
		public void Run()
		{
			foreach (var index in _filter)
			{
				ref var movementComponent = ref _filter.Get1(index);
				ref var followerComponent = ref _filter.Get2(index);

				var stopDistanceSqrt = followerComponent.StopDistance * followerComponent.StopDistance;
				var targetPosition = followerComponent.TargetTransform.position + followerComponent.Offset;
				var transformPosition = movementComponent.Transform.position;
				var direction = (targetPosition - transformPosition);
				var distanceSqrt = direction.sqrMagnitude;

				var speed = distanceSqrt <= stopDistanceSqrt ? 0 : movementComponent.SpeedInitial;
				
				movementComponent.DirectionNormalized = direction.normalized;
				movementComponent.Speed = speed;
			}
		}
	}
}