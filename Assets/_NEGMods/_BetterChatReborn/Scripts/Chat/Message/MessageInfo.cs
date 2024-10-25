using UnityEngine;
using System;
using TMPro;

namespace NEG.BetterChatReborn.Chat
{
	public sealed class MessageInfo : MonoBehaviour
	{
		public static MessageInfo NewInfo(Transform _parent, Player _sender, string _messageText)
		{
			var _base = BetterChatRebornEntry.BetterChatRebornAssets.LoadAsset<GameObject>("Chat-Message-Container");
			var _messageInfoContainer = _base.GetComponent<ChatMessageContainer>();
			_base.transform.SetParent(_parent);
			var _info = _base.AddComponent<MessageInfo>();

			_info.timeSinceMatchStart = DateTime.Now.Ticks;

			_info.MessageMeta = _messageInfoContainer.MessageMeta;
			_info.Message = _messageInfoContainer.MessageText;
			_info.Message.text = _messageText;

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