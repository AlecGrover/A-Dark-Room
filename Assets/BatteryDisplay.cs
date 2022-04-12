using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BatteryDisplay : MonoBehaviour
{

    public TextMeshProUGUI BatteryText;
    private int _lastUpdate = 100;

    public void UpdateBatteryText(float rawRatio)
    {
        int batteryPercentage = Mathf.RoundToInt(rawRatio * 100);
        if (batteryPercentage < _lastUpdate - 10)
        {
            _lastUpdate -= 10;
            BatteryText.text = String.Format("Flashlight Battery: {0}%", _lastUpdate);
        }
        
    }

}
