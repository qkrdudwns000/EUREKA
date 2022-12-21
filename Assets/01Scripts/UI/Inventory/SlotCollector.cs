using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotCollector : MonoBehaviour
{
    static public SlotCollector inst;

    public Slot clickSlot;

    private void Start()
    {
        inst = this;
    }
}
