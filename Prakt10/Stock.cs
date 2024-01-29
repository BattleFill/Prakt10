using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prakt10
{
        public class Stock : ICloneable
        {
            public int id;
            public string productName;
            public int quantityProduct;
            public int priceOne;

            public Stock(int id, string productName, int quantityProduct, int priceOne)
            {
                this.id = id;
                this.productName = productName;
                this.quantityProduct = quantityProduct;
                this.priceOne = priceOne;
            }

            public object Clone()
            {
                return (Stock)this.MemberwiseClone();
            }
        }
}
