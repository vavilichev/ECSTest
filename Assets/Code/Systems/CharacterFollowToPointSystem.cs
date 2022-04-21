using Components;
using Leopotam.Ecs;

namespace Systems
{
	public class CharacterFollowToPointSystem : IEcsRunSystem, IEcsInitSystem
	{
		private readonly EcsFilter<CharacterMovementComponent, CharacterFollowToPointComponent> _filter = null;
		
		public void Init()
		{
			foreach (var index in _filter)
			{
				ref var movementComponent = ref _filter.Get1(index);
				ref var followerComponent = ref _filter.Get2(index);

				followerComponent.TargetPosition = movementComponent.CharacterController.transform.position;
			}
		}
		
		public void Run()
		{
			foreach (var index in _filter)
			{
				ref var movementComponent = ref _filter.Get1(index);
				ref var followerComponent = ref _filter.Get2(index);

				var stopDistanceSqrt = followerComponent.StopDistance * followerComponent.StopDistance;
				var targetPosition = followerComponent.TargetPosition + followerComponent.Offset;
				var transformPosition = movementComponent.CharacterController.transform.position;
				var direction = (targetPosition - transformPosition);
				var distanceSqrt = direction.sqrMagnitude;

				var speed = distanceSqrt <= stopDistanceSqrt ? 0 : movementComponent.SpeedInitial;
				
				movementComponent.DirectionNormalized = direction.normalized;
				movementComponent.Speed = speed;
			}
		}
	}
}