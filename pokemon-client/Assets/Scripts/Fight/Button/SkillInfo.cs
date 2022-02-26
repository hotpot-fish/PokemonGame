using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
/// <summary>

// 继承：按下，抬起和离开的三个接口
public class SkillInfo : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
{
	// 延迟时间
	private float delay = 0.2f;

	// 按钮是否是按下状态
	private bool isDown = false;

	// 按钮最后一次是被按住状态时候的时间
	private float lastIsDownTime;

	GameObject info;

	void Start() {
		info = gameObject.transform.GetChild(1).gameObject;
		info.SetActive(false);
	}


	void Update()
	{
		// 如果按钮是被按下状态
		if (isDown)
		{
			// 当前时间 -  按钮最后一次被按下的时间 > 延迟时间0.2秒
			if (Time.time - lastIsDownTime > delay)
			{
				// 触发长按方法
				info.SetActive(true);
				// 记录按钮最后一次被按下的时间
				lastIsDownTime = Time.time;
				isDown = false;
			}
		}
	}

	// 当按钮被按下后系统自动调用此方法
	public void OnPointerDown(PointerEventData eventData)
	{
		isDown = true;
		lastIsDownTime = Time.time;
	}

	// 当按钮抬起的时候自动调用此方法
	public void OnPointerUp(PointerEventData eventData)
	{
		info.SetActive(false);
		isDown = false;
	}

	// 当鼠标从按钮上离开的时候自动调用此方法
	public void OnPointerExit(PointerEventData eventData)
	{
		info.SetActive(false);
		isDown = false;
	}
}