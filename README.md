Editado

# UR3 Project (in construction , V5).

 ## :stop_sign: Requirements: 
  - Ubuntu 18.04 +
  - ROS Melodic
  - URSim or Real Robot.

## :scroll: Contents: 
  - **Intefaz_v5**:    Contiene el proyecto construido en Unity3d.
  - **Ejecutable**: Ejecutable del proyecto.
  - [URSim Manual Install ](https://github.com/sebastian775/URSimManual/blob/sebas/README.md)
  - [Video Soporte](https://youtu.be/1Sj_1Pt1_pQ)
  
 ## :exclamation: For the next version (V6): 
  - Se agrega el control por velocidad para la parte cartesiana.
  - Se agrega función de copiar posición articular y cartesiana del robot.
  - Se agrega función eliminar cordenada definida y lista de cordenadas.
  - Se agrega función que muestra las cordenadas de orientación y posición actuales del robot en pantalla.
  - Mejora en precisión cartesiana, articular y apariencia en este panel.
  - Se corrige problema de interpolación en las rotaciones.
  - Se modifica el panel cartesiano sustituyendo cuaterniones por grados (RPY°).
  - Se modifican las unidades de las posiciones cartesianas a centímetros.
  - Se agrega el modelo 3D de la pinza propuesta.
  - Solución de algunos bugs visuales.
  
  
--------------------
  
   <p align="center">

 <img src="https://github.com/sebastian775/UR3Project/blob/v6/Resources/v5img.png" alt="Universal Robot e-Series family" style="width: 70%;"/>

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
El siguiente ejecutable es construido en Unity3d, se puede descargar de [ejecutable](https://github.com/sebastian775/UR3Project/releases/download/v5/Ejecutable.zip)
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
En la carpeta ***Interfaz_v5*** que se descarga se encuentra un archivo de pythotn llamado ***ListarDatos.py***, debe abrir una teminal en esta dirección y ejecutar este script con el siguiente comando:

**NOTA**: Se requiere contar con la versión de python3.

```bash
$ python3 ListarDatos.py
```
Ahora ejecutar el archivo llamado ***PPMUR3.x86_64*** contenido en esta carpeta (dar doble click sobre el ícono).

**Nota**: Puede navegar por la platorma con las diferentes opciones que permite ejecutar, sin embargo para esta versión está inabilitada la opción de ejecutar trayetorias predefinidas.


 ## Tabla de ejemplo para generar una trayectoria cuadrada.

| **x** | **w** | **z** | **xw** | **yw** | **zw** | **w** |
|-------|-------|-------|--------|--------|--------|-------|
| -0,25 | -0,15 | 0,14  | 1      | 0      | 0      | 0     |
| -0,1  | -0,25 | 0,14  | 1      | 0      | 0      | 0     |
| -0,25 | -0,3  | 0,14  | 1      | 0      | 0      | 0     |
| -0,25 | -0,15 | 0,14  | 1      | 0      | 0      | 0     |
