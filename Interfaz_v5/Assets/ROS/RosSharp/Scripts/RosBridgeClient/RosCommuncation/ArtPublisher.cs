using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


namespace RosSharp.RosBridgeClient
{
    public class ArtPublisher : UnityPublisher<MessageTypes.Trajectory.JointTrajectory>
    {
        //public Control_Cartesiano coordenadas;
        private int numer;
        private double[] vector;

        public string FrameId = "Unity";

        private MessageTypes.Trajectory.JointTrajectory message;
        private double[] ValorArt;
        
        protected override void Start()
        {
            base.Start();
            InitializeMessage();
        }

        private void InitializeMessage()
        {
            message = new MessageTypes.Trajectory.JointTrajectory();
            
        }

        public void joints()
        {
            ValorArt = new double[6];
            message.points = new MessageTypes.Trajectory.JointTrajectoryPoint[1];
            message.points[0] = new MessageTypes.Trajectory.JointTrajectoryPoint();
            //ValorArt.positions = new MessageTypes.Trajectory.JointTrajectoryPoint.positions[6];

            for (int j = 0; j < 6; j++)
            {
                ValorArt[j] = (GameObject.Find("SliderArt"+(j+1)).GetComponent<Slider>().value)*(Mathf.PI/180);
            }
            message.points[0].positions = ValorArt;

            UpdateMessage();
        }

        private void UpdateMessage()
        {
            Publish(message);
        }
    }
}