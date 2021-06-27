using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public static class ModelDataManager 
{
    private static string getFilePath() { return Application.persistentDataPath + "/ModelTransformData" + ".json"; }
    public static void Save(TransformDataWrapper transformDataWrapper)
    {
        //シリアライズ実行
        string jsonSerializedData = JsonUtility.ToJson(transformDataWrapper);
        Debug.Log(jsonSerializedData);

        //実際にファイル作って書き込む
        using (var sw = new StreamWriter(getFilePath(), false))
        {
            try
            {
                //ファイルに書き込む
                sw.Write(jsonSerializedData);
            }
            catch (Exception e) //失敗した時の処理
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
            //ファイルを読み込む
            using (FileStream fs = new FileStream(getFilePath(), FileMode.Open))
            using (StreamReader sr = new StreamReader(fs))
            {
                string result = sr.ReadToEnd();
                Debug.Log(result);

                //読み込んだJsonを構造体にぶちこむ
                jsonDeserializedData = JsonUtility.FromJson<TransformDataWrapper>(result);
            }
        }
        catch (Exception e) //失敗した時の処理
        {
            Debug.Log(e);
        }

        //デシリアライズした構造体を返す
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