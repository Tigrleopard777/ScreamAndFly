using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody ballRB;
    AudioClip clip;
    float[] samples;
    AudioSource source;
    GameObject mainCamera;
    float speed;
    public static int points = 0;

    void Start()
    {
        points = 0;
        speed = 5.0f;
        ballRB = this.GetComponent<Rigidbody>();
        source = this.GetComponent<AudioSource>();
        mainCamera = GameObject.Find("Main Camera");
        var dev = Microphone.devices;
        foreach(var d in dev)
        {
            print(d);
        }
        clip = Microphone.Start(null, true, 1, 44100);
        source.clip = clip;
        while (!(Microphone.GetPosition(null) > 0)) { }//ждём пока запись неначнётся
        source.Play();//проигрываем наш звук
        samples = new float[clip.channels * clip.samples];
        ballRB.velocity = new Vector3(0, 0, speed);
        mainCamera.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, speed);
    }

    // Update is called once per frame
    void Update()
    {
        if(points >= 9)
        {
            speed = points/8;
        }
        ballRB.velocity = new Vector3(0, 0, speed);
        mainCamera.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, speed);

        clip.GetData(samples, 0);
        float average = 0.0f;
        for (int i = 0; i < samples.Length; ++i)
        {
            average += Mathf.Abs(samples[i]);
        }
        average /= samples.Length;
        average = average * 30;
        if(average > 9.5f)
        {
            average = 9.5f;
        }
        if(average < 0.5f)
        {
            average = 0.5f;
        }
        print(average);
        this.transform.position = new Vector3(this.transform.position.x, average, this.transform.position.z);
       

    }
}
