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
    public partial class frmDetailOrder : Form
    {

        #region Global variables

        /// <summary>
        /// Order id.
        /// </summary>
        private Order _OrderData;

        private Customer _CustomerData;

        #endregion Global variables

        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="OrderId"></param>
        public frmDetailOrder(Customer CustomerData, Order OrderData)
        {
            InitializeComponent();

            _OrderData = OrderData;
            _CustomerData = CustomerData;

        }

        #endregion Costructor

        #region Events

        /// <summary>
        /// Initialize the form fields.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmDetailOrder_Load(object sender, EventArgs e)
        {
            InitilizeForm();
        }



        #endregion Events

        #region Private methods

        /// <summary>
        /// Initialize fields.
        /// </summary>
        private void InitilizeForm()
        {
            InitializeTxt();
        }

        /// <summary>
        /// Initialize the text fields.
        /// </summary>
        private void InitializeTxt()
        {

            txtName.Text = _CustomerData.Name;
            txtStore.Text = _CustomerData.Store;
            txtTerritory.Text = _CustomerData.Territory;

            txtOrderDate.Text = _OrderData.Date.ToShortDateString();
            txtOrderNumber.Text = _OrderData.Number.ToString();
            txtShippingMethod.Text = _OrderData.ShipMethod.ToString();
            
        }


        
        #endregion Private methods

        
    }
}
