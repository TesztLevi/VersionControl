using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Gyakorlat08.Abstractions;
using Gyakorlat08.Entities;

namespace Gyakorlat08
{
    public partial class Form1 : Form
    {
        private List<Toy> _toys = new List<Toy>();

        private Toy _nextToy;

        private IToyFactory _factory;

        public IToyFactory Factory
        {
            get { return _factory; }
            set { _factory = value;
                DisplayNext();
            }
        }

        private void DisplayNext()
        {
            if (_nextToy !=null)
            {
                Controls.Remove(_nextToy);
                _nextToy = Factory.CreateNew();
                _nextToy.Top = lblnext.Top + lblnext.Height + 20;
                _nextToy.Left = lblnext.Left;
                mainPanel.Controls.Add(_nextToy);
            }
        }

        public Form1()
        {
            InitializeComponent();
            Factory = new CarFactory();
        }

        private void CreateTimer_Tick(object sender, EventArgs e)
        {
            var Toy = Factory.CreateNew();
            _toys.Add(Toy);
            Toy.Left = -Toy.Width;
            mainPanel.Controls.Add(Toy);

        }

        private void ConveyorTimer_Tick(object sender, EventArgs e)
        {
            var maxposition = 0;
            foreach (var toy in _toys)
            {
                toy.MoveToy();
                if (toy.Left>maxposition)
                {
                    maxposition = toy.Left;
                }
            }
            if (maxposition > 1000)
            {
                var oldestToy = _toys[0];
                mainPanel.Controls.Remove(oldestToy);
                _toys.Remove(oldestToy);
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Factory = new CarFactory();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            Factory = new BallFactory();
        }
    }
}
