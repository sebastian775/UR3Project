using System.Collections;
using System.Collections.Generic;
using System;
using System.Diagnostics;
using UnityEngine;

public class terminal : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ExecuteCommand("gnome-terminal -x bash -ic 'cd $HOME; roscore'");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void ExecuteCommand(string command)
    {
        Process proc = new System.Diagnostics.Process ();
        proc.StartInfo.FileName = "/bin/bash";
        proc.StartInfo.Arguments = "-c \" " + command + " \"";
        proc.StartInfo.UseShellExecute = false; 
        proc.StartInfo.RedirectStandardOutput = true;
        proc.Start ();

        while (!proc.StandardOutput.EndOfStream) {
            Console.WriteLine (proc.StandardOutput.ReadLine ());
        }
    }

}
