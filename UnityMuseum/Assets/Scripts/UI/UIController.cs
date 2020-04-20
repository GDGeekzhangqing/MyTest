using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public enum DragDrawType
{
    Both,
    Left,
    Right
}

public class UIController : MonoBehaviour
{

    #region 变量

    public GameObject LeftController;
    public GameObject RightController;
    public GameObject CameraRig;

    public List<GameObject> ValidDestinationPoints=new List<GameObject>();

    private DragDrawType drawType=DragDrawType.Right;

    public GameObject DrawObj;

    private bool isShowUI = false;
    private bool isDragUI = false;
    private bool isPauseUI = false;

    public int rotaSpeed =15;

    private Vector3 curVec3 = Vector3.zero;
    private Vector3 lastVec3 = Vector3.zero;
    private float dragDistance = 0f;
    #endregion

    private void Awake()
    {
        EventCenter.AddListener(EventDefine.ShowUI,ShowUI);
        EventCenter.AddListener(EventDefine.unShowUI, UnShowUI);
        EventCenter.AddListener<bool>(EventDefine.DragUI, DragUI);
        EventCenter.AddListener<bool>(EventDefine.unDragUI,UnDragUI);
        EventCenter.AddListener<bool>(EventDefine.PauseUI, PauseUI);
        EventCenter.AddListener<bool>(EventDefine.unPauseUI, UnPauseUI);
    }

    private void OnDestory()
    {
        EventCenter.RemoveListener(EventDefine.ShowUI,ShowUI);
        EventCenter.RemoveListener(EventDefine.unShowUI, UnShowUI);
        EventCenter.RemoveListener<bool>(EventDefine.DragUI, DragUI);
        EventCenter.RemoveListener<bool>(EventDefine.unDragUI, UnDragUI);
        EventCenter.RemoveListener<bool>(EventDefine.PauseUI, PauseUI);
        EventCenter.RemoveListener<bool>(EventDefine.unPauseUI, UnPauseUI);
    }
 
    private void Update()
    {
        RoteDrawTrans();
        CalRoteTrans();

        if (isDragUI==false)
        {

        }
    }

    private void LateUpdate()
    {
        //Debug.Log("late");
    }

    /// <summary>
    /// 展示UI
    /// </summary>
    /// <param name="value"></param>
    private void ShowUI()
    {  
        isShowUI =!isShowUI;
        DrawObj.SetActive(isShowUI);
        Debug.Log("Show UI is:"+isShowUI);
    }

    private void UnShowUI()
    {
        Debug.Log("UnShow UI");
    }

    private void DragUI(bool value)
    {
        //是否一直握住
        bool isContinue = false;

        //获取当前手柄的初始位置
        curVec3 = GetLeftCtrlCurPos();
        Debug.Log("curVec3:"+ curVec3);

        if (value==true)
        {
          

        }
        
        if (value==false)
        {
            //获取最后手柄的位置
            lastVec3 = GetLeftCtrlCurPos();
            //计算起点和终点的距离
            dragDistance = Vector3.Distance(curVec3, lastVec3);
            //换算成换转的速率
            float dragSpeed = dragDistance / 100;
            //绕轴旋转
            gameObject.transform.RotateAround(CameraRig.transform.position, CameraRig.transform.up, dragSpeed);
            Debug.Log("这里有被调用到");
        }
        
        

        Debug.Log("Drag UI is:" + value);
    }

    private void UnDragUI(bool value)
    {
        lastVec3 = GetLeftCtrlCurPos();
        Debug.Log("当前位置:"+lastVec3);
        Debug.Log("ReleaseDragUI:" + value);
    }

    private void PauseUI(bool value)
    {
        isPauseUI = true;
        Debug.Log("current PauseUI is:" + value);
    }

    private void UnPauseUI(bool value)
    {
        Debug.Log("Un Pause UI");
    }

    /// <summary>
    /// 按钮点击
    /// </summary>
    /// <param name="index"></param>
    public void ClickDrawBtn(int index)
    {
        Debug.Log("Click Draw Btn,And Which btn is be Click:" + index);
        //界面隐藏
        DrawObj.SetActive(false);
        isShowUI = false;
        MoveToDraw(index);
    }

    /// <summary>
    /// 画移动过来
    /// </summary>
    private void MoveToDraw(int index)
    {
        //保证index与drawObj的映射关系
        //根据Index 拿到对应的位置
        Transform targetTrans = ValidDestinationPoints[index-1].transform;
        //角色移动到对应的画前面
        CameraRig.transform.position = targetTrans.position;
        
        Debug.Log("FlyDraw' position is:"+ValidDestinationPoints[index].transform.position);
        Debug.Log("移动的目标："+targetTrans.gameObject.name);
    }

    /// <summary>
    /// 获取当前左手柄transform
    /// </summary>
    /// <returns></returns>
    private Vector3 GetLeftCtrlCurPos()
    {
        return LeftController.transform.position;
    }

    /// <summary>
    /// 获取当前右手柄transform
    /// </summary>
    /// <returns></returns>
    private Vector3 GetRightCtrlCurPos()
    {
        return RightController.transform.position;
    }

    /// <summary>
    /// 计算X需要旋转角度
    /// </summary>
    private void CalRoteTrans()
    {
        if (isDragUI&&isShowUI)
        {
            switch (drawType)
            {
                case DragDrawType.Both:
                    Debug.Log("It's no things");           
                    break;
                case DragDrawType.Left:
                    lastVec3 = GetLeftCtrlCurPos();    
                    break;
                case DragDrawType.Right:
                    Debug.Log("Nothing can do ");
                    break;
            }

            Debug.Log("Calc Drag UI Rotation: "+lastVec3);
        }
    }

    /// <summary>
    /// 在Update中每帧旋转自身
    /// </summary>
    private void RoteDrawTrans()
    {
        ///Debug.Log("Rote UI...");
        if (isPauseUI!=true&&isShowUI==true)
        {
            gameObject.transform.RotateAround(CameraRig.transform.position, CameraRig.transform.up, rotaSpeed * Time.deltaTime);
            Debug.Log("RoteTrans UI...");
        }
    }    
}
