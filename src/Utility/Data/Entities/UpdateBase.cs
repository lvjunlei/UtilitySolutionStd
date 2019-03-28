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

        /// <summary>
        /// 相等比较
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(IEntity<TKey> other)
        {
            if (other == null)
            {
                return false;
            }
            return Id.Equals(other.Id);
        }
    }
}
