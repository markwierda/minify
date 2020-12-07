using System;

namespace mini_spotify.Model
{
    public class UpdateHitlistMenuEventArgs :  EventArgs
    {
        public Guid Id { get; }

        public UpdateHitlistMenuEventArgs(Guid id)
        {
            Id = id;
        }
    }
}
