using NEG.BetterChatReborn.Utility;
using UnityEngine;
using System;
using TMPro;

namespace NEG.BetterChatReborn.Chat.Messages
{
	public sealed class ChatHooks
	{
		internal ChatHooks(GameObject _chatContainer)
		{
			ChatContainer = _chatContainer;
			var _containerDetails = ChatContainer.GetComponent<ChatContainerInformation>();
			ChatTextContainer = _containerDetails.MessageContainer;
			TextInput = _containerDetails.MessageInputField;
			InputLockingState += (_state) =>
			{
				LastInputLockingState = _state;
			};

			TextInput.onSubmit.AddListener(_text =>
			{
				var _player = PlayerUtility.GetSelf();
				var _messageData = new MessageData(_player, _text, ChatTarget.All, DateTime.Now);
				var _message = MessageInfo.NewInfo(ChatTextContainer.transform, _player, _messageData);
				OnMostRecentText?.Invoke(_player, _message);
				InputLockingState(false);
			});
			TextInput.onSelect.AddListener(_text =>
			{
				InputLockingState(true);
			});
			TextInput.onDeselect.AddListener(_text =>
			{
				InputLockingState(false);
			});

			OnMenuEnable += OnMenuEnableBehaviour;
			OnMenuDisable += OnMenuDisableBehaviour;
		}
		private ChatHooks() { }

		internal GameObject ChatContainer { get; }
		internal GameObject ChatTextContainer { get; }
		internal TMP_InputField TextInput { get; }

		public event Action OnMenuEnable
		{
			add => ChatMenuManager.Instance.OnMenuEnable += value;
			remove => ChatMenuManager.Instance.OnMenuEnable -= value;
		}
		public event Action OnMenuDisable
		{ 
			add => ChatMenuManager.Instance.OnMenuDisable += value; 
			remove => ChatMenuManager.Instance.OnMenuDisable -= value;
		}
		public Action<bool> OnMenuStateChange { get; set; }
		public Action<Player, MessageInfo> OnMostRecentText { get; set; }
		public Action<bool> InputLockingState { get; set; }
		public bool LastInputLockingState { get; private set; }

		private void OnMenuEnableBehaviour()
		{
			OnMenuStateChange?.Invoke(true);
			TextInput.Select();
		}
		private void OnMenuDisableBehaviour()
		{
			OnMenuStateChange?.Invoke(false);
			TextInput.ReleaseSelection();
		}
	}
}