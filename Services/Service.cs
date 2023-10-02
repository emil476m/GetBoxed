using Infarstructure;

namespace Service;

public class Service
{
    private readonly Repository _repository;

    public Service(Repository repository)
    {
        _repository = repository;
    }
    
    public IEnumerable<BoxFeed> getBoxFeed()
    {
        try
        {
            return _repository.GetBoxFeed();
        }
        catch (Exception e)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(e);
            Console.ResetColor();
            throw new Exception("Could not get the Box feed");
        }
    }
    
    public Box GetBoxById(int boxId)
    {
        try
        {
            return _repository.getBoxById(boxId);
        }
        catch (Exception e)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(e);
            Console.ResetColor();
            throw new Exception("Could not Find This Specific box");
        }
    }
    
    public float GetPriceOfBox(int boxId)
    {
        try
        {
            return _repository.GetBoxPrice(boxId);
        }
        catch (Exception e)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(e);
            Console.ResetColor();
            throw new Exception("Could not get the price of this Box");
        }
    }
    
    public IEnumerable<BoxFeed> SearchForBoxes(string searchTerm, int amount)
    {
        try
        {
            return _repository.Search(searchTerm, amount);
        }
        catch (Exception e)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(e);
            Console.ResetColor();
            throw new Exception("An Error acoured while searching for Boxes");
        }
    }

    public Box CreateBox(string name, string size, string description, float price, string boxImgUrl)
    {
        try
        {
            return _repository.CreateBox(name, size, description, price, boxImgUrl);
        }
        catch (Exception e)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(e);
            Console.ResetColor();
            throw new Exception("Could not create this Box");
        }
    }
    
    public Box UpdateBox(int boxId, Box box)
    {
        try
        {
            return _repository.UpdateBox(boxId,box.name, box.size, box.description, box.price, box.boxImgUrl);
        }
        catch (Exception e)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(e);
            Console.ResetColor();
            throw new Exception("Could not update Box");
        }
    }

    public bool DeleteBoxById(int boxId)
    {
        try
        {
            return _repository.DeleteBox(boxId);
        }
        catch (Exception e)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(e);
            Console.ResetColor();
            throw new Exception("Could not delete this Box");
        }
    }
}