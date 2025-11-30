using Domain.Core.Enums;

namespace Application.Services;

public static class DateService
{
    public static List<DateTime> GetAllDaysByWeekDay(EWeekDays weekDay, DateTime startDate, DateTime? endDate)
    {
        var daysFound = new List<DateTime>();

        endDate ??= new DateTime(startDate.Year, 12, 31);

        for (var currentDto = startDate; currentDto <= endDate; currentDto = currentDto.AddDays(1))
        {
            EWeekDays currentDayFlag = ConvertSystemDayToEnum(currentDto.DayOfWeek);

            if ((weekDay & currentDayFlag) != 0)
            {
                daysFound.Add(currentDto);
            }
        }

        return daysFound;
    }

    private static EWeekDays ConvertSystemDayToEnum(DayOfWeek systemDay)
    {
        return systemDay switch
        {
            DayOfWeek.Monday => EWeekDays.Monday,
            DayOfWeek.Tuesday => EWeekDays.Tuesday,
            DayOfWeek.Wednesday => EWeekDays.Wednesday,
            DayOfWeek.Thursday => EWeekDays.Thursday,
            DayOfWeek.Friday => EWeekDays.Friday,
            DayOfWeek.Saturday => EWeekDays.Saturday,
            DayOfWeek.Sunday => EWeekDays.Sunday,
            _ => 0
        };
    }
}