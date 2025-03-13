﻿namespace verticalSliceArchitecture.Domain
{
    public abstract class EntityBase
    {
        public int Id { get; set; } 
        public DateTimeOffset Created { get; private set; } = DateTimeOffset.UtcNow;
        public DateTimeOffset LastModified { get; private set; } = DateTimeOffset.UtcNow;
        public void UpdateLastModified()
        {
            LastModified = DateTimeOffset.UtcNow;
        }
        public EntityBase()
        {
            Created = DateTimeOffset.UtcNow;
        }
    }
}
