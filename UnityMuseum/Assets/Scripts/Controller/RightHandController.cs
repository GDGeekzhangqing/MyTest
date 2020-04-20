using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightHandController : BaseHandController
{

	public override void DoButtonTwoTouchPressed()
	{
		EventCenter.Broadcast(EventDefine.ShowUI);
		Debug.Log("Right Hand DoButtonTwoTouchPressed");
	}

	public override void DoButtonTwoTouchReleased()
	{
		//EventCenter.Broadcast(EventDefine.ShowUI, false);
		Debug.Log("Right Hand DoButtonTwoTouchPressed");
	}

	public override void GripPressed()
	{
		Debug.Log("Right Hand GripPressed...");
	}

	public override void GripReleased()
	{
		Debug.Log("Right Hand GripReleased");
	}

	public override void TouchpadPressed()
	{
		Debug.Log("Right Hand TouchpadPressed");
	}

	public override void TouchpadReleased()
	{
		Debug.Log("Right Hand TouchpadReleased");
	}

	public override void TriggerPressed()
	{
		
		Debug.Log("Right Hand TriggerPressed");
	}

	public override void TriggerReleased()
	{
		
		Debug.Log("Right Hand TriggerReleased");
	}
}
