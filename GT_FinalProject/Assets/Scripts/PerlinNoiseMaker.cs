using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerlinNoiseMaker : MonoBehaviour
{
    public int width = 256, height = 256;
    public float scale = 60;

    // Start is called before the first frame update
    void Start()
    {
        //Find object's renderer and assign the generated texture to it
        Renderer renderer = GetComponent<Renderer>();
        renderer.material.mainTexture = GenerateTexture();
    }

    Texture2D GenerateTexture()
    {
        Texture2D groundTexture = new Texture2D(width, height);

        //Generate perlin noise map
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Color color = CalculateColor(x, y);
                groundTexture.SetPixel(x, y, color);
            }
        }
        //Applies the generated noise map to the texture
        groundTexture.Apply();

        return groundTexture;
    }

    Color CalculateColor(int x, int y)
    {
        float xPos = (float)x / width * scale;
        float yPos = (float)y / height * scale;

        float sampleColor = Mathf.PerlinNoise(xPos, yPos);
        return new Color(sampleColor, 0, 0.2f);

    }

}

