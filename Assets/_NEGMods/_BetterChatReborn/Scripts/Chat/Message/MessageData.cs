using NEG.BetterChatReborn.Utility;
using UnityEngine;
using Photon.Pun;
using System;

namespace NEG.BetterChatReborn.Chat.Messages
{
	public readonly struct MessageData
	{
		public MessageData(Player _sender, string _message, ChatTarget _target, DateTime _timeSent)
		{
			UnityEngine.Debug.Assert(_sender != null, $"{nameof(_sender)} is null");
			UnityEngine.Debug.Assert(!string.IsNullOrEmpty(_message), $"{nameof(_message)} is null");
			UnityEngine.Debug.Assert(_timeSent != null, $"{nameof(_timeSent)} is null");

			var _teamID = _sender.teamID;
			string _targetColour;
			if(_teamID > 0 && _target == ChatTarget.Team)
			{
				_targetColour = ChatUtility.GetPlayerColourHTML(_sender);
			}
			else
			{
				_targetColour = ColorUtility.ToHtmlStringRGB(Color.green);
			}

			Message = _message;
			From = PhotonNetwork.LocalPlayer?.NickName;
			From = string.IsNullOrEmpty(From) ? "Player 1" : From;
			if(_target == ChatTarget.All)
			{
				Target = "All";
			}
			else if(_target == ChatTarget.Team)
			{
				Target = "Team";
			}
			else
			{
				Target = LogInvalidTargetAndReturnDefault(_target);
			}

			TimeSent = $"{_timeSent.Hour}:{_timeSent.Minute}:{_timeSent.Second}";
			FromColour = ChatUtility.GetPlayerColourHTML(_sender);
			TargetColour = _targetColour;
		}
		/// <summary>
		/// Dummy message, only intended to be used for displaying a example message
		/// </summary>
		public MessageData(string _fromName, string _message, Color _colour, DateTime _timeSent)
		{
			Message = _message;
			From = _fromName;
			Target = "All";
			TimeSent = $"{_timeSent.Hour}:{_timeSent.Minute}:{_timeSent.Second}";
			FromColour = ColorUtility.ToHtmlStringRGB(_colour);
			TargetColour = ColorUtility.ToHtmlStringRGB(Color.white);
		}

		public string Message { get; }
		public string From { get; }
		public string Target { get; }
		public string TimeSent { get; }
		public string FromColour { get; }
		public string TargetColour { get; }

		private static string LogInvalidTargetAndReturnDefault(ChatTarget _target)
		{
			var _exception = new NotSupportedException($"{_target} is not yet a supported message target");
			UnityEngine.Debug.LogException(_exception);
			return "All";
		}
	}
}