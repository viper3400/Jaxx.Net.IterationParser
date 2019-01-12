namespace jaxx.net.iterationparser
{
    public interface IIterationRegExSelectorModel
    {
        RegExSelector TestIterationCountSelector { get; set; }
        RegExSelector TestIterationDateSelector { get; set; }
        RegExSelector TestIterationResultSelector { get; set; }
        RegExSelector TestIterationTypeSelector { get; set; }
    }
}