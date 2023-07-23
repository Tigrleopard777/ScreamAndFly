using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class BallController : MonoBehaviour
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
        if(points>=9)
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
        if(average>10)
        {
            average = 10;
        }
        if(average<1)
        {
            average = 1;
        }
        print(average);
        this.transform.position = new Vector3(this.transform.position.x, average, this.transform.position.z);
       
        /*
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ballRB.AddForce(Vector3.up, ForceMode.Impulse);
        }
        else
        {
            ballRB.AddForce(Vector3.down * 0.01f, ForceMode.Impulse);
        }
        */
    }
}
