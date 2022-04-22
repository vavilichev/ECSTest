using System;
using Code.Components;
using Leopotam.EcsLite;

namespace Code.Systems
{
	public class ButtonSystem : IEcsRunSystem
	{
		public void Run(EcsSystems systems)
		{
			var playerFilter = systems.GetWorld().Filter<PlayerComponent>().End();
			var buttonFilter = systems.GetWorld()
				.Filter<ButtonComponent>()
				.End();

			var buttonPool = systems.GetWorld().GetPool<ButtonComponent>();
			var positionPool = systems.GetWorld().GetPool<PositionComponent>();
			var radiusPool = systems.GetWorld().GetPool<RadiusComponent>();
			
			foreach (var buttonEntity in buttonFilter)
			{
				ref var buttonComponent = ref buttonPool.Get(buttonEntity);
				ref var buttonRadiusComponent = ref radiusPool.Get(buttonEntity);

				foreach (var playerEntity in playerFilter)
				{
					ref var playerPositionComponent = ref positionPool.Get(playerEntity);
					ref var buttonPositionComponent = ref positionPool.Get(buttonEntity);
					
					var distanceSqr = (buttonPositionComponent.Position - playerPositionComponent.Position).sqrMagnitude;
					var buttonRangeSqr = Math.Pow(buttonRadiusComponent.Raduis, 2);

					buttonComponent.IsPressed = distanceSqr <= buttonRangeSqr;
				}
			}
		}
	}
}