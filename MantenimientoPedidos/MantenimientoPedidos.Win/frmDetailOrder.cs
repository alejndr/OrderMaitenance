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
    public partial class frmDetailOrder : frmBase
    {

        #region Global variables
        
        private Order _OrderData;
        
        private Customer _CustomerData;

        private Product _ProductData;

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

        /// <summary>
        /// Selection changed grid event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grdOrderDetail_SelectionChanged(object sender, EventArgs e)
        {
            CheckEnableGridButtons();
        }

        /// <summary>
        /// Modify order event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnModify_Click(object sender, EventArgs e)
        {
            ShowProductDetail();
        }

        /// <summary>
        /// Remove Product event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRemove_Click(object sender, EventArgs e)
        {
            RemoveProduct();
        }

        /// <summary>
        /// Back button event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Open Product detail form on double click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grdOrderDetail_DoubleClick(object sender, EventArgs e)
        {
            ShowProductDetail();
        }


        #endregion Events

        #region Private methods

        /// <summary>
        /// Initialize fields.
        /// </summary>
        private void InitilizeForm()
        {
            InitializeTxt();

            InitializeGrid();

            GetOrderDetail();
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

            txtName.Enabled = false;
            txtStore.Enabled = false;
            txtTerritory.Enabled = false;
            txtOrderDate.Enabled = false;
            txtOrderNumber.Enabled = false;
            txtShippingMethod.Enabled = false;
            
        }

        /// <summary>
        /// Initialize the grid.
        /// </summary>
        private void InitializeGrid()
        {
            DataGridViewColumn colOrderID = new DataGridViewTextBoxColumn()
            {
                DataPropertyName = "ID",
                Name = "colOrderID",
                HeaderText = "Order ID",
                ValueType = typeof(int),
                Visible = false
            };

            DataGridViewColumn colOrderTrackingNumber = new DataGridViewTextBoxColumn()
            {
                DataPropertyName = "TrackingNumber",
                Name = "colOrderTrackingNumber",
                HeaderText = "Tracking Number",
                ValueType = typeof(string),
                Visible = true
            };

            DataGridViewColumn colOrderQty = new DataGridViewTextBoxColumn()
            {
                DataPropertyName = "OrderQty",
                Name = "colOrderQty",
                HeaderText = "Order quantity",
                ValueType = typeof(int),
                Visible = true
            };

            DataGridViewColumn colProductName = new DataGridViewTextBoxColumn()
            {
                DataPropertyName = "ProductName",
                Name = "colProductName",
                HeaderText = "Product Name",
                ValueType = typeof(string),
                Visible = true
            };

            DataGridViewColumn colSpecialOfferID = new DataGridViewTextBoxColumn()
            {
                DataPropertyName = "SpecialOfferID",
                Name = "colSpecialOfferID",
                HeaderText = "Special Offer ID",
                ValueType = typeof(int),
                Visible = false
            };

            DataGridViewColumn colSalesOrderDetailID = new DataGridViewTextBoxColumn()
            {
                DataPropertyName = "SalesOrderDetailID",
                Name = "colSalesOrderDetailID",
                HeaderText = "Sales Order Detail ID",
                ValueType = typeof(int),
                Visible = false
            };



            grdOrderDetail.Columns.Add(colOrderID);
            grdOrderDetail.Columns.Add(colOrderTrackingNumber);
            grdOrderDetail.Columns.Add(colOrderQty);
            grdOrderDetail.Columns.Add(colProductName);
            grdOrderDetail.Columns.Add(colSpecialOfferID);
            grdOrderDetail.Columns.Add(colSalesOrderDetailID);

            // Set properties
            
            grdOrderDetail.AutoGenerateColumns = false;
            grdOrderDetail.ReadOnly = true;
            grdOrderDetail.MultiSelect = false;
            grdOrderDetail.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grdOrderDetail.RowHeadersVisible = false;
            grdOrderDetail.AllowUserToResizeRows = false;
            grdOrderDetail.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Set grid buttons 
            btnModify.Enabled = false;
            btnRemove.Enabled = false;
        }

        /// <summary>
        /// Set the grid with the order details
        /// </summary>
        private void GetOrderDetail()
        {
            ProductBussinesLogic ProductBL = new ProductBussinesLogic();
            List<Product> orderDetailList = new List<Product>();

            // Get search criteria;
            int orderId = _OrderData.ID;

            // Get the list with the data
            orderDetailList = ProductBL.GetProduct(orderId);

            // Bind grid
            grdOrderDetail.DataSource = orderDetailList;
            grdOrderDetail.ClearSelection();

            // Enable/Diasable grid buttons
            CheckEnableGridButtons();
        }

        /// <summary>
        /// Check if grid buttons can be enabled.
        /// </summary>
        private void CheckEnableGridButtons()
        {
            bool existRows = grdOrderDetail.SelectedRows.Count > 0;

            btnModify.Enabled = existRows;
            btnRemove.Enabled = existRows;
        }

        /// <summary>
        /// Show product detail form
        /// </summary>
        private void ShowProductDetail()
        {

            if (grdOrderDetail.SelectedRows.Count > 0)
            {
                _ProductData = (Product)grdOrderDetail.CurrentRow.DataBoundItem;

                frmProduct frmProduct = new frmProduct(_ProductData);
                frmProduct.ShowDialog();
            }

        }

        /// <summary>
        /// Remove fisically a product
        /// </summary>
        private void RemoveProduct()
        {
            if (ShowConfirmationMessage("Are you sure?", "Remove Product") == DialogResult.Yes)
            {
                if (grdOrderDetail.SelectedRows.Count > 0)
                {
                    List<Product> productList = new List<Product>();
                    Product product = (Product)grdOrderDetail.SelectedRows[0].DataBoundItem;

                    
                    try
                    {
                        ProductBussinesLogic productBL = new ProductBussinesLogic();

                        productBL.RemoveProduct(product);

                        // Refresh Grid
                        int orderId = _OrderData.ID;

                        // Get the list with the data
                        productList = productBL.GetProduct(orderId);
                        
                        grdOrderDetail.DataSource = productList;
                        grdOrderDetail.ClearSelection();

                        ShowMessage("Product removed successfully.");
                        
                    }
                    catch 
                    {

                        throw;
                    }
                }
            }
        }
        
        

        #endregion Private methods
        
    }
}
