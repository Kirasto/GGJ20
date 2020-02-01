using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController instance = null;

    // Composants / Player Actions
    public enum ActionType
    {
        Jump = 0,
        Shoot,
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
        return actionInfos[(int)type].isUnlock;
    }

    public bool IsActivate(ActionType type)
    {
        return actionInfos[(int)type].isActivate;
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

    static public IEnumerator Respawn()
    {
        var operation = SceneManager.LoadSceneAsync(1);

        operation.allowSceneActivation = false;
        while (operation.progress < 0.9f)
        {
            Debug.Log(operation.progress);
            yield return null;
        }

        // Set Checkpoint

        operation.allowSceneActivation = true;
    }
}