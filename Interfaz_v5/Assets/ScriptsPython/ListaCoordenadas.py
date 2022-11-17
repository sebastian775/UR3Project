
import rospy
from geometry_msgs.msg import PoseArray
from t3 import TrajectoryClient

cli=TrajectoryClient()

topicSub = '/UR/Coordenadas'
topicSub2 = '/UR/Figuras'
variable = 0
listaPoses = []

def RecibirCoordenadas():
    rospy.init_node("python")
    #rospy.init_node('Python', anonymous=True)
    rospy.Subscriber(topicSub, PoseArray, Enlistar)
    rospy.Subscriber(topicSub2, PoseArray, Enlistar2)
    print('Ejecutando')

    # spin() simply keeps python from exiting until this node is stopped
    rospy.spin()

def Enlistar(Arraypose):

    global variable
    variable = variable+1
    #print('La variable es ',variable)
    mensaje = Arraypose
    for i in range(len(mensaje.poses)):
        listaPoses.append(mensaje.poses[i])
    
    #print(listaPoses)
    cli.send_cartesian_trajectory(listaPoses)
    #cli.send_joint_trajectory(listaPoses)
    variable = 0
    listaPoses.clear()

def Enlistar2(Arraypose):

    global variable
    variable = variable+1
    #print('La variable es ',variable)
    mensaje = Arraypose
    for i in range(len(mensaje.poses)):
        listaPoses.append(mensaje.poses[i])
    
    #print(listaPoses)
    cli.send_trajectory(listaPoses)
    #cli.send_joint_trajectory(listaPoses)
    variable = 0
    listaPoses.clear()
        

if __name__ == '__main__':
    RecibirCoordenadas()
    
