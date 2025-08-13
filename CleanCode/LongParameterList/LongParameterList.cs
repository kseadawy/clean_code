
using System;
using System.Collections.Generic;

namespace CleanCode.LongParameterList
{
    public class DateRange
    {
        public DateRange(DateTime dateFrom, DateTime dateTo)
        {
            DateFrom = dateFrom;
            DateTo = dateTo;
        }

        public DateTime DateFrom { get; }
        public DateTime DateTo { get; }
    }

    public class ReservationsQuery
    {
        public ReservationsQuery(DateRange dateRange, User user, int locationId, LocationType locationType, int? customerId = null)
        {
            DateRange = dateRange;
            User = user;
            LocationId = locationId;
            LocationType = locationType;
            CustomerId = customerId;
        }

        public DateRange DateRange { get; }
        public User User { get; }
        public int LocationId { get; }
        public LocationType LocationType { get; }
        public int? CustomerId { get; }
    }

    public class LongParameterList
    {
        public IEnumerable<Reservation> GetReservations(ReservationsQuery reservationsQuery)
        {
            var dateRange = reservationsQuery.DateRange;
            var dateFrom = dateRange.DateFrom;
            var dateTo = dateRange.DateTo;
            if (dateFrom >= DateTime.Now)
                throw new ArgumentNullException("dateFrom");
            if (dateTo <= DateTime.Now)
                throw new ArgumentNullException("dateTo");

            throw new NotImplementedException();
        }

        public IEnumerable<Reservation> GetUpcomingReservations(ReservationsQuery reservationsQuery)
        {
            var dateRange = reservationsQuery.DateRange;
            var dateFrom = dateRange.DateFrom;
            var dateTo = dateRange.DateTo;
            if (dateFrom >= DateTime.Now)
                throw new ArgumentNullException("dateFrom");
            if (dateTo <= DateTime.Now)
                throw new ArgumentNullException("dateTo");

            throw new NotImplementedException();
        }

        private static Tuple<DateTime, DateTime> GetReservationDateRange(DateRange dateRange, ReservationDefinition sd)
        {
            DateTime dateFrom = dateRange.DateFrom;
            DateTime dateTo = dateRange.DateTo;
            if (dateFrom >= DateTime.Now)
                throw new ArgumentNullException("dateFrom");
            if (dateTo <= DateTime.Now)
                throw new ArgumentNullException("dateTo");

            throw new NotImplementedException();
        }

        public void CreateReservation(DateRange dateRange, int locationId)
        {
            var dateFrom = dateRange.DateFrom;
            var dateTo = dateRange.DateTo;
            if (dateFrom >= DateTime.Now)
                throw new ArgumentNullException("dateFrom");
            if (dateTo <= DateTime.Now)
                throw new ArgumentNullException("dateTo");

            throw new NotImplementedException();
        }
    }

    internal class ReservationDefinition
    {
    }

    public class LocationType
    {
    }

    public class User
    {
        public object Id { get; set; }
    }

    public class Reservation
    {
    }
}
