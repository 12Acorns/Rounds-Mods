using NEG.BetterChatReborn.Chat;
using UnityEngine;
using HarmonyLib;

namespace NEG.BetterChatReborn.Patches
{
	[HarmonyPatch(typeof(MenuControllerHandler), "Start")]
	public sealed class HandlerStartPatch : MonoBehaviour
	{
#pragma warning disable IDE0051 // Remove unused private members
		private static void Postfix(MenuControllerHandler __instance)
		{
			if(__instance == MenuControllerHandler.instance)
			{
				__instance.gameObject.AddComponent<ChatMenuManager>();
			}
		}
#pragma warning restore IDE0051 // Remove unused private members
	}
}