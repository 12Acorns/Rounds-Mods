using UnityEngine;
using Photon.Pun;
using UnboundLib;

namespace NEG.BetterChatReborn.Chat.Messages
{
	public sealed class ChatMessageManager : MonoBehaviour
	{
		private ChatContainerInformation chatContainerDetails;

		internal void Init(ChatContainerInformation _chatMessageDetails)
		{
			chatContainerDetails = _chatMessageDetails;
		}

		/// <summary>
		/// A message sent to all clients
		/// </summary>
		[PunRPC]
		public void RPCA_CreateMessage(Player _sender, MessageData _data, bool _enablePreviewSelfAndOthers = true)
		{
			var _message = MessageInfo.NewInfo(chatContainerDetails, _sender, _data);
			ChatHistory.AddMessage(_sender, _message);
			if(_enablePreviewSelfAndOthers)
			{

				this.ExecuteAfterFrames(3, () =>
				{
					if(ChatMenuManager.Instance.MenuOpen)
					{
						return;
					}
					ChatMenuManager.Instance.EnablePreview();
				});
			}
		}
		/// <summary>
		/// Only the current client will see this messsage
		/// </summary>
		public void CreateClientMessage(MessageData _data)
		{
			_ = MessageInfo.NewInfo(chatContainerDetails, _data);
		}
		/// <summary>
		/// A message sent by the server
		/// </summary>
		//[PunRPC]
		//public void RPC_SendServerMessage()
		//{

		//}
	}
}