namespace EntityTesting.Fakes
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;

    /// <summary>
    /// This DB context can be used for testing to avoid real database connection.
    /// </summary>
    public sealed class FakeDbContext : DbContext
    {
        #region Fields

        private readonly IDictionary<Type, object> _storage =
            new Dictionary<Type, object>();

        #endregion

        #region Public Methods

        public override int SaveChanges()
        {
            return 0;
        }

        public override DbSet<TEntity> Set<TEntity>()
        {
            var key = typeof( TEntity );
            if ( !_storage.ContainsKey( key ) )
            {
                _storage.Add( key, new FakeDbSet<TEntity>() );
            }
            return (DbSet<TEntity>) _storage[ key ];
        }

        #endregion
    }
}