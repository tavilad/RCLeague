using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsMenuCanvas : MonoBehaviour {
    public void OnClickFULLHD() {
        Screen.SetResolution(1980, 1080, true);
    }

    public void OnClickHD() {
        Screen.SetResolution(1280, 720, true);
    }

    public void OnClickLowRes() {
        Screen.SetResolution(800, 600, true);
    }

    public void OnClickKPH() {
        GameManager.Instance.SpeedMeasurement = SpeedMeasurement.KPH;
    }

    public void OnClickMPH() {
        GameManager.Instance.SpeedMeasurement = SpeedMeasurement.MPH;
    }

    public void OnClickBack() {
        MainCanvasManager.Instance.MainMenuCanvas.transform.SetAsLastSibling();
    }
}