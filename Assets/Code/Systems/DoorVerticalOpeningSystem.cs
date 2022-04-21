using Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Systems
{
	public class DoorVerticalOpeningSystem : IEcsRunSystem
	{
		private readonly EcsFilter<TriggerSwitcherComponent> _triggersFilter = null;
		private readonly EcsFilter<TransformMovementComponent, MovementLimitationComponent, DoorComponent> _doorsFilter = null;
		
		public void Run()
		{
			foreach (var doorIndex in _doorsFilter)
			{
				ref var doorComponent = ref _doorsFilter.Get3(doorIndex);
				
				foreach (var triggerIndex in _triggersFilter)
				{
					ref var triggerSwitcherComponent = ref _triggersFilter.Get1(triggerIndex);

					if (triggerSwitcherComponent.GameObject == doorComponent.Switcher)
					{
						ref var transformMovementComponent = ref _doorsFilter.Get1(doorIndex);
						ref var movementLimitationComponent = ref _doorsFilter.Get2(doorIndex);
						
						var doorTransform = transformMovementComponent.Transform;

						if (triggerSwitcherComponent.IsOn)
						{
							if (doorTransform.position.y > movementLimitationComponent.TargetPosition.y)
							{
								transformMovementComponent.Speed = transformMovementComponent.SpeedInitial;
								transformMovementComponent.DirectionNormalized = Vector3.down;
							}
							else
							{
								transformMovementComponent.Speed = 0;
							}
						}
						else
						{
							transformMovementComponent.Speed = 0;
						}
					}
				}
			}
		}
	}
}