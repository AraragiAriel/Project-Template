using UnityEngine;

[System.Serializable]
public class UID
{
    public string id;

    public static implicit operator string(UID uid) =>  uid?.id;    
}
