using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;

public class Startup : MonoBehaviour
{

    public Texture2D _noiseTexture;

    private AndroidJavaObject mHapticView;// = new AndroidJavaObject("co.tanvas.haptics.service.model.HapticView");
    private AndroidJavaObject mHapticTexture;// = new AndroidJavaObject("co.tanvas.haptics.service.model.HapticTexture");
    private AndroidJavaObject mHapticMaterial;// = new AndroidJavaObject("co.tanvas.haptics.service.model.HapticMaterial");
    private AndroidJavaObject mHapticSprite;// = new AndroidJavaObject("co.tanvas.haptics.service.model.HapticSprite");

    void Awake()
    {
        AndroidJNIHelper.debug = true;

        initHaptics();
    }

    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    void initHaptics()
    {
        AndroidJavaClass HapticApplication = new AndroidJavaClass("co.tanvas.haptics.service.app.HapticApplication");
        AndroidJavaClass HapticView = new AndroidJavaClass("co.tanvas.haptics.service.model.HapticView");
        AndroidJavaClass HapticTexture = new AndroidJavaClass("co.tanvas.haptics.service.model.HapticTexture");
        AndroidJavaClass HapticMaterial = new AndroidJavaClass("co.tanvas.haptics.service.model.HapticMaterial");
        AndroidJavaClass HapticSprite = new AndroidJavaClass("co.tanvas.haptics.service.model.HapticSprite");

        AndroidJavaObject serviceAdapter = HapticApplication.CallStatic<AndroidJavaObject>("getHapticServiceAdapter");

        mHapticView = HapticView.CallStatic<AndroidJavaObject>("create", serviceAdapter);
        mHapticView.Call("activate");

        /*
        ORIENTATION_PORTRAIT, = 0 ?
        ORIENTATION_LANDSCAPE, = 1?
        ORIENTATION_REVERSE_PORTRAIT, = 2?
        ORIENTATION_REVERSE_LANDSCAPE; = 3?
         */
        AndroidJavaObject orientation = HapticView.CallStatic<AndroidJavaObject>("getOrientationFromAndroidDisplayRotation", 1);
        mHapticView.Call("setOrientation", orientation);

        Color[] pixels = _noiseTexture.GetPixels();
        byte[] textureData = new byte[pixels.Length / 4];

        for (int i = 0; i < pixels.Length / 4; i += 4)
        {
            textureData[i] = (byte)(pixels[i].a * 255);
            textureData[i + 1] = (byte)(pixels[i + 1].r * 255);
            textureData[i + 2] = (byte)(pixels[i + 2].g * 255);
            textureData[i + 3] = (byte)(pixels[i + 3].b * 255);

        }

        mHapticTexture = HapticTexture.CallStatic<AndroidJavaObject>("create", serviceAdapter);
        int textureDataWidth = _noiseTexture.width;
        int textureDataHeight = _noiseTexture.height;
        mHapticTexture.Call("setSize", textureDataWidth, textureDataHeight);
        mHapticTexture.Call("setData", AndroidJNIHelper.ConvertToJNIArray(textureData));

        mHapticMaterial = HapticMaterial.CallStatic<AndroidJavaObject>("create", serviceAdapter);
        mHapticMaterial.Call("setTexture", 0, mHapticTexture);
            

        mHapticSprite = HapticSprite.CallStatic<AndroidJavaObject>("create", serviceAdapter);
        mHapticSprite.Call("setMaterial", mHapticMaterial);

        mHapticSprite.Call("setSize", Screen.width, Screen.height);
        mHapticSprite.Call("setPosition", 0, 0);

        mHapticView.Call("addSprite", mHapticSprite);
    }

    void OnDestroy()
    {
        mHapticView.Call("deactivate");
    }
}
