using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

public class BinaryLoaderSaver {
    public static BinaryFormatter GetBinaryFormatter() {
        BinaryFormatter formatter = new BinaryFormatter();

        // how to handle any non-serializable types?
        SurrogateSelector selector = new SurrogateSelector();
        Vector3SerializationSurrogate vecSurrogate = new Vector3SerializationSurrogate();
        selector.AddSurrogate(typeof(Vector3), new StreamingContext(StreamingContextStates.All), vecSurrogate);
        formatter.SurrogateSelector = selector;
         
        return formatter;
    }
    
    public static void SaveInventoryAsBinary(string path, string filename, Inventory inventory) {
        if (!Directory.Exists(path)) {
            Directory.CreateDirectory(path);
        }

        BinaryFormatter formatter = GetBinaryFormatter();

        // serialize the object directly to the file
        FileStream file = File.Create(path + filename);
        formatter.Serialize(file, inventory);
        file.Close();
    }

    public static Inventory LoadInventoryFromBinary(string path, string filename) {
        if (File.Exists(path + filename)) {
            BinaryFormatter formatter = GetBinaryFormatter();

            FileStream file = File.Open(path + filename, FileMode.Open);
            Inventory inventory = null;

            try {
                inventory = (Inventory)formatter.Deserialize(file);
            } catch {
                Debug.LogError("Error reading file: " + path + filename);
                return null;
            } finally {
                file.Close();
            }
            return inventory;
        } else {
            Debug.LogError("Cannot find file: " + path + filename);
            return null;
        }
    }

    public static void SavePlayerDataAsBinary(string path, string filename, PlayerData playerData) {
        if (!Directory.Exists(path)) {
            Directory.CreateDirectory(path);
        }

        FileStream file = File.Create(path + filename);
        BinaryFormatter formatter = GetBinaryFormatter();
        formatter.Serialize(file, playerData);
        file.Close();
    }
    public static PlayerData LoadPlayerDataFromBinary(string path, string filename) {
        if (File.Exists(path + filename)) {
            BinaryFormatter formatter = GetBinaryFormatter();
            FileStream file = File.Open(path + filename, FileMode.Open);
            PlayerData playerData = null;
            try {
                playerData = (PlayerData)formatter.Deserialize(file);
            } catch {
                Debug.LogError("Cannot read file: " + path);
            } finally {
                file.Close();
            }
            return playerData;
        } else {
            Debug.LogError("Cannot find file: " + path);
        }
        return null;
    }
}
