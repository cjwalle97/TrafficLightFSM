using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrafficLightFSM
{
    class Program
    {
        class LightBehavior
        {
            static public void RedLightBehavior()
            {
                Console.WriteLine("I'm at the Redlight");
            }
            static public void GreenLigtBehavior()
            {
                
            }
            static public void YellowLightBehavior()
            {
                Console.WriteLine("I'm slowing down for the Yellowlight");
            }
        }

        public delegate void Handler();

        static void Main(string[] args)
        {
            FSM<State> trafficFSM = new FSM<State>();

            trafficFSM.AddTransition(LightState.Init, LightState.Red);
            trafficFSM.AddTransition(LightState.Red, LightState.Green);
            trafficFSM.AddTransition(LightState.Green, LightState.Yellow);
            trafficFSM.AddTransition(LightState.Yellow, LightState.Red);

            trafficFSM.GetState(LightState.Red).AddEnterFunction((Handler)LightBehavior.RedLightBehavior);

            trafficFSM.Start();

            while(true)
            {
                trafficFSM.Update();
            }
        }
    }
}