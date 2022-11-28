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

// Added allocation free alternatives
// UoK , 2019, Odysseas Doumas (od79@kent.ac.uk / odydoum@gmail.com)

using UnityEngine;
using UnityEngine.UI;
using System;


namespace RosSharp.RosBridgeClient
{
    public class NumCoordenadas : UnityPublisher<MessageTypes.Std.Int64>
    {
        //public Transform PublishedTransform;
        private int Numero;
        public Control_Cartesiano coordenadas;
        
        public string FrameId = "Unity";

        private MessageTypes.Std.Int64 message;

        protected override void Start()
        {
            base.Start();
            InitializeMessage();
        }

        /*private void FixedUpdate()
        {
            UpdateMessage();
        }*/

        private void InitializeMessage()
        {
            message = new MessageTypes.Std.Int64();
        }

        public void UpdateMessage()
        {
            Numero = coordenadas.NumCoor;
            //Debug.Log(Numero);
            message.data = Convert.ToInt64(Numero);
            Publish(message);
            
        }
    }
}
