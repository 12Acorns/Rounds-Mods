using NEG.BetterChatReborn.Utility;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.UI;

namespace NEG.BetterChatReborn.Chat.Messages
{
	public sealed class MessageInfo : MonoBehaviour
	{
		public static MessageInfo NewInfo(ChatContainerInformation _parent, Player _sender, MessageData _message)
		{
			UnityEngine.Debug.Assert(_parent != null, $"{nameof(_parent)} is null");
			UnityEngine.Debug.Assert(_sender != null, $"{nameof(_sender)} is null");

			var _baseObject = Instantiate(chatMessageAsset, _parent.ScrollRect.content);
			_baseObject.transform.SetAsLastSibling();
			LayoutRebuilder.MarkLayoutForRebuild((RectTransform)_parent.ScrollRect.transform);
			var _messageInfoContainer = _baseObject.GetComponent<ChatMessageContainer>();

			UnityEngine.Debug.Assert(_messageInfoContainer != null, $"{nameof(_messageInfoContainer)} is null");
			UnityEngine.Debug.Assert(_messageInfoContainer?.MessageMeta != null, $"{nameof(_messageInfoContainer.MessageMeta)} is null");
			UnityEngine.Debug.Assert(_messageInfoContainer?.MessageText != null, $"{nameof(_messageInfoContainer.MessageText)} is null");

			var _info = _baseObject.AddComponent<MessageInfo>();
			_info.timeSent = DateTime.Now.Ticks;
			_info.MessageMeta = _messageInfoContainer.MessageMeta;
			_info.Message = _messageInfoContainer.MessageText;
			_info.Message.text = _message.Message;

			var _formatText = _info.MessageMeta.text;
			var _fromText = $"<color={ChatUtility.EnsureHasHash(_message.FromColour)}>{_message.From}</color>";
			var _timeText = $"<color={ChatUtility.EnsureHasHash(ConfigBindings.GetTimeColour())}>({_message.TimeSent})</color>";
			var _metaText = string.Format(_formatText, _fromText, _timeText);
			_info.MessageMeta.text = _metaText;
			_info.Sender = _sender;
			return _info;
		}
		public static MessageInfo NewInfo(ChatContainerInformation _parent, MessageData _message)
		{
			return NewInfo(_parent, PlayerUtility.GetSelf(), _message);
		}

		private static readonly GameObject chatMessageAsset = 
			BetterChatRebornEntry.BetterChatRebornAssets.LoadAsset<GameObject>("Chat-Message-Container");

		public DateTime TimeSent => new DateTime(timeSent);
		public TextMeshProUGUI MessageMeta { get; private set; }
		public TextMeshProUGUI Message { get; private set; }
		public Player Sender { get; private set; }

		private long timeSent { get; set; }
	}
}