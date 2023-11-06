namespace WebAPIcheck.ToolCheck
{
    public class Check
    {
        public DayOfWeek ReturnDay(String input)
        {
            switch(input)
            {
                case "thu 2":
                    return DayOfWeek.Monday;
                case "thu 3":
                    return DayOfWeek.Tuesday;
                case "thu 4":
                    return DayOfWeek.Wednesday;
                case "thu 5":
                    return DayOfWeek.Thursday;
                case "thu 6":
                    return DayOfWeek.Friday;
                case "thu 7":
                    return DayOfWeek.Saturday;
                case "chu nhat":
                    return DayOfWeek.Sunday;
            }
            return 0;
        }
    }
}
