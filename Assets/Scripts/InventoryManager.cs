using UnityEngine;

public class InventoryManager : MonoBehaviour {
    public Inventory inventory = null;
    public PlayerData playerData = null;
     
    private string savePath;

    private void Awake() {
        savePath = Application.persistentDataPath + "/saveData/";
        Debug.Log("Save path: " + savePath);

        inventory = new Inventory();
        playerData = new PlayerData();
    }

    [ContextMenu("Save Inventory (JSON)")]
    public void SaveInventoryAsJSON() {
        Debug.Log("Saving inventory...");
        JSONLoaderSaver.SaveInventoryAsJSON(savePath, "inventory.json", this.inventory);
    }

    [ContextMenu("Load Inventory (JSON)")]
    public void LoadInventoryFromJSON() {
        Debug.Log("Loading inventory...");
        this.inventory = JSONLoaderSaver.LoadInventoryFromJSON(savePath, "inventory.json");
    }

    [ContextMenu("Save Inventory (Binary)")]
    public void SaveInventoryAsBinary() {
        Debug.Log("Saving inventory...");
        BinaryLoaderSaver.SaveInventoryAsBinary(savePath, "inventory.save", this.inventory);
    }

    [ContextMenu("Load Inventory (Binary)")]
    public void LoadInventoryFromBinary() {
        Debug.Log("Loading inventory...");
        this.inventory = BinaryLoaderSaver.LoadInventoryFromBinary(savePath, "inventory.save");
    }

    [ContextMenu("Save Player Data (Binary)")]
    void SavePlayerDataAsBinary() {
        Debug.Log("Saving player data...");
        BinaryLoaderSaver.SavePlayerDataAsBinary(savePath, "player_data.save", this.playerData);
    }

    [ContextMenu("Load Player Data (Binary)")]
    void LoadPlayerDataAsBinary() {
        Debug.Log("Loading player data...");
        this.playerData = BinaryLoaderSaver.LoadPlayerDataFromBinary(savePath, "player_data.save");
    }
}
