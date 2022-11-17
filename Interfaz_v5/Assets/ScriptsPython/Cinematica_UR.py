#ESte modelo esta basado en el trabajo de Oscar Andres Vivas

from contextlib import nullcontext
import math
import numpy as np
#import rospy
#import time


def rotation_matrix(theta1, theta2, theta3):
    """
    input
        theta1, theta2, theta3 = rotation angles in rotation order (degrees)
        oreder = rotation order of x,y,z　e.g. XZY rotation -- 'xzy'
    output
        3x3 rotation matrix (numpy array)
    """

    order='xyz'
    c1 = np.cos(theta1)# * np.pi / 180)
    s1 = np.sin(theta1)# * np.pi / 180)
    c2 = np.cos(theta2)# * np.pi / 180)
    s2 = np.sin(theta2)# * np.pi / 180)
    c3 = np.cos(theta3)# * np.pi / 180)
    s3 = np.sin(theta3)# * np.pi / 180)

    if order=='zyz':
        matrix=np.array([[c1*c2*c3-s1*s3, -c3*s1-c1*c2*s3, c1*s2],
                         [c1*s3+c2*c3*s1, c1*c3-c2*s1*s3, s1*s2],
                         [-c3*s2, s2*s3, c2]])
    elif order=='xyz':
        matrix=np.array([[c2*c3, -c2*s3, s2],
                         [c1*s3+c3*s1*s2, c1*c3-s1*s2*s3, -c2*s1],
                         [s1*s3-c1*c3*s2, c3*s1+c1*s2*s3, c1*c2]])
    elif order=='zyx':
        matrix=np.array([[c1*c2, c1*s2*s3-c3*s1, c1*c3*s2+s1*s3],
                         [c2*s1, s1*s2*s3+c1*c3, c3*s1*s2-c1*s3],
                         [-s2, c2*s3, c2*c3]])
    return matrix


def rotation_angles(matrix):
    """
    input
        matrix = 3x3 rotation matrix (numpy array)
        oreder(str) = rotation order of x, y, z : e.g, rotation XZY -- 'xzy'
    output
        theta1, theta2, theta3 = rotation angles in rotation order
    """
    order = 'xyz'
    r11, r12, r13 = matrix[0]
    r21, r22, r23 = matrix[1]
    r31, r32, r33 = matrix[2]

    if order == 'zyz':
        theta1 = np.arctan(r23 / r13)
        theta2 = np.arctan(r13 / (r33 *np.cos(theta1)))
        theta3 = np.arctan(-r32 / r31)

    elif order == 'zyx':
        theta1 = np.arctan(r21 / r11)
        theta2 = np.arctan(-r31 * np.cos(theta1) / r11)
        theta3 = np.arctan(r32 / r33)

    #theta1 = theta1* 180 / np.pi
    #theta2 = theta2 * 180 / np.pi
    #theta3 = theta3 * 180 / np.pi

    solucion = [theta1, theta2, theta3]

    return solucion


