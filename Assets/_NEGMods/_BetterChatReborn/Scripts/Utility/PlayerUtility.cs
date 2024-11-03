using JetBrains.Annotations;
using Photon.Pun;
using System.Linq;

namespace NEG.BetterChatReborn.Utility
{
	internal static class PlayerUtility
	{
		private static Player self;

		[CanBeNull]
		public static Player GetSelf()
		{
			if(self != null)
			{
				return self;
			}
			self = PlayerManager.instance?.players?.FirstOrDefault(_player => _player.GetComponent<PhotonView>().IsMine);
			if(self == null || self == default)
			{
				return null;
			}
			return self;
		}
	}
}