using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillActivator : MonoBehaviour
{
    public GameController.ActionType type;

    bool hasBeenUnlock = false;

    private void OnEnable()
    {
        GameController.instance.SetActivate(type);
    }

    private void OnDisable()
    {
        GameController.instance.SetActivate(type, false);
    }
}
