using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace NEG.BetterChatReborn.Chat
{
	// To determine whether to cache players or retrieve at runtime
	public sealed class ChatGroup
	{
		/// <param name="_groupKey">If <paramref name="_group"/> is <see cref="ChatTarget.Team"/> specify team key</param>
		public ChatGroup(ChatTarget _group, KeyCode _chatOpenKey, Func<int, int, bool> _recieveCondition)
		{
			group = _group;
			ChatOpenKey = _chatOpenKey;
			recieveCondition = _recieveCondition;
		}

		private readonly ChatTarget group;
		private readonly Func<int, int, bool> recieveCondition;

		public KeyCode ChatOpenKey { get; }

		public static bool CanViewChat(ChatGroup _group, Player _sender, Player _reciever) =>
			_group.recieveCondition(_sender.playerID, _reciever.playerID);
		public static IEnumerable<Player> GetPlayers(ChatGroup _group, Player _sender)
		{
			switch(_group.group)
			{
				case ChatTarget.All:
					return PlayerManager.instance.players;
				case ChatTarget.Team:
					return PlayerManager.instance.players.Where(_player => _player.teamID == _sender.teamID);
				default:
					goto case ChatTarget.All;
			}
		}
	}
}