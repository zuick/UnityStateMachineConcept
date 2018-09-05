using UnityEngine;

public class SimpleSpawner : MonoBehaviour
{
	public GameObject prefab;

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space) && prefab != null)
		{
			var randomAngle = Random.Range(0f, 360f);
			Instantiate(prefab, transform.position, Quaternion.Euler(0f, randomAngle, 0f));
		}
	}
}