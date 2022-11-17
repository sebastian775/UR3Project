import rospy
from geometry_msgs.msg import PoseArray
from trajectory_msgs.msg import JointTrajectory
from t3 import TrajectoryClient

cli=TrajectoryClient()

topicSub = '/UR/Coordenadas'
topicSub2 = '/UR/Figuras'
topicSub3 = '/UR/Articulaciones'
variable = 0
listaPoses = []
listaPosition = []

def RecibirDatos():
    rospy.init_node("python")
    #rospy.init_node('Python', anonymous=True)
    rospy.Subscriber(topicSub, PoseArray, Enlistar)
    rospy.Subscriber(topicSub2, PoseArray, Enlistar2)
    rospy.Subscriber(topicSub3, JointTrajectory, Enlistar3)
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

def Enlistar3(Arrayposition):

    global variable
    variable = variable+1
    #print('La variable es ',variable)
    mensaje = Arrayposition
    for i in range(len(mensaje.points)):
        listaPosition.append(mensaje.points[i].positions)
    
    #print(listaPoses)
    #cli.send_cartesian_trajectory(listaPoses)
    cli.send_joint_trajectory(listaPosition)
    variable = 0
    listaPosition.clear()
        

if __name__ == '__main__':
    RecibirDatos()
    
