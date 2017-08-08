using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ImageEffect : MonoBehaviour {

	[SerializeField] private Material _effectMaterial;
	[SerializeField] [Range(0f, 1f)] private float _iterator;

	private string _globalIteratorName = "_globalImgEffectIterator";

	void OnRenderImage(RenderTexture src, RenderTexture dest)
	{
		Shader.SetGlobalFloat(_globalIteratorName, _iterator);
		Graphics.Blit(src, dest, _effectMaterial);
	}
}
