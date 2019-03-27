namespace Utility.Data
{
    /// <summary>
    /// 要修改实体的基类
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public class UpdateBase<TKey> : IUpdateBase<TKey>
    {
        /// <summary>
        /// 要修改实体的主键
        /// </summary>
        public TKey Id { get; set; }
    }
}
