using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;

public class GameSettingsScreen : MonoBehaviour
{
    // STATIC

    public static void Open()
    {
        if(FindAnyObjectByType<GameSettingsScreen>())
            return;

        Instantiate(Res.data.gameSettingsScreen);
    }
}
