using NEG.BetterChatReborn.Chat;
using UnityEngine;
using HarmonyLib;

namespace NEG.BetterChatReborn.Patches
{
	[HarmonyPatch(typeof(MenuControllerHandler), "Start")]
	public sealed class HandlerStartPatch : MonoBehaviour
	{
		private static void Postfix(MenuControllerHandler __instance)
		{
			if(__instance == MenuControllerHandler.instance)
			{
				__instance.gameObject.AddComponent<ChatMenuManager>();
				UnityEngine.Debug.Assert(__instance.GetComponent<ChatMenuManager>() != null, 
					$"Could not add {nameof(ChatMenuManager)}");
			}
		}
	}
}