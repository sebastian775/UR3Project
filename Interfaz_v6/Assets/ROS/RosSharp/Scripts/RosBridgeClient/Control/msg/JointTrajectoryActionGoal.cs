/* 
 * This message is auto generated by ROS#. Please DO NOT modify.
 * Note:
 * - Comments from the original code will be written in their own line 
 * - Variable sized arrays will be initialized to array of size 0 
 * Please report any issues at 
 * <https://github.com/siemens/ros-sharp> 
 */



using RosSharp.RosBridgeClient.MessageTypes.Std;
using RosSharp.RosBridgeClient.MessageTypes.Actionlib;

namespace RosSharp.RosBridgeClient.MessageTypes.Control
{
    public class JointTrajectoryActionGoal : Message
    {
        public const string RosMessageName = "control_msgs/JointTrajectoryActionGoal";

        //  ====== DO NOT MODIFY! AUTOGENERATED FROM AN ACTION DEFINITION ======
        public Header header { get; set; }
        public GoalID goal_id { get; set; }
        public JointTrajectoryGoal goal { get; set; }

        public JointTrajectoryActionGoal()
        {
            this.header = new Header();
            this.goal_id = new GoalID();
            this.goal = new JointTrajectoryGoal();
        }

        public JointTrajectoryActionGoal(Header header, GoalID goal_id, JointTrajectoryGoal goal)
        {
            this.header = header;
            this.goal_id = goal_id;
            this.goal = goal;
        }
    }
}
