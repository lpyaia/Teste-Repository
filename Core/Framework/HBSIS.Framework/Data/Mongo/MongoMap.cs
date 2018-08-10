using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;

namespace HBSIS.Framework.Data.Mongo
{
    public class MongoMap
    {
        public virtual void Map()
        {
            var conventionPack = new ConventionPack { new IgnoreExtraElementsConvention(true) };
            ConventionRegistry.Register("IgnoreElements", conventionPack, type => true);
        }

        protected void RegisterIfNot<T>()
        {
            if (!BsonClassMap.IsClassMapRegistered(typeof(T)))
            {
                BsonClassMap.RegisterClassMap<T>();
            }
        }
    }
}