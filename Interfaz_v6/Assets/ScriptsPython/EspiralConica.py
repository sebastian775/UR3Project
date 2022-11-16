import math
import numpy as np
import rospy
from geometry_msgs.msg import PoseArray, Pose

topic = '/UR/Coordenadas'
pub = rospy.Publisher(topic, PoseArray, queue_size=10)
rospy.init_node('Trayectorias')


def EspiralConica():
    Tfinal=30.0
    Tem=0.05
    nbech=int(Tfinal/Tem)+1
    t=0
    x1 = np.zeros(nbech)
    y1 = np.zeros(nbech)
    z1 = np.zeros(nbech)

    vector = PoseArray()

    for h in range(nbech):
        t=t+Tem
        x1[h] = 0.004*t*math.sin(t)
        y1[h] = 0.004*t*math.cos(t)
        z1[h] = 0.008*t

        pose_msg = Pose()
        pose_msg.position.x =               -0.1 + x1[h]
        pose_msg.position.y =               -0.2 + y1[h]
        pose_msg.position.z =               0.1 + z1[h]
        pose_msg.orientation.x =            0
        pose_msg.orientation.y =            1
        pose_msg.orientation.z =            0
        pose_msg.orientation.w =            0
        #print(pose_msg.position.z)

        vector.poses.append(pose_msg)

    pub.publish(vector)
    # print the number of subscribers
    rospy.loginfo("I have %s subscribers"%pub.get_num_connections())



if __name__ == '__main__':
    try:
        EspiralConica()
    except rospy.ROSInterruptException: pass