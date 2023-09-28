using Infarstructure;

namespace Service;

public class Service
{
    private readonly Repository _repository;

    public Service(Repository repository)
    {
        _repository = repository;
    }

    public Box CreateBoc(string name, string size, string description, float price, string boxImgUrl)
    {
        try
        {
            return _repository.CreateBox(name, size, description, price, boxImgUrl);
        }
        catch (Exception e)
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine(e);
            Console.ResetColor();
            throw new Exception("Could not create this Box");
        }
    }
}