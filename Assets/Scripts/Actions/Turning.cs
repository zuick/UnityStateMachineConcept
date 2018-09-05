using UnityEngine;

public class Turning : MonoBehaviour
{
	public Transform targetTransform;

	[Range(-180, 180)]
	public float minAngle;
	[Range(-180, 180)]
	public float maxAngle;

	public float turnTime;

	float newAngle;

	void OnEnable()
	{
		newAngle = Random.Range(minAngle, maxAngle);
	}

	void Update()
	{
		if (targetTransform == null)
			return;

		targetTransform.Rotate(0f, newAngle * Time.deltaTime, 0f);
	}
}