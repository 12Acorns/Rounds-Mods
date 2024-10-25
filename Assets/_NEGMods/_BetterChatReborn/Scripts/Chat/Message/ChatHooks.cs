using System.Linq;
using UnityEngine;
using Photon.Pun;
using System;
using TMPro;

namespace NEG.BetterChatReborn.Chat
{
	public sealed class ChatHooks
	{
		internal ChatHooks(GameObject _chatContainer)
		{
			chatContainer = _chatContainer;
			var _containerDetails = chatContainer.GetComponent<ChatContainerInformation>();
			chatTextContainer = _containerDetails.MessageContainer;
			textInput = _containerDetails.MessageInputField;

			textInput.onSubmit.AddListener((_text) =>
			{
				var _player = PlayerManager.instance.players.First(_playerLocal => _playerLocal.GetComponent<PhotonView>().IsMine);
				var _message = MessageInfo.NewInfo(chatTextContainer.transform, _player, _text);
				OnMostRecentText(_player, _message);
			});
		}

		private readonly GameObject chatContainer;
		private readonly GameObject chatTextContainer;
		private readonly TMP_InputField textInput;


		public Action OnMenuDisable => ChatMenuManager.Instance.OnMenuDisable;
		public Action OnMenuEnable => ChatMenuManager.Instance.OnMenuEnable;
		public Action<Player, MessageInfo> OnMostRecentText { get; }
	}
}