/* 
 * This message is auto generated by ROS#. Please DO NOT modify.
 * Note:
 * - Comments from the original code will be written in their own line 
 * - Variable sized arrays will be initialized to array of size 0 
 * Please report any issues at 
 * <https://github.com/siemens/ros-sharp> 
 */



using RosSharp.RosBridgeClient.MessageTypes.Std;
using RosSharp.RosBridgeClient.MessageTypes.Geometry;

namespace RosSharp.RosBridgeClient.MessageTypes.CartesianControl
{
    public class CartesianTrajectoryPoint : Message
    {
        public const string RosMessageName = "cartesian_control_msgs/CartesianTrajectoryPoint";

        public Duration time_from_start { get; set; }
        public Pose pose { get; set; }
        public Twist twist { get; set; }
        public Accel acceleration { get; set; }
        //  A more suitable datatype would be good, see https://github.com/ros/common_msgs/issues/137
        public Accel jerk { get; set; }
        public CartesianPosture posture { get; set; }

        public CartesianTrajectoryPoint()
        {
            this.time_from_start = new Duration();
            this.pose = new Pose();
            this.twist = new Twist();
            this.acceleration = new Accel();
            this.jerk = new Accel();
            this.posture = new CartesianPosture();
        }

        public CartesianTrajectoryPoint(Duration time_from_start, Pose pose, Twist twist, Accel acceleration, Accel jerk, CartesianPosture posture)
        {
            this.time_from_start = time_from_start;
            this.pose = pose;
            this.twist = twist;
            this.acceleration = acceleration;
            this.jerk = jerk;
            this.posture = posture;
        }
    }
}
