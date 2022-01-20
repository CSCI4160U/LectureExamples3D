using UnityEngine;
using System.IO;

public class JSONLoaderSaver {
    public static void SaveInventoryAsJSON(string path, string filename, Inventory inventory) {
        // if the folder does not exist, create it
        if (!Directory.Exists(path)) {
            Directory.CreateDirectory(path);
        }

        // convert the object to JSON
        string json = JsonUtility.ToJson(inventory);

        // save the JSOn to the save file
        File.WriteAllText(path + filename, json);
    }

    public static Inventory LoadInventoryFromJSON(string path, string filename) {
        if (File.Exists(path + filename)) {
            // read the JSON from the file
            string json = File.ReadAllText(path + filename);

            // convert the JSON into an object
            Inventory inventory = JsonUtility.FromJson<Inventory>(json);
            return inventory;
        } else {
            Debug.LogError("Cannot find file: " + path + filename);
            return null;
        }
    }
}
