using System;

namespace Utility.AutoMapper
{
    /// <summary>
    /// The base class for attributes that map objects to other objects using AutoMapper.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public abstract class Mapptribute : Attribute
    {
        internal Mapptribute() { }

        /// <summary>
        /// If true, mapping will be configured in reverse as well.
        /// </summary>
        public bool ReverseMap { get; set; }
    }
}
