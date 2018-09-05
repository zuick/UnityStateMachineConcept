using UnityEngine;
using UnityEditor;
using System;
using System.Collections.Generic;
using AI;

[CustomPropertyDrawer(typeof(StateMachine.ConditionalTransition))]
public class ConditionTransitionPropertyDrawer : RowPropertyDrawer {
	public override bool showLabel { get { return false; }}
	public override List<string> GetColumns(){
		return new List<string>(){
			"state", "condition", "nextState", "delay", "inverseCondition", "active"
		};
	}
}

[CustomPropertyDrawer(typeof(StateMachine.AutoTransition))]
public class AutoTransitionPropertyDrawer : RowPropertyDrawer {
	public override bool showLabel { get { return false; }}
	public override List<string> GetColumns(){
		return new List<string>(){
			"state", "time", "nextState", "active"
		};
	}
}