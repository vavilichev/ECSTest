using Systems;
using Leopotam.Ecs;
using UnityEngine;
using Voody.UniLeo;

namespace Mono
{
    public partial class EcsGameBootstrap : MonoBehaviour
    {
        private EcsWorld _world;
        private EcsSystems _systems;

        private void Start()
        {
            _world = new EcsWorld();

            WorldHandler.Init(_world);
        
            _systems = new EcsSystems(_world);

            _systems.ConvertScene();
        
            AddInjections();
            AddFrames();
            AddSystems();
        
            _systems.Init();
        }

        private void AddInjections()
        {
        
        }
    
        private void AddSystems()
        {
            _systems
                .Add(new PointAndClickInputSystem())
                .Add(new TransformMovementSystem())
                .Add(new CharacterMovementSystem())
                .Add(new CharacterFollowToPointSystem())
                .Add(new FollowToTransformSystem())
                .Add(new ButtonSystem())
                .Add(new DoorVerticalOpeningSystem());
        }

        private void AddFrames()
        {
        
        }

        private void Update()
        {
            _systems.Run();
        }

        private void OnDestroy()
        {
            if (_systems != null)
            {
                _systems.Destroy();
                _systems = null;
            }

            if (_world != null)
            {
                _world.Destroy();
                _world = null;    
            }
        }
    }
}
