using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gyakorlat08.Abstractions;

namespace Gyakorlat08.Entities
{
    class Present : Toy
    {
        public Present(Color box, Color ribbon)
        {}

        protected override void DrawImage(Graphics g)
        {
            throw new NotImplementedException();
        }
    }
}
