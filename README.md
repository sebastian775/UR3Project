
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
El siguiente ejecutable es construido en Unity3d, se puede descargar de [ejecutable](https://github.com/sebastian775/UR3Project/releases/download/v6/executable_v6.zip)
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


 ## Tabla de ejemplo para prueba 4 puntos cartesianos 
 Configuración TCP (x:0 y:-46 z:126)
<p align="center">
<img src="https://github.com/sebastian775/UR3Project/blob/sebas/Resources/TCP.png" alt="Universal Robot e-Series family" style="width: 50%;"/>
</p>
 
<table align="center"><thead><tr><th>X</th><th>Y</th><th>Z<br></th><th>Xw </th><th>Yw<br></th><th>Zw<br></th></tr></thead><tbody><tr><td>-29,2275</td><td>-41,9512</td><td>0,3958</td><td>180</td><td>-0,001</td><td>-151,348</td></tr><tr><td>-32,9853</td><td>-37,2132</td><td>-0,61192</td><td>160,638</td><td>-22,208</td><td>-147,812</td></tr><tr><td>-37,0885</td><td>-32,2901</td><td>2,3661</td><td>160,638</td><td>-22,208</td><td>-147,812</td></tr><tr><td>-28,5508</td><td>-33,7300</td><td>-0,35579</td><td>160,638</td><td>-22,208</td><td>-147,812</td></tr></tbody></table>
