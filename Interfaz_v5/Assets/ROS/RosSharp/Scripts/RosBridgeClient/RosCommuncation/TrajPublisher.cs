using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


namespace RosSharp.RosBridgeClient
{
    public class TrajPublisher : UnityPublisher<MessageTypes.Geometry.PoseArray>
    {
        //public Control_Cartesiano coordenadas;
        private int numer;

        public string FrameId = "Unity";

        private MessageTypes.Geometry.PoseArray message;
        
        protected override void Start()
        {
            base.Start();
            InitializeMessage();
        }

        private void InitializeMessage()
        {
            message = new MessageTypes.Geometry.PoseArray();
        }

        /*public void coordinates()
        {
            numer = coordenadas.NumCoor;
            message.poses = new MessageTypes.Geometry.Pose[numer];

            for (int j = 0; j < numer; j++)
            {
                message.poses[j] = new MessageTypes.Geometry.Pose();

                message.poses[j].position.x = float.Parse(GameObject.Find("Coor"+(j)+"/VPosX").GetComponent<TMP_Text>().text);
                message.poses[j].position.y = float.Parse(GameObject.Find("Coor"+(j)+"/VPosY").GetComponent<TMP_Text>().text);
                message.poses[j].position.z = float.Parse(GameObject.Find("Coor"+(j)+"/VPosZ").GetComponent<TMP_Text>().text);
                message.poses[j].orientation.x = float.Parse(GameObject.Find("Coor"+(j)+"/VOriX").GetComponent<TMP_Text>().text);
                message.poses[j].orientation.y = float.Parse(GameObject.Find("Coor"+(j)+"/VOriY").GetComponent<TMP_Text>().text);
                message.poses[j].orientation.z = float.Parse(GameObject.Find("Coor"+(j)+"/VOriZ").GetComponent<TMP_Text>().text);
            }
            UpdateMessage();
        }*/
        public void Figuras(float[,] matriz)
        {
            message.poses = new MessageTypes.Geometry.Pose[500];
            for (int j = 0; j < 500; j++)
            {
                message.poses[j] = new MessageTypes.Geometry.Pose();

                message.poses[j].position.x = matriz[j,0];
                message.poses[j].position.y = matriz[j,1];
                message.poses[j].position.z = matriz[j,2];
                message.poses[j].orientation.x = 0;
                message.poses[j].orientation.y = 1;
                message.poses[j].orientation.z = 0;
                message.poses[j].orientation.w = 0;
            }
            UpdateMessage();
        }


        private void UpdateMessage()
        {
            Publish(message);
        }
    }
}