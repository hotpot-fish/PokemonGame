using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
/// <summary>

// �̳У����£�̧����뿪�������ӿ�
public class SkillInfo : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
{
	// �ӳ�ʱ��
	private float delay = 0.2f;

	// ��ť�Ƿ��ǰ���״̬
	private bool isDown = false;

	// ��ť���һ���Ǳ���ס״̬ʱ���ʱ��
	private float lastIsDownTime;

	GameObject info;

	void Start() {
		info = gameObject.transform.GetChild(1).gameObject;
		info.SetActive(false);
	}


	void Update()
	{
		// �����ť�Ǳ�����״̬
		if (isDown)
		{
			// ��ǰʱ�� -  ��ť���һ�α����µ�ʱ�� > �ӳ�ʱ��0.2��
			if (Time.time - lastIsDownTime > delay)
			{
				// ������������
				info.SetActive(true);
				// ��¼��ť���һ�α����µ�ʱ��
				lastIsDownTime = Time.time;
				isDown = false;
			}
		}
	}

	// ����ť�����º�ϵͳ�Զ����ô˷���
	public void OnPointerDown(PointerEventData eventData)
	{
		isDown = true;
		lastIsDownTime = Time.time;
	}

	// ����ţ̌���ʱ���Զ����ô˷���
	public void OnPointerUp(PointerEventData eventData)
	{
		info.SetActive(false);
		isDown = false;
	}

	// �����Ӱ�ť���뿪��ʱ���Զ����ô˷���
	public void OnPointerExit(PointerEventData eventData)
	{
		info.SetActive(false);
		isDown = false;
	}
}