using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoMoneyIndicator : MonoBehaviour
{
    [SerializeField]
    private GameObject _noMoneyIndicator = null;
    [SerializeField]
    private int _noMoneyIndicatorTime = 2;
    public void NoMoney()
    {
        if (_noMoneyIndicator == null) return;

        //Instantiate the indicator
        GameObject indicatorInstance = Instantiate(_noMoneyIndicator);

        //Destroy the indicator after the specified time
        Destroy(indicatorInstance, _noMoneyIndicatorTime);
    }
}
