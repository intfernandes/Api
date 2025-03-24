

using Api.Dtos;
using Api.Entities;
using Api.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Api.Data.Repositories
{
    public class OrdersRepository(DataContext context) : IOrdersRepository
    {
        public async Task<Order?> Create(OrderDto dto)
        {
            var order = new Order
            {
                CustomerId = dto.CustomerId,
                EmployeeId = dto.EmployeeId,
                
            };

            await context.Orders.AddAsync(order);
            await Save();
            return order;
        }

        public async Task<IEnumerable<Order>> Get()
        {
            return await context.Orders.ToListAsync();
        }

        public async Task<IEnumerable<Order>?> Search(string input)
        {
            var customers = await context.Customers
                .Include(x => x.Orders)
                .Where(x => x.FirstName.Contains(input) || x.Email.Contains(input))
                .ToListAsync();

            var employees = await context.Employees
                .Include(x => x.Orders)
                .Where(x => x.FirstName.Contains(input) || x.Email.Contains(input))
                .ToListAsync();

            var orders = new List<Order>();

            foreach (var customer in customers)
            {

                orders.AddRange(customer.Orders);
            }

            foreach (var employee in employees)
            {
                
                orders.AddRange(employee.Orders);
            }

            return [.. orders.Distinct()];
        }

        public async Task<Order?> GetById(Guid id)
        {
            return await context.Orders.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Order?> Update(OrderDto dto)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));
            

            var order = await GetById(dto.Id);

            if (order == null) return null;

            var items = context.OrderItems.Where(x => x.OrderId == order.Id).ToList();

            foreach (var item in items)
            {
                context.OrderItems.Remove(item);
            }

            if (dto.OrderItems != null)
            {
                foreach (var item in dto.OrderItems)
                {
                    var orderItem = new OrderItem
                    {
                        OrderId = order.Id,
                        ProductId = item.ProductId,
                        Quantity = item.Quantity,
                        Price = item.Price,
                    };

                    await context.OrderItems.AddAsync(orderItem);
                }
            }

            order.Status = dto.Status;
            order.TotalAmount = dto.TotalAmount;

            await Save();
            return order;
        }

        public async Task<bool> Delete(Guid id)
        {
            var order = await GetById(id);

            if (order == null) return false;

            context.Orders.Remove(order);

            return await Save(); 
        }

        public async Task<bool> Save()
        {
            return await context.SaveChangesAsync() > 0;
        }
        
    }
}