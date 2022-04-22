using Leopotam.EcsLite;

namespace Code
{
	public static class WorldInstance
	{
		private static EcsWorld _world;
		
		public static void SetWorld(EcsWorld world)
		{
			_world = world;
		}

		public static EcsWorld GetWorld()
		{
			return _world;
		}
	}
}