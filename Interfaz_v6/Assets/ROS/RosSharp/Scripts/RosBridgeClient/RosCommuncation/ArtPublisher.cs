using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


namespace RosSharp.RosBridgeClient
{
    public class ArtPublisher : UnityPublisher<MessageTypes.Trajectory.JointTrajectory>
    {
        private int numer;
        private double[] vector;

        public string FrameId = "Unity";

        private MessageTypes.Trajectory.JointTrajectory message;
        private double[] ValorArt;
        private double[] Velocidad;
        public Control_Articular posiciones;

        public int vel;
        
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
            
            message.points = new MessageTypes.Trajectory.JointTrajectoryPoint[1];
            message.points[0] = new MessageTypes.Trajectory.JointTrajectoryPoint();
            ValorArt = new double[6];

            for (int j = 0; j < 6; j++)
            {
                ValorArt[j] = (GameObject.Find("Art"+(j+1)+"/Slider").GetComponent<Slider>().value)*(Mathf.PI/180);
            }
            message.points[0].positions = ValorArt;
            Velocidad = new double[] {3,0,0,0,0,0};

            message.points[0].velocities = Velocidad;

            UpdateMessage();
        }
        public void Botones()
        {
            
            message.points = new MessageTypes.Trajectory.JointTrajectoryPoint[1];
            message.points[0] = new MessageTypes.Trajectory.JointTrajectoryPoint();
            ValorArt = new double[6];

            for (int j = 0; j < 6; j++)
            {
                ValorArt[j] = (GameObject.Find("Art"+(j+1)+"/Slider").GetComponent<Slider>().value)*(Mathf.PI/180);
            }
            message.points[0].positions = ValorArt;
            Velocidad = new double[] {0.1,0,0,0,0,0};

            message.points[0].velocities = Velocidad;

            UpdateMessage();
        }



        public void ListaPosiciones()
        {
            numer = posiciones.NumPos;   
            message.points = new MessageTypes.Trajectory.JointTrajectoryPoint[numer]; 

            for (int j = 0; j < numer; j++)
            {
                message.points[j] = new MessageTypes.Trajectory.JointTrajectoryPoint();
                ValorArt = new double[6];

                for (int i = 0; i < 6; i++)
                {
                    ValorArt[i] = (float.Parse(GameObject.Find("Posi"+(j)+"/VJoint"+(i+1)).GetComponent<TMP_Text>().text))*(Mathf.PI/180);
                }
                message.points[j].positions = ValorArt;
                
                Velocidad = new double[] {40,0,0,0,0,0};
                message.points[j].velocities = Velocidad;
            }
            UpdateMessage();
        }

        private void UpdateMessage()
        {
            Publish(message);
        }
    }
}