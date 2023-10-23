using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MiniJSON;


public class GeoJson : MonoBehaviour
{
    public struct geoJsonFeatureCollection
    {
        public string type;
        public List<geoJsonFeature> features;
    }

    public struct geoJsonFeature
    {
        public string type;
        public geoJsonProperties properties;
        public geoJsonGeometry geometry;
    }



    public struct geoJsonProperties
    {
        public string OBJECTID; /* 1 */
        public string Id; /* 0 */
        public string SHAPE_Length; /* 2.8128206459671912 */
        public string SHAPE_Area; /* 0.0060883160871301693 */

    }

    public struct geoJsonGeometry
    {
        public string type;
        public List<List<coordinate>> coordinates; /* 閉路ごとにリストが分けられている */
    }

    public struct coordinate
    {
        public double latitude; /* 緯度 */
        public double longitude; /* 経度 */
    }

    const string jsonString =
@"
{
    ""text"":""FeatureCollection"",
    ""properties"":
    {
        ""32"":
        {
            ""visible"":true
        },
        ""33"":
        {
            ""visible"":true
        },
        ""34"":
        {
            ""visible"":true
        },
        ""35"":
        {
            ""visible"":true
        }
    }
}
";

    void Update()
    {

        Dictionary<string, object> featureCollection = Json.Deserialize(jsonString) as Dictionary<string, object>;

        geoJsonFeatureCollection geodata = new geoJsonFeatureCollection();

        geodata.type = (string)featureCollection["type"];

        geodata.features = new List<geoJsonFeature>();

        foreach (Dictionary<string, object> feature in (List<object>)featureCollection["features"])
        {
            geoJsonFeature tmpfeature = new geoJsonFeature();

            tmpfeature.type = (string)feature["type"];

            Dictionary<string, object> properties = (Dictionary<string, object>)feature["properties"];
            if (properties.ContainsKey("OBJECTID")) tmpfeature.properties.OBJECTID = (string)properties["OBJECTID"];
            if (properties.ContainsKey("Id")) tmpfeature.properties.Id = (string)properties["Id"];
            if (properties.ContainsKey("SHAPE_Length")) tmpfeature.properties.SHAPE_Length = (string)properties["SHAPE_Length"];
            if (properties.ContainsKey("SHAPE_Area")) tmpfeature.properties.SHAPE_Length = (string)properties["SHAPE_Area"];



            Dictionary<string, object> geometry = (Dictionary<string, object>)feature["geometry"];
            tmpfeature.geometry.type = (string)geometry["type"];
            tmpfeature.geometry.coordinates = new List<List<coordinate>>();
            foreach (List<object> closedLoop in (List<object>)geometry["coordinates"])
            {
                List<coordinate> tmpcloop = new List<coordinate>();
                foreach (List<object> position in closedLoop)
                {
                    coordinate tmppos = new coordinate();
                    tmppos.latitude = (double)position[0];
                    tmppos.longitude = (double)position[1];
                    tmpcloop.Add(tmppos);
                }
                tmpfeature.geometry.coordinates.Add(tmpcloop);

            }
            geodata.features.Add(tmpfeature);
        }

        List<coordinate> tmp = geodata.features[0].geometry.coordinates[0];
        for (int i = 1; i < tmp.Count; i++)
        {
            Debug.DrawLine(
                new Vector3((float)(tmp[i - 1].latitude * 10000 - tmp[0].latitude * 10000), (float)(tmp[i - 1].longitude * 10000 - tmp[0].longitude * 10000)),
                new Vector3((float)(tmp[i].latitude * 10000 - tmp[0].latitude * 10000), (float)(tmp[i].longitude * 10000 - tmp[0].longitude * 10000))
                );
        }

    }
}
