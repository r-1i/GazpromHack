[System.Serializable]
public class CardJson 
{
    public string name;
    public string description;
    public string image;
    public CardJsonProperties properties_yes;
    public CardJsonProperties properties_no;
}

[System.Serializable]
public class CardJsonProperties
{
    public int red;
    public int yellow;
    public int green;
    public int blue;
}
