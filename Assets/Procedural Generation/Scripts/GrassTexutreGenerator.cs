using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassTexutreGenerator : MonoBehaviour
{	
    public MeshSettings meshSettings;
	public HeightMapSettings heightMapSettings;
    Renderer rend;
	public Texture2D output;
    public Texture2D inverted;
    public Material grassMap;

    private void Start() {
        HeightMap heightMap = HeightMapGenerator.GenerateHeightMap (meshSettings.numVertsPerLine, meshSettings.numVertsPerLine, heightMapSettings, Vector2.zero);
        
        output = TextureGenerator.TextureFromHeightMap (heightMap);
        inverted = TextureGenerator.GrassTextureFromHeightMap(heightMap);
        inverted.Resize(inverted.width * 50, inverted.height * 50);
        inverted.Apply();

        grassMap.SetTexture("_GrassMap", inverted);
    }

}
