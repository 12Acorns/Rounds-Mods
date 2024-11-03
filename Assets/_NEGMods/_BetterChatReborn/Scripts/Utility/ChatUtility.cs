using UnboundLib.Extensions;
using UnityEngine;

namespace NEG.BetterChatReborn.Utility
{
	internal static class ChatUtility
	{
		/// <returns>The string with a # at start if not present, does not modify <paramref name="_input"/></returns>
		public static string EnsureHasHash(string _input)
		{
			if(string.IsNullOrEmpty(_input))
			{
				return _input;
			}
			if(_input[0] != '#')
			{
				return $"#{_input}";
			}
			return _input;
		}
		/// <returns>A RGB string in form (hex) #RRGGBB</returns>
		public static string GetPlayerColourHTML(Player _player)
		{
			var _colour = GetPlayerColour(_player);
			return $"#{ColorUtility.ToHtmlStringRGB(_colour)}";
		}
		/// <returns>The colour of this players skin</returns>
		public static Color GetPlayerColour(Player _player)
		{
			var _colourID = _player.colorID();
			if(_colourID == -1)
			{
				return Color.white;
			}
			return PlayerSkinBank.GetPlayerSkinColors(_colourID).color;
		}
	}
}