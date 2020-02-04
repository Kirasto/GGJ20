using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutoActivator : MonoBehaviour
{
    private void Start()
    {
        GameController.instance.canUseAll = true;
    }

    private void OnEnable()
    {
        if (GameController.instance)
            GameController.instance.canUseAll = true;
    }

    private void OnDisable()
    {
        if (GameController.instance)
            GameController.instance.canUseAll = false;
    }
}
