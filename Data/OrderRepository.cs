
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
 
    public class OrderRepository
    {

        BrainWareEntities db;

        public OrderRepository()
        {
            db = new BrainWareEntities();
        }

        public async Task<IEnumerable<Order>> GetOrdersForCompany(int companyId)
        {
            //this will generate an SQL join with EF to give us the required DTO/model without having to write it here 
                return await db.Orders
                .Include(o => o.Company)
                .Include(i => i.orderproducts.Select(op => op.Product))
                .Where(o => o.company_id == companyId).ToListAsync();
       
         }
    }
}
