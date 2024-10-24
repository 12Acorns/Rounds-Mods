using UnboundLib.Cards;
using UnityEngine;

namespace NEG.UltraCards
{
	public sealed class RailGunCard : CustomCard
	{
		private const float DAMAGEMULTIPLIER = 3;
		private const float PROJECTILESPEEDMULTIPLIER = 12;
		private const float RECOILMULTIPLIER = 1.8f;
		private const float RELOADTIMEADDITIVE = 2.5f;

		private static readonly GameObject railObjectAsset =
			UltraCardsEntry.UltraCardsAssets.LoadAsset<GameObject>("A_RailDrillAmmo");
		private static readonly RayHitDrill railObjectDrill = railObjectAsset.GetComponent<RayHitDrill>();

		public override void SetupCard(CardInfo _cardInfo, Gun _gun, ApplyCardStats _cardStats,
			CharacterStatModifiers _statModifiers, Block _block)
		{
			UnityEngine.Debug.Log($"Rail Object Asset: {railObjectAsset.name}");

			_gun.useCharge = true;
			_gun.chargeSpeedTo = 1.2f;
			_gun.chargeRecoilTo = 1.5f;
			_gun.chargeDamageMultiplier = 1.5f;

			_gun.damageAfterDistanceMultiplier = 0.9f;
			_gun.damage = DAMAGEMULTIPLIER;
			_gun.recoil = RECOILMULTIPLIER;
			_gun.bodyRecoil = 2f;
			_gun.recoilMuiltiplier = 1.05f;
			_gun.shake = 1.2f;
			_gun.reflects = 0;
			_gun.projectileSpeed = PROJECTILESPEEDMULTIPLIER;
			_gun.gravity = 1.005f;
			_gun.drag = 1.01f;
			_gun.knockback = 1.75f;
			_gun.lockGunToDefault = true;
			_gun.reloadTimeAdd = RELOADTIMEADDITIVE;
			_gun.unblockable = true;
			_gun.bulletDamageMultiplier = 1.25f;
			_gun.projectileColor = Color.red;
			var _projectile = new ObjectsToSpawn()
			{
				numberOfSpawns = 1,
				AddToProjectile = railObjectAsset,
				scaleStackM = 0.8f,
			};
			_gun.objectsToSpawn = new ObjectsToSpawn[]
			{
				_projectile
			};

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
			//return new GameObject("RailGun Card Art");
		}
		protected override string GetDescription()
		{
			return
@"A single, powerful bullet which penertrates surfaces and obliterate anything in its path.
Charge to increase damage and penertration.";
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
					amount = $"+{DAMAGEMULTIPLIER * 100}%",
					stat = "Damage",
					simepleAmount = CardInfoStat.SimpleAmount.notAssigned
				},
				new CardInfoStat()
				{
					positive = true,
					amount = $"+{PROJECTILESPEEDMULTIPLIER * 100}%",
					stat = "Bullet Speed",
					simepleAmount = CardInfoStat.SimpleAmount.notAssigned
				},
				new CardInfoStat()
				{
					positive = true,
					amount = $"+{railObjectDrill.metersOfDrilling} Meters",
					stat = "Penertration",
					simepleAmount = CardInfoStat.SimpleAmount.notAssigned
				},
				new CardInfoStat()
				{
					positive = false,
					amount = $"+{RELOADTIMEADDITIVE:n0} Seconds",
					stat = "Reload Time",
					simepleAmount = CardInfoStat.SimpleAmount.notAssigned
				},
				new CardInfoStat()
				{
					positive = false,
					amount = "1 Bullet",
					stat = "Ammo",
					simepleAmount = CardInfoStat.SimpleAmount.notAssigned
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
			return UltraCardsEntry.MODINITIALS;
		}
	}
}