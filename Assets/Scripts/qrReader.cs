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
    byte[] fileData;
    private BarcodeReader barCodeReader;
    // Use this for initialization
    void Start()
    {
        barCodeReader = new BarcodeReader();
        StartCoroutine(InitializeCamera());
        if (File.Exists("/QR_codes/QR_2.png"))
        {
            Debug.Log("asd");
            fileData = File.ReadAllBytes("D:/hugfro/Devel/Project/Assets/QR_codes");
        }
    }
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
    void Update()
    {
        
        if (cameraInitialized)
        {
            try
            {
                var cameraFeed = CameraDevice.Instance.GetCameraImage(Image.PIXEL_FORMAT.GRAYSCALE);
                if (cameraFeed == null)
                {
                    return;
                }
                var data = barCodeReader.Decode(cameraFeed.Pixels, cameraFeed.BufferWidth, cameraFeed.BufferHeight, RGBLuminanceSource.BitmapFormat.Gray8);
                //var data = barCodeReader.Decode(fileData, cameraFeed.BufferWidth, cameraFeed.BufferHeight, RGBLuminanceSource.BitmapFormat.Gray8);
                if (data != null)
                {
                    float[] tmp = new float[4];
                    tmp[0] = this.X1 = data.ResultPoints[0].X; // index 0: bottom left
                    tmp[1] = this.X2 = data.ResultPoints[2].X; // index 2: top right
                    tmp[2] = this.Y1 = data.ResultPoints[2].Y; // index 2: top right
                    tmp[3] = this.Y2 = data.ResultPoints[0].Y; // index 0: bottom left   
                    OnQrDetected(data.Text, tmp);
                }
                else
                {
                    Debug.Log("nada");
                }
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
            }
        }
    }
}
