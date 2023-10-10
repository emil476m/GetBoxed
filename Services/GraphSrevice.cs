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
            foreach (var box in boxIds)
            {
                List<int> monthData = new List<int>();
                for (int monthDate = 1; monthDate <= 12; monthDate++)
                {
                    List<int> ordersIdinmonth = _repository.getORdersInASpecifikMonth(monthDate);
                    monthData.Add(_repository.getDataToBoxes( ordersIdinmonth, box));   
                }

                graphDataBox tempbox = new graphDataBox();
                tempbox.soldPrMonth = monthData;
                tempbox.boxid = box;
                tempbox.boxName = _repository.getboxname(box);
                
                graphdataList.Add(tempbox);
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