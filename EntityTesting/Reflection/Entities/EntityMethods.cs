namespace EntityTesting.Reflection.Entities
{
    using System;

    public static class EntityMethods
    {
        #region Public Methods

        private static object GetField( this object entity, string field_name )
        {
            var property_info = entity.GetType().GetProperty( field_name );
            if ( property_info == null )
            {
                throw new Exception(
                    string.Format( "Could not get property {0} for entity.", field_name )
                );
            }
            return property_info.GetValue( entity, null );
        }

        public static Guid GetId( this object entity )
        {
            return Guid.Parse( GetField( entity, "Id" ).ToString() );
        }

        #endregion
    }
}