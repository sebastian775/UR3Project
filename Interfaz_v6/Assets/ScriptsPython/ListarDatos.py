from email import message
import rospy
from geometry_msgs.msg import PoseArray
from trajectory_msgs.msg import JointTrajectory
from cartesian_control_msgs.msg import CartesianTrajectory
from t3 import TrajectoryClient

cli=TrajectoryClient()

topicSub = '/Coordenadas'
topicSub2 = '/Figuras'
topicSub3 = '/Articulaciones'
variable = 0
listaPoses = []
listaPosition = []

def RecibirDatos():
    rospy.init_node("python")
    #rospy.init_node('Python', anonymous=True)
    rospy.Subscriber(topicSub, CartesianTrajectory, Enlistar)
    rospy.Subscriber(topicSub2, PoseArray, Enlistar2)
    rospy.Subscriber(topicSub3, JointTrajectory, Enlistar3)
    print('Ejecutando')

    # spin() simply keeps python from exiting until this node is stopped
    rospy.spin()

def Enlistar(ArrayCartesiano):

    global variable
    variable = variable+1
    #print('La variable es ',variable)
    mensaje = ArrayCartesiano
    for i in range(len(mensaje.points)):
        listaPoses.append(mensaje.points[i].pose)
        Velocidad = mensaje.points[0].twist.linear.x
    
    #print(listaPoses)
    #print(cli.send_cartesian_trajectory(listaPoses, Velocidad))
    cli.send_cartesian_trajectory(listaPoses,Velocidad)
    variable = 0
    listaPoses.clear()

def Enlistar2(Arraypose):

    global variable
    variable = variable+1
    #print('La variable es ',variable)
    mensaje = Arraypose
    for i in range(len(mensaje.poses)):
        listaPoses.append(mensaje.poses[i])
    
    print("hola")
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
        Velocidad = mensaje.points[0].velocities[0]
    
    #print(listaPoses)
    #cli.send_cartesian_trajectory(listaPoses)
    cli.send_joint_trajectory(listaPosition, Velocidad)
    variable = 0
    listaPosition.clear()
        

if __name__ == '__main__':
    RecibirDatos()
    
