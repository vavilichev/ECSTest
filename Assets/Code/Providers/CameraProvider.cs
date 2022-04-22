using Code.Components;
using Code.Mono.Extensions;
using Leopotam.EcsLite;
using UnityEngine;

namespace Code.Providers
{
	public class CameraProvider : MonoBehaviour
	{
		[SerializeField] private Camera _camera;
		[SerializeField] private Transform _target;
		[SerializeField] private Vector3 _offset = new Vector3(22.5f, 38.4f, -25.47f);
		[SerializeField] private float _speed = 2f;
		
		public int Entity { get; private set; }
		
		private EcsWorld _world;
		
		private void Start()
		{
			_world = WorldInstance.GetWorld();
			Entity = _world.NewEntity();
			
			AddCameraComponent(Entity);
			AddCameraFollowComponent(Entity);

			this.AddMovementComponent(_world, Entity, _speed);
			this.AddPositionComponent(_world, Entity, transform.position);
			this.AddDirectionComponent(_world, Entity);
			this.AddTransformComponent(_world, Entity, transform);
		}
		
		private void AddCameraFollowComponent(int cameraEntity)
		{
			var cameraPool = _world.GetPool<CameraFollowComponent>();
			ref var cameraComponent = ref cameraPool.Add(cameraEntity);
			
			cameraComponent.Offset = _offset;
			cameraComponent.Target = _target;
		}

		private void AddCameraComponent(int cameraEntity)
		{
			var cameraPool = _world.GetPool<CameraComponent>();
			ref var cameraComponent = ref cameraPool.Add(cameraEntity);
			
			cameraComponent.Camera = _camera;
		}
	}
}