using System.Collections.Generic;
using UnityEngine;

public class GeoJSONLineDrawer : MonoBehaviour
{
    public TextAsset geoJSONData; // Assign your GeoJSON data in the Unity editor
    public Material lineMaterial; // Assign a material for the LineRenderer in the Unity editor

    void Start()
    {
        // Parse the GeoJSON data
        GeoJSONFeatureCollection featureCollection = JsonUtility.FromJson<GeoJSONFeatureCollection>(geoJSONData.text);

        if (featureCollection != null && featureCollection.features.Length > 0)
        {
            foreach (GeoJSONFeature feature in featureCollection.features)
            {
                if (feature.geometry.type == "MultiPolygon")
                {
                    List<Vector3> flattenedCoordinates = new List<Vector3>();

                    // Iterate through the polygons within the MultiPolygon
                    foreach (var polygon in feature.geometry.coordinates)
                    {
                        // Iterate through the coordinates of each polygon
                        foreach (var ring in polygon)
                        {
                            for (int i = 0; i < ring.x; i++)
                            {
                                var coordinate = ring[i];

                                // Convert each coordinate and add it to the flattened list
                                Vector3 worldPosition = GeoJSONToUnityCoordinates(new Vector3(coordinate, 0f, 0f));
                                // Apply scaling factor (adjust as needed)
                                flattenedCoordinates.Add(worldPosition);
                            }
                        }
                    }

                    // Convert the List<Vector3> to a Vector3[]
                    Vector3[] flattenedArray = flattenedCoordinates.ToArray();

                    // Pass the flattened array to the DrawPolygon function
                    DrawPolygon(flattenedArray);
                }
            }
        }
    }

    void DrawPolygon(Vector3[] coordinates)
    {
        // Create an empty GameObject to hold the polygon
        GameObject polygonObject = new GameObject("GeoJSONPolygon");

        // Add a LineRenderer component to the GameObject
        LineRenderer lineRenderer = polygonObject.AddComponent<LineRenderer>();

        // Configure LineRenderer properties
        lineRenderer.material = lineMaterial;
        lineRenderer.startColor = Color.blue;
        lineRenderer.endColor = Color.blue;
        lineRenderer.startWidth = 0.05f;
        lineRenderer.endWidth = 0.05f;

        // Extract and convert coordinates from the GeoJSON data
        Vector3[] linePositions = new Vector3[coordinates.Length];

        for (int i = 0; i < coordinates.Length; i++)
        {
            // Convert GeoJSON coordinates to Unity world space coordinates
            Vector3 worldPosition = GeoJSONToUnityCoordinates(coordinates[i]);
            linePositions[i] = worldPosition;
        }

        // Set the line positions
        lineRenderer.positionCount = linePositions.Length;
        lineRenderer.SetPositions(linePositions);
    }

    // Implement coordinate conversion if necessary
    Vector3 GeoJSONToUnityCoordinates(Vector3 geoJSONCoords)
    {
        // Implement the conversion logic here if needed
        // You may need to consider scaling, positioning, and any coordinate transformations
        return new Vector3(geoJSONCoords.x, 0f, geoJSONCoords.y);
    }
}

[System.Serializable]
public class GeoJSONFeatureCollection
{
    public GeoJSONFeature[] features;
}

[System.Serializable]
public class GeoJSONFeature
{
    public GeoJSONGeometry geometry;
}

[System.Serializable]
public class GeoJSONGeometry
{
    public string type;
    public Vector3[][] coordinates;
}
