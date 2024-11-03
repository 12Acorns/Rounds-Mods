using UnityEngine;

namespace NEG.BetterChatReborn.Extensions
{
	internal static class ClipBoardExtensions
	{
		public static void CopyToClipBoard(string _message)
		{
			var _clipBoard = new TextEditor
			{
				text = _message
			};
			_clipBoard.SelectAll();
			_clipBoard.Copy();
		}
	}
}