import rospy
from geometry_msgs.msg import PoseArray
from trajectory_msgs.msg import JointTrajectory
from t3 import TrajectoryClient

cli=TrajectoryClient()

topicSub = '/UR/Articulaciones'
variable = 0
listaPosition = []

def RecibirPuntos():
    rospy.init_node("python")
    #rospy.init_node('Python', anonymous=True)
    rospy.Subscriber(topicSub, JointTrajectory, Enlistar)

    # spin() simply keeps python from exiting until this node is stopped
    rospy.spin()

def Enlistar(Arrayposition):

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
    RecibirPuntos()
