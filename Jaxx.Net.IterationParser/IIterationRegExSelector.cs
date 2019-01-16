namespace Jaxx.Net.IterationParser
{
    public interface IIterationRegExSelector
    {
        RegExSelector SingleLineSelector { get; set; }
        RegExSelector TestIterationCountSelector { get; set; }
        RegExSelector TestIterationDateSelector { get; set; }
        RegExSelector TestIterationResultSelector { get; set; }
        RegExSelector TestIterationTypeSelector { get; set; }
        RegExSelector TestIterationLineSelector { get; set; }
    }
}