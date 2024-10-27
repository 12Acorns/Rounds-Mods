using NEG.BetterChatReborn.Chat;
using UnityEngine;
using HarmonyLib;
using System;

[HarmonyPatch(typeof(Input), nameof(Input.GetKeyDown), new Type[] { typeof(KeyCode) })]
public sealed class InputPatch
{
	private static bool Prefix(Input __instance, KeyCode key)
	{
		var _lastInputState = ChatMenuManager.Instance.Hooks.LastInputLockingState;
		return !(_lastInputState && key != KeyCode.Return && key != KeyCode.UpArrow && key != KeyCode.DownArrow);
	}
}
