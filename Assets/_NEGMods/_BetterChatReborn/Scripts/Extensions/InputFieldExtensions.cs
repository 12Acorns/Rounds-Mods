using UnityEngine.EventSystems;
using TMPro;

namespace NEG.BetterChatReborn.Extensions
{
	internal static class InputFieldExtensions
	{
		public static void DoSelectEvent(this TMP_InputField _inputField)
		{
			_inputField.OnSelect(new BaseEventData(EventSystem.current));
			_inputField.Select();
		}
		public static void DoDeselectEvent(this TMP_InputField _inputField)
		{
			_inputField.OnDeselect(new BaseEventData(EventSystem.current));
			_inputField.ReleaseSelection();
		}
	}
}