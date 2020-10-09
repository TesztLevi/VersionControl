using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Gyakorlat05.Entities;

namespace Gyakorlat05
{
    public partial class Form1 : Form
    {
        PortfolioEntities context = new PortfolioEntities();

        List<PortfoliaItem> Portfolio = new List<PortfoliaItem>;

        List<Tick> Ticks;
        public Form1()
        {
            InitializeComponent();

            context.Ticks.ToList();
            dataGridView1.DataSource = Ticks;

            Createportfolio();

        }

        private void Createportfolio()
        {
            Portfolio.Add(new PortfoliaItem() { Index = "OTP", Volume = 10 });
            Portfolio.Add(new PortfoliaItem() { Index = "ZWACK", Volume = 10 });
            Portfolio.Add(new PortfoliaItem() { Index = "ELMU", Volume = 10 });

            dataGridView2.DataSource = Portfolio;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
