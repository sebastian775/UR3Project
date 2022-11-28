# UR3 Project (in construction , V6)

 ## :stop_sign: Requirements: 
  - Ubuntu 18.04 +
  - ROS Melodic
  - URSim or Real Robot.
  - Unity 2021.3.0f1

## :scroll: Contents: 
  - **Intefaz_v6**:    Contiene el proyecto construido en Unity3d.
  - **Ejecutable**: Ejecutable del proyecto.
  - [URSim Manual Install ](https://github.com/sebastian775/UR3Project/releases/tag/v5)
  - [Video Soporte](https://youtu.be/1Sj_1Pt1_pQ)
  
 ## :exclamation: For the next version (V7): 
  - Trayectorias Libres
  - Solución de algunos bugs visuales.
  
  
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
$ sudo apt-get install ros-melodic-rosbridge-server
```

### Download v5 executable:
El siguiente ejecutable es construido en Unity3d, se puede descargar de [ejecutable](https://github.com/sebastian775/UR3Project/releases/download/executable_v5/executable_v5.zip)
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

**Nota**: Puede navegar por la platorma con las diferentes opciones que permite ejecutar, sin embargo para esta versión está inabilitada la opción de ejecutar trayetorias predefinidas.


 ## Tabla de ejemplo para prueba 3 puntos cartesianos 
 Configuración TCP y:0  y:-46 z:126
 
 <table><thead><tr><th>X</th><th>Y</th><th>Z<br></th><th>Xw </th><th>Yw<br></th><th>Zw<br></th></tr></thead><tbody><tr><td>-29,2275</td><td>-41,9512</td><td>0,3958</td><td>180</td><td>-0,001</td><td>-151,348</td></tr><tr><td>-32,9853</td><td>-37,2132</td><td>-0,61192</td><td>160,638</td><td>-22,208</td><td>-147,812</td></tr><tr><td>-37,0885</td><td>-32,2901</td><td>2,3661</td><td>160,638</td><td>-22,208</td><td>-147,812</td></tr><tr><td>-28,5508</td><td>-33,7300</td><td>-0,35579</td><td>160,638</td><td>-22,208</td><td>-147,812</td></tr></tbody></table>

