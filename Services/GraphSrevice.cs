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
            foreach (var box in _repository.getAllBoxes())
            {
                List<int> monthData = new List<int>();
                for (int monthDate = 1; monthDate <= 12; monthDate++)
                {
                    List<int> ordersIdinmonth = _repository.getORdersInASpecifikMonth(monthDate);

                   if (ordersIdinmonth.Count >= 0) {
                        monthData.Add(_repository.getDataToBoxes( ordersIdinmonth, box));
                    } else {
                       monthData.Add(0);
                   }
                }

                graphDataBox tempbox = new graphDataBox();
                tempbox.data = monthData;
                tempbox.boxid = box;
                tempbox.label = _repository.getboxname(box);
                
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