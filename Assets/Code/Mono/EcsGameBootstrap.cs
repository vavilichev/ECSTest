using Code.Systems;
using Leopotam.EcsLite;
using Leopotam.EcsLite.UnityEditor;
using UnityEngine;

namespace Code.Mono
{
    public class EcsGameBootstrap : MonoBehaviour
    {
        private EcsWorld _world;
        private EcsSystems _initSystems;
        private EcsSystems _updateSystems;

        private void Awake()
        {
            _world = new EcsWorld();
            WorldInstance.SetWorld(_world);
            
            _initSystems = new EcsSystems(_world);
            _updateSystems = new EcsSystems(_world);
            
            AddSystems();
        
            _initSystems.Init();
            _updateSystems.Init();
        }

        private void AddSystems()
        {
            _initSystems
                .Add(new CameraInitSystem())
                .Add(new PlayerInitSystem())
#if UNITY_EDITOR
                .Add(new EcsWorldDebugSystem())
#endif
                ;
            
            _updateSystems
                .Add(new PointAndClickInputSystem())
                .Add(new PlayerFollowToPointSystem())
                .Add(new CharacterMovementSystem())
                .Add(new TransformMovementSystem())
                .Add(new CameraFollowSystem())
                .Add(new ButtonSystem())
                .Add(new DoorSystem());
        }

        private void Update()
        {
            _updateSystems.Run();
        }

        private void OnDestroy()
        {
            if (_updateSystems != null)
            {
                _updateSystems.Destroy();
                _updateSystems = null;
            }

            if (_world != null)
            {
                _world.Destroy();
                _world = null;    
            }
        }
    }
}
