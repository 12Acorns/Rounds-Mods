using UnboundLib.Cards;
using HarmonyLib;
using BepInEx;

namespace NEG.UltraCards
{
	[BepInDependency("com.willis.rounds.unbound")]
	[BepInDependency("pykess.rounds.plugins.moddingutils")]
	[BepInDependency("pykess.rounds.plugins.cardchoicespawnuniquecardpatch")]
	[BepInPlugin(MODID, MODNAME, MODVERSION)]
	[BepInProcess("Rounds.exe")]
	public sealed class UltraRoundsEntry : BaseUnityPlugin
	{
		private const string MODID = "com.NEG.UltraCards";
		private const string MODNAME = "UltraCards";
		private const string MODVERSION = "1.0.0";
		public const string MODINITIALS = "UC";

		void Awake()
		{
			var harmony = new Harmony(MODID);
			harmony.PatchAll();
		}
		void Start()
		{
			CustomCard.BuildCard<RailGunCard>();
		}
	}
}