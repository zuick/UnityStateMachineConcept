using UnityEngine;

public class MoveForvard : MonoBehaviour {
	public Rigidbody rbody;
	public float speed;

	void Update() {
		if(rbody == null)
			return;

		rbody.velocity = new Vector3(
			rbody.transform.forward.x * speed, 
			rbody.velocity.y, 
			rbody.transform.forward.z * speed
		);
	}
}