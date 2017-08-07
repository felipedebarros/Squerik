using UnityEngine;

[ExecuteInEditMode]
public class MirrorCamera : MonoBehaviour {

	[HideInInspector]
	[SerializeField] private Camera _camera;
	[SerializeField] private Transform _reflectionPoint;

	private string _globalTextureName = "_GlobalMirrorTex";

	void Update()
	{
		Vector2 dist = _reflectionPoint.position - Camera.main.transform.position;
		transform.position = _reflectionPoint.position + (Vector3) dist;
	}

	void OnEnable()
	{
		GenerateRT();
	}

	void GenerateRT()
	{
		_camera = GetComponent<Camera>();

		if(_camera.targetTexture != null)
		{
			RenderTexture temp = _camera.targetTexture;

			_camera.targetTexture = null;
			DestroyImmediate(temp);
		}

		_camera.targetTexture = new RenderTexture(_camera.pixelWidth, _camera.pixelHeight, 16);
		_camera.targetTexture.filterMode = FilterMode.Bilinear;

		Shader.SetGlobalTexture(_globalTextureName, _camera.targetTexture);
	}
}
