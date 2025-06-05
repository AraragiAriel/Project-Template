using UnityEngine;

public class Achievement : MonoBehaviour
{
    public string id;

    protected void Trigger(){
        AchievementManager.instance.Trigger(id);
    }
}
