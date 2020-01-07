using System.ComponentModel;

namespace Application.Common.Enums
{
    public enum InvalidDateMessageEnum
    {
        [Description("Start date can not be after or the same as end date.")]
        StartAfterEnd,
        [Description("Selected car is not available for the selected travel period.")]
        TravelPeriodTaken
    }
}
