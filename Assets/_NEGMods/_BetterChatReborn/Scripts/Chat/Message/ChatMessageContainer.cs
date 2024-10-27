using UnityEngine;
using TMPro;

namespace NEG.BetterChatReborn.Chat.Messages
{
	public sealed class ChatMessageContainer : MonoBehaviour
	{
		[SerializeField] private TextMeshProUGUI messageMeta;
		[SerializeField] private TextMeshProUGUI messageText;

		public TextMeshProUGUI MessageMeta => messageMeta;
		public TextMeshProUGUI MessageText => messageText;
	}
}