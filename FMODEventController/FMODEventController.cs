using UnityEngine;
using FMODUnity;

public class FMODEventController : MonoBehaviour
{
	[Header("触发对象")]
	[SerializeField] private GameObject startTrigger; // 启用时播放事件的物件
	[SerializeField] private GameObject stopTrigger;  // 禁用时停止事件的物件

	[Header("FMOD事件")]
	[SerializeField] private EventReference fmodEvent; // 在Inspector中拖拽FMOD事件路径

	private FMOD.Studio.EventInstance eventInstance;
	private bool wasStartActive = false;
	private bool wasStopActive = true; // 初始假设stopTrigger是激活的

	void Start()
	{
		// 初始化状态
		if (startTrigger != null)
		{
			wasStartActive = startTrigger.activeSelf;
		}
		if (stopTrigger != null)
		{
			wasStopActive = stopTrigger.activeSelf;
		}

		// 如果startTrigger初始就激活，立即播放
		if (startTrigger != null && startTrigger.activeSelf)
		{
			PlayEvent();
		}
	}

	void Update()
	{
		// 检测startTrigger从禁用变为启用
		if (startTrigger != null)
		{
			bool isStartActive = startTrigger.activeSelf;
			if (!wasStartActive && isStartActive)
			{
				PlayEvent();
			}
			wasStartActive = isStartActive;
		}

		// 检测stopTrigger从启用变为禁用
		if (stopTrigger != null)
		{
			bool isStopActive = stopTrigger.activeSelf;
			if (wasStopActive && !isStopActive)
			{
				StopEvent();
			}
			wasStopActive = isStopActive;
		}
	}

	private void PlayEvent()
	{
		if (eventInstance.isValid())
		{
			eventInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
			eventInstance.release();
		}

		eventInstance = RuntimeManager.CreateInstance(fmodEvent);
		eventInstance.start();
	}

	private void StopEvent()
	{
		if (eventInstance.isValid())
		{
			eventInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
			eventInstance.release();
			eventInstance.clearHandle(); // 清理实例
		}
	}

	void OnDestroy()
	{
		// 清理事件实例
		if (eventInstance.isValid())
		{
			eventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
			eventInstance.release();
		}
	}
}