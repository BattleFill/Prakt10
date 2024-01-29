using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prakt10
{
        public class CashRegister : ICloneable
        {
            public int id;
            public string productName;
            public int priceOne;
            public int quantity;

            public CashRegister(int id, string productName, int priceOne, int quantity)
            {
                this.id = id;
                this.productName = productName;
                this.priceOne = priceOne;
                this.quantity = quantity;
            }

            public object Clone()
            {
                return (CashRegister)this.MemberwiseClone();
            }
        }
}
