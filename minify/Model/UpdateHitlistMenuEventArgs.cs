using System;

namespace minify.Model
{
    public class UpdateHitlistMenuEventArgs : EventArgs
    {
        public Guid Id { get; }

        public UpdateHitlistMenuEventArgs(Guid id)
        {
            Id = id;
        }
    }
}