namespace jaxx.net.iterationparser
{
    public interface IIterationRegExSelector
    {
        RegExSelector SingleLineSelector { get; set; }
        RegExSelector TestIterationCountSelector { get; set; }
        RegExSelector TestIterationDateSelector { get; set; }
        RegExSelector TestIterationResultSelector { get; set; }
        RegExSelector TestIterationTypeSelector { get; set; }
    }
}