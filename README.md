# UR3 Project (in construction , V5)

## Requirements: 
  - Ubuntu 18.04
  - ROS Melodic
  - URSim or Real Robot
  
   <p align="center">

 <img src="https://github.com/sebastian775/UR3Project/blob/main/v5img.png" alt="Universal Robot e-Series family" style="width: 70%;"/>

 </p>

**Note**: It is very important to have a clean workstation (catkin_ws), especially if there are Universal Robot files in it, as this may cause a conflict when compiling the catkin_ws.

The following commands are executed consecutively in a single terminal:
This allows you to create both the workspace and install packages, drives etc, that allow you to manipulate the UR robots.

```bash
# source global ros
$ source /opt/ros/melodic/setup.bash

# create a catkin workspace
$ mkdir -p catkin_ws/src && cd catkin_ws

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
sudo apt-get install ros-melodic-rosbridge-server
```

### Download v5 executable:
El siguiente ejecutable es cosntruido en Unity3d se puede descargar de [ejecutable_v5](https://github.com/sebastian775/UR3Project/releases/download/v5/Ejecutable_v5.zip) el cual dede descomprimirse en .

###  Uso de ejecutable:

**Nota**: Deberá tener inicialiado el simulador con su configuración respectiva mensionada aquí.

Desde una teminal ejecuta lo siguiente:

```bash
# Se inicicializa el controlador del robot

$ cd catkin_ws
$ source devel/setup.bash
# "Comando rápido", esto ejecuta el controlador del robot
$ roslaunch ur_robot_driver ur3_bringup.launch robot_ip:=<YOUR_IP> limited:=true
```
En una nueva terminal ejecutar:

```bash
$ roslaunch rosbridge_server rosbridge_websocket.launch
```
En la carpeta ***Interfaz_v5*** que se descarga se encuentra un archivo de pythotn llamado ***ListarDatos.py***, debe abrir una teminal en esta dirección y ejecutar este script con el siguiente comando.

```bash
$ python ListarDatos.py
```
Ahora ejecutar el archivo llamado ***PPMUR3.x86_64*** 

**Nota**: Puede navegar por la platorma con las diferentes opciones que permite ejecutar, sin embargo para esta versión está inabilitada la opción de ejecutar trayetorias predefinidas.
