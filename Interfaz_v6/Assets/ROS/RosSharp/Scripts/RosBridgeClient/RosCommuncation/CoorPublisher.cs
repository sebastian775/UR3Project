using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using RosSharp.RosBridgeClient.MessageTypes.Std;
using Unity.Mathematics;


namespace RosSharp.RosBridgeClient
{
    public class CoorPublisher : UnityPublisher<MessageTypes.CartesianControl.CartesianTrajectory>
    {
        public Control_Cartesiano coordenadas;
        private int numer;
        private MessageTypes.CartesianControl.CartesianTrajectory message;
        private float velocidad;

        protected override void Start()
        {
            base.Start();
            InitializeMessage();
        }

        private void InitializeMessage()
        {
            message = new MessageTypes.CartesianControl.CartesianTrajectory();
        }

        public void coordinates()
        {
            numer = coordenadas.NumCoor;
            message.points = new MessageTypes.CartesianControl.CartesianTrajectoryPoint[numer];
            Vector3 angulos = new Vector3();

            for (int j = 0; j < numer; j++)
            {
                message.points[j] = new MessageTypes.CartesianControl.CartesianTrajectoryPoint();

                message.points[j].pose.position.x = ((float.Parse(GameObject.Find("Coor"+(j)+"/VPosX").GetComponent<TMP_Text>().text))*0.01);
                message.points[j].pose.position.y = ((float.Parse(GameObject.Find("Coor"+(j)+"/VPosY").GetComponent<TMP_Text>().text))*0.01);
                message.points[j].pose.position.z = ((float.Parse(GameObject.Find("Coor"+(j)+"/VPosZ").GetComponent<TMP_Text>().text))*0.01);
                
                angulos.x = (float.Parse(GameObject.Find("Coor"+(j)+"/VOriX").GetComponent<TMP_Text>().text));
                angulos.y = (float.Parse(GameObject.Find("Coor"+(j)+"/VOriY").GetComponent<TMP_Text>().text));
                angulos.z = (float.Parse(GameObject.Find("Coor"+(j)+"/VOriZ").GetComponent<TMP_Text>().text));
                Quaternion cuaternions = ToQuaternion(angulos);
                message.points[j].pose.orientation.x = cuaternions.x;
                message.points[j].pose.orientation.y = cuaternions.y;
                message.points[j].pose.orientation.z = cuaternions.z;
                message.points[j].pose.orientation.w = cuaternions.w;

                velocidad = GameObject.Find("SliderVel").GetComponent<Slider>().value*0.001f;
                message.points[j].twist.linear.x = velocidad;
                Debug.Log(CoputeAngles(cuaternions));
            }
            UpdateMessage();
        }

        public void coordenadaUnica()
        {
            message.points = new MessageTypes.CartesianControl.CartesianTrajectoryPoint[1];
            Vector3 angulos = new Vector3();

            message.points[0] = new MessageTypes.CartesianControl.CartesianTrajectoryPoint();

            message.points[0].pose.position.x = ((float.Parse(GameObject.Find("Cartesiano/PosX").GetComponent<TMP_InputField>().text))*0.01);
            message.points[0].pose.position.y = ((float.Parse(GameObject.Find("Cartesiano/PosY").GetComponent<TMP_InputField>().text))*0.01);
            message.points[0].pose.position.z = ((float.Parse(GameObject.Find("Cartesiano/PosZ").GetComponent<TMP_InputField>().text))*0.01);
            
            angulos.x = (float.Parse(GameObject.Find("Cartesiano/OriX").GetComponent<TMP_InputField>().text));
            angulos.y = (float.Parse(GameObject.Find("Cartesiano/OriY").GetComponent<TMP_InputField>().text));
            angulos.z = (float.Parse(GameObject.Find("Cartesiano/OriZ").GetComponent<TMP_InputField>().text));
            Quaternion cuaternions = ToQuaternion(angulos);
            message.points[0].pose.orientation.x = cuaternions.x;
            message.points[0].pose.orientation.y = cuaternions.y;
            message.points[0].pose.orientation.z = cuaternions.z;
            message.points[0].pose.orientation.w = cuaternions.w;

            velocidad = GameObject.Find("SliderVel").GetComponent<Slider>().value*0.001f;
            message.points[0].twist.linear.x = velocidad;
            //Debug.Log(CoputeAngles(cuaternions));

            UpdateMessage();
        }

    


        public static Quaternion ToQuaternion(Vector3 v)
        {

            v.x = Mathf.Deg2Rad*v.x;
            v.y = Mathf.Deg2Rad*v.y;
            v.z = Mathf.Deg2Rad*v.z;

            float cy = (float)Math.Cos(v.z * 0.5);
            float sy = (float)Math.Sin(v.z * 0.5);
            float cp = (float)Math.Cos(v.y * 0.5);
            float sp = (float)Math.Sin(v.y * 0.5);
            float cr = (float)Math.Cos(v.x * 0.5);
            float sr = (float)Math.Sin(v.x * 0.5);

            return new Quaternion
            {
                w = (cr * cp * cy + sr * sp * sy),
                x = (sr * cp * cy - cr * sp * sy),
                y = (cr * sp * cy + sr * cp * sy),
                z = (cr * cp * sy - sr * sp * cy)
            };

        }   

        private void UpdateMessage()
        {
            Publish(message);
        }

        public static float ComputeXAngle(Quaternion q)
        {
            float sinr_cosp = 2 * (q.w * q.x + q.y * q.z);
            float cosr_cosp = 1 - 2 * (q.x * q.x + q.y * q.y);
            return math.atan2(sinr_cosp, cosr_cosp);
        }

        public static float ComputeYAngle( Quaternion q)
        {
            float sinp = 2 * (q.w * q.y - q.z * q.x);
            if (math.abs(sinp) >= 1)
                return math.PI / 2 * math.sign(sinp); // use 90 degrees if out of range
            else
                return math.asin(sinp);
        }

        public static float ComputeZAngle(Quaternion q)
        {
            float siny_cosp = 2 * (q.w * q.z + q.x * q.y);
            float cosy_cosp = 1 - 2 * (q.y * q.y + q.z * q.z);
            return math.atan2(siny_cosp, cosy_cosp);
        }

        public static Vector3 CoputeAngles(Quaternion q)
        {
            return new Vector3((float)(ComputeXAngle(q)*(180/Math.PI)),(float)(ComputeYAngle(q)*(180/Math.PI)),(float)(ComputeZAngle(q)*(180/Math.PI)));
        }

        
    }
}