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
			var _teamID = _sender.teamID;
			var _targetColour = string.Empty;
			if(_teamID > 0 && _target == ChatTarget.Team)
			{
				_targetColour = ChatUtility.GetPlayerColour(_sender);
			}
			else
			{
				_targetColour = ColorUtility.ToHtmlStringRGB(Color.green);
			}

			Message = _message;
			From = PhotonNetwork.LocalPlayer.NickName;
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
				Target = "All";
			}

			TimeSent = $"{_timeSent.Hour}:{_timeSent.Minute}:{_timeSent.Second}";
			FromColour = ChatUtility.GetPlayerColour(_sender);
			TargetColour = _targetColour;
		}

		public string Message { get; }
		public string From { get; }
		public string Target { get; }
		public string TimeSent { get; }
		public string FromColour { get; }
		public string TargetColour { get; }

		private string LogInvalidTargetAndReturnDefault(ChatTarget _target)
		{
			var _exception = new NotSupportedException($"{_target} is not yet a supported message target");
			UnityEngine.Debug.LogException(_exception);
			return "All";
		}
	}
}