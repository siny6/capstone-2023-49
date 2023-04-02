using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    public CSVReader CSVReader;
    public List<Dictionary<string, object>> data;
    // Start is called before the first frame update
    void Start()
    {
        CSVReader = transform.GetComponent<CSVReader>();
        data = CSVReader.Read("LevelUpTable");
        /*for (var i = 0; i < data.Count; i++)
        {
            print("index " + (i).ToString() + " : " + data[i]["Table"] + " " + data[i]["Level1"]);
        }*/
    }
}
