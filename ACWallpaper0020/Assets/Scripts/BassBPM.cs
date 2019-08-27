using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Un4seen.Bass;
using Un4seen.Bass.Misc;
using System.Runtime.InteropServices;
using System;

//Info: A Class to be able to detect desktop audio BPM
//Personal project to learn a bit of C# and try to implement this into a Unity 3D app for Wallpaper Engine

//Misc Info(What you need to know and whats used):
//Bass Library: An Audio library and API written in C with a .NET wrapper to process audio in realtime by radio42
//Callback function: Is Executable code that is passed as an argument to other code so the function can be called back (executed) at a given time;

public class BassBPM : MonoBehaviour
{
    //(Where is the data from,pointer to the buffer containing the recorded sample data,number of bytes in the buffer,user instance data given when BASS_RecordStart)
    private RECORDPROC _myRecProc;  //user defined callback function to process recorded sample data
    private int _byteswritten = 0;  //bytes of data written
    private byte[] _recBuffer;  //Create a byte array to use as buffer
    private int recHandle;  //Create an int to assing the Audio Handler
    //(frequency of your timer callback, samplerate of the audio signal to process)
    static public BPMCounter _bpm = new BPMCounter(20, 44100);    //Instatiate a class with methods to detect beats
    BASSTimer timerBPM = new BASSTimer(20);     //New instance of a timer from the Bass Lib (interval for a Tick event 20ms)

    // Start is called before the first frame update
    void Start()
    {
        if (Bass.BASS_RecordInit(-1))   //If the default device(-1) is initializaed successfully
        {
            Debug.Log("Device initialized");    //Tell that the device can be used
            _myRecProc = new RECORDPROC(MyRecording);   //Set _myRecProc as the code from MyRecording

            //Get channel handle (Record at samplerate,with num of channels,Start the recording paused,User instance data to pass to the callback function)
            recHandle = Bass.BASS_RecordStart(44100, 2, BASSFlag.BASS_RECORD_PAUSE, _myRecProc, IntPtr.Zero); //start recording but paused AND set recHandle = to recording's handle
            Bass.BASS_ChannelPlay(recHandle, false);    //resume playback of (recHandle,don't restart from beggining)
            Debug.Log("recHandle:" + recHandle.ToString()); //Show the handle ID

            //get samplerate
            BASS_CHANNELINFO info = new BASS_CHANNELINFO();    //create a new info instance
            Bass.BASS_ChannelGetInfo(recHandle, info);  //Retrieve info of the channel (channel to get info from, where to store info)
            Debug.Log("Channels: " + info.chans.ToString() + " Freq: " + info.freq.ToString());
            if (Bass.BASS_ChannelIsActive(recHandle) == BASSActive.BASS_ACTIVE_PLAYING) //Check if the channel is active
            {
                Debug.Log("The Channel is playing/recording");
            }
            timerBPM.Tick += timerBPM_Tick;     //Define the callback function for the timer
            //if I have a got a recHandle && the playback status of the recHandle is successful
            if (recHandle != 0 && Bass.BASS_ChannelPlay(recHandle, false))
            {
                _bpm.Reset(info.freq);      //set bpm  frequency to the one retrieved from the channel
                this.timerBPM.Start();      //Start the timer
            }
        }
        else
        {
            Debug.Log("Device could not be initialized");   //Tell that the operation failed
        }
    }

    void OnApplicationQuit()
    {
        Debug.Log("Application ending after " + Time.time + " seconds");    //How much time did the app run
        Bass.BASS_RecordFree();     //Free the recording resources(Is recomended to return true to instead of this method)
        Bass.BASS_Free();       //Free from memory
    }

    private bool MyRecording(int handle, IntPtr buffer, int length, IntPtr user)    //Method who recives (handler, buffer, length, user)
    {
        bool cont = false; // sets the default return value (true = stop recording)

        if (length > 0 && buffer != IntPtr.Zero)
        {   //If there is data to analize
            //increase rec buffer as needed
            if (_recBuffer == null || _recBuffer.Length < length)
            { //If the buffer is empty or less than the length
                _recBuffer = new byte[length];  //create a new buffer
            }
            //copy from unmanaged memory
            Marshal.Copy(buffer, _recBuffer, 0, length);    //copy data memory
            _byteswritten += length;    //increase the length (It is set to 44100 for 2 channels = 4 bytes )

            //stop recording after certain amount
            if (_byteswritten > 4)  //If the 4 bytes has been reached
            {
                cont = true;    //stop recording
            }
        }
        return cont;
    }

    private void timerBPM_Tick(object sender, System.EventArgs e)
    {
        if (recHandle == 0)
        {   //If there is no handle
            this.timerBPM.Stop();   //stop timer as nothing is going to be done
            Debug.Log("Error: Device not handled");
            return;
        }
        if (Bass.BASS_ChannelIsActive(recHandle) != BASSActive.BASS_ACTIVE_PLAYING)
        { //If the channel is inactive (not recording) or not handled
            this.timerBPM.Stop();
            Debug.Log("Error: Device not playing or not handled");
            return;
        }
        bool beat = _bpm.ProcessAudio(recHandle, false);    //process audio (true if successful) from the recHandle source and set false to get BPM continuosly
        if (beat)
        { //If the process audio worked, get the bpm
            //display calculated bpm
            //Debug.Log("BPM: " + _bpm.BPM.ToString("#00.0"));
        }
    }
}
