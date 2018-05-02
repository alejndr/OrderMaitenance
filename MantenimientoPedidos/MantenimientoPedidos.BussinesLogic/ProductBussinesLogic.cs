using MantenimientoPedidos.DataAccess;
using MantenimientoPedidos.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MantenimientoPedidos.BussinesLogic
{
    public class ProductBussinesLogic
    {

        #region Global variables

        private ProductDataAccess _ProductDataAccess;

        #endregion Global variables

        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public ProductBussinesLogic()
        {
            _ProductDataAccess = new ProductDataAccess();
        }

        #endregion Constructor

        #region Public Methods

        /// <summary>
        /// Call the method GetOrderDetail.
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public List<Product> GetProduct(int orderId)
        {
            return _ProductDataAccess.GetProduct(orderId);
        }

        /// <summary>
        /// Call the method RemoveProduct.
        /// </summary>
        /// <param name="product"></param>
        public void RemoveProduct(Product product)
        {
            _ProductDataAccess.RemoveProduct(product);
        }

        /// <summary>
        /// Call the method ModifyOrderQty
        /// </summary>
        /// <param name="product"></param>
        /// <param name="orderQty"></param>
        public void ModifyOrderQty(Product product, int orderQty)
        {
            _ProductDataAccess.ModifyOrderQty(product, orderQty);
        }

        #endregion Public Methods
    }
}
