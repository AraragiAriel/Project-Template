using UnityEngine;

public class RID
{
    // STATIC
    private static int lastUsed = 0;
    public static int get{
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
            if(!initialized)
                _id = get;
            return _id;
        }
    }
    
    public static implicit operator int(RID rid) =>
        rid != null ? rid.id : 0;
}
