using Code.Components;
using Leopotam.EcsLite;
using UnityEngine;

namespace Code.Mono.Extensions
{
	public static class EcsMonoExtensions
	{
		public static void AddTransformComponent(this MonoBehaviour mono, EcsWorld world, int entity, Transform transform)
		{
			var transformPool = world.GetPool<TransformComponent>();
			ref var transformComponent = ref transformPool.Add(entity);

			transformComponent.Transform = transform;
		}
		
		public static void AddDirectionComponent(this MonoBehaviour mono, EcsWorld world, int entity)
		{
			var directionPool = world.GetPool<DirectionComponent>();
			ref var directionComponent = ref directionPool.Add(entity);

			directionComponent.Direction = Vector3.zero;
		}
		
		public static void AddPositionComponent(this MonoBehaviour mono, EcsWorld world, int entity, Vector3 defaultPosition)
		{
			var positionPool = world.GetPool<PositionComponent>();
			ref var positionComponent = ref positionPool.Add(entity);

			positionComponent.Position = defaultPosition;
		}
		
		public static void AddMovementComponent(this MonoBehaviour mono, EcsWorld world, int entity, float speed)
		{
			var movementPool = world.GetPool<MovementComponent>();
			ref var movementComponent = ref movementPool.Add(entity);

			movementComponent.Speed = speed;
		}
	}
}