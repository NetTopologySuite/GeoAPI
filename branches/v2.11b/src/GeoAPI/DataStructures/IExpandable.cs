namespace GeoAPI.DataStructures
{
    ///<summary>
    ///</summary>
    ///<typeparam name="T"></typeparam>
    public interface IExpandable<T>
    {
        ///<summary>
        ///</summary>
        ///<param name="item"></param>
        ///<returns></returns>
        T ExpandToInclude(T item);
    }
}
