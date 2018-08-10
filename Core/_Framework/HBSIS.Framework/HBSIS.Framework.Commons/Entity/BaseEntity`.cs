using System;
using System.Collections.Generic;

namespace HBSIS.Framework.Commons.Entity
{
    public abstract class BaseEntity<TId> : IEntity<TId>, IEquatable<IEntity<TId>>, IEqualityComparer<IEntity<TId>>
       where TId : IEquatable<TId>
    {
        public virtual TId Id { get; set; }

        public static bool operator ==(BaseEntity<TId> left, BaseEntity<TId> right)
        {
            return object.Equals(left, right);
        }

        public static bool operator !=(BaseEntity<TId> left, BaseEntity<TId> right)
        {
            return !object.Equals(left, right);
        }

        public virtual bool Equals(IEntity<TId> other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }
            if (ReferenceEquals(this, other))
            {
                return true;
            }
            return other.Id.Equals(Id);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
            if (!(obj is IEntity<TId>))
            {
                return false;
            }
            return Equals((IEntity<TId>)obj);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public virtual bool Equals(IEntity<TId> x, IEntity<TId> y)
        {
            if (x == null && y == null)
            {
                return true;
            }
            return x != null && x.Equals(y);
        }

        public virtual int GetHashCode(IEntity<TId> obj)
        {
            return GetHashCode();
        }
    }
}