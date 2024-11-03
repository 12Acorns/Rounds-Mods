using NEG.BetterChatReborn.Extensions;
using System.Runtime.CompilerServices;
using NEG.BetterChatReborn.Utility;
using System.Reflection;
using UnityEngine;
using Photon.Pun;
using UnboundLib;
using System;

namespace NEG.BetterChatReborn.Chat.Messages
{
	public sealed class ChatHooks : MonoBehaviour
	{
		private static readonly Type devConsole = typeof(DevConsole);

		internal void Init(GameObject _chatContainer)
		{
			ChatContainer = _chatContainer;
			ChatContainerDetails = ChatContainer.GetComponent<ChatContainerInformation>();

			UnityEngine.Debug.Assert(ChatContainer != null, "Chat Container is null");

			UnityEngine.Debug.Assert(ChatContainerDetails?.ScrollRect != null, "Chat-Text-Container is null");
			UnityEngine.Debug.Assert(ChatContainerDetails?.MessageInputField != null, "Text-Container is null");

			On.MainMenuHandler.Awake += (_orig, _self) =>
			{
				ChatMenuManager.Instance.ExecuteAfterSeconds(0.2f, ChatMenuManager.Instance.ClearChatAndDisable);
				_orig(_self);
			};

			InputLockingState += (_state) =>
			{
				LastInputLockingState = _state;
				Unbound.lockInputBools["chatLock"] = LastInputLockingState;
			};

			ChatContainerDetails.MessageInputField.onEndEdit.AddListener(_text =>
			{
				if(!Input.GetKeyDown(KeyCode.Return)) return;
				ChatMenuManager.Instance.ExecuteAfterFrames(1, ChatMenuManager.Instance.DisableChat);
				if(string.IsNullOrEmpty(_text)) return;
				if(PhotonNetwork.CurrentRoom == null || PhotonNetwork.CurrentRoom?.PlayerCount == 0) return;

				SendVanillaChat(_text);

				var _player = PlayerUtility.GetSelf();
				var _messageData = new MessageData(_player, _text, ChatTarget.All, DateTime.Now);
				var _view = MenuControllerHandler.instance.GetComponent<PhotonView>();
				_view?.RPC(nameof(ChatMenuManager.Instance.MessageManager.RPCA_CreateMessage),
						RpcTarget.All, _player, _messageData, true);

				ChatHistory.TryGetMostRecentMessage(_player, out var _recentMessage);
				OnMostRecentText?.Invoke(_player, _recentMessage);

				ChatContainerDetails.MessageInputField.text = string.Empty;
				ChatContainerDetails.MessageInputField.selectionAnchorPosition = 0;
				InputLockingState(false);
			});
			ChatContainerDetails.MessageInputField.onSelect.AddListener(_text =>
			{
				InputLockingState(true);
			});
			ChatContainerDetails.MessageInputField.onDeselect.AddListener(_text =>
			{
				InputLockingState(false);
			});

			OnMenuEnable += OnMenuEnableBehaviour;
			OnMenuDisable += OnMenuDisableBehaviour;
		}

		internal GameObject ChatContainer { get; private set; }
		internal ChatContainerInformation ChatContainerDetails { get; private set; }

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
			ChatContainerDetails.MessageInputField.DoSelectEvent();
		}
		private void OnMenuDisableBehaviour()
		{
			OnMenuStateChange?.Invoke(false);
			ChatContainerDetails.MessageInputField.DoDeselectEvent();
		}
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static void SendVanillaChat(string _text)
		{
			if(PlayerManager.instance?.players?.Count == 0)
			{
				return;
			}

			devConsole.InvokeMember("Send", 
				BindingFlags.Instance | 
				BindingFlags.InvokeMethod | 
				BindingFlags.NonPublic, 
				null, MenuControllerHandler.instance.GetComponent<DevConsole>(), 
				new object[] { _text });
		}
	}
}