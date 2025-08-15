public class FossilItem : BaseItem
{
    public FossilStat stat;
    
    public FossilItem(FossilStat fossilStat)
    {
        stat = fossilStat;
        statObject = stat;
    }
}
