/*
© Siemens AG, 2017-2018
Author: Dr. Martin Bischoff (martin.bischoff@siemens.com)

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at
<http://www.apache.org/licenses/LICENSE-2.0>.
Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
*/

using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace RosSharp.RosBridgeClient
{
    public class CoordenadaSubscriber : UnitySubscriber<MessageTypes.Tf2.TFMessage>
    {
        //public Transform PublishedTransform;

        private Vector3 position;
        private Quaternion rotation;

        public TMP_Text X;
        public TMP_Text Y;
        public TMP_Text Z;
        public TMP_Text OX;
        public TMP_Text OY;
        public TMP_Text OZ;
        public TMP_Text OW;
        private bool isMessageReceived;

        protected override void Start()
        {
			base.Start();
		}
		
        private void Update()
        {
            /*if (isMessageReceived)
                ProcessMessage();*/
                
            if(Input.GetKeyDown(KeyCode.P))
            {
                ProcessMessage();
            }
        }

        protected override void ReceiveMessage(MessageTypes.Tf2.TFMessage message)
        {
            position = GetPosition(message);
            rotation = GetRotation(message);
            //isMessageReceived = true;
        }

        private void ProcessMessage()
        {
            //PublishedTransform.position = position;
            //Debug.Log(position);
            X.text = ("x: ")+position.x.ToString("f4");
            Y.text = ("y: ")+position.y.ToString("f4");
            Z.text = ("z: ")+position.z.ToString("f4");

            OX.text = ("ox: ")+rotation.x.ToString("f4");
            OY.text = ("oy: ")+rotation.y.ToString("f4");
            OZ.text = ("oz: ")+rotation.z.ToString("f4");
            OW.text = ("ow: ")+rotation.w.ToString("f4");
            //PublishedTransform.rotation = rotation;
        }

        private Vector3 GetPosition(MessageTypes.Tf2.TFMessage message)
        {        
            /*return new Vector3(1,2,3);*/
            Vector3 posi; 
                posi.x = (float)message.transforms[0].transform.translation.x;
                posi.y = (float)message.transforms[0].transform.translation.y;
                posi.z = (float)message.transforms[0].transform.translation.z;
            
            return posi;
        }

        private Quaternion GetRotation(MessageTypes.Tf2.TFMessage message)
        {
            //return new Quaternion(
            Quaternion ori;
            ori.x = (float)message.transforms[0].transform.rotation.x;
            ori.y = (float)message.transforms[0].transform.rotation.y;
            ori.z = (float)message.transforms[0].transform.rotation.z;
            ori.w = (float)message.transforms[0].transform.rotation.w;
            return ori;
        }
    }
}