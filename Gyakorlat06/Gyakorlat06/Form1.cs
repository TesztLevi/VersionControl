using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Xml;
using Gyakorlat06.Entities;
using Gyakorlat06.MnbServiceReference;

namespace Gyakorlat06
{
    public partial class Form1 : Form
    {
        BindingList<RateDate> Rates = new BindingList<RateDate>();


        public Form1()
        {
            InitializeComponent();

            dataGridView1.DataSource = Rates;

            


            var mnbService = new MNBArfolyamServiceSoapClient();

            var request = new GetExchangeRatesRequestBody()
            {
                currencyNames = "EUR",
                startDate = "2020-01-01",
                endDate = "2020-06-30"
            };

            var response = mnbService.GetExchangeRates(request);

            var result = response.GetExchangeRatesResult;

            //MessageBox.Show(result);

            var xml = new XmlDocument();

            xml.LoadXml(result);

            foreach (XmlElement element in xml.DocumentElement)
            {
                var rate = new RateDate();
                Rates.Add(rate);

                rate.Date= DateTime.Parse(element.GetAttribute("date"));

                var childElement = (XmlElement)element.ChildNodes[0];
                rate.Currency = childElement.GetAttribute("curr");

                var unit = decimal.Parse(childElement.GetAttribute("unit"));
                var value = decimal.Parse(childElement.InnerText);
                if (unit != 0)
                    rate.Value = value / unit;
            }


            Dia();

        }

        private void Dia()
        {
            chart1.DataSource = Rates;

            var series = chart1.Series[0];

            series.ChartType = SeriesChartType.Line;
            series.XValueMember = "Date";
            series.YValueMembers = "Value";
            series.BorderWidth = 2;

            var legend = chart1.Legends[0];
            legend.Enabled = false;

            var chartArea = chart1.ChartAreas[0];
            chartArea.AxisX.MajorGrid.Enabled = false;
            chartArea.AxisY.MajorGrid.Enabled = false;
            chartArea.AxisY.IsStartedFromZero = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
