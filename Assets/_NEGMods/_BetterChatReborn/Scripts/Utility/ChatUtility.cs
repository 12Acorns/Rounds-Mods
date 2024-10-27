using System;
using System.Collections.Generic;
using UnboundLib.Extensions;
using UnityEngine;

namespace NEG.BetterChatReborn.Utility
{
	internal static class ChatUtility
	{
		public static string GetPlayerColour(Player _player)
		{
			var _colourID = _player.colorID();
			var _colour = PlayerSkinBank.GetPlayerSkinColors(_colourID).color;
			if(_colourID == -1)
			{
				return "white";
			}
			return $"#{ColorUtility.ToHtmlStringRGB(_colour)}";
		}
	}
}