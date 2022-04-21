using Components;
using Voody.UniLeo;

namespace Providers
{
	public class TransformProvider : MonoProvider<TransformComponent>
	{
		private void Reset()
		{
			if (value.Transform == null)
			{
				value.Transform = transform;
			}
		}
	}
}