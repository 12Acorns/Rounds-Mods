using NEG.BetterChatReborn.Chat.Messages;
using UnityEngine;
using System;

namespace NEG.BetterChatReborn.Chat
{
	public sealed class ChatMenuManager : MonoBehaviour
	{
		public static ChatMenuManager Instance { get; private set; }
		public ChatMessageManager MessageManager { get; private set; }
		public ChatHooks Hooks { get; private set; }

		internal Action OnMenuDisable { get; set; }
		internal Action OnMenuEnable { get; set; }
		internal RectTransform MenuBase { get; private set; }

		private (GameObject Object, Canvas Canvas, CanvasGroup Group) chatCanvas;

		void Awake()
		{
			if(Instance != null)
			{
				var _exception = new Exception($"Cannot have more than one {nameof(ChatMenuManager)} instance at a time");
				UnityEngine.Debug.LogException(_exception);
				return;
			}
			Instance = this;

			InstantiateCanvas();

			var _menuBaseAsset = BetterChatRebornEntry.BetterChatRebornAssets.LoadAsset<GameObject>("Chat-Container")
				.GetComponent<RectTransform>();
			MenuBase = Instantiate(_menuBaseAsset, chatCanvas.Object.transform);
			UnityEngine.Debug.Assert(MenuBase != null, "Could not find Chat-Container.");

			UnityEngine.Debug.Log("Menu Name: " + MenuBase.gameObject.name);

			Hooks = new ChatHooks(MenuBase.gameObject);
			MessageManager = new ChatMessageManager(MenuBase.gameObject);

			DisableChat();

			UnityEngine.Debug.Assert(Hooks != null, $"'{nameof(ChatHooks)}' is null");
			UnityEngine.Debug.Assert(MessageManager != null, $"'{nameof(ChatMessageManager)}' is null");

			UnityEngine.Debug.Log($"[{nameof(BetterChatReborn)}] Initialised");
		}

		public void EnableChat()
		{
			OnMenuEnable?.Invoke();
			MenuBase.gameObject.SetActive(true);
			chatCanvas.Group.blocksRaycasts = true;
			UnityEngine.Debug.Assert(MenuBase.gameObject.activeSelf, "Menu not enabled");
			if(MenuBase.gameObject.activeSelf)
			{
				UnityEngine.Debug.Log("Menu Enabled");
			}
		}
		public void DisableChat()
		{
			OnMenuDisable?.Invoke();
			MenuBase.gameObject.SetActive(false);
			chatCanvas.Group.blocksRaycasts = false;
			UnityEngine.Debug.Assert(!MenuBase.gameObject.activeSelf, "Menu not disabled");
			if(!MenuBase.gameObject.activeSelf)
			{
				UnityEngine.Debug.Log("Menu Disabled");
			}
		}
		public void ClearChat()
		{
			DisableOldChats();
			ChatHistory.ClearHistory();
		}
		private void DisableOldChats()
		{
			var _children = Hooks.ChatTextContainer.transform.childCount;
			for(int i = 0; i < _children; i++)
			{
				var _child = Hooks.ChatTextContainer.transform.GetChild(i);
				_child.gameObject.SetActive(false);
				_child.SetParent(null);
			}
		}
		private void InstantiateCanvas()
		{
			var _canvasAsset = BetterChatRebornEntry.BetterChatRebornAssets.LoadAsset<GameObject>("Chat-Canvas");
			UnityEngine.Debug.Assert(_canvasAsset != null, "Could not get Chat-Canvas");

			chatCanvas.Object = Instantiate(_canvasAsset);
			chatCanvas.Canvas = chatCanvas.Object.GetComponent<Canvas>();
			chatCanvas.Group = chatCanvas.Object.GetComponent<CanvasGroup>();
			DontDestroyOnLoad(chatCanvas.Object);
		}
	}
}