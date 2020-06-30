using UnityEngine;
using System;
using System.Collections;

using Vuforia;
using System.Threading;
using Assets;
using UnityEngine.UI; 
using ZXing;
using ZXing.QrCode;
using ZXing.Common;


[AddComponentMenu("System/VuforiaScanner")]
public class QrCode : MonoBehaviour
{    
    private bool cameraInitialized;
    private BarcodeReader barCodeReader;

    void Start()
    {        
        barCodeReader = new BarcodeReader();
        StartCoroutine(InitializeCamera());
    }

    private IEnumerator InitializeCamera()
    {
        // Waiting a little seem to avoid the Vuforia's crashes.
        yield return new WaitForSeconds(1.25f);

        var isFrameFormatSet = CameraDevice.Instance.SetFrameFormat(PIXEL_FORMAT.RGB888, true);
        Debug.Log(String.Format("FormatSet : {0}", isFrameFormatSet));

        // Force autofocus.
        var isAutoFocus = CameraDevice.Instance.SetFocusMode(CameraDevice.FocusMode.FOCUS_MODE_CONTINUOUSAUTO);
        if (!isAutoFocus)
        {
            CameraDevice.Instance.SetFocusMode(CameraDevice.FocusMode.FOCUS_MODE_NORMAL);
        }
        Debug.Log(String.Format("AutoFocus : {0}", isAutoFocus));
        cameraInitialized = true;
    }

    private void Update()
    {
        if (cameraInitialized)
        {
            try
            {
                var cameraFeed = CameraDevice.Instance.GetCameraImage(PIXEL_FORMAT.RGB888);
                if (cameraFeed == null)
                {
                    return;
                }
                var data = barCodeReader.Decode(cameraFeed.Pixels, cameraFeed.BufferWidth, cameraFeed.BufferHeight, RGBLuminanceSource.BitmapFormat.RGB24);
                if (data != null)
                {
                    // QRCode detected.
                    Init.qrCode = data.Text;
                    var menu = new Menu();
                    menu.ChangeScene(ScenesNames.MapaFurb);
                    Debug.Log(data.Text);
                }
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
            }
        }
    }    
}