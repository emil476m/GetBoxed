using System.Collections;
using Infarstructure;

namespace Service;

public class GraphSrevice
{
    private readonly Graphrepo _repository;

    public GraphSrevice(Graphrepo repository)
    {
        _repository = repository;
    }

    public IEnumerable<graphDataBox> getGraphDataForAllBoxes()
    {
        try
        {
            List<graphDataBox> graphdataList = new List<graphDataBox>();
            List<int> boxIds = _repository.getAllBoxes();
            foreach (var boxes in boxIds)
            {
                List<int> monthData = new List<int>();
                for (int i = 1; i <= 12; i++)
                {
                    List<int> ordersIdinmonth = _repository.getORdersInASpecifikMonth(i);
                    monthData.Add(_repository.getDataToBoxes(boxes, ordersIdinmonth));    
                }

                graphDataBox tempbox = new graphDataBox();
            }
            return graphdataList;
        }
        catch (Exception e)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(e);
            Console.ResetColor();
            throw new Exception("Could get box data");
        }
    }
}