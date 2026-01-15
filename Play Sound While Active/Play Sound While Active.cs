using UnityEngine;
using FMODUnity;

public class PlaySoundWhileActive : MonoBehaviour
{
	public EventReference eventPath; // 在Inspector中设置FMOD事件路径

	private FMOD.Studio.EventInstance eventInstance;

	private void OnEnable()
	{
		// 当GameObject被启用时，创建并启动FMOD事件实例
		eventInstance = RuntimeManager.CreateInstance(eventPath);
		eventInstance.set3DAttributes(RuntimeUtils.To3DAttributes(transform));
		eventInstance.start();
	}

	private void OnDisable()
	{
		// 当GameObject被禁用时，停止并释放FMOD事件实例
		if (eventInstance.isValid())
		{
			eventInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
			eventInstance.release();
		}
	}
}