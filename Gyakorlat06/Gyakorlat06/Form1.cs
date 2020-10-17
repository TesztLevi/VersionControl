using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
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



        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
