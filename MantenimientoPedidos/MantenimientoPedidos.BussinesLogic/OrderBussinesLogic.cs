﻿using MantenimientoPedidos.DataAccess;
using MantenimientoPedidos.Entities;
using System;
using System.Collections.Generic;


namespace MantenimientoPedidos.BussinesLogic
{
    public class OrderBussinesLogic
    {

        #region Global variables

        private OrderDataAccess _OrderDataAccess;

        #endregion Global variables

        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public OrderBussinesLogic()
        {
            _OrderDataAccess = new OrderDataAccess();
        }

        #endregion Constructor

        #region Public methods

        /// <summary>
        /// Call the method Search.
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="dateFrom"></param>
        /// <param name="dateTo"></param>
        /// <returns></returns>
        public List<Order> Search(int customerId, DateTime dateFrom, DateTime dateTo)
        {
            return _OrderDataAccess.Search(customerId, dateFrom, dateTo);
        }

        /// <summary>
        /// Call the method orderToRemove.
        /// </summary>
        /// <param name="orderToRemove"></param>
        public void RemoveOrder(Order orderToRemove)
        {
            _OrderDataAccess.RemoveOrder(orderToRemove);
        }
        
        #endregion Public methods
    }
}
