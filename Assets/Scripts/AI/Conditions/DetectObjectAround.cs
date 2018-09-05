using UnityEngine;

namespace AI
{
	[CreateAssetMenu(fileName = "If object near", menuName = "AI/DetectObjectAround", order = 0)]
	public class DetectObjectAround : Condition
	{
		public LayerMask objectsLayer;
		public float radius;

		public override bool Check(StateMachine sm)
		{
			var hits = Physics.OverlapSphere(sm.transform.position, radius, objectsLayer);
			return hits.Length > 0;
		}
	}
}