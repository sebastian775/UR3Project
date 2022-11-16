#!/usr/bin/python3

import math
import numpy as np
import rospy
from geometry_msgs.msg import PoseArray, Pose
from trajectory_msgs.msg import JointTrajectory, JointTrajectoryPoint
import matlab.engine

topic = '/UR/Puntos'
#pub = rospy.Publisher(topic, PoseArray, queue_size=10)
pub = rospy.Publisher(topic, JointTrajectory, queue_size=10)
rospy.init_node('test_move2')

#eng = matlab.engine.start_matlab()


def circular():

    Tfinal = 50; Tem = 1
    nbench = int(Tfinal/Tem)
    tray = nbench

    t = 0

    dimensiones = (tray, 1)

    x1=np.zeros(dimensiones)
    y1=np.zeros(dimensiones)

    vector = PoseArray()
    vector2 = JointTrajectory()

    
    for h in range(0,nbench-1,1):
        t=t+Tem
        val = 2*math.pi*(1/(Tfinal*t))

        x1[h] = 0.1*math.sin(val)
        y1[h] = 0.1*math.cos(val)-0.05

        Puntos = JointTrajectoryPoint()
        Puntos.positions = [0.170 + x1[h], 0.407 + y1[h], 0.478, -3.090, 0.000, 0.000]
        pose_msg = Pose()
        pose_msg.position.x =               0.170 + x1[h]
        pose_msg.position.y =               0.407 + y1[h]
        pose_msg.position.z =               0.478#*np.ones(dimensiones)
        pose_msg.orientation.x =            -3.090#*np.ones(dimensiones)
        pose_msg.orientation.y =            0.000#*np.ones(dimensiones)
        pose_msg.orientation.z =            0.000#*np.ones(dimensiones)
        
        #rospy.sleep(Tem)
        vector.poses.append(pose_msg)
        vector2.points.append(Puntos)
        #art = eng.MGI_UR5_robot(x,y,z,ox,oy,oz)
        #print(art)
        print(pose_msg.position.x)

    #pub.publish(vector2)


    # print the number of subscribers
    #rospy.loginfo("I have %s subscribers"%pub.get_num_connections())


    
        
if __name__ == '__main__':
    try:
        circular()
    except rospy.ROSInterruptException: pass