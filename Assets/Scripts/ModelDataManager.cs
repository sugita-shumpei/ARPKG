using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public static class ModelDataManager 
{
    //Application.persistentDataPath
    private static string GetFilePath() { return  "Assets/Resources/ModelTransformData" + ".json"; }
    public static void Save(TransformDataWrapper transformDataWrapper)
    {

        string jsonSerializedData = JsonUtility.ToJson(transformDataWrapper);
        Debug.Log("SAVE RESULT: " + jsonSerializedData);


        using (var sw = new StreamWriter(GetFilePath(), false))

        {
            try
            {
                sw.Write(jsonSerializedData);
            }
            catch (Exception e) 
            {
                Debug.Log(e);
            }
        }
    }

    public static TransformDataWrapper Load()
    {
        TransformDataWrapper jsonDeserializedData = new TransformDataWrapper();
        try
        {

            using (FileStream fs = new FileStream(GetFilePath(), FileMode.Open))


            using (StreamReader sr = new StreamReader(fs))
            {
                string result = sr.ReadToEnd();
                Debug.Log("LOAD RESULT :" + result);

                jsonDeserializedData = JsonUtility.FromJson<TransformDataWrapper>(result);
            }
        }
        catch (Exception e) 
        {
            Debug.Log(e);
        }

        return jsonDeserializedData;
    }
}

[System.Serializable]
public struct TransformData
{
    public string name;
    public Vector3 position;
    public Vector3 Scale;
}
public class TransformDataWrapper
{
    public List<TransformData> DataList = new List<TransformData>();
}