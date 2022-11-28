/* 
 * This message is auto generated by ROS#. Please DO NOT modify.
 * Note:
 * - Comments from the original code will be written in their own line 
 * - Variable sized arrays will be initialized to array of size 0 
 * Please report any issues at 
 * <https://github.com/siemens/ros-sharp> 
 */



using RosSharp.RosBridgeClient.MessageTypes.Geometry;
using RosSharp.RosBridgeClient.MessageTypes.Std;

namespace RosSharp.RosBridgeClient.MessageTypes.Control
{
    public class PointHeadGoal : Message
    {
        public const string RosMessageName = "control_msgs/PointHeadGoal";

        //  ====== DO NOT MODIFY! AUTOGENERATED FROM AN ACTION DEFINITION ======
        public PointStamped target { get; set; }
        public Vector3 pointing_axis { get; set; }
        public string pointing_frame { get; set; }
        public Duration min_duration { get; set; }
        public double max_velocity { get; set; }

        public PointHeadGoal()
        {
            this.target = new PointStamped();
            this.pointing_axis = new Vector3();
            this.pointing_frame = "";
            this.min_duration = new Duration();
            this.max_velocity = 0.0;
        }

        public PointHeadGoal(PointStamped target, Vector3 pointing_axis, string pointing_frame, Duration min_duration, double max_velocity)
        {
            this.target = target;
            this.pointing_axis = pointing_axis;
            this.pointing_frame = pointing_frame;
            this.min_duration = min_duration;
            this.max_velocity = max_velocity;
        }
    }
}
