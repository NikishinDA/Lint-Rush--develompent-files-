using System.Collections.Generic;
using UnityEngine;

namespace _Internal._Dev.Management.Scripts
{
    public class GameEventsHandler
    {
        public static readonly GameOverEvent GameOverEvent = new GameOverEvent();
        public static readonly GameStartEvent GameStartEvent = new GameStartEvent();
        public static readonly GamePauseEvent GamePauseEvent = new GamePauseEvent();
        public static readonly CoinPickUpEvent CoinPickUpEvent = new CoinPickUpEvent();
        public static readonly ShowWinPopUp ShowWinPopUp = new ShowWinPopUp();
        public static readonly GameInitializeEvent GameInitializeEvent = new GameInitializeEvent();
        public static readonly MinigameStartEvent MinigameStartEvent = new MinigameStartEvent();
        public static readonly ShowToyEvent ShowToyEvent = new ShowToyEvent();
        public static readonly GameProgressEvent GameProgressEvent = new GameProgressEvent();
        public static readonly DebugEvent DebugEvent = new DebugEvent();
        public static readonly PickUpHair PickUpHair = new PickUpHair();
        public static readonly PickUpObstacle PickUpObstacle = new PickUpObstacle();
        public static readonly MinigamePlayerShotEvent MinigamePlayerShotEvent = new MinigamePlayerShotEvent();
    }

    public class GameEvent
    {
    
    }


    public class GamePauseEvent : GameEvent
    {
        public bool SetPause;
    }

    public class GameOverEvent : GameEvent
    {
        public bool IsWin;
        public float Progress;
    }

    public class GameProgressEvent : GameEvent { }

    public class GameStartEvent : GameEvent
    {
        public int LevelSetLength;
        public float SetSpeedZ;
        public float SetSpeedX;
        public float SetSpeedZMax;
    }

    public class DebugEvent : GameEvent
    {
        public float HSpeed;
        public float VSpeed;
        public float PlusProg;
        public float MinusProg;
    }

    public class CoinPickUpEvent : GameEvent { }


    public class GameInitializeEvent : GameEvent
    {
        public int LevelLength;
    }
    public class ShowWinPopUp : GameEvent
    {
    }

    public class MinigameStartEvent : GameEvent
    {
        public GameObject Player;
    }
    public class ShowToyEvent : GameEvent
    {
    
    }

    public class PickUpHair : GameEvent
    {
    }

    public class PickUpObstacle : GameEvent
    {
    
    }

    public class MinigamePlayerShotEvent : GameEvent
    {
    
    }

    public class CameraInPositionEvent : GameEvent
    {
    
    }
}