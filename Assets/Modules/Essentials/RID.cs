using UnityEngine;

public class RID
{
    // STATIC
    private static int lastUsed = 0;
    private static int next{
        get{
            lastUsed++;
            return lastUsed;
        }
    }

    public static void Initialize(){
        lastUsed = 0;
    }

    // INSTANCE
    private bool initialized = false;
    private int _id = 0;
    private int id{
        get{
            if(!initialized){
                _id = next;
                initialized = true;
            }
            return _id;
        }
    }
    
    public static implicit operator int(RID rid) =>
        rid != null ? rid.id : 0;
    
    public static implicit operator string(RID rid) =>
        rid != null ? rid.id.ToString() : "";
}
