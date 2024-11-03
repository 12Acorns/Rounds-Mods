using NEG.BetterChatReborn.Chat;
using UnityEngine;
using HarmonyLib;

namespace NEG.BetterChatReborn.Patches
{
	[HarmonyPatch(typeof(MenuControllerHandler), "Start")]
	public sealed class HandlerStartPatch : MonoBehaviour
	{
		private static void Postfix()
		{
			MenuControllerHandler.instance.gameObject.AddComponent<ChatMenuManager>();
			UnityEngine.Debug.Assert(MenuControllerHandler.instance.gameObject.GetComponent<ChatMenuManager>() != null, 
				$"Could not add {nameof(ChatMenuManager)}");
		}
	}
}