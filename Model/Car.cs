using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Car : IEquipment
    {
        public int Quality { get; }
        public int Performance { get; }
        public int Speed { get; }
        public bool IsBroken { get; }

        public Car(int quality, int performance, int speed, bool isBroken)
        {
            Quality = quality;
            Performance = performance;
            Speed = speed;
            IsBroken = isBroken;
        }
    }
}
