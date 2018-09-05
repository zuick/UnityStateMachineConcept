using UnityEngine;
using UnityEngine.Events;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace AI
{
	public class StateMachine : MonoBehaviour
	{
		public class UnityEventString: UnityEvent<string>{}

		[Serializable]
		public class Transition
		{
			public GameObject state;
			public GameObject nextState;
			public bool active = true;
		}
		[Serializable]
		public class ConditionalTransition : Transition
		{
			public Condition condition;
			public bool inverseCondition = false;
			public float delay;
		}

		[Serializable]
		public class AutoTransition : Transition
		{
			public float time;
		}

		public UnityEventString OnStateChanged = new UnityEventString();

		public ConditionalTransition[] conditionalTransitions;
		public AutoTransition[] autoTransitions;

		public GameObject currentState;

		void Awake()
		{
			if (currentState == null)
				return;

			for (var i = 0; i < currentState.transform.parent.childCount; i++)
			{
				currentState.transform.parent.GetChild(i).gameObject.SetActive(false);
			}

			DoTransition(null, currentState);
		}

		void Update()
		{
			if (currentState == null)
				return;

			var newTransition = conditionalTransitions.FirstOrDefault(
				t => t.active &&
					(t.state == currentState || t.state == null) && //  current state or any
					t.condition != null && t.condition.Check(this) ^ t.inverseCondition &&
					t.nextState != null
			);

			if (newTransition != null)
			{
				if (newTransition.delay == 0f)
					DoTransition(currentState, newTransition.nextState);
				else
					StartCoroutine(DoTransition(currentState, newTransition.nextState, newTransition.delay));
			}

		}

		void DoTransition(GameObject oldState, GameObject newState)
		{
			if (oldState == newState)
				return;

			StopAllCoroutines();

			if (oldState != null)
				oldState.SetActive(false);

			if (newState != null)
			{
				newState.SetActive(true);
				currentState = newState;
				OnStateChanged.Invoke(newState.name);
				var autoTransition = autoTransitions.FirstOrDefault(t => t.active && t.state == newState);
				if (autoTransition != null)
				{
					if (autoTransition.time > 0)
						StartCoroutine(DoTransition(autoTransition.state, autoTransition.nextState, autoTransition.time));
					else
						DoTransition(autoTransition.state, autoTransition.nextState);
				}
			}
		}

		IEnumerator DoTransition(GameObject oldState, GameObject newState, float time)
		{
			yield return new WaitForSeconds(time);
			if (currentState == oldState)
				DoTransition(oldState, newState);
		}
	}
}