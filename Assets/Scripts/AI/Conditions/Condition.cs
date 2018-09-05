using UnityEngine;

namespace AI
{
	public abstract class Condition : ScriptableObject
	{
		public virtual bool Check(StateMachine sm)
		{
			return false;
		}
	}
}