def MGI_UR(posx, posy, posz, orix, oriy, oriz):

    OriEuler = [orix, oriy, oriz]
    #Convierte ángulos de Euler a matrices de transformación:
    #SNA = eul2rotm(OriEuler,'XYZ')
    #SNA = eul2rotm(OriEuler,'ZYZ')
    SNA =  rotation_matrix(orix, oriy, oriz)

    sx=SNA[0,0]
    sy=SNA[1,0]
    sz=SNA[2,0]

    nx=SNA[0,1]
    ny=SNA[1,1]
    nz=SNA[2,1]

    ax=SNA[0,2]
    ay=SNA[1,2]
    az=SNA[2,2]

    Px=posx
    Py=posy
    Pz=posz

    #Parámetros geométricos del UR3:
    D3= 0.425
    D4= 0.39225

    R2 = 0.08920
    R3= 0.11
    R4= 0.09475
    R5= 0.08250

    #Primeras tres soluciones (t1, t5, t6) proporcionadas por SYMORO:
    #Perfecto con -1;-1;-1 ; 
    e1 = -1
    e2 = -1
    e3 = -1
    #t1=0;t2=0;t3=0;t4=0;t5=0;t6=0
    #SOLUCION PARA T1**********************************
    X=-Py
    Y=Px
    Z= R2 + R3 + R4

    SQ1 = (Y*Z + e1*X*np.sqrt(X**2 + Y**2 - Z**2))/(X**2 + Y**2)
    CQ1 = (X*Z - e1*Y*np.sqrt(X**2 + Y**2 - Z**2))/(X**2 + Y**2)
    t1 = np.arctan2( SQ1,CQ1 )

    #SOLUCION PARA T5**********************************
    Xa=-1

    Ya=  ((ay*np.cos(t1)) - (ax*np.sin(t1)))

    CQ = Ya/Xa
    t5 = np.arctan2((np.sqrt(1 - (CQ**2))*e2) , CQ )

    #SOLUCION PARA T6**********************************
    X=np.sin(t5)
    Y=0

    Z= sy*np.cos(t1) - sx*np.sin(t1)

    SQ = (Y*Z + e3*X*np.sqrt(X**2 + Y**2 - Z**2))/(X**2 + Y**2)
    CQ = (X*Z - e3*Y*np.sqrt(X**2 + Y**2 - Z**2))/(X**2 + Y**2)
    t6 = np.arctan2( SQ , CQ )

    #SOLUCION PARA T2 T3 T4 SUPONIENDO TRES ARTICULACIONES ROTOIDES EN UN PLANO
    #1T0*U0*6T4 = 1T4

    T10 = [[np.cos(t1), np.sin(t1), 0, 0], [-np.sin(t1), np.cos(t1), 0, 0], [0, 0, 1, 0], [0, 0, 0, 1]]
    U0 = [[sx, nx, ax, Px], [sy, ny, ay, Py], [sz, nz, az, Pz], [0, 0, 0, 1]]
    T65 = [[np.cos(t6), 0, np.sin(t6), 0], [-np.sin(t6), 0, np.cos(t6), 0], [0, -1, 0, 0], [0, 0, 0, 1]]
    T54 = [[np.cos(t5), 0, -np.sin(t5), 0], [-np.sin(t5), 0, -np.cos(t5), 0], [0, 1, 0, -R5], [0, 0, 0, 1]]
    #Ta = T10*U0*T65*T54
    r1 = np.dot(T10, U0)
    r2 = np.dot(T65,T54)

    Ta = np.dot(r1,r2)

    P14x = Ta[0,3]
    P14y = Ta[1,3]
    P14z = Ta[2,3]
    

    P14xz = np.sqrt((P14x**2) + (P14z**2))

    t3 = abs(np.arccos(((abs(P14xz)**2) - (D3**2) - (D4**2))/(2*D3*D4)))

    t2 = np.arctan2(P14z,P14x) - np.arcsin((D4*np.sin(t3))/(abs(P14xz)))


    T32 = [[np.cos(t3), np.sin(t3), 0, D3*np.cos(t3)], [-np.sin(t3), np.cos(t3), 0, D3*np.sin(t3)], [0, 0, 1, -R3], [0, 0, 0, 1]]
    T21 = [[np.cos(t2), 0, np.sin(t2), 0], [-np.sin(t2), 0, np.cos(t2), 0], [0, -1, 0, -R2], [0, 0, 0, 1]]
    #Tb = T32*T21*T10*U0*T65*T54  #Esto es igual a 3T4

    r3 =np.dot(T32,T21)
    Tb = np.dot(r3,Ta)

    Tb1 = Tb[0,0] #C4
    Tb2 = Tb[1,0] #S4


    t4 = np.arctan2(Tb2,Tb1)

    qsalida = [t1, t2, t3, t4, t5, t6]

    return qsalida


