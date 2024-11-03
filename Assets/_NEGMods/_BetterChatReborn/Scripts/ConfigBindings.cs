using BepInEx;
using BepInEx.Configuration;
using UnityEngine;

namespace NEG.BetterChatReborn
{
	internal static class ConfigBindings
	{
		private static readonly ConfigDefinition menuWidthDefinition =
			new ConfigDefinition(
				"Menu",
				"Width of the chat menu");
		private static readonly ConfigDefinition menuHeightDefinition =
			new ConfigDefinition(
				"Menu",
				"Height of the chat menu");
		private static readonly ConfigDefinition menuPosXDefinition =
			new ConfigDefinition(
				"Menu",
				"Position of the chat menus X position");
		private static readonly ConfigDefinition menuPosYDefinition =
			new ConfigDefinition(
				"Menu",
				"Position of the chat menus Y position");
		private static readonly ConfigDefinition timeColourDefinition =
			new ConfigDefinition(
				"Menu",
				"Colour of the time sent (Give in form (hex) #RRGGBB, use a RGB to hex tool if needed)");

		private static ConfigEntry<float> menuWidth;
		private static ConfigEntry<float> menuHeight;
		private static ConfigEntry<int> menuPosX;
		private static ConfigEntry<int> menuPosY;
		private static ConfigEntry<string> timeColour;

		public static void InitBindings(BaseUnityPlugin _entry)
		{
			menuWidth = _entry.Config.Bind(menuWidthDefinition, 600f);
			menuHeight = _entry.Config.Bind(menuHeightDefinition, 360f);
			menuPosX = _entry.Config.Bind(menuPosXDefinition, Screen.width);
			menuPosY = _entry.Config.Bind(menuPosYDefinition, Screen.height);
			timeColour = _entry.Config.Bind(timeColourDefinition, $"#{ColorUtility.ToHtmlStringRGB(Color.green)}");
		}
		public static void SetWidth(float _width) => menuWidth.Value = _width;
		public static void SetHeight(float _width) => menuHeight.Value = _width;
		public static void SetPosX(int _width) => menuPosX.Value = _width;
		public static void SetPosY(int _width) => menuPosY.Value = _width;
		public static void SetTimeColour(Color _colour) => timeColour.Value = ColorUtility.ToHtmlStringRGB(_colour);

		public static float GetWidth() => menuWidth.Value;
		public static float GetHeight() => menuHeight.Value;
		public static int GetPosX() => menuPosX.Value;
		public static int GetPosY() => menuPosY.Value;
		public static string GetTimeColour() => timeColour.Value;
	}
}