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
        //�V���A���C�Y���s
        string jsonSerializedData = JsonUtility.ToJson(transformDataWrapper);
        Debug.Log(jsonSerializedData);

        //���ۂɃt�@�C������ď�������
        using (var sw = new StreamWriter(getFilePath(), false))
        {
            try
            {
                //�t�@�C���ɏ�������
                sw.Write(jsonSerializedData);
            }
            catch (Exception e) //���s�������̏���
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
            //�t�@�C����ǂݍ���
            using (FileStream fs = new FileStream(getFilePath(), FileMode.Open))
            using (StreamReader sr = new StreamReader(fs))
            {
                string result = sr.ReadToEnd();
                Debug.Log(result);

                //�ǂݍ���Json���\���̂ɂԂ�����
                jsonDeserializedData = JsonUtility.FromJson<TransformDataWrapper>(result);
            }
        }
        catch (Exception e) //���s�������̏���
        {
            Debug.Log(e);
        }
        //�f�V���A���C�Y�����\���̂�Ԃ�
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