using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseHandler : MonoBehaviour
{
    public GameManager manager;

    public void Finish()
    {
        manager.Complete();
    }
}
