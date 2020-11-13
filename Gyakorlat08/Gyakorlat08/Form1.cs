using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Gyakorlat08.Entities;

namespace Gyakorlat08
{
    public partial class Form1 : Form
    {
        private List<Ball> _balls = new List<Ball>();

        private BallFactory _factory;

        public BallFactory Factory
        {
            get { return _factory; }
            set { _factory = value; }
        }







        public Form1()
        {
            InitializeComponent();
            Factory = new BallFactory();
        }

        private void CreateTimer_Tick(object sender, EventArgs e)
        {
            var Ball = Factory.CreateNew();
            _balls.Add(Ball);
            Ball.Left = -Ball.Width;
            mainPanel.Controls.Add(Ball);

        }

        private void ConveyorTimer_Tick(object sender, EventArgs e)
        {
            var maxposition = 0;
            foreach (var ball in _balls)
            {
                ball.MoveToy();
                if (ball.Left>maxposition)
                {
                    maxposition = ball.Left;
                }
            }
            if (maxposition > 1000)
            {
                var oldestBall = _balls[0];
                mainPanel.Controls.Remove(oldestBall);
                _balls.Remove(oldestBall);
            }
        }
    }
}
