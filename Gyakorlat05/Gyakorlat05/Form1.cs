using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
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

        List<Tick> Ticks;



        List<PortfoliaItem> Portfolio = new List<PortfoliaItem>();

        

        public Form1()
        {
            InitializeComponent();

            Ticks = context.Ticks.ToList();

            dataGridView1.DataSource = Ticks;
            

            Createportfolio();

            decimal GetPortfolioValue(DateTime date)
            {
                decimal value = 0;
                foreach (var item in Portfolio)
                {
                    var last = (from x in Ticks
                                where item.Index == x.Index.Trim()
                                   && date <= x.TradingDay
                                select x)
                                .First();
                    value += (decimal)last.Price * item.Volume;
                }
                return value;
            }

            List<decimal> Nyereségek = new List<decimal>();
            int intervalum = 30;
            DateTime kezdőDátum = (from x in Ticks select x.TradingDay).Min();
            DateTime záróDátum = new DateTime(2016, 12, 30);
            TimeSpan z = záróDátum - kezdőDátum;
            for (int i = 0; i < z.Days - intervalum; i++)
            {
                decimal ny = GetPortfolioValue(kezdőDátum.AddDays(i + intervalum))
                           - GetPortfolioValue(kezdőDátum.AddDays(i));
                Nyereségek.Add(ny);
                Console.WriteLine(i + " " + ny);
            }

            var nyereségekRendezve = (from x in Nyereségek
                                      orderby x
                                      select x)
                                        .ToList();
            MessageBox.Show(nyereségekRendezve[nyereségekRendezve.Count() / 5].ToString());




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

        private void Button1_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            if (sfd.ShowDialog() != DialogResult.OK) return;
            using (StreamWriter sw = new StreamWriter(sfd.FileName, false, Encoding.UTF8))
            {
                sw.Write("időszak");
                sw.Write(";");
                sw.Write("Nyereség");
                sw.WriteLine();

                foreach (var s in Ticks)
                { 
                    sw.Write(s.TradingDay);
                    sw.Write(";");
                    sw.Write(s.Volume);

                    sw.WriteLine();
                }
            }
        }
    }
}
