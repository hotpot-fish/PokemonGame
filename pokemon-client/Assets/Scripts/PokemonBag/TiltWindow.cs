//搭载在Scene下的plane组件上，使界面可以随点击而晃动，可以不要
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiltWindow : MonoBehaviour {

    public Vector2 range = new Vector2(5f, 3f);

    Transform mTrans;
    Quaternion mStart;
    Vector2 mRot = Vector2.zero;

    // Use this for initialization
    void Start () {
        mTrans = transform; // 获得panel的transform
        mStart = mTrans.localRotation; // 获得panel的自身旋转角度
    }
	
	// Update is called once per frame
	void Update () {
        Vector3 pos = Input.mousePosition;
        float halfWidth = Screen.width * 0.5f;
        float halfHeight = Screen.height * 0.5f;
        float x = Mathf.Clamp((pos.x - halfWidth) / halfWidth, -1f, 1f);
        float y = Mathf.Clamp((pos.y - halfHeight) / halfHeight, -1f, 1f);
        mRot = Vector2.Lerp(mRot, new Vector2(x, y), Time.deltaTime * 5f);
        // 根据鼠标移动修改panel旋转角度
        mTrans.localRotation = mStart * Quaternion.Euler(-mRot.y * range.y, mRot.x * range.x, 0f);
    }
}
