using System.Collections.Generic;
using NEG.BetterChatReborn.Chat;

namespace NEG.BetterChatReborn.Extensions
{
	public static class ChatGroupExtensions
	{
		public static bool CanPlayerViewChat(this ChatGroup _group, Player _sender, Player _reciever) =>
			ChatGroup.CanViewChat(_group, _sender, _reciever);
		public static IEnumerable<Player> GetPlayersInGroup(this ChatGroup _group, Player _sender) =>
			ChatGroup.GetPlayers(_group, _sender);
	}
}