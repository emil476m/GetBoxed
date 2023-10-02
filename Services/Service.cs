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
    
    public Box GetBOxById(int boxId)
    {
        try
        {
            //TODO Implement this
            return null;
            //return _repository.getBoxByID(boxId);
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
            throw new NotImplementedException();
            //return _repository.GetBoxPrice(boxId);
        }
        catch (Exception e)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(e);
            Console.ResetColor();
            throw new Exception("Could not get the price of this Box");
        }
    }
    
    public IEnumerable<Box> SearchForBoxes(string searchTerm, int amount)
    {
        try
        {
            throw new NotImplementedException();
            //return _repository.Search(searchTerm, amount);
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
            throw new NotImplementedException();
            //return _repository.UpdateBox(boxId,box);
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