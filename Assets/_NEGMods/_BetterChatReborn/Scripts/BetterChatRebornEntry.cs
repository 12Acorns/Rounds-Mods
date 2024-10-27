using NEG.BetterChatReborn.Chat;
using UnboundLib.GameModes;
using System.Collections;
using Jotunn.Utils;
using UnityEngine;
using HarmonyLib;
using System.IO;
using BepInEx;

namespace NEG.BetterChatReborn
{
	[BepInDependency("com.willis.rounds.unbound")]
	[BepInDependency("pykess.rounds.plugins.moddingutils")]
	[BepInPlugin(MODID, MODNAME, MODVERSION)]
	[BepInProcess("Rounds.exe")]
	public sealed class BetterChatRebornEntry : BaseUnityPlugin
	{
		public const string MODID = "com.NEG.BetterChatReborn";
		public const string MODNAME = "BetterChat Reborn";
		public const string MODVERSION = "1.0.0";

		private static readonly string path = typeof(BetterChatRebornEntry).Assembly.Location;
		private static readonly string betterChatRebornLocation = Directory.GetParent(path).FullName
			.Substring(Paths.PluginPath.Length + 1) + "\\neg_betterchatreborn";

		public static AssetBundle BetterChatRebornAssets { get; } =
			AssetUtils.LoadAssetBundle(betterChatRebornLocation);

		void Awake()
		{
			UnityEngine.Debug.Assert(BetterChatRebornAssets != null, "Could not load required assets");
			
			var _harmony = new Harmony(MODID);
			_harmony.PatchAll();

			ConfigBindings.InitBindings(this);
			MenuConfig.Init();

			GameModeManager.AddHook(GameModeHooks.HookBattleStart, OnBattleStart);
		}

		private static IEnumerator OnBattleStart(IGameModeHandler _handler)
		{

			yield return null;
		}
	}
}