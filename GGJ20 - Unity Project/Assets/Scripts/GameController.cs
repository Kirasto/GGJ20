using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController instance = null;

    // Composants / Player Actions
    public enum ActionType
    {
        Jump = 0,
        _Count
    };

    public struct ActionInfo
    {
        public bool isUnlock;
        public bool isActivate;
    }

    public ActionInfo[] actionInfos;

    public bool IsUnlock(ActionType type)
    {
        return actionInfos[(int)type].isUnlock;
    }

    public bool IsActivate(ActionType type)
    {
        return actionInfos[(int)type].isActivate;
    }

    private void Awake()
    {
        actionInfos = new ActionInfo[(int)ActionType._Count];

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
}