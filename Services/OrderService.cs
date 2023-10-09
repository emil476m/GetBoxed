using Infarstructure;

namespace Service;

public class OrderService
{
    private readonly OrderReposetory _repository;

    public OrderService(OrderReposetory repository)
    {
        _repository = repository;
    }

    public IEnumerable<OrderFeed> getAllOrders()
    {
        try
        {
            return _repository.getOrderFeed();
        }
        catch (Exception e)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(e);
            Console.ResetColor();
            throw new Exception("Could not get the Order feed");
        }
    }

    public Order createOrder(int orderCustomerId, float orderTotalPrice, DateTime orderDate , List<Orders> orderBoxOrder)
    {
        int id = -1;
        try
        {
            id = _repository.CreateOrder(orderCustomerId, orderDate ,orderTotalPrice, orderBoxOrder);
            return _repository.GetOrderById(id);
        }
        catch (Exception e)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(e);
            Console.ResetColor();
            if (id != -1)
            {
                throw new Exception("order was successfully created but could not return data");
            }
            else
            {
                throw new Exception("An error occurred while creating this order");
            }
        }
    }

    public Order getOrderById(int orderId)
    {
        try
        {
            return _repository.GetOrderById(orderId);
        }
        catch (Exception e)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(e);
            Console.ResetColor();
            throw new Exception("An error occurred while fetching this order");
        }
    }

    public IEnumerable<Order> getAllOrdersByCustomerId(int customerId)
    {
        try
        {
            return _repository.GetOrderByCustoemrId(customerId);
        }
        catch (Exception e)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(e);
            Console.ResetColor();
            throw new Exception("An error occurred while fetching orders for customer");
        }
    }    
}