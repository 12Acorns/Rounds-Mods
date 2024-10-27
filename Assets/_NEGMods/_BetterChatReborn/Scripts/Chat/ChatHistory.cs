using NEG.BetterChatReborn.Chat.Messages;
using System.Collections.Generic;
using System.Linq;

namespace NEG.BetterChatReborn.Chat
{
	internal static class ChatHistory
	{
		private static readonly Dictionary<int, PlayerChatHistory> playerChatMap =
			new Dictionary<int, PlayerChatHistory>();

		public static void AddMessage(Player _player, MessageInfo _message)
		{
			if(!playerChatMap.TryGetValue(_player.playerID, out var _value))
			{
				var _playerHistory = new PlayerChatHistory(_player);
				_playerHistory.AddMessage(_message);
				playerChatMap.Add(_player.playerID, _playerHistory);
				return;
			}
			_value.AddMessage(_message);
			playerChatMap[_player.playerID] = _value;
		}
		public static bool TryGetChatHistory(Player _player, out PlayerChatHistory _history)
		{
			return playerChatMap.TryGetValue(_player.playerID, out _history);
		}
		public static IEnumerable<MessageInfo> GetAllChatHistoryOrdered()
		{
			return GetAllChatHistoryUnOrdered().OrderBy(x => x.TimeSinceMatchStart.Ticks);
		}
		public static IEnumerable<MessageInfo> GetAllChatHistoryUnOrdered()
		{
			return playerChatMap.SelectMany(x => x.Value.GetHistory());
		}
		public static void ClearHistory()
		{
			playerChatMap.Clear();
		}
	}
}