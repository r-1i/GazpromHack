using UnityEngine.Serialization;

[System.Serializable]
public class CardJson
{
    public int id;
    public bool is_start;
    public int type;
    public string name;
    public string description;
    public string image;
    public CardChoice properties_yes;
    public CardChoice properties_no;
}

[System.Serializable]
public class CardChoice
{
    public string text;
    public PosNegNeu pos;
    public PosNegNeu neg;
    public PosNegNeu neu;
}

[System.Serializable]
public class PosNegNeu
{
    public int id_next;
    public int moves_next;
    public float chance;
    public CardJsonProperties properties;
}

[System.Serializable]
public class CardJsonProperties
{
    [FormerlySerializedAs("red")] public int mood;
    [FormerlySerializedAs("green")] public int family;
    [FormerlySerializedAs("yellow")] public int money;
    [FormerlySerializedAs("blue")] public int investitions;
}
