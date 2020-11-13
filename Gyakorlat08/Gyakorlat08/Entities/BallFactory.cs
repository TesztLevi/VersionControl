using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gyakorlat08.Abstractions;

namespace Gyakorlat08.Entities
{
    public class BallFactory : IToyFactory
    {
        public Toy CreateNew()
        {
            return new Ball(BallColor);
        }

        public Color BallColor { get; set; }

        
    }
}
