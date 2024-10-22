using UnboundLib.Cards;
using Jotunn.Utils;
using UnityEngine;
using HarmonyLib;
using BepInEx;
using System.IO;

namespace NEG.UltraCards
{
	[BepInDependency("com.willis.rounds.unbound")]
	[BepInDependency("pykess.rounds.plugins.moddingutils")]
	[BepInDependency("pykess.rounds.plugins.cardchoicespawnuniquecardpatch")]
	[BepInPlugin(MODID, MODNAME, MODVERSION)]
	[BepInProcess("Rounds.exe")]
	public sealed class UltraCardsEntry : BaseUnityPlugin
	{
		private const string MODID = "com.NEG.UltraCards";
		private const string MODNAME = "UltraCards";
		private const string MODVERSION = "1.0.0";
		public const string MODINITIALS = "UC";

		private static readonly string path = typeof(UltraCardsEntry).Assembly.Location;
		private static readonly string ultraCardsLocation = Directory.GetParent(path).FullName
			.Substring(Paths.PluginPath.Length + 1) + "\\neg_ultracards";

		public static AssetBundle UltraCardsAssets { get; } = 
			AssetUtils.LoadAssetBundle(ultraCardsLocation);

		void Awake()
		{
			var _harmony = new Harmony(MODID);
			_harmony.PatchAll();
			UnityEngine.Debug.Log("Target Path Minus DLL: " + path);
			UnityEngine.Debug.Log("Base Path: " + Directory.GetParent(path).FullName);
			UnityEngine.Debug.Log("Relative Target Path: " + ultraCardsLocation);
			UnityEngine.Debug.Log("Full Path: " + Paths.PluginPath + ultraCardsLocation);
		}
		void Start()
		{
			CustomCard.BuildCard<RailGunCard>();
			CustomCard.BuildCard<ParticleAcceleratorCard>();
		}
	}
}