using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum GameState{
    Free,
    UI,
    Cutscene,
    Dialogue,
}

public struct GameLayer{
    public GameState state;
    public int id;

    public GameLayer(GameState state, int id){
        this.state = state;
        this.id = id;
    }
}

public static class GameStateManager
{
    public static GameState gameState {get; private set;}
    
    private static List<GameLayer> _layers = new();
    private static List<GameLayer> layers{
        get => _layers;
        set{
            _layers = value;
            
            if(layers.Count == 0)
                gameState = GameState.Free;
            else {
                // DON'T USE PRIORITY
                // gameState = layers.Last().state;

                // USE PRIORITY
                foreach(GameState state in priority){
                    if(layers.Any(layer => layer.state == state)){
                        gameState = state;
                        break;
                    }
                }
            }
            
            StaticActions.OnGameStateChange?.Invoke(gameState);
        }
    }

    private static readonly GameState[] priority = new GameState[] {
        GameState.UI,
        GameState.Cutscene,
        GameState.Dialogue,
        GameState.Free
    };
    
    public static void Initialize(){
        layers.Clear();
    }

    public static void AddLayer(GameLayer layer){
        layers.Add(layer);
    }

    public static void RemoveLayer(int id){
        layers.RemoveAll(layer => layer.id == id);
    }
}
