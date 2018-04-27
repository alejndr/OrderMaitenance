using MantenimientoPedidos.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MantenimientoPedidos.Win
{
    public partial class frmProduct : frmBase
    {
        #region Global variables

        public Product _OrderDetailData;

        #endregion Global variables

        #region Constructor

        public frmProduct(Product orderDetail)
        {
            InitializeComponent();

            _OrderDetailData = orderDetail;
        }

        #endregion Constructor

        #region Events

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void frmProduct_Load(object sender, EventArgs e)
        {
            InitializeForm();
        }




        #endregion Events

        #region Private methods

        public void InitializeForm()
        {
            InitializeTxt();

        }


        /// <summary>
        /// Initialize the text fields.
        /// </summary>
        private void InitializeTxt()
        {
            txtTrackingNumber.Text = _OrderDetailData.TrackingNumber;
            nudQuantity.Value = _OrderDetailData.OrderQty;
            txtProductName.Text = _OrderDetailData.ProductName;

            txtTrackingNumber.Enabled = false;
            txtProductName.Enabled = false;
        }

        #endregion Private methods

        
    }
}
