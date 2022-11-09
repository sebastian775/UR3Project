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

namespace RosSharp.RosBridgeClient
{
    public class Articulaciones : UnitySubscriber<MessageTypes.Geometry.PoseStamped>
    {
        //public Transform PublishedTransform;

        public GameObject UR3;
        private GameObject[] jointList = new GameObject[6];

        private Vector3 pos;
        private Quaternion rot;

        private Vector3 position;
        private Quaternion rotation;
        private bool isMessageReceived;

        protected override void Start()
        {
			base.Start();
            Inicializar_Art();
		}
		
        private void Update()
        {
            if (isMessageReceived)
                ProcessMessage();
        }

        protected override void ReceiveMessage(MessageTypes.Geometry.PoseStamped message)
        {
            position = GetPosition(message).Ros2Unity();
            rotation = GetRotation(message).Ros2Unity();
            isMessageReceived = true;
        }

        private void ProcessMessage()
        {
            //pos = position;
            //rot = rotation;

            for ( int i = 0; i < 6; i ++) {
            Vector3 Rotacion = jointList[i].transform.localEulerAngles;
            //Debug.Log(Rotacion);
                switch(i){
                    case 0:
                    //Rotacion.y = jointValues[i];
                    Rotacion.y = position.x;
                    break;
                    case 1:
                    Rotacion.z = position.y;
                    break;
                    case 2:
                    Rotacion.z = position.z;
                    break;
                    case 3:
                    Rotacion.y = 0;
                    break;
                    case 4:
                    Rotacion.z = 0;
                    break;
                    case 5:
                    Rotacion.y = 0;
                    break;
                }
            jointList[i].transform.localEulerAngles = Rotacion;

        }
        }
        private Vector3 GetPosition(MessageTypes.Geometry.PoseStamped message)
        {
            return new Vector3(
                (float)message.pose.position.x,
                (float)message.pose.position.y,
                (float)message.pose.position.z);
        }

        private Quaternion GetRotation(MessageTypes.Geometry.PoseStamped message)
        {
            return new Quaternion(
                (float)message.pose.orientation.x,
                (float)message.pose.orientation.y,
                (float)message.pose.orientation.z,
                (float)message.pose.orientation.w);
        }

        void Inicializar_Art() {
            var RobotChildren = UR3.GetComponentsInChildren<Transform>();
            for (int i = 0; i < RobotChildren.Length; i++) {
                if (RobotChildren[i].name == "Base01") {
                    jointList[0] = RobotChildren[i].gameObject;
                }
                else if (RobotChildren[i].name == "Shoulder") {
                    jointList[1] = RobotChildren[i].gameObject;
                }
                else if (RobotChildren[i].name == "Elbow") {
                    jointList[2] = RobotChildren[i].gameObject;
                }
                else if (RobotChildren[i].name == "Wrist01") {
                    jointList[3] = RobotChildren[i].gameObject;
                }
                else if (RobotChildren[i].name == "Wrist02") {
                    jointList[4] = RobotChildren[i].gameObject;
                }
                else if (RobotChildren[i].name == "Wrist03") {
                    jointList[5] = RobotChildren[i].gameObject;
                }
            }
        }

    }
}
