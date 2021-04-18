using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlWinPanelAnimation : MonoBehaviour
{
    public void StopAnimation() { GetComponent<Animator>().enabled = false; }
}
