using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gyakorlat08.Abstractions;

namespace Gyakorlat08.Entities
{
    class Car : Toy
    {
        protected override void DrawImage(Graphics g)
        {
            Image i = Image.FromFile("C:/Users/Nemet/Asztali gép/Informatikai rendszerek fejlesztése/GitHube/Gyakorlat08/Gyakorlat08/image/car.png");
            g.DrawImage(i, new Rectangle(0, 0, Width, Height)) ;

        }
    }
}
