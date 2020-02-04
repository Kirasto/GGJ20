using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController instance = null;

    public bool canUseAll = false;

    // Composants / Player Actions
    public enum ActionType
    {
        Jump = 0,
        Shoot,
        Dash,
        Grappling,
        SlowDown,

        None,

        Move,
        Hp,

        _Count
    };

    [System.Serializable]
    public struct ActionInfo
    {
        public bool isUnlock;
        public bool isActivate;
    }

    public ActionInfo[] actionInfos = new ActionInfo[(int)ActionType._Count];

    public bool IsUnlock(ActionType type)
    {
        return actionInfos[(int)type].isUnlock || canUseAll;
    }

    public bool IsActivate(ActionType type)
    {
        return actionInfos[(int)type].isActivate || canUseAll;
    }

    public void SetUnlock(ActionType type)
    {
        actionInfos[(int)type].isUnlock = true;
    }

    public void SetActivate(ActionType type, bool isActivate = true)
    {
        actionInfos[(int)type].isActivate = isActivate;
    }

    private void Awake()
    {
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

    public static void Respawn()
    {
        GameObject.FindGameObjectWithTag("Player").transform.position = new Vector3(1.45f, -1f, 2);
    }
}