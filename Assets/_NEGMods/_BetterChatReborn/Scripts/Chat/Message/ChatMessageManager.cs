using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

namespace NEG.BetterChatReborn.Chat.Messages
{
	public sealed class ChatMessageManager
	{
		public ChatMessageManager(GameObject _chatMessageContainer)
		{
			chatMessageContainer = _chatMessageContainer;
			targetMap = new Dictionary<ChatTarget, IEnumerable<Player>>()
			{
				{ ChatTarget.All, PlayerManager.instance.players },
				{ ChatTarget.Team, PlayerManager.instance.players }
			};
		}

		private readonly Dictionary<ChatTarget, IEnumerable<Player>> targetMap;
		private readonly GameObject chatMessageContainer;

		[PunRPC]
		public void RPC_SendClientMessage(Player _player, MessageData _data)
		{
			MessageInfo.NewInfo(chatMessageContainer.transform, _player, _data);

		}
		public void CreateClientMessage()
		{

		}
		public void SendServerMessage()
		{

		}
	}
}