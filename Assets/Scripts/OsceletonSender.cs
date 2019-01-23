//
//	  UnityOSC - Example of usage for OSC receiver
//
//	  Copyright (c) 2012 Jorge Garcia Martin
//	  Last edit: Gerard Llorach 2nd August 2017
//
// 	  Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated 
// 	  documentation files (the "Software"), to deal in the Software without restriction, including without limitation
// 	  the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, 
// 	  and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// 
// 	  The above copyright notice and this permission notice shall be included in all copies or substantial portions 
// 	  of the Software.
//
// 	  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED 
// 	  TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL 
// 	  THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF 
// 	  CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS
// 	  IN THE SOFTWARE.
//

using UnityEngine;
using System;
using System.Net;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityOSC;

public class OsceletonSender : MonoBehaviour {

    public OscSkeletonRenderer astra;
    public string outIP = "127.0.0.1";
    public int outPort = 7110;
    public float fps = 30f;
    public float scaler = 0.001f;

    private OSCServer myServer;

    // Script initialization
    void Start() {
        // init OSC
        OSCHandler.Instance.Init();
        OSCHandler.Instance.CreateClient("myClient", IPAddress.Parse(outIP), outPort);
        fps = 1f / fps;

        StartCoroutine(SendOsc());
    }

    // Reads all the messages received between the previous update and this one
    IEnumerator SendOsc() {
        while (true) {
            foreach (var body in astra._bodies) {
                if (body.Status == Astra.BodyStatus.NotTracking) {
                    continue;
                }

                for (int i = 0; i < body.Joints.Length; i++) {
                    var bodyJoint = body.Joints[i];

                    if (bodyJoint.Status != Astra.JointStatus.NotTracked) {
                        // the Astra SDK uses a different vector class than Unity
                        Vector3 pos = new Vector3(bodyJoint.WorldPosition.X, bodyJoint.WorldPosition.Y, bodyJoint.WorldPosition.Z) * scaler;

                        /*
                        // The original Osceleton didn't use rotation, but it can be calculated here
                        // orientation matrix:
                        // 0, 			1,	 		2,
                        // 3, 			4, 			5,
                        // 6, 			7, 			8
                        // -------
                        // right(X),	up(Y), 		forward(Z)
                        
                        Vector3 jointRight = new Vector3(
                            bodyJoint.Orientation.M00,
                            bodyJoint.Orientation.M10,
                            bodyJoint.Orientation.M20);

                        Vector3 jointUp = new Vector3(
                            bodyJoint.Orientation.M01,
                            bodyJoint.Orientation.M11,
                            bodyJoint.Orientation.M21);

                        Vector3 jointForward = new Vector3(
                            bodyJoint.Orientation.M02,
                            bodyJoint.Orientation.M12,
                            bodyJoint.Orientation.M22);

                        Quaternion rot = Quaternion.LookRotation(jointForward, jointUp);
                        */

                        OSCHandler.Instance.SendMessageToClient("myClient", "/1/fader1", 0f);
                    }
                }
            }

            yield return new WaitForSeconds(fps);
        }
    }

}