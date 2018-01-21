namespace EntityTesting.Fakes
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using Reflection.Entities;

    public sealed class FakeDbSet<TEntity>
        : DbSet<TEntity>, IDbSet<TEntity> where TEntity : class
    {
        #region Fields

        private readonly Dictionary<Guid, TEntity> _storage =
            new Dictionary<Guid, TEntity>();

        #endregion

        #region Public Methods

        public override TEntity Add( TEntity entity )
        {
            _storage.Add( entity.GetId(), entity );
            return entity;
        }

        public override IEnumerable<TEntity> AddRange( IEnumerable<TEntity> entities )
        {
            return entities.Select( Add ).ToList();
        }

        public override TEntity Find( params object[] key_values )
        {
            var id = Guid.Parse( key_values[ 0 ].ToString() );
            if ( !_storage.ContainsKey( id ) )
            {
                throw new Exception(
                    string.Format( "Could not find entity with id {0}", id )
                );
            }
            return _storage[ id ];
        }

        public IEnumerator<TEntity> GetEnumerator()
        {
            return _storage.Values.GetEnumerator();
        }

        public override TEntity Remove( TEntity entity )
        {
            _storage.Remove( entity.GetId() );
            return entity;
        }

        public override IEnumerable<TEntity> RemoveRange( IEnumerable<TEntity> entities )
        {
            return entities.Select( Remove ).ToList();
        }

        #endregion
    }
}