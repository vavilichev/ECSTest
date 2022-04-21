using Components;
using Voody.UniLeo;

namespace Providers
{
	public class TriggerSwitcherProvider : MonoProvider<TriggerSwitcherComponent>
	{
		private void Reset()
		{
			if (value.GameObject == null)
			{
				value.GameObject = gameObject;
			}
		}
	}
}