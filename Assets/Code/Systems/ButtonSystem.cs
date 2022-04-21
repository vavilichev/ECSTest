using Components;
using Leopotam.Ecs;

namespace Systems
{
	public class ButtonSystem : IEcsRunSystem
	{
		private readonly EcsFilter<ButtonComponent, TriggerSwitcherComponent, TransformComponent, MovementLimitationComponent> _filter;

		public void Run()
		{
			foreach (var index in _filter)
			{
				var triggerSwitcherComponent = _filter.Get2(index);
				var transformComponent = _filter.Get3(index);
				var movementLimitationComponent = _filter.Get4(index);
				
				if (triggerSwitcherComponent.IsOn)
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