def MGD_UR(t1, t2, t3, t4, t5, t6):
    
    #estructura del robot
    D3= 0.24365     #UR3        #0.425      #UR5
    D4= 0.21325     #UR3        #0.39225    #UR5

    R2= 0.1519      #UR3        #0.08920    #UR5
    R3= 0.11235     #UR3        #0.11       #UR5
    R4= 0.08535     #UR3        #0.09475    #UR5
    R5= 0.0819      #UR3        #0.08250    #UR5


    S1 = np.sin(t1)
    C1 = np.cos(t1)
    S2 = np.sin(t2)
    C2 = np.cos(t2)
    S3 = np.sin(t3)
    C3 = np.cos(t3)
    S4 = np.sin(t4)
    C4 = np.cos(t4)
    S5 = np.sin(t5)
    C5 = np.cos(t5)
    S6 = np.sin(t6)
    C6 = np.cos(t6)
    S234 = np.cos(t2+t3+t4)
    C234 = np.sin(t2+t3+t4)

    #El MGD sale de la multiplicación simbólica presente en el archivo "Simbolico.m"
    
    Px = R2*S1 + R3*S1 + R4*S1 - R5*(C4*(C1*C2*S3 + C1*C3*S2) + S4*(C1*C2*C3 - C1*S2*S3)) + D4*(C1*C2*C3 - C1*S2*S3) + C1*C2*D3

    Py = D4*(C2*C3*S1 - S1*S2*S3) - R5*(C4*(C2*S1*S3 + C3*S1*S2) + S4*(C2*C3*S1 - S1*S2*S3)) - C1*R2 - C1*R3 - C1*R4 + C2*D3*S1

    Pz = D4*(C2*S3 + C3*S2) + D3*S2 - R5*(C4*(S2*S3 - C2*C3) + S4*(C2*S3 + C3*S2))

    # sx = C1*C234*C5*C6 - C6^2*S1*S5 - C1^2*S234*S6
    # sy = C234*C5*C6*S1 + C1*C6^2*S5- S1*S234*S6
    # sz = C5*C6*S234 + C234*S6
    # nx = -C1*C6*S234 - C1*C234*C5*S6 + S1*S5*S6
    # ny = -C6*S1*S234 - C234*C5*S1*S6 - C1*S5*S6
    # nz =  C234*C6 - C5*S234*S6
    # ax =  C5*S1 +C1*C234*S5
    # ay = -C1*C5 + C234*S1*S5
    # az =  S234*S5

    sx = - S6*(C4*(C1*C2*S3 + C1*C3*S2) + S4*(C1*C2*C3 - C1*S2*S3)) - C6*(S1*S5 - C5*(C4*(C1*C2*C3 - C1*S2*S3) - S4*(C1*C2*S3 + C1*C3*S2)))
    sy =  C6*(C5*(C4*(C2*C3*S1 - S1*S2*S3) - S4*(C2*S1*S3 + C3*S1*S2)) + C1*S5) - S6*(C4*(C2*S1*S3 + C3*S1*S2) + S4*(C2*C3*S1 - S1*S2*S3))
    sz =  C5*C6*(C4*(C2*S3 + C3*S2) - S4*(S2*S3 - C2*C3)) - S6*(C4*(S2*S3 - C2*C3) + S4*(C2*S3 + C3*S2))
    nx =  S6*(S1*S5 - C5*(C4*(C1*C2*C3 - C1*S2*S3) - S4*(C1*C2*S3 + C1*C3*S2))) - C6*(C4*(C1*C2*S3 + C1*C3*S2) + S4*(C1*C2*C3 - C1*S2*S3))
    ny = - C6*(C4*(C2*S1*S3 + C3*S1*S2) + S4*(C2*C3*S1 - S1*S2*S3)) - S6*(C5*(C4*(C2*C3*S1 - S1*S2*S3) - S4*(C2*S1*S3 + C3*S1*S2)) + C1*S5)
    nz =  - C6*(C4*(S2*S3 - C2*C3) + S4*(C2*S3 + C3*S2)) - C5*S6*(C4*(C2*S3 + C3*S2) - S4*(S2*S3 - C2*C3))
    ax =  S5*(C4*(C1*C2*C3 - C1*S2*S3) - S4*(C1*C2*S3 + C1*C3*S2)) + C5*S1
    ay =  S5*(C4*(C2*C3*S1 - S1*S2*S3) - S4*(C2*S1*S3 + C3*S1*S2)) - C1*C5
    az =  S5*(C4*(C2*S3 + C3*S2) - S4*(S2*S3 - C2*C3))

    rotm = [[sx, nx, ax], [sy, ny, ay], [sz, nz, az]]

    A = rotation_angles(rotm)

    rx = A[0]
    ry = A[1]
    rz = A[2]

    # rx= -3.070
    # ry=  0.238
    # rz=  0.058

    salida = [Px, Py, Pz, rx, ry, rz]	


    return salida

