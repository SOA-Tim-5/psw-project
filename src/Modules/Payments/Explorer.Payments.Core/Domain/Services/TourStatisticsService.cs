using Explorer.Payments.API.Public;

namespace Explorer.Payments.Core.Domain.Services;

public class TourStatisticsService : ITourStatisticsService
{
    private ITourTokenService _tourTokenService;
    //private IInternalTourService _tourService;
    public TourStatisticsService(ITourTokenService tourTokenService)
    {
        _tourTokenService = tourTokenService;
        //_tourService = tourService;
    }
    
    public int GetNumberOfBoughtToursForAuthor(long authorId)
    {
        /*
        IEnumerable<long> tourIds = _tourService.GetAuthorsTours(authorId);
        var tokens = _tourTokenService.GetAll();
        int counter = 0;

        foreach (var token in tokens.Value)
        {
            if (tourIds.Contains(token.TourId))
            {
                counter++;
            }
        }

        return counter;
        */
        return 0;
    }
    

    public int GetNumberOfTimesTheTourWasSold(long tourId)
    {
        var tokens = _tourTokenService.GetAll();
        int counter = 0;

        foreach (var token in tokens.Value)
        {
            if (tourId == token.TourId)
            {
                counter++;
            }
        }

        return counter;
    }


}
