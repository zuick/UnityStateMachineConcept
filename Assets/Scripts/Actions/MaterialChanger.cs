using UnityEngine;

public class MaterialChanger : MonoBehaviour
{
	public MeshRenderer targetMesh;
	public Material newMaterial;

	private Material oldMaterial;

	private void OnEnable()
	{
		if (targetMesh == null || newMaterial == null)
			return;

		oldMaterial = targetMesh.materials[0];
		targetMesh.materials = new Material[]{ newMaterial };
	}

	private void OnDisable()
	{
		if (targetMesh == null || newMaterial == null)
			return;

		targetMesh.materials = new Material[]{ oldMaterial };
	}
}