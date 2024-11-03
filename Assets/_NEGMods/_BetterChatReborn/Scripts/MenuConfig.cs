using NEG.BetterChatReborn.Chat;
using UnboundLib.Utils.UI;
using UnityEngine.UI;
using UnityEngine;
using UnboundLib;
using NEG.BetterChatReborn.Chat.Messages;
using System;

namespace NEG.BetterChatReborn
{
	internal static class MenuConfig
	{
		private static readonly float menuWidthMax = Screen.width / 2;
		private static readonly float menuHeightMax = Screen.height / 2;
		private static readonly float menuWidthDefault = Screen.width / 6;
		private static readonly float menuHeightDefault = Screen.width / 6 / 2.06f;

		public static void Init()
		{
			InitUnbound();
		}
		private static void InitUnbound()
		{
			MarkAsClientMod();
			RegisterMenuPreviewInModMenuConfig();
			RegisterHandShake();
		}
		private static void MarkAsClientMod()
		{
			Unbound.RegisterClientSideMod(BetterChatRebornEntry.MODID);
		}
		private static void RegisterHandShake()
		{
			Unbound.RegisterHandshake(BetterChatRebornEntry.MODID, OnHandShakeComplete);
		}
		private static void RegisterMenuPreviewInModMenuConfig()
		{
			Unbound.RegisterMenu("Better Chat Reborn", () =>
			{
				ChatMenuManager.Instance.EnableChat();
				var _messageA = new MessageData("Player 1", "Lorem ipsum odor amet, consectetuer adipiscing elit.",
					Color.red, DateTime.Now);
				var _messageB = new MessageData("Player 2", "Gravida dictum ad egestas cubilia massa gravida adipiscing ad.",
					Color.green, DateTime.Now);
				var _messageC = new MessageData("Player 3", "Ac ultrices lacus euismod consectetur diam natoque nec.",
					Color.yellow, DateTime.Now);
				var _messageD = new MessageData("Player 4", "Orci arcu tristique ridiculus varius venenatis parturient laoreet velit.",
					Color.cyan, DateTime.Now);
				ChatMenuManager.Instance.MessageManager.CreateClientMessage(_messageA);
				ChatMenuManager.Instance.MessageManager.CreateClientMessage(_messageB);
				ChatMenuManager.Instance.MessageManager.CreateClientMessage(_messageC);
				ChatMenuManager.Instance.MessageManager.CreateClientMessage(_messageD);
			}, CreateMenuConfigOptions, null, true);
		}
		private static void CreateMenuConfigOptions(GameObject _menu)
		{
			const int _FONTSIZE = 40;
			const int _MENUSIZEMIN = 100;

			MenuHandler.CreateText("To open Vanilla Chat, press shift + enter\n", _menu, out _, 40);

			var _menuBase = ChatMenuManager.Instance.MenuBase;
			var _menuBaseRect = _menuBase.rect;

			MenuHandler.CreateSlider("Chat Width", _menu, _FONTSIZE, 
				_MENUSIZEMIN, menuWidthMax, ConfigBindings.GetWidth(), _width =>
			{
				ConfigBindings.SetWidth(_width);
				_menuBase.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, _width);
			}, out var _widthSlider, true);
			MenuHandler.CreateSlider("Chat Height", _menu, _FONTSIZE, 
				_MENUSIZEMIN, menuHeightMax, ConfigBindings.GetHeight(), _height =>
			{
				ConfigBindings.SetHeight(_height);
				_menuBase.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, _height);
			}, out var _heightSlider, true);

			MenuHandler.CreateSlider("Chat Position X", _menu, _FONTSIZE, _menuBaseRect.width / 2,
				Screen.width - _menuBaseRect.width, ConfigBindings.GetPosX(), 
				_posX =>
				{
					ConfigBindings.SetPosX((int)_posX);
					_menuBase.SetXPosition(_posX);
				}, out var _posXSlider, true);
			MenuHandler.CreateSlider("Chat Position Y", _menu, _FONTSIZE, _menuBaseRect.height / 2, 
				Screen.height - _menuBaseRect.height, ConfigBindings.GetPosY(), 
				_posY =>
				{
					ConfigBindings.SetPosY((int)_posY);
					_menuBase.SetYPosition(_posY);
				}, out var _posYSlider, true);

			MenuHandler.CreateButton("Reset", _menu, () =>
			{
				_widthSlider.value = Screen.width / 6 / 2.06f;
				_heightSlider.value = Screen.height / 6 / 2.06f;
				_posXSlider.value = Screen.width - _menuBaseRect.width;
				_posYSlider.value = _menuBaseRect.height;
			});

			_menu.transform.Find("Group/Back").gameObject
				.GetComponent<Button>().onClick.AddListener(() =>
			{
				ChatMenuManager.Instance.ClearChat();
				ChatMenuManager.Instance.DisableChat();
			});
		}
		private static void OnHandShakeComplete()
		{

		}
	}
}