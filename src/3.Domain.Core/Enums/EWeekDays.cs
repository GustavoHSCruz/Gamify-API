namespace Domain.Core.Enums;

[Flags]
public enum EWeekDays
{
    Sunday = 1 << 0,    //1
    Monday = 1 << 1,    //2
    Tuesday = 1 << 2,   //4
    Wednesday = 1 << 3, //8
    Thursday = 1 << 4,  //16
    Friday = 1 << 5,    //32
    Saturday = 1 << 6,  //64
}