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

            BindingList<String> Currencies = new BindingList<string>();
            //comboBox1.DataSource = Currencies;

            






            RefreData();


            




            
        }

        private void RefreData()
        {
            Rates.Clear();
            dataGridView1.DataSource = Rates;

            Dia();
            String Result()
            {
                var mnbService = new MNBArfolyamServiceSoapClient();

                var request = new GetExchangeRatesRequestBody()
                {
                    currencyNames = comboBox1.SelectedItem.ToString(),
                    startDate = Pick1.Value.ToString(),
                    endDate = Pick2.Value.ToString()
                };

                var response = mnbService.GetExchangeRates(request);

                var result = response.GetExchangeRatesResult;

                return result;
            }




            var xml = new XmlDocument();

            xml.LoadXml(Result());

            foreach (XmlElement element in xml.DocumentElement)
            {
                var rate = new RateDate();
                Rates.Add(rate);

                rate.Date = DateTime.Parse(element.GetAttribute("date"));

                var childElement = (XmlElement)element.ChildNodes[0];
                rate.Currency = childElement.GetAttribute("curr");

                var unit = decimal.Parse(childElement.GetAttribute("unit"));
                var value = decimal.Parse(childElement.InnerText);
                if (unit != 0)
                    rate.Value = value / unit;
            }
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

        private void DateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            RefreData();
        }

        private void DateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            RefreData();
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreData();
        }
    }
}
