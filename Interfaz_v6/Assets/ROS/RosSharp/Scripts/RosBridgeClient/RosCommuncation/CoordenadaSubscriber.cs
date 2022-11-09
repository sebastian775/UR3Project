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
using Unity.Mathematics;
using System.Collections;
using System.Collections.Generic;
using TMPro;

namespace RosSharp.RosBridgeClient
{
    public class CoordenadaSubscriber : UnitySubscriber<MessageTypes.Tf2.TFMessage>
    {
        //public Transform PublishedTransform;

        private Vector3 position;
        private Quaternion rotationQ;
        private Vector3 rotationD;

        public List<Text> Cartesiano = new List<Text>(); 
        public List<TMP_Text> Posicion = new List<TMP_Text>(); 
        /*public Text X;
        public Text Y;
        public Text Z;
        public Text OX;
        public Text OY;
        public Text OZ;*/
        private bool Visible;

        protected override void Start()
        {
			base.Start();
            Visible = false;
		}
		
        private void Update()
        {
            if (Visible)
            {
                ProcessMessage();
            }  
            else
            {
                HideMessage();
            }
            if(Input.GetKeyDown(KeyCode.P))
            {
                Visible=!Visible;
                //ProcessMessage();
            }
            MensajeCartesiano();
        }

        protected override void ReceiveMessage(MessageTypes.Tf2.TFMessage message)
        {
            position = GetPosition(message);
            rotationQ = GetRotation(message);
        }

        private void ProcessMessage()
        {
            Posicion[0].text = ("Px: ")+position.x.ToString("f3");
            Posicion[1].text = ("Py: ")+position.y.ToString("f3");
            Posicion[2].text = ("Pz: ")+position.z.ToString("f3");

            rotationD = ConvertirAngulos(rotationQ);
            
            Posicion[3].text = ("Rx: ")+rotationD.x.ToString("f2");
            Posicion[4].text = ("Ry: ")+rotationD.y.ToString("f2");
            Posicion[5].text = ("Rz: ")+rotationD.z.ToString("f2");
        }

        private void MensajeCartesiano()
        {
            //Cartesiano
            Cartesiano[0].text = position.x.ToString("f3");
            Cartesiano[1].text = position.y.ToString("f3");
            Cartesiano[2].text = position.z.ToString("f3");

            rotationD = ConvertirAngulos(rotationQ);

            Cartesiano[3].text = rotationD.x.ToString("f2");
            Cartesiano[4].text = rotationD.y.ToString("f2");
            Cartesiano[5].text = rotationD.z.ToString("f2");
        }
        private void HideMessage()
        {
            Posicion[0].text = ("");
            Posicion[1].text = ("");
            Posicion[2].text = ("");
            Posicion[3].text = ("");
            Posicion[4].text = ("");
            Posicion[5].text = ("");
        }

        private Vector3 GetPosition(MessageTypes.Tf2.TFMessage message)
        {        
            Vector3 posi; 
                posi.x = (float)message.transforms[0].transform.translation.x*100f;
                posi.y = (float)message.transforms[0].transform.translation.y*100f;
                posi.z = (float)message.transforms[0].transform.translation.z*100f;
            
            return posi;
        }

        private Quaternion GetRotation(MessageTypes.Tf2.TFMessage message)
        {
            Quaternion ori;
            ori.x = (float)message.transforms[0].transform.rotation.x;
            ori.y = (float)message.transforms[0].transform.rotation.y;
            ori.z = (float)message.transforms[0].transform.rotation.z;
            ori.w = (float)message.transforms[0].transform.rotation.w;
            return ori;
        }

        public static float XAngulo(Quaternion q)
        {
            float sinr_cosp = 2 * (q.w * q.x + q.y * q.z);
            float cosr_cosp = 1 - 2 * (q.x * q.x + q.y * q.y);
            return math.atan2(sinr_cosp, cosr_cosp);
        }

        public static float YAngulo( Quaternion q)
        {
            float sinp = 2 * (q.w * q.y - q.z * q.x);
            if (math.abs(sinp) >= 1)
                return math.PI / 2 * math.sign(sinp); // use 90 degrees if out of range
            else
                return math.asin(sinp);
        }

        public static float ZAngulo(Quaternion q)
        {
            float siny_cosp = 2 * (q.w * q.z + q.x * q.y);
            float cosy_cosp = 1 - 2 * (q.y * q.y + q.z * q.z);
            return math.atan2(siny_cosp, cosy_cosp);
        }

        public static Vector3 ConvertirAngulos(Quaternion q)
        {
            Vector3 angulos = new Vector3();
            angulos.x = (float)(XAngulo(q)*(180/math.PI));
            angulos.y = (float)(YAngulo(q)*(180/math.PI));
            angulos.z = (float)(ZAngulo(q)*(180/math.PI));
            return angulos;
        }

    }
}