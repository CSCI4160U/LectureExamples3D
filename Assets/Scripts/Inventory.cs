[System.Serializable]
public class Inventory {
    public string chest;
    public string legs;
    public string head;

    public Inventory() {
        this.chest = "none";
        this.legs = "none";
        this.head = "none";
    }

    public Inventory(string chest, string legs, string head) {
        this.chest = chest;
        this.legs = legs;
        this.head = head;
    }
}
