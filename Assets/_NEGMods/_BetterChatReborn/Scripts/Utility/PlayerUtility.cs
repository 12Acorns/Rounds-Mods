using System.Linq;

namespace NEG.BetterChatReborn.Utility
{
	internal static class PlayerUtility
	{
		private static Player self;

		public static Player GetSelf()
		{
			if(self == null)
			{
				self = PlayerManager.instance.players.First(_player => _player.data.view.IsMine);
			}
			return self;
		}
	}
}