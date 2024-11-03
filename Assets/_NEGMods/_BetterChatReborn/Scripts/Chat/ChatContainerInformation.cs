using UnityEngine.UI;
using UnityEngine;
using TMPro;

namespace NEG.BetterChatReborn.Chat
{
	public sealed class ChatContainerInformation : MonoBehaviour
	{
		[SerializeField] private ScrollRect messageContainer;
		[SerializeField] private TMP_InputField messageInputField;

		public ScrollRect ScrollRect => messageContainer;
		public TMP_InputField MessageInputField => messageInputField;
	}
}