using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class LeftHandController : BaseHandController
{

    public override void DoButtonTwoTouchPressed()
    {
        EventCenter.Broadcast(EventDefine.PauseUI, true);
        Debug.Log("Left Hand DoButton TwoTouch Pressed");
    }

    public override void DoButtonTwoTouchReleased()
    {
        //EventCenter.Broadcast(EventDefine.ShowUI, false);
        Debug.Log("Left Hand DoButton TwoTouch Released");
    }

    public override void GripPressed()
    {
        Debug.Log("Left Hand Grip Rressed...");
    }

    public override void GripReleased()
    {
        Debug.Log("Left Hand Grip Released...");
    }

    public override void TouchpadPressed()
    {
        Debug.Log("Left Hand Touchpad Pressed...");
    }

    public override void TouchpadReleased()
    {
        Debug.Log("Left Hand Touchpad Released...");
    }

    public override void TriggerPressed()
    {
        EventCenter.Broadcast(EventDefine.DragUI, true);
        Debug.Log("Left Hand Trigger Pressed...");
    }

    public override void TriggerReleased()
    {
        EventCenter.Broadcast(EventDefine.unDragUI, false);
        Debug.Log("Left Hand Trigger Released...");
    }
}
