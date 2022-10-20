#!/usr/bin/python3

import rospy
from geometry_msgs.msg import PoseStamped
from trajectory_msgs.msg import JointTrajectory, JointTrajectoryPoint
from std_msgs.msg import Int64
from Cinematica_UR import MGI_UR

Trayectoria = JointTrajectory()
topicPub = '/UR/trayectoria'
topicSub = '/UR/Coordenadas'
topicSub2 = '/UR/NumCoor'

variable = 0
Cantidad = 0



def RecibirCoordenadas():
    rospy.init_node('Python', anonymous=True)

    rospy.Subscriber(topicSub2, Int64, NumeroCoordenadas)
    rospy.Subscriber(topicSub, PoseStamped, MGInverso)

    # spin() simply keeps python from exiting until this node is stopped
    rospy.spin()


def NumeroCoordenadas(data):
    global Cantidad
    Cantidad = data.data
    #print(Cantidad)


def MGInverso(pose):
    global variable
    global Cantidad
    variable = variable+1
    mensaje = pose
    
    c1 = mensaje.pose.position.x
    c2 = mensaje.pose.position.y
    c3 = mensaje.pose.position.z
    c4 = mensaje.pose.orientation.x
    c5 = mensaje.pose.orientation.y
    c6 = mensaje.pose.orientation.z
    #print(mensaje.pose.position.x, mensaje.pose.orientation.x)

    qmatriz = MGI_UR(c1, c2, c3, c4, c5, c6) 
    punto = JointTrajectoryPoint()
    #print(qmatriz)

    #punto.positions =  [0.4, 0.4, 0.4, 0.4, 0.4, 0.4]
    punto.positions =  [qmatriz[0], qmatriz[1], qmatriz[2], qmatriz[3], qmatriz[4], qmatriz[5]]
    punto.velocities = [0.5, 0.5, 0.5, 0.5, 0.5, 0.5]

    Trayectoria.points.append(punto)

    if variable == Cantidad:
        Trayectoria.header.frame_id = "Python"
        Trayectoria.header.stamp = rospy.Time.now()
        Trayectoria.joint_names = ["shoulder_link", "upper_arm_link", "forearm_link", "wrist_1_link", "wrist_2_link", "wrist_3_link"]
        pub.publish(Trayectoria)
        variable = 0
        Cantidad = 0
        ##Trayectoria.points = []
    #print(variable)


if __name__ == '__main__':
    pub = rospy.Publisher(topicPub, JointTrajectory, queue_size=10)
    RecibirCoordenadas()

