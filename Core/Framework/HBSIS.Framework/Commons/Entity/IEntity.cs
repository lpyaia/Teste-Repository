using System;

namespace HBSIS.Framework.Commons.Entity
{
    public interface IEntity<TId>
        where TId :  IEquatable<TId>
    {
        TId Id { get; set; }
    }
}