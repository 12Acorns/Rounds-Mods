using TMPro;
using UnityEngine;

namespace NEG.BetterChatReborn.Chat
{
	public sealed class ChatContainerInformation : MonoBehaviour
	{
		[SerializeField] private GameObject messageContainer;
		[SerializeField] private TMP_InputField messageInputField;

		public GameObject MessageContainer => messageContainer;
		public TMP_InputField MessageInputField => messageInputField;
	}
}