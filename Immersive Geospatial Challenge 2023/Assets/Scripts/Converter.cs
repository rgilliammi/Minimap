using UnityEngine;
using System.IO;
using Newtonsoft.Json;

[System.Serializable]
public class Data
{
    public string data;
}

public class Converter : MonoBehaviour
{
    void Start()
    {
        string txtContent = File.ReadAllText("Assets/Json/jsontest.txt");
        Data dataObject = new Data { data = txtContent };

        string json = JsonConvert.SerializeObject(dataObject, Formatting.Indented);
        File.WriteAllText("Assets/Json/output.json", json);
    }
}
