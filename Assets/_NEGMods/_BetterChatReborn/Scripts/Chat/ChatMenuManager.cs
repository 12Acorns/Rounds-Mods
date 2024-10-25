using UnityEngine;
using System;

namespace NEG.BetterChatReborn.Chat
{
	public sealed class ChatMenuManager : MonoBehaviour
	{
		public static ChatMenuManager Instance { get; private set; }
		public ChatHooks Hooks { get; private set; }

		internal Action OnMenuDisable;
		internal Action OnMenuEnable;
		internal GameObject MenuBase;

		void Awake()
		{
			if(Instance != null)
			{
				var _exception = new Exception($"Cannot have more than one {nameof(ChatMenuManager)} instance at a time");
				UnityEngine.Debug.LogException(_exception);
				return;
			}
			Instance = this;

			MenuBase = BetterChatRebornEntry.BetterChatRebornAssets.LoadAsset<GameObject>("Chat-Container");
			UnityEngine.Debug.Assert(MenuBase != null, "Menu not found");
			Hooks = new ChatHooks(MenuBase);

			UnityEngine.Debug.Log($"[{nameof(BetterChatReborn)}] Initialised");
		}

		public void EnableChat()
		{
			OnMenuEnable();
			MenuBase.SetActive(true);
		}
		public void DisableChat()
		{
			OnMenuDisable();
			MenuBase.SetActive(false);
		}
	}
}