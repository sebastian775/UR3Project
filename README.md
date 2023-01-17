# UR3 Project

 ## :stop_sign: Requirements: 
  - Ubuntu 18.04 +
  - ROS Melodic +
  - URSim or Real Robot.
  - Unity 2021.3.0f1 +

## :scroll: Contents: 
  - **Intefaz_v7**:    Contiene el proyecto construido en Unity3d.
  - **Ejecutable**: Ejecutable del proyecto.
  - [URSim Manual Install ](https://github.com/sebastian775/UR3Project/releases/tag/v5)
  - [Video de Soporte](https://youtu.be/s2sXCVT-Ih8)
  - [Freetraj](https://youtu.be/Zs1cdkPNsmA)
   
 // ## :exclamation:

  
--------------------

   <p align="center">

 <img src="https://github.com/sebastian775/UR3Project/blob/sebas/Resources/v6img.png" alt="Universal Robot e-Series family" style="width: 70%;"/>

 </p>

**Note**: It is very important to have a clean workstation (catkin_ws), especially if there are Universal Robot files in it, as this may cause a conflict when compiling the catkin_ws.

The following commands are executed consecutively in a single terminal:
This allows you to create both the workspace and install packages, drives etc, that allow you to manipulate the UR robots.

```bash
# source global ros
$ source /opt/ros/melodic/setup.bash

# create a catkin workspace
$ mkdir UR3e && cd UR3e && mkdir -p catkin_ws/src && cd catkin_ws

# clone the driver
$ git clone https://github.com/UniversalRobots/Universal_Robots_ROS_Driver.git src/Universal_Robots_ROS_Driver

# clone the description. Currently, it is necessary to use the melodic-devel-staging branch.
$ git clone -b melodic-devel-staging https://github.com/ros-industrial/universal_robot.git src/universal_robot

# clone  the ur control cartesian
$ git clone https://github.com/UniversalRobots/Universal_Robots_ROS_controllers_cartesian.git src/Universal_Robots_ROS_controllers_cartesian

# install dependencies
$ sudo apt update -qq
$ rosdep update
$ rosdep install --from-paths src --ignore-src -y

# build the workspace
$ catkin_make

# activate the workspace (ie: source it)
$ source devel/setup.bash
```

## Rosbridge installation:
Rosbridge proporciona una API JSON para comunicar programas con ROS.

```bash
$ sudo apt-get install ros-melodic-rosbridge-server
```

### Download v6 executable:
El siguiente ejecutable es construido en Unity3d, se puede descargar de [ejecutable](https://github.com/sebastian775/UR3Project/releases/download/v7/Executable_v7.zip)

### PackegeUR3e Alternative
El siguiente [enlace](https://github.com/sebastian775/UR3Project/releases/download/v7/UR3ePackage.zip) permite descargar el paquete del proyecto opción alternativa a la sección de descarga.

###  Uso de ejecutable:

**Nota**: Se debe tener inicializado el simulador con su configuración respectiva mencionada [aquí](https://github.com/sebastian775/URSimManual/blob/sebas/README.md).

Desde una teminal ejecutar lo siguiente:

```bash
# Se inicializa el controlador del robot

$ cd catkin_ws
$ source devel/setup.bash
# "Comando rápido", esto ejecuta el controlador del robot
$ roslaunch ur_robot_driver ur3e_bringup.launch robot_ip:=<YOUR_IP> limited:=true
```
En una nueva terminal ejecutar:

```bash
$ roslaunch rosbridge_server rosbridge_websocket.launch
```
En la carpeta ***Interfaz_v6*** que se descarga se encuentra un archivo de pythotn llamado ***ListarDatos.py***, debe abrir una teminal en esta dirección y ejecutar este script con el siguiente comando:

**NOTA**: Se requiere contar con la versión de python3.

```bash
$ python3 ListarDatos.py
```
Ahora ejecutar el archivo llamado ***PPMUR3.x86_64*** contenido en esta carpeta (dar doble click sobre el ícono).


