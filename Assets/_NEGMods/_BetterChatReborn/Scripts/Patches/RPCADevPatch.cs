using NEG.BetterChatReborn.Chat.Messages;
using NEG.BetterChatReborn.Chat;
using HarmonyLib;
using Photon.Pun;
using System;

namespace NEG.BetterChatReborn.Patches
{
	[HarmonyPatch(typeof(DevConsole), RPCASENDCHAT)]
	internal sealed class RPCADevPatch
	{
		private const string RPCASENDCHAT = "RPCA_SendChat";

		private static bool Prefix(string message, int playerViewID)
		{
			if(string.IsNullOrEmpty(message))
				return false;

			var _playerView = PhotonNetwork.GetPhotonView(playerViewID);
			if(!_playerView.IsMine)
				return false;

			var _player = _playerView.GetComponent<Player>();
			MenuControllerHandler.instance.GetComponent<PhotonView>().
				RPC(nameof(ChatMenuManager.Instance.MessageManager.RPCA_CreateMessage),
				RpcTarget.All, _player, new MessageData(_player, message,
				ChatTarget.All, DateTime.Now), true);
			return false;
		}
	}
}