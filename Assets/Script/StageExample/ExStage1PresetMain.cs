using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExStage1PresetMain : PresetMain
{
    public override void BossStartEvent()
    {
        base.BossStartEvent();
        Debug.Log("child BossStartEvent");
    }
}
