using UnityEngine;
using System;
using TMPro;

namespace NEG.BetterChatReborn.Chat.Messages
{
	public sealed class MessageInfo : MonoBehaviour
	{
		public static MessageInfo NewInfo(Transform _parent, Player _sender, MessageData _message)
		{
			var _base = BetterChatRebornEntry.BetterChatRebornAssets.LoadAsset<GameObject>("Chat-Message-Container");
			var _messageInfoContainer = _base.GetComponent<ChatMessageContainer>();
			_base.transform.SetParent(_parent);
			var _info = _base.AddComponent<MessageInfo>();

			_info.timeSinceMatchStart = DateTime.Now.Ticks;

			_info.MessageMeta = _messageInfoContainer.MessageMeta;
			_info.Message = _messageInfoContainer.MessageText;
			_info.Message.text = _message.Message;

			var _formatText = _info.MessageMeta.text;
			var _fromText = $"<color>{_message.FromColour}<color/>{_message.From}";
			var _toText = $"<color>{_message.TargetColour}<color/>{_message.Target}";
			var _metaText = string.Format(_formatText, _fromText, _toText, _message.TimeSent);
			_info.MessageMeta.text = _metaText;

			_info.Sender = _sender;
			return _info;
		}

		public DateTime TimeSinceMatchStart => new DateTime(timeSinceMatchStart);
		public TextMeshProUGUI MessageMeta { get; private set; }
		public TextMeshProUGUI Message { get; private set; }
		public Player Sender { get; private set; }


		private long timeSinceMatchStart { get; set; }
	}
}