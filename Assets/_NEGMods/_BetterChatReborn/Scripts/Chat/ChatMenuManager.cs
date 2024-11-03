using NEG.BetterChatReborn.Chat.Messages;
using UnityEngine;
using System;
using UnboundLib;

namespace NEG.BetterChatReborn.Chat
{
	public sealed class ChatMenuManager : MonoBehaviour
	{
		public static ChatMenuManager Instance { get; private set; }
		public ChatMessageManager MessageManager { get; private set; }
		public ChatHooks Hooks { get; private set; }
		public bool PreviewOpen { get; private set; } = false;
		public bool MenuOpen { get; private set; } = false;

		internal Action OnMenuDisable { get; set; }
		internal Action OnMenuEnable { get; set; }
		internal RectTransform MenuBase { get; private set; }

		private (GameObject Object, Canvas Canvas, CanvasGroup Group) chatCanvas;
		private bool cancelPreview;

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

			Hooks = gameObject.AddComponent<ChatHooks>();
			Hooks.Init(MenuBase.gameObject);
			MessageManager = gameObject.AddComponent<ChatMessageManager>();
			MessageManager.Init(Hooks.ChatContainerDetails);

			DisableChat();

			UnityEngine.Debug.Assert(Hooks != null, $"'{nameof(ChatHooks)}' is null");
			UnityEngine.Debug.Assert(MessageManager != null, $"'{nameof(ChatMessageManager)}' is null");

			UnityEngine.Debug.Log($"[{nameof(BetterChatReborn)}] Initialised");
		}

		// This sorta defeats seperating ChatMenuManager and ChatMessagemanager,
		// as these were to seperate role of responsibility.
		public void EnablePreview()
		{
			this.ExecuteAfterSeconds(10, DisablePreview);

			PreviewOpen = true;

			MenuBase.gameObject.SetActive(true);
			chatCanvas.Group.blocksRaycasts = false;
			UnityEngine.Debug.Assert(MenuBase.gameObject.activeSelf, "Preview not enabled");
			if(MenuBase.gameObject.activeSelf)
			{
				UnityEngine.Debug.Log("Preview Enabled");
			}
			Hooks.ChatContainerDetails.MessageInputField.gameObject.SetActive(false);
		}
		public void EnableChat()
		{
			MenuOpen = true;
			OnMenuEnable?.Invoke();
			MenuBase.gameObject.SetActive(true);
			Hooks.ChatContainerDetails.MessageInputField.gameObject.SetActive(true);
			chatCanvas.Group.blocksRaycasts = true;
			UnityEngine.Debug.Assert(MenuBase.gameObject.activeSelf, "Menu not enabled");
			if(MenuBase.gameObject.activeSelf)
			{
				UnityEngine.Debug.Log("Menu Enabled");
			}
		}
		public void DisablePreview()
		{
			if(cancelPreview)
			{
				cancelPreview = false;
				return;
			}
			if(MenuOpen)
			{
				return;
			}

			PreviewOpen = false;
			MenuBase.gameObject.SetActive(false);
			chatCanvas.Group.blocksRaycasts = false;
			UnityEngine.Debug.Assert(!MenuBase.gameObject.activeSelf, "Preview not disabled");
			if(!MenuBase.gameObject.activeSelf)
			{
				UnityEngine.Debug.Log("Preview Disabled");
			}
		}
		public void CancelDisablePostPreview()
		{
			cancelPreview = true;
		}
		public void DisableChat()
		{
			MenuOpen = false;
			OnMenuDisable?.Invoke();
			MenuBase.gameObject.SetActive(false);
			chatCanvas.Group.blocksRaycasts = false;
			UnityEngine.Debug.Assert(!MenuBase.gameObject.activeSelf, "Menu not disabled");
			if(!MenuBase.gameObject.activeSelf)
			{
				UnityEngine.Debug.Log("Menu Disabled");
			}
		}
		public void ClearChatAndDisable()
		{
			DisableOldChats();
			ChatHistory.ClearHistory();
			DisableChat();
		}
		public void ClearChat()
		{
			DisableOldChats();
			ChatHistory.ClearHistory();
		}
		private void DisableOldChats()
		{
			var _children = Hooks.ChatContainerDetails.ScrollRect.content.transform.childCount;
			for(int i = 0; i < _children; i++)
			{
				var _child = Hooks.ChatContainerDetails.ScrollRect.content.transform.GetChild(i);
				_child.gameObject.SetActive(false);
			}
			Hooks.ChatContainerDetails.ScrollRect.content.transform.DetachChildren();
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