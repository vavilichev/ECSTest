using Code.Components;
using Leopotam.EcsLite;

namespace Code.Systems
{
	public class ButtonSystem : IEcsRunSystem
	{
		public void Run(EcsSystems systems)
		{
			var buttonFilter = systems.GetWorld()
				.Filter<ButtonComponent>()
				.Inc<MovementLimitationComponent>()
				.End();
			var buttonPool = systems.GetWorld().GetPool<ButtonComponent>();
			var movementLimitationPool = systems.GetWorld().GetPool<MovementLimitationComponent>();
			
			foreach (var entity in buttonFilter)
			{
				var buttonComponent = buttonPool.Get(entity);
				var movementLimitationComponent = movementLimitationPool.Get(entity);
				
				if (buttonComponent.IsPressed)
				{
					buttonComponent.Transform.localPosition = movementLimitationComponent.TargetPosition;
				}
				else
				{
					buttonComponent.Transform.localPosition = movementLimitationComponent.StartPosition;
				}
			}
		}
	}
}