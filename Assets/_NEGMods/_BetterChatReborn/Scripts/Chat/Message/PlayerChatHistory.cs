using System.Collections.Generic;

namespace NEG.BetterChatReborn.Chat.Messages
{
	public sealed class PlayerChatHistory
	{
		public PlayerChatHistory(Player _player)
		{
			messageHistory = new List<MessageInfo>();
			player = _player;
		}

		private readonly List<MessageInfo> messageHistory;
		private readonly Player player;

		public Player Player => player;
		/// <summary>
		/// Default if no messages added
		/// </summary>
		public MessageInfo MostRecentMessage => messageHistory.Count == 0 ? default : messageHistory[messageHistory.Count - 1];

		public void AddMessage(MessageInfo _message)
		{
			messageHistory.Add(_message);
		}
		public IEnumerable<MessageInfo> GetHistory()
		{
			return messageHistory;
		}
	}
}