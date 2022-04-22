using Code.Components;
using Leopotam.EcsLite;

namespace Code.Systems
{
	public class UnityButtonSystem : IEcsRunSystem
	{
		public void Run(EcsSystems systems)
		{
			var buttonFilter = systems.GetWorld()
				.Filter<ButtonComponent>()
				.Inc<TransformComponent>()
				.Inc<MovementLimitationComponent>()
				.End();

			var buttonPool = systems.GetWorld().GetPool<ButtonComponent>();
			var transformPool = systems.GetWorld().GetPool<TransformComponent>();
			var movementLimitationPool = systems.GetWorld().GetPool<MovementLimitationComponent>();

			foreach (var buttonEntity in buttonFilter)
			{
				ref var buttonComponent = ref buttonPool.Get(buttonEntity);
				ref var transformComponent = ref transformPool.Get(buttonEntity);
				ref var movementLimitationComponent = ref movementLimitationPool.Get(buttonEntity);

				if (buttonComponent.IsPressed)
				{
					transformComponent.Transform.localPosition = movementLimitationComponent.TargetPosition;
				}
				else
				{
					transformComponent.Transform.localPosition = movementLimitationComponent.StartPosition;
				}
			}
		}
	}
}