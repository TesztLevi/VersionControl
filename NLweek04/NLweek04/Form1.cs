using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;

namespace NLweek04
{
    public partial class Form1 : Form
    {
        RealEstateEntities context = new RealEstateEntities();
        List<Flat> Flats;

        Excel.Application xlApp; 
        Excel.Workbook xlWB; 
        Excel.Worksheet xlSheet;


        public Form1()
        {
            InitializeComponent();
            LoadData();
            CreateExcel();

        }

        private void CreateExcel()
        {
            try
            {
                
                xlApp = new Excel.Application();

                
                xlWB = xlApp.Workbooks.Add(Missing.Value);

                
                xlSheet = xlWB.ActiveSheet;

                
                //CreateTable(); // Ennek megírása a következő feladatrészben következik

                
                xlApp.Visible = true;
                xlApp.UserControl = true;
            }
            catch (Exception ex)
            {
                string errMsg = string.Format("Error: {0}\nLine: {1}", ex.Message, ex.Source);
                MessageBox.Show(errMsg, "Error");

              
                xlWB.Close(false, Type.Missing, Type.Missing);
                xlApp.Quit();
                xlWB = null;
                xlApp = null;
            }
        }

        private void LoadData()
        {
            Flats = context.Flat.ToList();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }
    }
}
