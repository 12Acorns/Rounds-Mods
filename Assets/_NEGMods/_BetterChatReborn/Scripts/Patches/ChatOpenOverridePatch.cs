using NEG.BetterChatReborn.Chat;
using UnityEngine;
using Photon.Pun;
using HarmonyLib;

namespace NEG.BetterChatReborn.Patches
{
	[HarmonyPatch(typeof(DevConsole), "Update")]
	internal sealed class ChatOpenOverridePatch
	{
		private static bool Prefix(DevConsole __instance)
		{
			if(Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.Return))
			{
				return true;
			}
			if(Input.GetKeyDown(KeyCode.Escape) && ChatMenuManager.Instance.MenuOpen)
			{
				ChatMenuManager.Instance.DisableChat();
				return false;
			}
			if(Input.GetKeyDown(KeyCode.Return))
			{
				UnityEngine.Debug.Assert(PhotonNetwork.IsConnected, "PhotonNetwork is not connected");
				if(__instance.inputField.gameObject.activeSelf)
				{
					return true;
				}
				if(PhotonNetwork.IsConnected && !ChatMenuManager.Instance.MenuOpen)
				{
					if(ChatMenuManager.Instance.PreviewOpen)
					{
						ChatMenuManager.Instance.DisablePreview();
						ChatMenuManager.Instance.CancelDisablePostPreview();
					}
					ChatMenuManager.Instance.EnableChat();
				}
				return false;
			}
			if(Input.anyKeyDown &&
				!(Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.Mouse1)
				|| Input.GetKeyDown(KeyCode.Mouse2) || Input.GetKeyDown(KeyCode.Mouse3)
				|| Input.GetKeyDown(KeyCode.Mouse4) || Input.GetKeyDown(KeyCode.Mouse5)
				|| Input.GetKeyDown(KeyCode.Mouse6)) && ChatMenuManager.Instance.MenuOpen)
			{
				ChatMenuManager.Instance.Hooks.ChatContainerDetails.MessageInputField.Select();
			}
			return false;
		}
	}
}