using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class PathGenerate : MonoBehaviour
{
    public TextAsset textJSON;

    public class Properties
    {
        public string name { get; set; }
        public string type { get; set; }
    }

    public class Crs
    {
        public string type { get; set; }
        public Properties properties { get; set; }
    }

    public class Geometry
    {
        public string type { get; set; }
        public List<List<List<float>>> coordinates { get; set; }
    }

    public class Feature
    {
        public string type { get; set; }
        public Properties properties { get; set; }
        public Geometry geometry { get; set; }
    }

    public class Geojson
    {
        public string type { get; set; }
        public string name { get; set; }
        public Crs crs { get; set; }
        public List<Feature> features { get; set; }
    }

    private Geojson geojson = new Geojson();
    public List<Vector3> routeCoords;
    float altitude = 6.5f;
    bool pathLoaded = false;

    void Start()
    {
        // Run LoadPath() later than terrain and flyover initialization
    }

    void Update()
    {
        if (!pathLoaded)
        {
            LoadPath();
        }
    }

    void LoadPath()
    {
        geojson = JsonConvert.DeserializeObject<Geojson>(textJSON.text);

        foreach (Feature f in geojson.features)
        {
            foreach (List<List<float>> polygons in f.geometry.coordinates)
            {
                foreach (List<float> coordinates in polygons)
                {
                    // Process each point in the polygon
                    HandleCoordinate(coordinates);
                }
            }
        }

        pathLoaded = true;
    }

    void HandleCoordinate(List<float> cs)
    {
        // Raycast for each point
        RaycastHit hit;
        Ray downRay = new Ray(new Vector3(800000 - cs[0], altitude + 4.7f, 800000 - cs[1]), -Vector3.up);

        if (Physics.Raycast(downRay, out hit, 20))
        {
            altitude = hit.point.y + 0.5f;
        }

        routeCoords.Add(new Vector3(800000 - cs[0], altitude, 800000 - cs[1]));
    }

    void CreateLineRenderer()
    {
        var lr = gameObject.AddComponent<LineRenderer>();
        lr.positionCount = routeCoords.Count;
        lr.SetPositions(routeCoords.ToArray());
        lr.startWidth = 0.3f;
        lr.endWidth = 0.3f;
        lr.material.SetColor("_Color", new Color(2f, 2f, 0f, 0.1f));
    }
}
