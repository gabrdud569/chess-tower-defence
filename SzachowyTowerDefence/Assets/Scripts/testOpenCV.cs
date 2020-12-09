using System.Runtime.InteropServices;
using UnityEngine;

public class testOpenCV : MonoBehaviour
{
    [DllImport("OpenCVDll", EntryPoint = "detectChessPiece", CallingConvention = CallingConvention.StdCall
    )] // deklaracja importu
    public static extern void detectChessPiece(long length, int width, int height, byte[] data, out float tlx, out float tly, out float brx, out float bry); // deklaracja funkcji

    [DllImport("OpenCVDll", EntryPoint = "loadClassifier", CallingConvention = CallingConvention.StdCall)]
    public static extern void loadClassifier();

    [DllImport("OpenCVDll", EntryPoint = "getValue", CallingConvention = CallingConvention.StdCall)]
    public static extern int getValue();


    MeshRenderer renderer;
    WebCamTexture webcamTexture;
    Texture defalultTexture;
    bool run = false;

    void Start()
    {
        renderer = GetComponent<MeshRenderer>();
        defalultTexture = renderer.material.mainTexture;
    }

    public void OnClickDetected(string name)
    {
        if(name == gameObject.name)
        {
            if(!run)
            {
                Enable();
            }
            else
            {
                Disable();
            }
        }
    }

    public void Enable()
    {
        webcamTexture = new WebCamTexture();
        renderer.sharedMaterial.mainTexture = webcamTexture;
        webcamTexture.Play();
        renderer.sharedMaterial.SetTexture("MainTex", webcamTexture);
        run = true;
    }

    private void Update()
    {
        if (run)
        {
            Texture2D t = new Texture2D(renderer.material.mainTexture.width, renderer.material.mainTexture.height, TextureFormat.RGB24, false);
            t.SetPixels(webcamTexture.GetPixels());
            t.Apply();
            byte[] bytes = t.GetRawTextureData();
            float tlx = 0, tly = 0, brx = 0, bry = 0;
            loadClassifier();
            detectChessPiece(bytes.Length, t.width, t.height, bytes, out tlx, out tly, out brx, out bry);
            Debug.Log(" FaceCoords :(" + tlx + ", " + tly + ", " + brx + ", " + bry + " ,) ");
            renderer.sharedMaterial.SetFloat("tlx", tlx/webcamTexture.width);
            renderer.sharedMaterial.SetFloat("tly", tly / webcamTexture.height);
            renderer.sharedMaterial.SetFloat("brx",  brx/ webcamTexture.width);
            renderer.sharedMaterial.SetFloat("bry", bry/ webcamTexture.height);
            Debug.Log(webcamTexture.width + " " + webcamTexture.height);
        }
    }


    public void Disable()
    {
        renderer.sharedMaterial.SetFloat("tlx", 0);
        renderer.sharedMaterial.SetFloat("tly", 0);
        renderer.sharedMaterial.SetFloat("brx", 0);
        renderer.sharedMaterial.SetFloat("bry", 0);

        run = false;
        renderer.material.mainTexture = defalultTexture;
        webcamTexture.Stop();
    }

    private void OnDisable()
    {
        webcamTexture?.Stop();
    }
}
