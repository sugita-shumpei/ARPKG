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
        //ï¿½Vï¿½ï¿½ï¿½Aï¿½ï¿½ï¿½Cï¿½Yï¿½ï¿½ï¿½s
        string jsonSerializedData = JsonUtility.ToJson(transformDataWrapper);
        Debug.Log("SAVE RESULT: " + jsonSerializedData);

<<<<<<< HEAD
        //ÀÛ‚Éƒtƒ@ƒCƒ‹ì‚Á‚Ä‘‚«‚Ş
        using (var sw = new StreamWriter(GetFilePath(), false))
=======
        //ï¿½ï¿½ï¿½Û‚Éƒtï¿½@ï¿½Cï¿½ï¿½ï¿½ï¿½ï¿½ï¿½Äï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½
        using (var sw = new StreamWriter(getFilePath(), false))
>>>>>>> 9db99185b078e449585971f45b6a840d6f373854
        {
            try
            {
                //ï¿½tï¿½@ï¿½Cï¿½ï¿½ï¿½Éï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½
                sw.Write(jsonSerializedData);
            }
            catch (Exception e) //ï¿½ï¿½ï¿½sï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½Ìï¿½ï¿½ï¿½
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
<<<<<<< HEAD
            //ƒtƒ@ƒCƒ‹‚ğ“Ç‚İ‚Ş
            using (FileStream fs = new FileStream(GetFilePath(), FileMode.Open))
=======
            //ï¿½tï¿½@ï¿½Cï¿½ï¿½ï¿½ï¿½Ç‚İï¿½ï¿½ï¿½
            using (FileStream fs = new FileStream(getFilePath(), FileMode.Open))
>>>>>>> 9db99185b078e449585971f45b6a840d6f373854
            using (StreamReader sr = new StreamReader(fs))
            {
                string result = sr.ReadToEnd();
                Debug.Log("LOAD RESULT :" + result);

                //ï¿½Ç‚İï¿½ï¿½ï¿½Jsonï¿½ï¿½ï¿½\ï¿½ï¿½ï¿½Ì‚É‚Ô‚ï¿½ï¿½ï¿½ï¿½ï¿½
                jsonDeserializedData = JsonUtility.FromJson<TransformDataWrapper>(result);
            }
        }
        catch (Exception e) //ï¿½ï¿½ï¿½sï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½Ìï¿½ï¿½ï¿½
        {
            Debug.Log(e);
        }
        //ï¿½fï¿½Vï¿½ï¿½ï¿½Aï¿½ï¿½ï¿½Cï¿½Yï¿½ï¿½ï¿½ï¿½ï¿½\ï¿½ï¿½ï¿½Ì‚ï¿½Ô‚ï¿½
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