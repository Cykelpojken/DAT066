using UnityEngine;
using System;
using System.Collections;

using Vuforia;

using System.Threading;

using ZXing;
using ZXing.QrCode;
using ZXing.Common;
using System.IO;

public class QrReader : MonoBehaviour
{
    public event Action<String, float[]> OnQrDetected;
    private bool cameraInitialized;
    public float X1 { get; set; }
    public float X2 { get; set; }
    public float Y1 { get; set; }
    public float Y2 { get; set; }
    private BarcodeReader barCodeReader;
    // Use this for initialization
    /**
     * Starts a new incatnce of barcode reader 
     * And tries to initialize a camrea and starts it as a Coroutine.
     * Is called wneh game object is added 
     */ 
    void Start()
    {
        barCodeReader = new BarcodeReader();
        StartCoroutine(InitializeCamera());
    }

    /**
     * Starts Vuforia, uisng vuforia scanner
     * Sets various camera options
     * 
     */
    [AddComponentMenu("System/VuforiaScanner")]
    private IEnumerator InitializeCamera()
    {
        // Waiting a little seem to avoid the Vuforia's crashes.
        yield return new WaitForSeconds(1.50f);

        var isFrameFormatSet = CameraDevice.Instance.SetFrameFormat(Image.PIXEL_FORMAT.GRAYSCALE, true);
        
        // Force autofocus.
        var isAutoFocus = CameraDevice.Instance.SetFocusMode(CameraDevice.FocusMode.FOCUS_MODE_NORMAL);
        if (!isAutoFocus)
        {
            CameraDevice.Instance.SetFocusMode(CameraDevice.FocusMode.FOCUS_MODE_NORMAL);
        }
        cameraInitialized = true;
      
    }
    // Update is called once per frame
    /**
     * Updated once per frame
     * Action OnQrDetected is performed on a qr detect
     * All listners will get the text data from the qr code
     * as well as the position in a Float array stored as following:
     *      X1 index 0: bottom left
     *      X2 index 2: top right
     *      Y1 index 2: top right
     *      Y2 index 0: bottom left   
     * 
     */
    void Update()
    {
        
        if (cameraInitialized)
        {
            try
            {
                var cameraFeed = CameraDevice.Instance.GetCameraImage(Image.PIXEL_FORMAT.GRAYSCALE);
                //cameraFeed.Pixels
                if (cameraFeed == null)
                {
                    return;
                }
                var data = barCodeReader.Decode(cameraFeed.Pixels, cameraFeed.BufferWidth, cameraFeed.BufferHeight, RGBLuminanceSource.BitmapFormat.Gray8);
                //var data = barCodeReader.Decode(fileData, cameraFeed.BufferWidth, cameraFeed.BufferHeight, RGBLuminanceSource.BitmapFormat.RGB32);
                if (data != null)
                {
                    float[] tmp = new float[4];
                    tmp[0] = this.X1 = data.ResultPoints[0].X; // index 0: bottom left
                    tmp[1] = this.X2 = data.ResultPoints[2].X; // index 2: top right
                    tmp[2] = this.Y1 = data.ResultPoints[2].Y; // index 2: top right
                    tmp[3] = this.Y2 = data.ResultPoints[0].Y; // index 0: bottom left   
                    try
                    {
                        OnQrDetected(data.Text, tmp);
                    }
                    catch (NullReferenceException E)
                    { }

                }
                else
                {
                    //No qr found
                }
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
            }
        }
    }
}
