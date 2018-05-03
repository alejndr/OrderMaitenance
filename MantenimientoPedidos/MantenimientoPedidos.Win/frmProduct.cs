using MantenimientoPedidos.BussinesLogic;
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

        public frmDetailOrder _ParentForm;

        #endregion Global variables

        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="orderDetail"></param>
        /// <param name="parent"></param>
        public frmProduct(Product orderDetail, frmDetailOrder parent)
        {
            InitializeComponent();

            _ParentForm = parent;
            _OrderDetailData = orderDetail;
        }

        #endregion Constructor

        #region Events

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Initialize the form data.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmProduct_Load(object sender, EventArgs e)
        {
            InitializeForm();
        }

        /// <summary>
        /// Modify the quantity.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            ModifyQty();
            
            this.Close();
            
        }

        /// <summary>
        /// Exit the form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
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

        /// <summary>
        /// Modify the order quantity
        /// </summary>
        private void ModifyQty()
        {

            if (nudQuantity.Value > 0)
            {
                
                if (ShowConfirmationMessage("Are you sure?", "Modify Quantity") == DialogResult.Yes)
                    {
                    Product product = _OrderDetailData;
                    int quantity = (int)nudQuantity.Value;

                    try
                    {
                        ProductBussinesLogic ProductBL = new ProductBussinesLogic();

                        ProductBL.ModifyOrderQty(product, quantity);
                        
                        _ParentForm.GetOrderDetail();
                        _ParentForm.ShowMessage("Quantity modified succesfully.");

                    }
                    catch 
                    {

                        throw;
                    }
                }
                
                
            }
            else
            {
                ShowMessage("The quantity must be greater than zero.", "Error", MessageType.Error);

            }

        }
        
        #endregion Private methods

       
    }
}
