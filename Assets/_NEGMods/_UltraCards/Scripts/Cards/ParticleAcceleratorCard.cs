using UnboundLib.Cards;
using UnityEngine;

namespace NEG.UltraCards
{
	public sealed class ParticleAcceleratorCard : CustomCard
	{
		private const int BOUNCES = 12;
		private const float DAMAGEMULTIPLIERPERBOUNCE = 1.15f;
		private const float SPEEDMULTIPLIERPERBOUNCE = 1.05f;

		private const float VISUALDAMAGEPERCENT = 
			(DAMAGEMULTIPLIERPERBOUNCE * 100) >= 100 
			? (DAMAGEMULTIPLIERPERBOUNCE * 100) - 100 
			: (DAMAGEMULTIPLIERPERBOUNCE * 100);
		private const float VISUALSPEEDPERCENT = 
			(SPEEDMULTIPLIERPERBOUNCE * 100) >= 100
			? (SPEEDMULTIPLIERPERBOUNCE * 100) - 100
			: (SPEEDMULTIPLIERPERBOUNCE * 100);

		public override void SetupCard(CardInfo _cardInfo, Gun _gun, ApplyCardStats _cardStats,
			CharacterStatModifiers _statModifiers, Block _block)
		{
			_gun.reflects = BOUNCES;
			_gun.dmgMOnBounce = DAMAGEMULTIPLIERPERBOUNCE;
			_gun.speedMOnBounce = SPEEDMULTIPLIERPERBOUNCE;

			UnityEngine.Debug.Log($"[{UltraCardsEntry.MODINITIALS}][Card] {GetTitle()} has been setup.");
		}
		public override void OnAddCard(Player _player, Gun _gun, GunAmmo _gunAmmo,
			CharacterData _data, HealthHandler _health, Gravity _gravity,
			Block _block, CharacterStatModifiers _characterStats)
		{
			_gunAmmo.maxAmmo = 1;
			UnityEngine.Debug.Log($"[{UltraCardsEntry.MODINITIALS}][Card] {GetTitle()} has been added to player {_player.playerID}.");
		}
		public override void OnRemoveCard(Player _player, Gun _gun, GunAmmo _gunAmmo, CharacterData _data,
			HealthHandler _health, Gravity _gravity, Block _block, CharacterStatModifiers _characterStats)
		{
			UnityEngine.Debug.Log($"[{UltraCardsEntry.MODINITIALS}][Card] {GetTitle()} has been removed from player {_player.playerID}.");
		}
		protected override GameObject GetCardArt()
		{
			return null;
			//return new GameObject("Particle Accelerator Card Art");
		}
		protected override string GetDescription()
		{
			return
@"Converts your gun into a Particle Accelerator.
A single, slow-starting bullet which will increase in speed and damage per bounce.";
		}
		protected override CardInfo.Rarity GetRarity()
		{
			return CardInfo.Rarity.Rare;
		}
		protected override CardInfoStat[] GetStats()
		{
			return new CardInfoStat[]
			{
			new CardInfoStat()
			{
				positive = true,
				amount = $"+{BOUNCES} Bounces",
				stat = "Bounces",
				simepleAmount = CardInfoStat.SimpleAmount.notAssigned
			},
			new CardInfoStat()
			{
				positive = true,
				amount = $"+{VISUALDAMAGEPERCENT:n0}%/Bounce",
				stat = "Damage",
				simepleAmount = CardInfoStat.SimpleAmount.notAssigned
			},
			new CardInfoStat()
			{
				positive = true,
				amount = $"+{VISUALSPEEDPERCENT:n0}%/Bounce",
				stat = "Speed",
				simepleAmount = CardInfoStat.SimpleAmount.notAssigned
			},
			};
		}
		protected override CardThemeColor.CardThemeColorType GetTheme()
		{
			return CardThemeColor.CardThemeColorType.FirepowerYellow;
		}
		protected override string GetTitle()
		{
			return "Particle Accelerator";
		}
		public override string GetModName()
		{
			return UltraCardsEntry.MODINITIALS;
		}
	}
}