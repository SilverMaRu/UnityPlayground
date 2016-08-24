﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

public abstract class TriggerBase : MonoBehaviour
{
	
	//actionitems can be connected to GameplayAction scripts, and execute their one action (the method ExecuteAction implemented in each child class)
	public List<ActionItem> actions = new List<ActionItem>();



	//custom actions are more complicated to setup but more powerful, and appear only if useCustomActions is enabled
	public bool useCustomActions = false;
	public UnityEvent customActions;



	//to make the trigger fire only once
	public bool happenOnlyOnce = false;
	private bool alreadyHappened = false;



	public void ExecuteAllActions()
	{		
		//first check if the action has already been executed
		if(happenOnlyOnce && alreadyHappened)
			return;

		//first execute the simple GameplayActions, if present
		foreach(ActionItem ga in actions)
		{
			if(ga.connectedAction != null)
			{
				(ga.connectedAction as IGameplayAction).ExecuteAction();
			}
		}

		//execute the custom actions, if present and enabled
		if(useCustomActions)
		{
			customActions.Invoke();
		}

		alreadyHappened = true; //will prevent re-executing the actions if happenOnlyOnce is true
	}
}