using System;
using System.Collections.Generic;
using rosapi = RosSharp.RosBridgeClient.MessageTypes.Rosapi;

namespace RosSharp.RosBridgeClient
{
    public class SetSpeed : UnityServiceCaller<rosapi.GetParamRequest, rosapi.GetParamResponse>
    {   
        protected override void ServiceCall(rosapi.GetParamResponse response)
        {
            Console.WriteLine("ROS distro: " + response.value);
        }

        protected override void valor(rosapi.GetParamRequest request)
        {
            Console.WriteLine("ROS distro: " );
        }
    }


}