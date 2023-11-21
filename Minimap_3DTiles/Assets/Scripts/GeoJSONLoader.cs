using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class GeoJSONFeature
{
    public int FID;
    public float Shape_Leng;
    public float SHAPE_Length;
    public float SHAPE_Area;
    public List<List<List<float>>> coordinates; // Change the data structure to match your GeoJSON
}

[System.Serializable]
public class GeoJSONData
{
    public List<GeoJSONFeature> features;
}

public class GeoJSONLoader : MonoBehaviour
{
    public TextAsset geoJSONFile; // Assign your GeoJSON file to this field
    public GameObject shapePrefab; // A prefab for your shape

    void Start()
    {
        if (geoJSONFile != null)
        {
            GeoJSONData geoJSONData = JsonUtility.FromJson<GeoJSONData>(geoJSONFile.text);

            foreach (GeoJSONFeature feature in geoJSONData.features)
            {
                // Create a shape GameObject for each feature
                GameObject shapeObject = Instantiate(shapePrefab);

                // Access the coordinates and create a mesh or adjust the position/scale of the GameObject
                // Here, you might need to work with the coordinates and create a visual shape.

                // Make sure to set the position, rotation, and scale of the shapeObject accordingly.
            }
        }
        else
        {
            Debug.LogError("GeoJSON file not assigned.");
        }
    }
}
