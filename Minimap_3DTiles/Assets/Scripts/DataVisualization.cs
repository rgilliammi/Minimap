using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json; // For JSON deserialization
using System.IO;

public class DataVisualization : MonoBehaviour
{
    public GameObject prefab; // Prefab for your 3D object
    public Material customMaterial; // Material with your custom shader

    [System.Serializable]
    public class ShapeData
    {
        public int FID;
        public float Shape_Leng;
        public float SHAPE_Length;
        public float SHAPE_Area;
    }



    void Start()
    {
        // JSON file path
        string jsonFileName = Path.Combine(Application.dataPath, "Resources/Climate.json");

        
        // Load and parse the JSON data
        string json = System.IO.File.ReadAllText(jsonFileName);
       List<ShapeData> shapeDataList = JsonConvert.DeserializeObject<List<ShapeData>>(json);


        // Process the data and create objects
        foreach (ShapeData data in shapeDataList)
        {
    
            // Create a new object based on the prefab
            GameObject obj = Instantiate(prefab, Vector3.zero, Quaternion.identity);

            // Adjust the object's position, scale, or other properties based on the data
            obj.transform.position = new Vector3(data.FID, data.Shape_Leng, data.SHAPE_Area);
            obj.transform.localScale = new Vector3(data.Shape_Leng, data.SHAPE_Area, data.Shape_Leng);

            // Assign the custom material to the object
            MeshRenderer renderer = obj.GetComponent<MeshRenderer>();
            renderer.material = customMaterial;
        }
    }
}
