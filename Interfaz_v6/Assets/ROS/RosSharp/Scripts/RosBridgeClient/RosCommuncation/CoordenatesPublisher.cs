using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


namespace RosSharp.RosBridgeClient
{
    public class CoordenatesPublisher : UnityPublisher<MessageTypes.Geometry.PoseStamped>
    {
        private Vector3 Pos;
        private Quaternion Ori;
        private float[] var = new float[6];
        public Control_Cartesiano coordenadas;
        private int numer;



        public string FrameId = "Unity";

        private MessageTypes.Geometry.PoseStamped message;
        
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
            message = new MessageTypes.Geometry.PoseStamped
            {
                header = new MessageTypes.Std.Header()
                {
                    frame_id = FrameId
                }
            };
        }

        public void coordinates()
        {
            numer = coordenadas.NumCoor;

            for (int j = 0; j < numer; j++)
            {
                /*for (int i = 0; i < 6; i++)
                {
                    string NombreCoor = coordenadas.NumToCoor(i);
                    MatrizText[j,i] = GameObject.Find("Coor"+(j)+"/V"+NombreCoor).GetComponent<TMP_Text>();
                    Debug.Log(MatrizText[j,i]);
                }*/
                //for(int i = 0; i<4; i++)
                //{
                    //TMP_Text texto = GameObject.Find("Coor"+(j)+"/V"+NombreCoor).GetComponent<TMP_Text>().text;
                    Pos.x = float.Parse(GameObject.Find("Coor"+(j)+"/VPosX").GetComponent<TMP_Text>().text);
                    Pos.y = float.Parse(GameObject.Find("Coor"+(j)+"/VPosY").GetComponent<TMP_Text>().text);
                    Pos.z = float.Parse(GameObject.Find("Coor"+(j)+"/VPosZ").GetComponent<TMP_Text>().text);
                    Ori.x = float.Parse(GameObject.Find("Coor"+(j)+"/VOriX").GetComponent<TMP_Text>().text);
                    Ori.y = float.Parse(GameObject.Find("Coor"+(j)+"/VOriY").GetComponent<TMP_Text>().text);
                    Ori.z = float.Parse(GameObject.Find("Coor"+(j)+"/VOriZ").GetComponent<TMP_Text>().text);
                    Ori.w = 1;
                /*otro[j].position.x = float.Parse(GameObject.Find("Coor"+(j)+"/VPosX").GetComponent<TMP_Text>().text);
                otro[j].position.y = float.Parse(GameObject.Find("Coor"+(j)+"/VPosY").GetComponent<TMP_Text>().text);
                otro[j].position.z = float.Parse(GameObject.Find("Coor"+(j)+"/VPosZ").GetComponent<TMP_Text>().text);
                otro[j].orientation.x = float.Parse(GameObject.Find("Coor"+(j)+"/VOriX").GetComponent<TMP_Text>().text);
                otro[j].orientation.y = float.Parse(GameObject.Find("Coor"+(j)+"/VOriY").GetComponent<TMP_Text>().text);
                otro[j].orientation.z = float.Parse(GameObject.Find("Coor"+(j)+"/VOriZ").GetComponent<TMP_Text>().text);
                //}*/

                
                //otro[j] = message;
                /*for(int i = 3; i<6; i++)
                {
                    string NombreCoor = coordenadas.NumToCoor(i);
                    TMP_Text texto = GameObject.Find("Coor"+(j)+"/V"+NombreCoor).GetComponent<TMP_Text>();
                    Ori[i] = float.Parse(texto.text);
                }*/
                //Debug.Log(Pos);
                //Debug.Log(Ori);
                UpdateMessage();
            }

        }

        private void UpdateMessage()
        {
            message.header.Update();
            //newMessage.poses = otro;

            GetGeometryPoint(Pos, message.pose.position);
            GetGeometryQuaternion(Ori, message.pose.orientation);

            Publish(message);

        }

        private static void GetGeometryPoint(Vector3 position, MessageTypes.Geometry.Point geometryPoint)
        {
            geometryPoint.x = position.x;
            geometryPoint.y = position.y;
            geometryPoint.z = position.z;
        }

        private static void GetGeometryQuaternion(Quaternion quaternion, MessageTypes.Geometry.Quaternion geometryQuaternion)
        {
            geometryQuaternion.x = quaternion.x;
            geometryQuaternion.y = quaternion.y;
            geometryQuaternion.z = quaternion.z;
            geometryQuaternion.w = quaternion.w;
        }

    }
}