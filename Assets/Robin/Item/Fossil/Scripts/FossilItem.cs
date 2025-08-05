public class FossilItem : BaseItem
{
    public readonly FossilStat stat;
    
    public FossilItem(FossilStat fossilStat)
    {
        stat = fossilStat;
        statObject = stat;
    }
}
