/* This class was taken from user naked_chicken on the unity forums */


using UnityEngine;    // For Debug.Log, etc.

using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Reflection;

public class SOSaver  {
  public static string currentFilePath = Application.persistentDataPath + "/save.dat";    // Edit this for different save files

  // Call this to write data
  public static void Save(ScriptableObject toSave) {
    Save(currentFilePath, toSave);
  }
  public static void Save(string filePath, ScriptableObject toSave) {
    //First we make a new data object. This is where we will store all the things like our current crew, running Scenarios and things of that nature.
    SOPSaveData newData = new SOPSaveData();

    //TODO - temp for now, just making one Scenario Holder object to test. Need to rename all these things.
    SOHolder holder = new SOHolder();

    Type sType = toSave.GetType();
    holder.typeName = sType.Name;

    List<string> intNameList = new List<string>();
    List<int> intValueList = new List<int>();

    List<string> stringNameList = new List<string>();
    List<string> stringValueList = new List<string>();

    List<string> stringArrayNameList = new List<string>();
    List<string[]> stringArrayValueList = new List<string[]>();

    List<string> stringListNameList = new List<string>();
    List<List<string>> stringListValueList = new List<List<string>>();

    FieldInfo[] fields = sType.GetFields();
    foreach (FieldInfo f in fields) {
      if (f.FieldType == typeof(int)) {
        intNameList.Add(f.Name);
        intValueList.Add((int)f.GetValue(toSave));
      } else if (f.FieldType == typeof(string)) {
        stringNameList.Add(f.Name);
        stringValueList.Add((string)f.GetValue(toSave));
      } else if (f.FieldType == typeof(string[])) {
        stringArrayNameList.Add(f.Name);
        stringArrayValueList.Add((string[])f.GetValue(toSave));
      } else if (f.FieldType == typeof(List<string>)) {
        stringListNameList.Add(f.Name);
        stringListValueList.Add((List<string>)f.GetValue(toSave));
      } else {
        Debug.Log(f.FieldType);
      }
    }

    holder.stringNames = stringNameList.ToArray();
    holder.stringValues = stringValueList.ToArray();

    holder.stringArrayNames = stringArrayNameList.ToArray();
    holder.stringArrayValues = stringArrayValueList.ToArray();

    holder.stringListNames = stringListNameList.ToArray();
    holder.stringListValues = stringListValueList.ToArray();

    holder.intNames = intNameList.ToArray();
    holder.intValues = intValueList.ToArray();

    //We'll add the SO holders to the data list
    newData.holders.Add(holder);

    //Now write to the given path (default for now)
    Stream stream = File.Open(filePath, FileMode.Create);
    BinaryFormatter bformatter = new BinaryFormatter();
    bformatter.Binder = new VersionDeserializationBinder();
    bformatter.Serialize(stream, newData);
    stream.Close();
  }

  // Call this to load from a file into "data"
  //TODO - only returning an SOToSave for test purposes right now. Need to fix so everything is just written to GameManager
  public static ScriptableObject Load() {
    return Load(currentFilePath);
  }

  public static ScriptableObject Load(string filePath) {
    if (!File.Exists(filePath)) {
      Debug.Log("NO FILE");
      return null;
    }

    Stream stream = File.Open(filePath, FileMode.Open);
    BinaryFormatter bformatter = new BinaryFormatter();
    bformatter.Binder = new VersionDeserializationBinder();
    SOPSaveData loadData = (SOPSaveData)bformatter.Deserialize(stream);
    stream.Close();


    if (loadData != null) {
      if (loadData.holders != null && loadData.holders.Count > 0){
        SOHolder newHolder = loadData.holders[0];

        if (newHolder.typeName != null) {
          ScriptableObject newSO = ScriptableObject.CreateInstance(newHolder.typeName);
          if (newSO != null) {
            Type mType = newSO.GetType();
            for (int i = 0; newHolder.stringNames != null && i < newHolder.stringNames.Length; i++){
              mType.GetField(newHolder.stringNames[i]).SetValue(newSO, newHolder.stringValues[i]);
            }

            for (int i = 0; newHolder.intNames != null && i < newHolder.intNames.Length; i++){
              mType.GetField(newHolder.intNames[i]).SetValue(newSO, newHolder.intValues[i]);
            }

            for (int i = 0; newHolder.stringArrayNames != null && i < newHolder.stringArrayNames.Length; i++){
              mType.GetField(newHolder.stringArrayNames[i]).SetValue(newSO, newHolder.stringArrayValues[i]);
            }

            for (int i = 0; newHolder.stringListNames != null && i < newHolder.stringListNames.Length; i++){
              mType.GetField(newHolder.stringListNames[i]).SetValue(newSO, newHolder.stringListValues[i]);
            }

            return newSO;
          }
        }
      }
    }
    return null;
  }

}

[Serializable]
public class SOPSaveData : ISerializable {
  public List<SOHolder> holders = new List<SOHolder>();

  public SOPSaveData() { }

  public SOPSaveData(SerializationInfo info, StreamingContext context) {
    holders = (List<SOHolder>)info.GetValue("holders", typeof(List<SOHolder>));
  }

  public void GetObjectData(SerializationInfo info, StreamingContext ctxt) {
    info.AddValue("holders", holders);
  }
}

[Serializable]
public class SOHolder {
  public string typeName;

  public string[] intNames;
  public int[] intValues;

  public string[] floatNames;
  public float[] floatValues;

  public string[] stringNames;
  public string[] stringValues;

  public string[] stringArrayNames;
  public string[][] stringArrayValues;

  public string[] stringListNames;
  public List<string>[] stringListValues;

  public string[] boolNames;
  public bool[] boolValues;
}

public sealed class VersionDeserializationBinder : SerializationBinder {
  public override Type BindToType(string assemblyName, string typeName) {
    if (!string.IsNullOrEmpty(assemblyName) && !string.IsNullOrEmpty(typeName))
        {
      Type typeToDeserialize = null;

      assemblyName = Assembly.GetExecutingAssembly().FullName;

      // The following line of code returns the type.
      typeToDeserialize = Type.GetType(String.Format("{0}, {1}", typeName, assemblyName));

      return typeToDeserialize;
    }

    return null;
  }
}