using NEG.BetterChatReborn.Chat;
using HarmonyLib;

namespace NEG.BetterChatReborn.Patches
{
	[HarmonyPatch(typeof(CardChoice), "DoPlayerSelect")]
	[HarmonyPriority(Priority.First)]
	internal class CardSelectPatch
	{
		private static bool Prefix()
		{
			var _lockingInput = ChatMenuManager.Instance.Hooks.LastInputLockingState;
			return !_lockingInput;
		}
	}
}