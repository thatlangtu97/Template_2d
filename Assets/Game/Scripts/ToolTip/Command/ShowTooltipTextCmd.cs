using System.Collections;
using System.Collections.Generic;
using strange.extensions.command.impl;
using UnityEngine;

public class ShowTooltipTextCmd : Command
{
    [Inject] public string textTooltip { get; set; }
    [Inject] public bool autoHide { get; set; }
    public override void Execute()
    {
        ToolTipText.Instance.Show(textTooltip);
    }
}
