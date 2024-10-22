using UnboundLib.Cards;
using UnityEngine;

namespace NEG.UltraCards
{
	public sealed class RailGunCard : CustomCard
	{
		public override void SetupCard(CardInfo _cardInfo, Gun _gun, ApplyCardStats _cardStats,
			CharacterStatModifiers _statModifiers, Block _block)
		{
			_gun.damage = 120;
			_gun.recoil = 0.25f;
			_gun.bodyRecoil = 0.05f;
			_gun.shake = 0.4f;
			_gun.reflects = 0;
			_gun.projectileSpeed = 200;
			_gun.gravity = 0.05f;
			_gun.drag = 0.1f;
			_gun.chargeDamageMultiplier = 1.05f;
			_gun.ammo = 1;
			_gun.reloadTime = 2.5f;
			_gun.unblockable = true;
			_gun.useCharge = true;
			_gun.spread = 0;
			_gun.bursts = 1;
			_gun.bulletDamageMultiplier = 1.25f;
		}
		public override void OnAddCard(Player _player, Gun _gun, GunAmmo _gunAmmo,
			CharacterData _data, HealthHandler _health, Gravity _gravity,
			Block _block, CharacterStatModifiers _characterStats)
		{
		}
		public override void OnRemoveCard(Player _player, Gun _gun, GunAmmo _gunAmmo, CharacterData _data,
			HealthHandler _health, Gravity _gravity, Block _block, CharacterStatModifiers _characterStats)
		{
			_gun.damage = Mathf.Max(1, _gun.damage -= 120);
			_gun.recoil = Mathf.Max(0, _gun.recoil -= 0.25f);
			_gun.bodyRecoil = Mathf.Max(0, _gun.bodyRecoil -= 0.05f);
			_gun.shake = Mathf.Max(0, _gun.shake -= 0.4f);
		}
		protected override GameObject GetCardArt()
		{
			return null;
		}
		protected override string GetDescription()
		{
			return
@"
Converts your gun into a RailGun.
A single, powerful bullet which can penertrate surfaces and obliterate anything within its path.
Charge to increase damage and penertration.
";
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
					amount = "120 Base Damage",
					stat = "Damage"
				},
				new CardInfoStat()
				{
					positive = true,
					amount = "+ 25%",
					stat = "Bullet Damage"
				},
				new CardInfoStat()
				{
					positive = true,
					amount = "20m",
					stat = "Penertration"
				},
				new CardInfoStat()
				{
					positive = false,
					amount = "0.25",
					stat = "Recoil"
				},
				new CardInfoStat()
				{
					positive = false,
					amount = "0.05",
					stat = "Body Recoil"
				},
				new CardInfoStat()
				{
					positive = false,
					amount = "0.4",
					stat = "Shake"
				},
				new CardInfoStat()
				{
					positive = false,
					amount = "2.5 Seconds",
					stat = "Reload Time"
				}
			};
		}
		protected override CardThemeColor.CardThemeColorType GetTheme()
		{
			return CardThemeColor.CardThemeColorType.FirepowerYellow;
		}
		protected override string GetTitle()
		{
			return "RailGun";
		}
		public override string GetModName()
		{
			return UltraRoundsEntry.MODINITIALS;
		}
	}
}