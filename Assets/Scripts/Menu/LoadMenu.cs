using System.Data;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using TMPro;
using UnityEngine;
using Newtonsoft.Json;

public class LoadMenu : MonoBehaviour
{
    [SerializeField]GameObject SaveSlotPrefab;
    public static Action RefreshSaveList;
    private void OnEnable()
    {
        RefreshSaveList += ShowSaveList;
    }

    private void OnDisable()
    {
        RefreshSaveList -= ShowSaveList;
    }

    public void ShowSaveList()
    {
        if (SaveManager.GetSaveFilePaths() != null)
        {
            GameObject loadList = GameObject.Find("LoadList");
            foreach (Transform child in loadList.transform)
            {
                Destroy(child.gameObject);
            }
            foreach (string path in SaveManager.GetSaveFilePaths())
            {
                if (File.Exists(path))
                {
                    /*                        BinaryFormatter bf = new BinaryFormatter();
                                         FileStream stream = new FileStream(path, FileMode.Open);
                                         GameData data = bf.Deserialize(stream) as GameData;*/

                    string loadedJsonData = File.ReadAllText(path);

                    var settings = new JsonSerializerSettings();
                    settings.Converters.Add(new Vector3Converter());
                    GameData data = JsonConvert.DeserializeObject<GameData>(loadedJsonData, settings);
                    /*GameData data = JsonUtility.FromJson<GameData>(loadedJsonData);*/


                    GameObject saveSlot = Instantiate(SaveSlotPrefab);
                    TextMeshProUGUI[] textFields = saveSlot.GetComponentsInChildren<TextMeshProUGUI>();
                    textFields[0].text = data.playerData.playerName;
                    textFields[2].text = data.time;
                    textFields[4].text = data.currentArea.GetAreaName();
                    textFields[6].text = data.playerData.kills.ToString();
                    textFields[8].text = path;
                    saveSlot.transform.SetParent(loadList.transform);
                    saveSlot.transform.localScale = Vector3.one;

                    /*stream.Close();*/
                }
                else 
                {
                    Debug.Log("File Missing");
                }
            }

        }
        else 
        {
            Debug.Log("List of save paths was null");
        }
    }



}
public class Vector3Converter : JsonConverter<Vector3>
{
    public override void WriteJson(JsonWriter writer, Vector3 value, JsonSerializer serializer)
    {
        writer.WriteStartObject();
        writer.WritePropertyName("x");
        writer.WriteValue(value.x);
        writer.WritePropertyName("y");
        writer.WriteValue(value.y);
        writer.WritePropertyName("z");
        writer.WriteValue(value.z);
        writer.WriteEndObject();
    }

    public override Vector3 ReadJson(JsonReader reader, Type objectType, Vector3 existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        if (reader.TokenType == JsonToken.StartObject)
        {
            reader.Read(); // Move to the first property
            float x = float.Parse(reader.Value.ToString());

            reader.Read(); // Move to the second property
            reader.Read(); // Move to the value of the second property
            float y = float.Parse(reader.Value.ToString());

            reader.Read(); // Move to the third property
            reader.Read(); // Move to the value of the third property
            float z = float.Parse(reader.Value.ToString());

            reader.Read(); // Move to EndObject
            return new Vector3(x, y, z);
        }
        else if (reader.TokenType == JsonToken.StartArray)
        {
            float[] values = serializer.Deserialize<float[]>(reader);
            if (values.Length >= 3)
            {
                return new Vector3(values[0], values[1], values[2]);
            }
        }

        throw new JsonSerializationException("Unexpected token type for Vector3.");
    }

    public override bool CanRead => true;
    public override bool CanWrite => true;
}
