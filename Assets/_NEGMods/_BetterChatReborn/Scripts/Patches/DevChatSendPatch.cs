using System.Reflection;
using UnityEngine;
using Photon.Pun;
using HarmonyLib;

namespace NEG.BetterChatReborn.Patches
{
	[HarmonyPatch(typeof(DevConsole), "Send")]
	internal sealed class DevChatSendPatch
	{
		private static bool Prefix(DevConsole __instance, string message)
		{
			if(string.IsNullOrWhiteSpace(message))
				return false;
			var _devType = typeof(DevConsole);
			if(Application.isEditor || (GM_Test.instance && GM_Test.instance.gameObject.activeSelf))
			{
				_devType.InvokeMember("SpawnCard", BindingFlags.Instance | BindingFlags.InvokeMethod | BindingFlags.NonPublic, null, MenuControllerHandler.instance.GetComponent<DevConsole>(), new object[] { message });
			}
			if(Application.isEditor)
			{
				_devType.InvokeMember("SpawnMap", BindingFlags.Instance | BindingFlags.InvokeMethod | BindingFlags.NonPublic, null, MenuControllerHandler.instance.GetComponent<DevConsole>(), new object[] { message });
			}

			var _viewID = ((Player)typeof(PlayerManager).InvokeMember("GetPlayerWithActorID",
				BindingFlags.Instance | BindingFlags.InvokeMethod | BindingFlags.NonPublic, null,
				PlayerManager.instance, new object[] { PhotonNetwork.LocalPlayer.ActorNumber })).data.view.ViewID;

			__instance.GetComponent<PhotonView>().RPC("RPCA_SendChat", RpcTarget.Others, new object[]
			{
				"‌" + message,
				_viewID
			});
			return false;
		}
	}
}