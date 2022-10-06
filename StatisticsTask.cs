using System.Collections.Generic;
using System.Linq;

namespace linq_slideviews
{
	public class StatisticsTask
	{
		public static double GetMedianTimePerSlide(List<VisitRecord> visits, SlideType slideType)
		{
            var visitsList = visits.ToList();

            if (!visits.Any()) return 0.0;

            visitsList.Sort((first, second) =>
            {
                var r1 = first.UserId.CompareTo(second.UserId);
                var r2 = first.DateTime.CompareTo(second.DateTime);
                return (r1 != 0) ? r1 : r2;
            });

            var filteredVisitsList = visitsList.Bigrams()
                .Where(tuple => tuple.Item1.UserId == tuple.Item2.UserId 
                    && tuple.Item1.SlideId != tuple.Item2.SlideId
                    && tuple.Item1.SlideType == slideType)
                .Select(tuple => tuple.Item2.DateTime.Subtract(tuple.Item1.DateTime).TotalMinutes)
                .Where(time => (1.0 <= time && time <= 120.0))
                .ToList();

            if (!filteredVisitsList.Any()) return 0.0;

            return filteredVisitsList.Median();
        }
	}
}